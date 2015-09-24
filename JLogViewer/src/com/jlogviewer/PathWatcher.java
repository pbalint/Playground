package com.jlogviewer;

import java.io.File;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Paths;
import java.nio.file.StandardWatchEventKinds;
import java.nio.file.WatchEvent;
import java.nio.file.WatchKey;
import java.nio.file.WatchService;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class PathWatcher extends Thread {
    public interface Listener {
        void PathChanged( WatchEvent< ? > event );
    }

    private class WatchedPath {
        private String   path;
        private Listener listener;
        private WatchKey watchKey;

        public WatchedPath( WatchKey key, String path, Listener listener ) {
            this.watchKey = key;
            this.path = path;
            this.listener = listener;
        }

        public Listener getListener() {
            return listener;
        }

        @SuppressWarnings( "unused" )
        public String getPath() {
            return path;
        }

        public WatchKey getWatchKey() {
            return watchKey;
        }
    }

    private WatchService                 watchService;
    private Map< WatchKey, WatchedPath > watchKeyMapping = new HashMap<>();
    private Map< String, WatchedPath >   pathMapping     = new HashMap<>();
    private Object                       lock            = new Object();
    private boolean                      running         = true;

    public PathWatcher() throws IOException {
        watchService = FileSystems.getDefault().newWatchService();
        start();
    }

    public void addPath( String pathName, Listener listener ) throws IOException {
        String dirName = pathName;
        File f = new File( pathName );
        if ( ( f.isFile() ) ) {
            dirName = f.getParent();
        }
        WatchKey watchKey = Paths.get( dirName ).register( watchService,
                                                           StandardWatchEventKinds.ENTRY_CREATE,
                                                           StandardWatchEventKinds.ENTRY_MODIFY,
                                                           StandardWatchEventKinds.ENTRY_DELETE );
        WatchedPath entry = new WatchedPath( watchKey, pathName, listener );
        synchronized ( lock ) {
            watchKeyMapping.put( watchKey, entry );
            pathMapping.put( pathName, entry );
        }
    }

    public void removePath( String pathName ) {
        synchronized ( lock ) {
            WatchedPath entry = pathMapping.remove( pathName );
            if ( entry != null ) {
                watchKeyMapping.remove( entry.getWatchKey() );
            }
            entry.getWatchKey().cancel();
        }
    }

    public void clear() {
        synchronized ( lock ) {
            pathMapping.clear();
            for ( WatchKey watchKey : watchKeyMapping.keySet() ) {
                watchKey.cancel();
            }
            watchKeyMapping.clear();
        }
    }

    public List< String > getWatchedPaths() {
        List< String > paths = new ArrayList<>();
        synchronized ( lock ) {
            for ( String path : pathMapping.keySet() ) {
                paths.add( path );
            }
        }
        return paths;
    }

    @Override
    public void run() {
        WatchKey key;
        while ( running ) {
            try {
                key = watchService.take();
                Listener listener;
                synchronized ( lock ) {
                    listener = watchKeyMapping.get( key ).getListener();
                }
                for ( WatchEvent< ? > event : key.pollEvents() ) {
                    listener.PathChanged( event );
                }
                key.reset();
            }
            catch ( InterruptedException e ) {
                running = false;
            }
        }
    }

}
