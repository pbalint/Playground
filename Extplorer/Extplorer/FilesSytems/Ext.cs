using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;

namespace Extplorer.FilesSytems
{
    class ExtFile : File
    {
        protected UInt32    inode_number    = 0;

        public ExtFile( FileSystem filesystem, string name, FileType file, UInt32 inode_number )
            : base( filesystem, name, file )
        {
            this.inode_number   = inode_number;
        }

        public override void List( out List< File > files, out List< File > dirs )
        {
            files       = new List< File >();
            dirs        = new List< File >();
            this.files  = files;
            this.dirs   = dirs;

            Ext fs = (Ext)filesystem;
            if ( Type == FileType.Directory )
            {
                Ext.INode   inode = fs.GetINode( inode_number );
                Byte[]      block = new Byte[ fs.BlockSize ];

                if ( ( inode.flags & 0x80000 ) == 0 ) // old, indirect blocks
                {
                    IndirectBlockIterator it = new IndirectBlockIterator( fs, inode.block );
                    while ( it.ReadNextBlock( block ) )
                    {
                        AddFilesFromLinearBlock( block, files, dirs ); // dir hash present: ( inode.flags & 0x1000 ) != 0
                    }
                }
                else // extents
                {
                    ExtentBlockIterator it = new ExtentBlockIterator( fs, inode.block );
                    while ( it.ReadNextBlock( block ) )
                    {
                        AddFilesFromLinearBlock( block, files, dirs ); // dir hash present: ( inode.flags & 0x1000 ) != 0
                    }
                }
            }
        }

        public override void Save( string path, BackgroundWorker worker )
        {
            List< File > files;
            List< File > dirs;

            if ( worker != null ) worker.ReportProgress( 0, path + "\\" + name );

            List( out files, out dirs );

            if ( type == FileType.Directory )
            {

                Directory.CreateDirectory( path + "\\" + name );

                foreach ( File file in dirs )
                {
                    file.Save( path + "\\" + name, worker );

                    if ( worker != null && worker.CancellationPending ) return;
                }
                foreach ( File file in files )
                {
                    file.Save( path + "\\" + name, worker );

                    if ( worker != null && worker.CancellationPending ) return;
                }

                if ( worker != null )
                {
                    worker.ReportProgress( 100 );
                }
            }
            else if ( type == FileType.File || type == FileType.Link )
            {
                Ext fs = (Ext)filesystem;

                Ext.INode   inode = fs.GetINode( inode_number );
                Byte[]      block = new Byte[ fs.BlockSize ];

                FileStream stream = new FileStream( path + "\\" + name, FileMode.Create );

                if ( type == FileType.Link && inode.size < 60 ) // fast symlink
                {
                    Byte[] raw_inode_blocks = new Byte[ 60 ];
                    Utils.LoadByteArrayFromUInt32Array( raw_inode_blocks, inode.block, 15 );
                    stream.Write( raw_inode_blocks, 0, (int)inode.size );
                }
                else // files, slow symlinks
                {
                    Int64 size_remaining = inode.size;
                    if ( ( inode.flags & 0x80000 ) == 0 ) // old, indirect blocks
                    {
                        IndirectBlockIterator it = new IndirectBlockIterator( fs, inode.block );
                        while ( it.ReadNextBlock( block ) )
                        {
                            if ( size_remaining < fs.BlockSize )
                            {
                                stream.Write( block, 0, (int)size_remaining );
                                size_remaining = 0;
                            }
                            else
                            {
                                stream.Write( block, 0, (int)fs.BlockSize );
                                size_remaining -= fs.BlockSize;
                            }

                            if ( worker != null )
                            {
                                worker.ReportProgress( 100 - (int)( 100*size_remaining / inode.size ) );
                                if ( worker.CancellationPending )
                                {
                                    stream.Close();
                                    return;
                                }
                            }
                        }
                    }
                    else // extents
                    {
                        ExtentBlockIterator it = new ExtentBlockIterator( fs, inode.block );
                        while ( it.ReadNextBlock( block ) )
                        {
                            if ( size_remaining < fs.BlockSize )
                            {
                                stream.Write( block, 0, (int)size_remaining );
                                size_remaining = 0;
                            }
                            else
                            {
                                stream.Write( block, 0, (int)fs.BlockSize );
                                size_remaining -= fs.BlockSize;
                            }

                            if ( worker != null )
                            {
                                worker.ReportProgress( 100 - (int)( 100*size_remaining / inode.size ) );
                                if ( worker.CancellationPending )
                                {
                                    stream.Close();
                                    return;
                                }
                            }
                        }
                    }
                }

                stream.Close();
            }
        }

