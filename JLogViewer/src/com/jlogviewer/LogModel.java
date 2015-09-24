package com.jlogviewer;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import javax.swing.ListModel;
import javax.swing.event.ListDataListener;
import javax.swing.event.TreeModelListener;
import javax.swing.tree.TreeModel;
import javax.swing.tree.TreePath;

public class LogModel implements TreeModel, ListModel< String > {

    private File                    logFile;
    private Map< String, LogEntry > processMap = new HashMap<>();
    private LogEntry                root;

    public void parseFile( File logFile, Config config ) {
        if ( logFile == null )
                              return;

        this.logFile = logFile;
        root = new LogEntry( -1, logFile.getName() );
        processMap.clear();

        LogEntry lastRecognizedLogEntry = null;
        BufferedReader reader = null;
        int row = 0;
        try {
            reader = new BufferedReader( new FileReader( logFile ) );
            while ( reader.ready() ) {
                String line = reader.readLine();
                if ( line.length() < 1 ) continue;

                if ( line.contains( "Warning:" ) ) {
                    int a = 5;
                }
                
                LogEntry entry = LogEntry.buildFrom( row, line );
                if ( entry == null ) {
                    if ( lastRecognizedLogEntry != null ) {
                        lastRecognizedLogEntry.addChild( new LogEntry( -1, line ) );
                    }
                    else {
                        // root.add( new DefaultMutableTreeNode( entry.getRawEntry() ) );
                        System.out.println( line );
                    }
                }
                else {
                    lastRecognizedLogEntry = entry;
                    if ( entry.getMethod().length() > 0 && config.getFilters().contains( entry.getMethod() ) ) {
                        continue;
                    }

                    LogEntry node = processMap.get( entry.getProcessName() );
                    if ( node == null ) {
                        node = new LogEntry( -1, entry.getProcessName() );
                        processMap.put( node.getMessage(), node );
                        root.addChild( node );
                    }
                    
                    if ( entry.getMode() == 'E' ) {
                        node.addChild( entry );
                        processMap.put( entry.getProcessName(), entry );
                    }
                    else if ( entry.getMode() == 'L' ) {
                        if ( node.getParent() == root ) {
                            node.addNewIntermediateChild( entry );
                        }
                        else {
                            processMap.put( entry.getProcessName(), node.getParent() );
                        }
                    }
                    else {
                        node.addChild( entry );
                    }
                }
            }
        }
        catch ( IOException e ) {
        }
        finally {
            if ( reader != null ) {
                try {
                    reader.close();
                }
                catch ( IOException e ) {
                }
            }
        }
    }

    public void reparseFile() {
        if ( logFile == null )
                              return;
    }

    @Override
    public Object getRoot() {
        return root;
    }

    @Override
    public Object getChild( Object parent, int index ) {
        return ( (LogEntry)parent ).getChild( index );
    }

    @Override
    public int getChildCount( Object parent ) {
        return ( (LogEntry)parent ).getChildCount();
    }

    @Override
    public boolean isLeaf( Object node ) {
        return ( (LogEntry)node ).getChildCount() == 0;
    }

    @Override
    public int getIndexOfChild( Object parent, Object child ) {
        return ( (LogEntry)parent ).getIndexOfChild( child );
    }

    @Override
    public void valueForPathChanged( TreePath path, Object newValue ) {
    }

    @Override
    public void addTreeModelListener( TreeModelListener l ) {
    }

    @Override
    public void removeTreeModelListener( TreeModelListener l ) {
    }

    @Override
    public int getSize() {
        return 0;
    }

    @Override
    public String getElementAt( int index ) {
        return null;
    }

    @Override
    public void addListDataListener( ListDataListener l ) {
    }

    @Override
    public void removeListDataListener( ListDataListener l ) {
    }
}
