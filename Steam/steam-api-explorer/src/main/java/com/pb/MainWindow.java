package com.pb;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.EventQueue;
import java.awt.GridLayout;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.Callable;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JSplitPane;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.JTree;
import javax.swing.SwingConstants;
import javax.swing.SwingUtilities;
import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;
import javax.swing.border.Border;
import javax.swing.border.EmptyBorder;
import javax.swing.border.EtchedBorder;
import javax.swing.event.TreeSelectionEvent;
import javax.swing.event.TreeSelectionListener;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeCellRenderer;
import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.TreePath;

import com.jgoodies.forms.factories.FormFactory;
import com.jgoodies.forms.layout.ColumnSpec;
import com.jgoodies.forms.layout.FormLayout;
import com.jgoodies.forms.layout.RowSpec;
import com.pb.steam.SteamApi;
import com.pb.steam.requestsresponses.GetSupportedApiList;
import com.pb.steam.requestsresponses.GetSupportedApiList.Interface;
import com.pb.steam.requestsresponses.GetSupportedApiList.Interface.Method;
import com.pb.steam.requestsresponses.GetSupportedApiList.Interface.Method.Parameter;
import com.pb.steam.requestsresponses.SteamApiRequest;
import com.pb.ui.InterfaceTreeNode;
import com.pb.ui.MethodTreeNode;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.awt.Font;

import javax.swing.JMenu;
import javax.swing.JMenuBar;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

public class MainWindow {
    private static final String       CONFIG_FILE_NAME = "config.xml";
    private JFrame                    frmSteamApiExplorer;
    private JTree                     jtApiList;
    private SteamApi                  api              = new SteamApi();
    private JLabel                    jlStatusBar;
    private JSplitPane                jspInner;
    private JPanel                    jpRequest;
    private JButton                   btnNewButton;
    private JPanel                    jpRequestParameters;
    private JTextArea                 jtaResponse;
    private Map< String, JTextField > parameterEditors = new HashMap<>();
    private JMenu                     menuRefresh;
    private JMenu                     menuOptions;
    private ExecutorService           workerPool       = Executors.newFixedThreadPool( 1 );
    private Config                    config;

    public static void main( String[] args ) {
        EventQueue.invokeLater( new Runnable() {
            public void run() {
                try {
                    MainWindow window = new MainWindow();
                    window.frmSteamApiExplorer.setVisible( true );
                }
                catch ( Exception e ) {
                    e.printStackTrace();
                }
            }
        } );
    }

    public MainWindow() {
        config = Utils.deserializeFromXmlFile( Config.getDefault(), CONFIG_FILE_NAME );

        initialize();
        setLookAndFeel();
        EventQueue.invokeLater( new Runnable() {
            @Override
            public void run() {
                refreshApiList();
            }
        } );
    }

    private void refreshApiList() {
        jlStatusBar.setText( "Requesting API list..." );
        Future< GetSupportedApiList > workerThread = workerPool.submit( new Callable< GetSupportedApiList >() {
            @Override
            public GetSupportedApiList call() throws Exception {
                Map< String, String > parameters = new HashMap<>();
                parameters.put( "key", config.getApiKey() );
                return api.getResponse( GetSupportedApiList.getRequest( parameters ), GetSupportedApiList.class );
            }
        } );

        EventQueue.invokeLater( new Runnable() {
            public void run() {
                GetSupportedApiList apiList = null;
                try {
                    apiList = workerThread.get();
                }
                catch ( InterruptedException | ExecutionException e ) {
                    e.printStackTrace();
                }
                if ( apiList != null ) {
                    DefaultMutableTreeNode root = new DefaultMutableTreeNode();
                    for ( Interface steamInterface : apiList.interfaces ) {
                        InterfaceTreeNode interfaceNode = new InterfaceTreeNode( steamInterface );
                        root.add( interfaceNode );
                        for ( Method method : steamInterface.methods ) {
                            interfaceNode.add( new MethodTreeNode( method ) );
                        }
                    }
                    jtApiList.setModel( new DefaultTreeModel( root ) );
                    jlStatusBar.setText( "Done" );
                }
                else {
                    jlStatusBar.setText( "Error" );
                }
            }
        } );
    }