        protected class IndirectBlockIterator
        {
            Ext                         filesystem                  = null;
            UInt32[]                    direct_block                = null;
            UInt32                      direct_position             = 0;
            LinkedList< Byte[] >        indirect_block              = new LinkedList< Byte[] >();
            LinkedList< UInt32 >        indirect_position           = new LinkedList< UInt32 >();
            LinkedListNode< Byte[] >    current_indirect_block      = null;
            LinkedListNode< UInt32 >    current_indirect_position   = null;
            int                         indirect_level              = 0;

            public IndirectBlockIterator( Ext filesystem, UInt32[] direct_block )
            {
                this.filesystem     = filesystem;
                this.direct_block   = direct_block;
            }

            public bool ReadNextBlock( Byte[] block )
            {
                if ( direct_position < 12 )
                {
                    if ( direct_block[ direct_position ] == 0 ) return false;

                    filesystem.ReadBlock( block, direct_block[ direct_position++ ] );
                    return true;
                }
                else
                {
                    if ( direct_block[ direct_position ] == 0 || direct_position > 14 ) return false;

                    while ( indirect_level < direct_position - 11 )
                    {
                        if ( indirect_level == 0 )
                        {
                            indirect_position.AddLast( 0 );
                            Byte[] ptr_block = new Byte[ filesystem.BlockSize ];
                            filesystem.ReadBlock( ptr_block, direct_block[ direct_position ] );
                            indirect_block.AddLast( ptr_block );

                            current_indirect_block      = indirect_block.Last;
                            current_indirect_position   = indirect_position.Last;
                            indirect_level++;
                        }
                        else
                        {
                            UInt32 intermediate_block_id;
                            Utils.LoadLE32( out intermediate_block_id,
                                            current_indirect_block.Value,
                                            current_indirect_position.Value );
                            if ( intermediate_block_id == 0 ) return false;

                            indirect_position.AddLast( 0 );
                            Byte[] ptr_block = new Byte[ filesystem.BlockSize ];
                            filesystem.ReadBlock( ptr_block, intermediate_block_id );
                            indirect_block.AddLast( ptr_block );

                            current_indirect_block      = indirect_block.Last;
                            current_indirect_position   = indirect_position.Last;
                            indirect_level++;
                        }
                    }

                    UInt32 final_block_id;
                    Utils.LoadLE32( out final_block_id, current_indirect_block.Value, current_indirect_position.Value );
                    if ( final_block_id == 0 ) return false;
                    filesystem.ReadBlock( block, final_block_id );

                    while ( indirect_level > 0 && current_indirect_position.Value + 4 >= filesystem.BlockSize )
                    {
                        current_indirect_position   = current_indirect_position.Previous;
                        current_indirect_block      = current_indirect_block.Previous;
                        indirect_position.RemoveLast();
                        indirect_block.RemoveLast();
                        indirect_level--;
                    }

                    if ( indirect_level == 0 )
                    {
                        direct_position++;
                    }
                    else
                    {
                        current_indirect_position.Value += 4;
                    }
                }

                return true;
            }
        }

        protected class ExtentBlockIterator
        {
            protected class Extent
            {
                public UInt32  id;
                public Int64   start;
                public UInt32  length;

                public Extent( UInt32 id, Int64 start, UInt32 length )
                {
                    this.id     = id;
                    this.start  = start;
                    this.length = length;
                }
            }

            Ext                         filesystem                  = null;
            LinkedList< Extent >        extents                     = new LinkedList< Extent >();
            LinkedListNode< Extent >    current_extent              = null;
            Int64                       current_block               = 0;

            public ExtentBlockIterator( Ext filesystem, UInt32[] block )
            {
                this.filesystem     = filesystem;

                Byte[] raw_inode_blocks = new Byte[60];
                Utils.LoadByteArrayFromUInt32Array( raw_inode_blocks, block, 15 );

                ProcessBlock( raw_inode_blocks );
            }

