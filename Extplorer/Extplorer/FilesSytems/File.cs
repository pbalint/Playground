using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Extplorer.FilesSytems
{
    public abstract class File : IComparable<File>
    {
        protected   string                      name;
        protected   FileType                    type;
        protected   FileSystem                  filesystem;
        protected   List< File >                files;
        protected   List< File >                dirs;

        public FileType Type { get { return type; } }
        public string Name { get { return name; } }

        public File( FileSystem filesystem, string name, FileType type )
        {
            this.filesystem = filesystem;
            this.name = name;
            this.type = type;
        }

        public void GetCachedList( out List<File> files, out List<File> dirs )
        {
            if ( this.files == null || this.dirs == null )
            {
                List( out files, out dirs );
            }
            else
            {
                files = this.files;
                dirs = this.dirs;
            }
        }
        public abstract void List( out List<File> files, out List<File> dirs );
        public abstract void Save( string path, BackgroundWorker worker );

        public int CompareTo( File other )
        {
            return this.name.CompareTo( other.name );
        }
    };
}
