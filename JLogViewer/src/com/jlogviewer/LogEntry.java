package com.jlogviewer;

import java.util.ArrayList;

public class LogEntry {
    private LogEntry              parent;
    private int                   row;

    private String                line;
    private int                   processStart;
    private int                   processEnd;
    private int                   methodStart;
    private int                   methodEnd;
    private int                   messageStart;
    private int                   messageEnd;
    private int                   timeStart;
    private int                   timeEnd;
    private char                  mode;

    private ArrayList< LogEntry > children = new ArrayList<>();

    public LogEntry( int row, String line ) {
        this.row = row;
        this.line = line;
        messageEnd = line.length();
    }

    public String getLine() {
        return line;
    }

    public String getProcessName() {
        return line.substring( processStart, processEnd );
    }

    public String getMethod() {
        return line.substring( methodStart, methodEnd );
    }

    public String getMessage() {
        return line.substring( messageStart, messageEnd );
    }

    public String getTime() {
        return line.substring( timeStart, timeEnd );
    }

    public char getMode() {
        return mode;
    }

    public int getRow() {
        return row;
    }

    public LogEntry getParent() {
        return parent;
    }

    public void setParent( LogEntry parent ) {
        this.parent = parent;
    }

    public void appendMessage( String text ) {
        line += text;
        messageEnd += text.length();
    }

    public void addChild( LogEntry child ) {
        children.add( child );
        child.parent = this;
    }
    
    public void addNewIntermediateChild( LogEntry intermediateChild ) {
        intermediateChild.children.addAll( children );
        children.clear();
        children.add( intermediateChild );
    }
    
    public int getChildCount() {
        return children.size();
    }
    
    public LogEntry getChild( int index ) {
        return children.get( index );
    }
    
    public int getIndexOfChild( Object child ) {
        return children.indexOf( child );
    }
    
    public String toString() {
        StringBuilder builder = new StringBuilder();
        if ( methodEnd - methodStart > 0 ) {
            builder.append( getMethod() );
            builder.append( " " );
        }
        builder.append( getMessage() );
        return builder.toString();
    }

    private static int FirstNonSpace( int pos, String line ) {
        while ( pos < line.length() && line.charAt( pos ) == ' ' )
            pos++;
        return pos < line.length() ? pos : -1;
    }

    private static int FirstSpace( int pos, String line ) {
        while ( pos < line.length() && line.charAt( pos ) != ' ' )
            pos++;
        return pos < line.length() ? pos : -1;
    }

    public static LogEntry buildFrom( int row, String line ) {
        LogEntry entry = new LogEntry( row, line );
        int start = 0;
        int end = 0;

        // Process
        start = FirstNonSpace( end, line );
        if ( start == -1 ) {
            return null;
        }
        end = FirstSpace( start, line );
        if ( end == -1 ) {
            return null;
        }
        entry.processStart = start;
        entry.processEnd = end;

        // Address
        start = FirstNonSpace( end, line );
        if ( start == -1 ) {
            return null;
        }
        end = FirstSpace( start, line );
        if ( end == -1 ) {
            return null;
        }

        // Time
        start = FirstNonSpace( end, line );
        if ( start == -1 ) {
            return null;
        }
        end = FirstSpace( start, line );
        if ( end == -1 ) {
            return null;
        }
        entry.timeStart = start;
        entry.timeEnd = end;
        if ( !entry.getTime().matches( "\\[..:..:..![0-9]+\\]" ) ) {
            return null; // rudimentary validation
        }

        // ???
        start = FirstNonSpace( end, line );
        if ( start == -1 ) {
            return null;
        }
        end = FirstSpace( start, line );
        if ( end == -1 ) {
            return null;
        }

        // Mode
        start = FirstNonSpace( end, line );
        if ( start == -1 ) {
            return null;
        }
        end = FirstSpace( start, line );
        if ( end == -1 ) {
            return null;
        }
        entry.mode = line.charAt( start );

        // Method
        start = FirstNonSpace( end, line );
        if ( start == -1 ) {
            return null;
        }
        end = FirstSpace( start, line );
        if ( end == -1 ) {
            return null;
        }
        // The first line of each process looks like:
        // ReaderServer (b7f676d0:15161,005c4c40) [15:49:41!806015] B S ::+++++ NEW START +++++ Wed Apr 2 15:49:41 2014 +++++
        if ( entry.getMode() == 'S' ) {
            entry.messageStart = start;
            entry.messageEnd = line.length();
            return entry;
        }
        else {
            entry.methodStart = start;
            entry.methodEnd = end;
        }

        // Message
        start = FirstNonSpace( end, line );
        entry.messageStart = Math.max( entry.methodEnd, start );
        entry.messageEnd = line.length();

        return entry;
    }
}