            protected void ProcessBlock( Byte[] block )
            {
                if ( block[ 0 ] != 0x0a || block[ 1 ] != 0xf3 ) throw new Exception( "Missing extent tree header!" );

                UInt32 entry_count;
                UInt32 max_entry_count;
                UInt32 depth;

                Utils.LoadLE16( out entry_count, block, 2 );
                Utils.LoadLE16( out max_entry_count, block, 4 );
                Utils.LoadLE16( out depth, block, 6 );

                if ( depth == 0 )
                {
                    UInt32  extent_id;
                    UInt32  extent_length;
                    Int64   start_block;
                    Int64   temp;
                    for ( uint i=0; i<entry_count; i++ )
                    {
                        Utils.LoadLE32( out extent_id,          block, 12 + i*12     );
                        Utils.LoadLE16( out extent_length,      block, 12 + i*12 + 4 );
                        Utils.LoadLE16To64( out temp,           block, 12 + i*12 + 6 );
                        Utils.LoadLE32To64( out start_block,    block, 12 + i*12 + 8 );
                        start_block += temp * 0x100000000;

                        extents.AddLast( new Extent( extent_id, start_block, extent_length ) );
                        if ( current_extent == null )
                        {
                            current_extent = extents.First;
                        }
                    }
                }
                else
                {
                    UInt32  id;
                    Int64   next_node;
                    Int64   temp;
                    for ( uint i=0; i<entry_count; i++ )
                    {
                        Utils.LoadLE32(     out id,         block, 12 + i*12     );
                        Utils.LoadLE32To64( out next_node,  block, 12 + i*12 + 4 );
                        Utils.LoadLE16To64( out temp,       block, 12 + i*12 + 8 );
                        next_node += temp * 0x100000000;

                        Byte[] child_block = new Byte[ filesystem.BlockSize ];
                        filesystem.ReadBlock( child_block, next_node );
                        ProcessBlock( child_block );
                    }
                }
            }

            public bool ReadNextBlock( Byte[] block )
            {
                if (    current_extent == null ||
                        extents.Count == 0 ||
                        current_block >= current_extent.Value.length ) return false;

                filesystem.ReadBlock( block, current_extent.Value.start + current_block );
                if ( current_block + 1 >= current_extent.Value.length )
                {
                    current_extent = current_extent.Next;
                    current_block = 0;
                }
                else
                {
                    current_block++;
                }
                return true;
            }
        }

        protected void AddFilesFromLinearBlock( Byte[] block, List< File > files, List< File > dirs )
        {
            UInt32      inode           = 0;
            string      file_name       = "";
            UInt32      offset          = 0;
            UInt32      record_length   = 0;
            FileType    file_type       = FileType.Other;

            while ( offset < block.Length )
            {
                Utils.LoadLE32( out inode, block, offset );
                Utils.LoadLE16( out record_length, block, offset + 4 );
                if ( inode == 0 || record_length >= block.Length ) return;

                switch ( block[offset + 7] )
                {
                    case 1:
                        file_type = FileType.File;
                        break;
                    case 2:
                        file_type = FileType.Directory;
                        break;
                    case 3:
                        file_type = FileType.CharacterDevice;
                        break;
                    case 4:
                        file_type = FileType.BlockDevice;
                        break;
                    case 5:
                        file_type = FileType.Buffer;
                        break;
                    case 6:
                        file_type = FileType.Socket;
                        break;
                    case 7:
                        file_type = FileType.Link;
                        break;
                    default:
                        file_type = FileType.Other;
                        break;
                };
                Utils.LoadString( out file_name, block[offset + 6], block, offset + 8 );

                if ( file_name != "." && file_name != ".." && file_name != "" )
                {
                    if ( file_type == FileType.Directory )
                    {
                        dirs.Add( new ExtFile( filesystem, file_name, file_type, inode ) );
                    }
                    else
                    {
                        files.Add( new ExtFile( filesystem, file_name, file_type, inode ) );
                    }
                }

                offset += record_length;
            }
        }

    };

    class Ext : FileSystem
    {
        public class BlockGroupDescriptor
        {
            public UInt32  block_bitmap_id     = 0;
            public UInt32  inode_bitmap_id     = 0;
            public UInt32  inode_table_id      = 0;
            public UInt32  free_blocks_count   = 0;
            public UInt32  free_inodes_count   = 0;
            public UInt32  dir_count           = 0;
        };

        public class INode
        {
            public UInt32      inode        = 0;
            public UInt32      mode         = 0;
            public UInt32      uid          = 0;
            public Int64       size         = 0;
            public UInt32      atime        = 0;
            public UInt32      ctime        = 0;
            public UInt32      mtime        = 0;
            public UInt32      dtime        = 0;
            public UInt32      gid          = 0;
            public UInt32      link_count   = 0;
            public Int64       sector_count = 0;
            public UInt32      flags        = 0;
            public UInt32[]    block        = new UInt32[15];
            public UInt32      file_acl     = 0;

            public INode() { for ( int i=0; i<block.Length; i++ ) block[i] = 0; }
        };

