package com.pb.ui;

import javax.swing.tree.DefaultMutableTreeNode;

import com.pb.steam.requestsresponses.GetSupportedApiList.Interface;

@SuppressWarnings( "serial" )
public class InterfaceTreeNode extends DefaultMutableTreeNode {
    private Interface apiIterface;
    
    public InterfaceTreeNode( Interface apiIterface ) {
        this.apiIterface = apiIterface;
        setUserObject( apiIterface.name );
    }
    
    public Interface getInterface() {
        return apiIterface;
    }

}
