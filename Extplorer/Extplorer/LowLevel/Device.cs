using Extplorer.FilesSytems;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management;

namespace Extplorer
{
    class Device
    {
        protected string                            name                    = "";
        protected string                            model                   = "";
        protected UInt32                            sector_size             = 0;
        protected UInt64                            size                    = 0;

        protected FileStream                        stream                  = null;
        protected static HashSet< PartitionType >   known_partition_types   = new HashSet< PartitionType >();

        public UInt32       SectorSize    { get { return sector_size; } }
        public FileStream   Stream        { get { return stream; } }
        public string       DeviceName    { get { return model; } }

        static Device()
        {
            known_partition_types.Add( PartitionType.Ext );
        }

        public Device( string full_name, string file_name, UInt32 sector_size )
        {
            this.name           = full_name;
            this.model          = file_name;
            this.sector_size    = sector_size;
            try
            {
                stream          = new FileStream( full_name, FileMode.Open );
                if ( stream.Length < 2048 ) // object too small to contain a filesystem
                {
                    stream.Close();
                    stream = null;
                }
            }
            catch ( Exception ) { }
        }

        public Device( ManagementObject disk_drive )
        {
            name                    = disk_drive[ "Name" ].ToString();
            model                   = disk_drive[ "Model" ].ToString();
            sector_size             = Convert.ToUInt32( disk_drive[ "BytesPerSector" ].ToString() );
            size                    = Convert.ToUInt64( disk_drive[ "Size" ].ToString() );

            SafeFileHandle handle   = Utils.OpenRawDevice( name );
            if ( !handle.IsInvalid )
            {
                stream = new FileStream( handle, FileAccess.ReadWrite );
            }
            else
            {
                stream = null;
            }
        }

        ~Device()
        {
            Close();
        }

        public void Close()
        {
            if ( stream != null ) stream.Close();
        }
        
        public List< FileSystem > GetFileSystems()
        {
            List< FileSystem > filesystems = new List< FileSystem >();

            if ( stream == null || !stream.CanRead ) return filesystems;

            Byte[] sector = new Byte[ sector_size ];
            stream.Read( sector, 0, (int)sector_size );

            if ( sector[ 510 ] != 0x55 || sector[ 511 ] != 0xaa ) return filesystems; // mbr magic number missing

            List< MBRPartitionEntry > mbr_partitions = new List< MBRPartitionEntry >();
            MBRPartitionEntry   current_partition;
            int                 partition_id = 0;
            for ( UInt32 i=0; i<4; i++ )
            {
                current_partition = new MBRPartitionEntry();
                current_partition.Parse( sector, 446 + i*16 );
                if ( known_partition_types.Contains( current_partition.type ) ) // known
                {
                    current_partition.id = partition_id;
                    mbr_partitions.Add( current_partition );
                    ++partition_id;
                }
                else if ( current_partition.type == PartitionType.Extended ) // extended partition: contains others
                {
                    Byte[] extended_sector              = new Byte[ sector_size ];
                    MBRPartitionEntry extended_entry    = new MBRPartitionEntry();
                    MBRPartitionEntry extended_next     = new MBRPartitionEntry();

                    do
                    {
                        stream.Seek( ( ( Int64 ) current_partition.start_lba + ( Int64 ) extended_next.start_lba ) * ( Int64 ) sector_size, SeekOrigin.Begin );
                        stream.Read( extended_sector, 0, (int)sector_size );

                        extended_entry.Parse( extended_sector, 446 );
                        extended_entry.start_lba += current_partition.start_lba + extended_next.start_lba;
                        extended_next.Parse( extended_sector, 446 + 16 );

                        if ( known_partition_types.Contains( extended_entry.type ) )
                        {
                            extended_entry.id = partition_id;
                            mbr_partitions.Add( extended_entry );
                            extended_entry = new MBRPartitionEntry();
                        }
                        ++partition_id;
                    } while ( extended_next.size_lba != 0 );
                }
                else // unknown
                {
                    ++partition_id;
                }
            }

            foreach ( MBRPartitionEntry mbr_entry in mbr_partitions )
            {
                FileSystem fs = null;
                switch ( mbr_entry.type )
                {
                    case PartitionType.Ext:
                    {
                        FileSystem temp = null;
                        try
                        {
                            temp = new Ext( this, mbr_entry );
                            fs   = temp;
                        }
                        catch ( Exception ) {}
                        break;
                    }
                }

                if ( fs != null ) filesystems.Add( fs );
            }
            return filesystems;
        }

        public static List< Device > GetPhysicalDevices()
        {
            List< Device > devices = new List< Device >();

            ManagementObjectSearcher diskdrives = new ManagementObjectSearcher( "SELECT * FROM Win32_DiskDrive" );
            foreach ( ManagementObject diskdrive in diskdrives.Get() )
            {
                devices.Add( new Device( diskdrive ) );
            }

            return devices;
        }

    }
}