        Partition                       partition               = null;
        UInt32                          inode_count             = 0;
        UInt32                          total_block_count       = 0;
        UInt32                          reserved_block_count    = 0;
        UInt32                          free_block_count        = 0;
        UInt32                          free_inode_count        = 0;
        UInt32                          first_data_block        = 0;
        UInt32                          block_size_pow          = 0;
        UInt32                          blocks_per_group        = 0;
        UInt32                          inodes_per_group        = 0;
        UInt32                          minor_revision          = 0;
        UInt32                          revision                = 0;
        UInt32                          first_useable_inode     = 0;
        UInt32                          inode_size              = 0;
        UInt32                          features_compatible     = 0;
        UInt32                          features_incompatible   = 0;
        UInt32                          features_ro_compatible  = 0;
        UInt32[]                        volume_id               = new UInt32[4];
        string                          volume_name             = "";
        UInt32                          prealloc_file           = 0;
        UInt32                          prealloc_dir            = 0;
        string                          journal_uuid            = "";
        UInt32                          journal_inode           = 0;
        UInt32                          journal_last_orphan     = 0;
        UInt32[]                        hash_seed               = new UInt32[4];
        UInt32                          hash_version            = 0;
        UInt32                          first_meta_block_group  = 0;
        List< BlockGroupDescriptor >    bg_descriptors          = new List< BlockGroupDescriptor >();
        bool                            ext4                    = false;

        const UInt32                    root_inode_number       = 2;

        public uint BlockSize { get { return (UInt32)1024<<(Int32)block_size_pow; } }

        public Ext( Device device, MBRPartitionEntry mbr_entry )
        {
            partition = new Partition( device, mbr_entry );
            Byte[] sector = new Byte[ device.SectorSize ];
            partition.Read( sector, 1024, device.SectorSize );

            if ( sector[ 56 ] != 0x53 || sector[ 57 ] != 0xef ) throw new Exception( "Partition is not Ext2/3/4" );

            Utils.LoadLE32( out inode_count,            sector, 0 );
            Utils.LoadLE32( out total_block_count,      sector, 4 );
            Utils.LoadLE32( out reserved_block_count,   sector, 8 );
            Utils.LoadLE32( out free_block_count,       sector, 12 );
            Utils.LoadLE32( out free_inode_count,       sector, 16 );
            Utils.LoadLE32( out first_data_block,       sector, 20 );
            Utils.LoadLE32( out block_size_pow,         sector, 24 );
            Utils.LoadLE32( out blocks_per_group,       sector, 32 );
            Utils.LoadLE32( out inodes_per_group,       sector, 40 );
            Utils.LoadLE16( out minor_revision,         sector, 62 );
            Utils.LoadLE32( out revision,               sector, 76 );
            Utils.LoadLE32( out first_useable_inode,    sector, 84 );
            Utils.LoadLE16( out inode_size,             sector, 88 );
            Utils.LoadLE32( out features_compatible,    sector, 92 );
            Utils.LoadLE32( out features_incompatible,  sector, 96 );
            Utils.LoadLE32( out features_ro_compatible, sector, 100 );
            Utils.LoadLE32( out volume_id[0],           sector, 104 );
            Utils.LoadLE32( out volume_id[1],           sector, 108 );
            Utils.LoadLE32( out volume_id[2],           sector, 112 );
            Utils.LoadLE32( out volume_id[3],           sector, 116 );
            Utils.LoadString( out volume_name, 16,      sector, 120 );
            prealloc_file   = sector[204];
            prealloc_dir    = sector[205];
            Utils.LoadString( out journal_uuid, 16,     sector, 208 );
            Utils.LoadLE32( out journal_inode,          sector, 224 );
            Utils.LoadLE32( out journal_last_orphan,    sector, 232 );
            Utils.LoadLE32( out hash_seed[0],           sector, 236 );
            Utils.LoadLE32( out hash_seed[1],           sector, 240 );
            Utils.LoadLE32( out hash_seed[2],           sector, 244 );
            Utils.LoadLE32( out hash_seed[3],           sector, 248 );
            hash_version    = sector[252];
            Utils.LoadLE32( out first_meta_block_group, sector, 260 );

            ext4 =  ( ( features_ro_compatible & 0x0008 ) != 0 ) ||
                    ( ( features_ro_compatible & 0x0010 ) != 0 ) ||
                    ( ( features_ro_compatible & 0x0020 ) != 0 ) ||
                    ( ( features_ro_compatible & 0x0040 ) != 0 ) ||
                    ( ( features_incompatible & 0x0040 ) != 0 ) ||
                    ( ( features_incompatible & 0x0080 ) != 0 ) ||
                    ( ( features_incompatible & 0x0100 ) != 0 ) ||
                    ( ( features_incompatible & 0x0200 ) != 0 ) ||
                    ( ( features_incompatible & 0x0400 ) != 0 ) ||
                    ( ( features_incompatible & 0x1000 ) != 0 );

            uint block_group_count = ( total_block_count + blocks_per_group-1 )/blocks_per_group;
            for ( uint i=0; i<block_group_count; ++i )
            {
                bg_descriptors.Add( GetGroupDescriptor( i ) );
            }
        }

