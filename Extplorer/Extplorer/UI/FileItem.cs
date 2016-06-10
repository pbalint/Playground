using Extplorer.FilesSytems;
using System.Windows.Forms;

namespace Extplorer.UI
{
    public class FileItem : ListViewItem
    {
        private File file;
        public File File { get { return file; } }

        public FileItem( File file )
        {
            this.file = file;
            Text = file.Name;
            Name = file.Name;

            if ( this.file.Type == FileType.Directory )
            {
                ImageIndex = (int)ListViewIcons.Dir;
            }
            else
            {
                ImageIndex = (int)ListViewIcons.DirSelected;
            }
        }
    }
}
