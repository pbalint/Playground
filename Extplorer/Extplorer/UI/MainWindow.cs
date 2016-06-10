using Extplorer.FilesSytems;
using Extplorer.UI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Extplorer
{
    public partial class MainWindow : Form
    {
        private List< Device > devices;

        public MainWindow()
        {
            InitializeComponent();

            RefreshDevices();
        }

        private void RefreshDevices()
        {
            tv_dirs.Nodes.Clear();
            lv_files.Clear();

            if ( devices != null )
            {
                foreach ( Device dev in devices ) dev.Close();
            }

            devices = Device.GetPhysicalDevices();

            foreach ( Device dev in devices )
            {
                TreeNode root_node = new TreeNode( dev.DeviceName );
                root_node.ImageIndex = 0;
                root_node.SelectedImageIndex = 0;
                tv_dirs.Nodes.Add( root_node );
                List< FileSystem > filesystems = dev.GetFileSystems();

                foreach ( FileSystem fs in filesystems )
                {
                    FileNode node = new FileNode( fs.GetRootDir(), true );
                    root_node.Nodes.Add( node );
                }

                root_node.Expand();
            }
        }

        private void MainWindow_FormClosed( object sender, FormClosedEventArgs e )
        {
            foreach ( Device dev in devices )
            {
                dev.Close();
            }
        }

        private void tv_dirs_AfterSelect( object sender, TreeViewEventArgs e )
        {
            if ( e.Node.GetType().Name == "TreeNode" ) return;

            List< File > files;
            List< File > dirs;
            ( (FileNode)e.Node ).File.GetCachedList( out files, out dirs );

            if ( files.Count > 1 ) files.Sort();
            if ( dirs.Count > 1 ) dirs.Sort();

            lv_files.Clear();
            foreach ( File dir in dirs )
            {
                lv_files.Items.Add( new FileItem( dir ) );
            }
            foreach ( File file in files )
            {
                lv_files.Items.Add( new FileItem( file ) );
            }
            lv_files.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );
        }

        private void tv_dirs_BeforeExpand( object sender, TreeViewCancelEventArgs e )
        {
            if ( e.Node.GetType().Name == "TreeNode" ) return;

            foreach ( FileNode child in e.Node.Nodes )
            {
                child.LoadChildDirs();
            }
        }

        private void tv_dirs_AfterCollapse( object sender, TreeViewEventArgs e )
        {
            if ( e.Node.GetType().Name == "TreeNode" ) return;

            foreach ( FileNode child in e.Node.Nodes )
            {
                child.RemoveChildDirs();
            }
        }

        private void ts_extract_Click( object sender, System.EventArgs e )
        {
            string path = "";
            FolderBrowserDialog fb_dialog = new FolderBrowserDialog();
            if ( fb_dialog.ShowDialog() == DialogResult.OK )
            {
                path = fb_dialog.SelectedPath;
            }
            else
            {
                return;
            }

            List< File > files_to_extract = new List<File>();
            if ( tv_dirs.Focused )
            {
                if ( tv_dirs.SelectedNode == null || tv_dirs.SelectedNode.Level == 0 ) return;

                files_to_extract.Add( ((FileNode)tv_dirs.SelectedNode).File );
            }
            else
            {
                if ( lv_files.SelectedItems.Count <= 0 ) return;
                foreach ( FileItem fi in lv_files.SelectedItems )
                {
                    files_to_extract.Add( fi.File );
                }
            }

            ExtractionProgressBar epb = new ExtractionProgressBar( files_to_extract, path );
            epb.ShowDialog( this );
        }

        private void ts_refresh_Click( object sender, System.EventArgs e )
        {
            RefreshDevices();
        }

        private void lv_files_DoubleClick( object sender, System.EventArgs e )
        {
            int mouse_x = lv_files.PointToClient( Cursor.Position ).X;
            int mouse_y = lv_files.PointToClient( Cursor.Position ).Y;
            FileItem file = (FileItem)lv_files.GetItemAt( mouse_x, mouse_y );
            if ( file == null || file.File.Type != FileType.Directory ) return;

            TreeNode[] children = tv_dirs.SelectedNode.Nodes.Find( file.File.Name, false );
            tv_dirs.SelectedNode = children[ 0 ];

        }

        private void mi_exit_Click( object sender, System.EventArgs e )
        {
            this.Close();
        }

        private void ts_open_Click( object sender, System.EventArgs e )
        {
            FileSelector file_selector = new FileSelector();
            if ( file_selector.ShowDialog( this ) != DialogResult.OK ||
                 file_selector.Path == "" )
            {
                return;
            }

            Device dev = new Device( file_selector.Path, file_selector.FileName, file_selector.SectorSize );
            tv_dirs.Nodes.Clear();
            TreeNode root_node = new TreeNode( dev.DeviceName );
            tv_dirs.Nodes.Add( root_node );
            List< FileSystem > filesystems = dev.GetFileSystems();

            foreach ( FileSystem fs in filesystems )
            {
                FileNode node = new FileNode( fs.GetRootDir(), true );
                root_node.Nodes.Add( node );
            }
            root_node.Expand();
        }

        private void tv_dirs_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e )
        {
            tv_dirs.SelectedNode = e.Node;
        }

        private void aboutToolStripMenuItem_Click( object sender, System.EventArgs e )
        {

        }
    }
}