        public INode GetINode( UInt32 inode_number )
        {
            INode inode = new INode();
            inode.inode = inode_number;

            inode_number--;
            BlockGroupDescriptor block_group_descriptor = bg_descriptors[ (int) (inode_number / inodes_per_group) ];

            Byte[] block = new Byte[ BlockSize ];
            uint inodes_per_block       = BlockSize / inode_size;
            uint inode_number_in_bg     = inode_number % inodes_per_group;
            uint inode_block_index      = inode_number_in_bg / inodes_per_block;
            uint inode_offset           = ( inode_number_in_bg % inodes_per_block ) * inode_size;
            ReadBlock( block, block_group_descriptor.inode_table_id + inode_block_index );

            Utils.LoadLE16( out inode.mode,         block, inode_offset + 0 );
            Utils.LoadLE16( out inode.uid,          block, inode_offset + 2 );

            Utils.LoadLE32To64( out inode.size,     block, inode_offset + 108 );  // top 32 bits
            inode.size *= 0x100000000;                          // << 32
            UInt32 temp;
            Utils.LoadLE32( out temp,     block, inode_offset + 4 );    // bottom 32 bits
            inode.size += temp;

            Utils.LoadLE32( out inode.atime,        block, inode_offset + 8 );
            Utils.LoadLE32( out inode.ctime,        block, inode_offset + 12 );
            Utils.LoadLE32( out inode.mtime,        block, inode_offset + 16 );
            Utils.LoadLE32( out inode.dtime,        block, inode_offset + 20 );
            Utils.LoadLE16( out inode.gid,          block, inode_offset + 24 );
            Utils.LoadLE16( out inode.link_count,   block, inode_offset + 26 );
            Utils.LoadLE32To64( out inode.sector_count, block, inode_offset + 28 );
            Utils.LoadLE32( out inode.flags,        block, inode_offset + 32 );
            for ( UInt32 i=0; i<inode.block.Length; i++ )
            {
                Utils.LoadLE32( out inode.block[ i ], block, inode_offset + 40 + i*4 );
            }
            Utils.LoadLE32( out inode.file_acl,     block, inode_offset + 104 );
            return inode;
        }

        protected BlockGroupDescriptor GetGroupDescriptor( UInt32 block_group_id )
        {
            Byte[] block = new Byte[ BlockSize ];
            const uint gd_entry_size = 32;
            Int64 containing_block_id = block_group_id / ( BlockSize / gd_entry_size );
            block_group_id = block_group_id % ( BlockSize / gd_entry_size );

            if ( BlockSize == 1024 )
            {
                ReadBlock( block, 2 + containing_block_id );
            }
            else
            {
                ReadBlock( block, 1 + containing_block_id );
            }


            BlockGroupDescriptor descriptor = new BlockGroupDescriptor();

            Utils.LoadLE32( out descriptor.block_bitmap_id,     block, block_group_id * gd_entry_size + 0 );
            Utils.LoadLE32( out descriptor.inode_bitmap_id,     block, block_group_id * gd_entry_size + 4 );
            Utils.LoadLE32( out descriptor.inode_table_id,      block, block_group_id * gd_entry_size + 8 );
            Utils.LoadLE16( out descriptor.free_blocks_count,   block, block_group_id * gd_entry_size + 12 );
            Utils.LoadLE16( out descriptor.free_inodes_count,   block, block_group_id * gd_entry_size + 14 );
            Utils.LoadLE16( out descriptor.dir_count,           block, block_group_id * gd_entry_size + 16 );
            return descriptor;
        }

        public void ReadBlock( Byte[] block, Int64 block_id )
        {
            partition.Read( block, BlockSize * block_id, BlockSize );
        }

        public File GetRootDir()
        {
            return new ExtFile( this, "Partition " + partition.ID.ToString(), FileType.Directory, root_inode_number );
        }
    }
}