    private void setLookAndFeel() {
        try {
            UIManager.setLookAndFeel( config.getLookAndFeel() );
        }
        catch ( ClassNotFoundException | InstantiationException | IllegalAccessException | UnsupportedLookAndFeelException e ) {
            e.printStackTrace();
        }
        SwingUtilities.updateComponentTreeUI( frmSteamApiExplorer );

        jtApiList.setShowsRootHandles( true );
        ( (DefaultTreeCellRenderer)jtApiList.getCellRenderer() ).setOpenIcon( null );
        ( (DefaultTreeCellRenderer)jtApiList.getCellRenderer() ).setClosedIcon( null );
        ( (DefaultTreeCellRenderer)jtApiList.getCellRenderer() ).setLeafIcon( null );

    }

    private void initialize() {
        frmSteamApiExplorer = new JFrame();
        frmSteamApiExplorer.addWindowListener( new WindowAdapter() {
            @Override
            public void windowClosing( WindowEvent arg0 ) {
                onWindowClosing();
            }
        } );
        frmSteamApiExplorer.setTitle( "Steam API Explorer" );
        frmSteamApiExplorer.setBounds( 100, 100, 760, 484 );
        frmSteamApiExplorer.setDefaultCloseOperation( JFrame.EXIT_ON_CLOSE );
        frmSteamApiExplorer.getContentPane().setLayout( new BorderLayout( 4, 0 ) );

        jlStatusBar = new JLabel( "" );
        jlStatusBar.setHorizontalTextPosition( SwingConstants.LEFT );
        jlStatusBar.setHorizontalAlignment( SwingConstants.LEFT );
        jlStatusBar.setBorder( new EmptyBorder( 2, 2, 2, 2 ) );
        frmSteamApiExplorer.getContentPane().add( jlStatusBar, BorderLayout.SOUTH );

        JSplitPane jspOuter = new JSplitPane();
        jspOuter.setResizeWeight( 0.5 );
        jspOuter.setDividerSize( 2 );
        frmSteamApiExplorer.getContentPane().add( jspOuter );

        JMenuBar menu = new JMenuBar();
        frmSteamApiExplorer.getContentPane().add( menu, BorderLayout.NORTH );

        menuRefresh = new JMenu( "Refresh" );
        menuRefresh.addMouseListener( new MouseAdapter() {
            @Override
            public void mouseClicked( MouseEvent arg0 ) {
                refreshApiList();
            }
        } );
        menu.add( menuRefresh );

        menuOptions = new JMenu( "Options" );
        menuOptions.addMouseListener( new MouseAdapter() {
            @Override
            public void mouseClicked( MouseEvent e ) {
                onOptionsClicked();
            }
        } );
        menu.add( menuOptions );

        jspInner = new JSplitPane();
        jspInner.setDividerSize( 3 );
        jspInner.setResizeWeight( 0.4 );
        jspOuter.setRightComponent( jspInner );

        jtaResponse = new JTextArea();
        jtaResponse.setFont( new Font( "Monospaced", Font.PLAIN, 11 ) );
        jspInner.setRightComponent( new JScrollPane( jtaResponse ) );

        jpRequest = new JPanel();
        jspInner.setLeftComponent( jpRequest );
        jpRequest.setLayout( new FormLayout( new ColumnSpec[] {
                FormFactory.RELATED_GAP_COLSPEC,
                ColumnSpec.decode( "default:grow" ),
                FormFactory.RELATED_GAP_COLSPEC,
                ColumnSpec.decode( "default:grow" ),
                FormFactory.RELATED_GAP_COLSPEC, },
                                             new RowSpec[] {
                                                     FormFactory.RELATED_GAP_ROWSPEC,
                                                     FormFactory.DEFAULT_ROWSPEC,
                                                     RowSpec.decode( "default:grow" ), } ) );

        btnNewButton = new JButton( "Send request" );
        btnNewButton.addActionListener( new ActionListener() {
            public void actionPerformed( ActionEvent arg0 ) {
                onSendRequest();
            }
        } );
        jpRequest.add( btnNewButton, "2, 2, 3, 1, fill, top" );

        jpRequestParameters = new JPanel();
        jpRequestParameters.setBorder( new EtchedBorder( EtchedBorder.LOWERED, null, null ) );
        jpRequest.add( jpRequestParameters, "2, 3, 3, 1, fill, top" );
        jpRequestParameters.setLayout( new GridLayout( 0, 3, 0, 0 ) );
        jspInner.setDividerLocation( 250 );

        jtApiList = new JTree();
        jtApiList.setRootVisible( false );
        jtApiList.addTreeSelectionListener( new TreeSelectionListener() {
            public void valueChanged( TreeSelectionEvent event ) {
                onTreeSelectionChanged( event );
            }
        } );
        jspOuter.setLeftComponent( new JScrollPane( jtApiList ) );
        jspOuter.setDividerLocation( 250 );
    }

