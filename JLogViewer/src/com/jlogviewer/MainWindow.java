package com.jlogviewer;

import java.awt.BorderLayout;
import java.awt.EventQueue;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.WatchEvent;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JScrollPane;
import javax.swing.JToolBar;
import javax.swing.JTree;
import javax.swing.SwingUtilities;
import javax.swing.Timer;
import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;
import javax.swing.tree.DefaultTreeCellRenderer;

import com.jlogviewer.PathWatcher.Listener;

import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

import javax.swing.JSplitPane;
import javax.swing.JList;

public class MainWindow {
    private static final String programName          = "Log viewer";
    private static final int    logFileCheckInterval = 1000;
    private static final String configFileName       = "config.xml";

    private JFrame              frmLogViewer;
    private JTree               jTreeLogTree;
    private PathWatcher         watcher;
    private File                logFile;
    private volatile boolean    readLogFile          = true;
    private LogModel            logModel             = new LogModel();
    private Timer               logUpdateTimer       = new Timer( logFileCheckInterval, new ActionListener() {
                                                         @Override
                                                         public void actionPerformed( ActionEvent arg0 ) {
                                                             if ( readLogFile ) {
                                                                 updateLogUI();
                                                             }
                                                         }
                                                     } );
    private Config              config               = new Config();
    private JList< String >     jListLogLines;
    private JSplitPane          splitPane;

    public static void main( String[] args ) {
        EventQueue.invokeLater( new Runnable() {
            public void run() {
                try {
                    MainWindow window = new MainWindow();
                    window.frmLogViewer.setVisible( true );
                }
                catch ( Exception e ) {
                    e.printStackTrace();
                }
            }
        } );
    }

    private void setLookAndFeel( String lookAndFeel ) {
        try {
            UIManager.setLookAndFeel( lookAndFeel );
            SwingUtilities.updateComponentTreeUI( frmLogViewer );
            jTreeLogTree.setShowsRootHandles( true );
            ( (DefaultTreeCellRenderer)jTreeLogTree.getCellRenderer() ).setOpenIcon( null );
            ( (DefaultTreeCellRenderer)jTreeLogTree.getCellRenderer() ).setClosedIcon( null );
            ( (DefaultTreeCellRenderer)jTreeLogTree.getCellRenderer() ).setLeafIcon( null );
        }
        catch ( ClassNotFoundException
                |
                InstantiationException
                |
                IllegalAccessException
                |
                UnsupportedLookAndFeelException e ) {
            config.setLookAndFeel( UIManager.getSystemLookAndFeelClassName() );
        }
    }

    public MainWindow() throws IOException {
        initialize();

        onStartup();
    }

