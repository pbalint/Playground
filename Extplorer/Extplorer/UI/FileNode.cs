using Extplorer.FilesSytems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Extplorer.UI
{
    public class FileNode : TreeNode
    {
        File            file;
        public File File { get { return file; } }

        public FileNode( File file, bool load_child_dirs )
        {
            this.file = file;
            Text = file.Name;
            Name = file.Name;
            ImageIndex = (int)ListViewIcons.Drive;
            SelectedImageIndex = (int)ListViewIcons.Dir;

            if ( load_child_dirs )
            {
                LoadChildDirs();
            }
        }

        public void LoadChildDirs()
        {
            List< File >  files;
            List< File >  dirs;
            file.List( out files, out dirs );

            dirs.Sort();

            foreach ( File f in dirs )
            {
                this.Nodes.Add( new FileNode( f, false ) );
            }
        }

        public void RemoveChildDirs()
        {
            foreach ( FileNode child in Nodes )
            {
                Nodes.Remove( child );
            }
        }
    };
}