    private void onTreeSelectionChanged( TreeSelectionEvent event ) {
        Object leaf = event.getNewLeadSelectionPath().getLastPathComponent();
        jpRequestParameters.removeAll();
        parameterEditors.clear();
        if ( leaf instanceof MethodTreeNode ) {
            Method method = ( (MethodTreeNode)leaf ).getMethod();
            jpRequestParameters.removeAll();
            GridLayout layout = (GridLayout)jpRequestParameters.getLayout();
            layout.setRows( method.parameters.size() );
            Border border = new EmptyBorder( 2, 2, 2, 2 );
            for ( Parameter parameter : method.parameters ) {
                JLabel label = new JLabel( parameter.name + " (" + parameter.type + ")" );
                if ( parameter.optional ) {
                    label.setForeground( Color.LIGHT_GRAY );
                    label.setText( label.getText() + " Optional" );
                }
                label.setToolTipText( parameter.description );
                label.setBorder( border );
                jpRequestParameters.add( label );

                JTextField textField = new JTextField();
                parameterEditors.put( parameter.name, textField );
                jpRequestParameters.add( textField );
            }
        }
        jpRequestParameters.revalidate();
    }

    private void onSendRequest() {
        TreePath selection = jtApiList.getSelectionPath();
        if ( selection == null ) return;
        Object leaf = selection.getLastPathComponent();
        if ( leaf instanceof MethodTreeNode ) {
            InterfaceTreeNode interfaceNode = (InterfaceTreeNode)selection.getPath()[ 1 ];
            MethodTreeNode methodNode = (MethodTreeNode)leaf;
            Method method = methodNode.getMethod();
            Map< String, String > parameters = new HashMap<>();
            for ( Entry< String, JTextField > parameterEditor : parameterEditors.entrySet() ) {
                parameters.put( parameterEditor.getKey(), parameterEditor.getValue().getText() );
            }
            jtaResponse.setText( api.getResponseRaw( interfaceNode.getInterface().name,
                                                     method.name,
                                                     method.version,
                                                     SteamApiRequest.Type.valueOf( method.method.toUpperCase() ),
                                                     parameters ) );
        }
    }

    private void onOptionsClicked() {
        ConfigurationPanel panel = new ConfigurationPanel( config );
        if ( JOptionPane.showOptionDialog( null,
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
                    setLookAndFeel();
                }
            }
        }
    }

    private void onWindowClosing() {
        Utils.serializeToXmlFile( config, CONFIG_FILE_NAME );
    }
}