    private void initialize() {
        frmLogViewer = new JFrame();
        frmLogViewer.addWindowListener( new WindowAdapter() {
            @Override
            public void windowClosing( WindowEvent arg0 ) {
                onShutdown();
            }
        } );
        frmLogViewer.setTitle( programName );
        frmLogViewer.setIconImage( Toolkit.getDefaultToolkit().getImage( MainWindow.class.getResource( "/com/jlogviewer/resources/logviewer.png" ) ) );
        frmLogViewer.setBounds( 100, 100, 518, 418 );
        frmLogViewer.setDefaultCloseOperation( JFrame.EXIT_ON_CLOSE );

        JToolBar toolBar = new JToolBar();
        toolBar.setFloatable( false );
        frmLogViewer.getContentPane().add( toolBar, BorderLayout.NORTH );

        JButton buttonOpenLog = new JButton( "" );
        buttonOpenLog.setMnemonic( 'O' );
        buttonOpenLog.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent arg0 ) {
                openLogClicked();
            }
        } );
        buttonOpenLog.setToolTipText( "Open log..." );
        buttonOpenLog.setIcon( new ImageIcon( MainWindow.class.getResource( "/com/jlogviewer/resources/log.png" ) ) );
        toolBar.add( buttonOpenLog );

        JButton buttonOpenFilter = new JButton( "" );
        buttonOpenFilter.setMnemonic( 'F' );
        buttonOpenFilter.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent arg0 ) {
                filterClicked();
            }
        } );
        buttonOpenFilter.setIcon( new ImageIcon( MainWindow.class.getResource( "/com/jlogviewer/resources/filter.png" ) ) );
        buttonOpenFilter.setToolTipText( "Filters..." );
        toolBar.add( buttonOpenFilter );

        JButton buttonExpandAll = new JButton( "" );
        buttonExpandAll.setMnemonic( 'E' );
        buttonExpandAll.setIcon( new ImageIcon( MainWindow.class.getResource( "/com/jlogviewer/resources/expand.png" ) ) );
        buttonExpandAll.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent arg0 ) {
                expandAllClicked();
            }
        } );
        buttonExpandAll.setToolTipText( "Expand all" );
        toolBar.add( buttonExpandAll );

        JButton buttonSearch = new JButton( "" );
        buttonSearch.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent e ) {
                searchClicked();
            }
        } );
        buttonSearch.setToolTipText( "Search..." );
        buttonSearch.setMnemonic( 'S' );
        buttonSearch.setIcon( new ImageIcon( MainWindow.class.getResource( "/com/jlogviewer/resources/search.png" ) ) );
        toolBar.add( buttonSearch );

        JButton buttonOptions = new JButton( "" );
        buttonOptions.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent e ) {
                optionsClicked();
            }
        } );
        buttonOptions.setToolTipText( "Configuration..." );
        buttonOptions.setIcon( new ImageIcon( MainWindow.class.getResource( "/com/jlogviewer/resources/gear.png" ) ) );
        buttonOptions.setMnemonic( 'C' );
        toolBar.add( buttonOptions );

        splitPane = new JSplitPane();
        splitPane.setDividerSize( 4 );
        splitPane.setContinuousLayout( true );
        splitPane.setOneTouchExpandable( true );
        splitPane.setOrientation( JSplitPane.VERTICAL_SPLIT );
        frmLogViewer.getContentPane().add( splitPane, BorderLayout.CENTER );

        JScrollPane scrollPaneLogTree = new JScrollPane();
        splitPane.setTopComponent( scrollPaneLogTree );

        jTreeLogTree = new JTree();
        scrollPaneLogTree.setViewportView( jTreeLogTree );

        JScrollPane scrollPaneLogLines = new JScrollPane();
        splitPane.setRightComponent( scrollPaneLogLines );

        jListLogLines = new JList<>();
        scrollPaneLogLines.setViewportView( jListLogLines );

        jTreeLogTree.setModel( logModel );
    }

    private void updateLogUI() {
        logModel.reparseFile();
    }

    private void openLogClicked() {
        JFileChooser fileChooser = new JFileChooser();
        if ( fileChooser.showOpenDialog( null ) == JFileChooser.APPROVE_OPTION ) {
            logFile = fileChooser.getSelectedFile();
            /*
             * watcher.clear(); try { watcher.addPath( logFile.getAbsolutePath(), new Listener() {
             * @Override public void PathChanged( WatchEvent< ? > event ) { readLogFile = true; } } ); } catch ( IOException e ) {
             * JOptionPane.showMessageDialog( null, e ); }
             */
            logModel.parseFile( logFile, config );
            jTreeLogTree.updateUI();
        }
    }

    private void filterClicked() {
        FilterPanel panel = new FilterPanel( config.getFilters() );
        if ( JOptionPane.showOptionDialog( frmLogViewer,
                                           panel,
                                           "Add/remove filters",
                                           JOptionPane.OK_CANCEL_OPTION,
                                           JOptionPane.PLAIN_MESSAGE,
                                           null, null, null ) == JOptionPane.OK_OPTION ) {
            config.setFilters( panel.getFilters() );
            logModel.parseFile( logFile, config );
            jTreeLogTree.updateUI();
        }
    }

    private void expandAllClicked() {
        int row = 0;
        while ( row < jTreeLogTree.getRowCount() ) {
            jTreeLogTree.expandRow( row++ );
        }
    }

    private void optionsClicked() {
        ConfigurationPanel panel = new ConfigurationPanel( config );
        if ( JOptionPane.showOptionDialog( frmLogViewer,
                                           panel,
                                           "Change configuration",
                                           JOptionPane.OK_CANCEL_OPTION,
                                           JOptionPane.PLAIN_MESSAGE,
                                           null,
                                           null,
                                           null ) == JOptionPane.OK_OPTION ) {
            String oldLookAndFeel = config.getLookAndFeel();
            panel.applyConfig( config );
            if ( !config.getLookAndFeel().equals( oldLookAndFeel ) ) {
                // I've seen X window system exceptions when switching to GTK...
                // :(
                if ( config.getLookAndFeel().contains( "GTK" ) ) {
                    JOptionPane.showMessageDialog( null, "The selected look and feel will take effect on the next restart" );
                }
                else {
                    setLookAndFeel( config.getLookAndFeel() );
                }
            }
        }
    }

    private void searchClicked() {

    }

    private void onShutdown() {
        config.setDividerLocation( splitPane.getDividerLocation() );
        config.setWindowX( frmLogViewer.getX() );
        config.setWindowY( frmLogViewer.getY() );
        config.setWindowWidth( frmLogViewer.getWidth() );
        config.setWindowHeight( frmLogViewer.getHeight() );

        Utils.serializeToXmlFile( config, configFileName );
    }

    private void onStartup() throws IOException {
        config = Utils.deserializeFromXmlFile( config, configFileName );

        setLookAndFeel( config.getLookAndFeel() );
        splitPane.setDividerLocation( config.getDividerLocation() );
        frmLogViewer.setBounds( config.getWindowX(), config.getWindowY(), config.getWindowWidth(), config.getWindowHeight() );

        watcher = new PathWatcher();
        logUpdateTimer.start();
    }

}
