using Extplorer.FilesSytems;
using System;
using System.IO;

namespace Extplorer
{

    class MBRPartitionEntry
    {

        public PartitionType    type            = PartitionType.Unknown;
        public UInt32           bootable        = 0;
        public UInt32           start_lba       = 0;
        public UInt32           size_lba        = 0;
        public int              id              = 0;

        public void Parse( Byte[] buffer, UInt32 offset )
        {
            bootable = buffer[ offset + 0 ];
            switch ( (PartitionType)buffer[ offset + 4 ] )
            {
                case PartitionType.Ext:
                    type = PartitionType.Ext;
                    break;
                case PartitionType.Extended:
                    type = PartitionType.Extended;
                    break;
                default:
                    type = PartitionType.Unknown;
                    break;
            }
            Utils.LoadLE32( out start_lba, buffer, offset + 8 );
            Utils.LoadLE32( out size_lba, buffer, offset + 12 );
        }
    };

    enum PartitionType
    {
        Unknown     = 0,
        Extended    = 0x0f,
        Ext         = 0x83
    };

    class Partition
    {
        protected Device    device;
        protected int       partition_id        = 0;
        protected Int64     partition_start     = 0;
        protected Int64     partition_length    = 0;

        public int ID { get { return partition_id; } }
        public Partition( Device device, MBRPartitionEntry entry )
        {
            this.device         = device;
            partition_id        = entry.id;
            partition_start     = ( Int64 ) entry.start_lba * ( Int64 ) device.SectorSize;
            partition_length    = ( Int64 ) entry.size_lba * ( Int64 ) device.SectorSize;
        }

        public static FileSystem GetFileSystem( Device device, MBRPartitionEntry entry )
        {
            return new Ext( device, entry );
        }

        public void Read( Byte[] buffer, Int64 offset, UInt32 buffer_size )
        {
            device.Stream.Seek( partition_start + offset, SeekOrigin.Begin );
            device.Stream.Read( buffer, 0, (int)buffer_size );
        }
    }
}
