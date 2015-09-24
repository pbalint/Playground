package com.pb.ui;

import javax.swing.tree.DefaultMutableTreeNode;

import com.pb.steam.requestsresponses.GetSupportedApiList.Interface.Method;

@SuppressWarnings( "serial" )
public class MethodTreeNode extends DefaultMutableTreeNode {
    private Method method;

    public MethodTreeNode( Method method ) {
        this.method = method;
        if ( method.description != null && !method.description.isEmpty() ) {
            setUserObject( method.name + " v" + Integer.toString( method.version ) + " (" + method.description + ")" );
        }
        else {
            setUserObject( method.name + " v" + Integer.toString( method.version ) );
        }
    }

    public Method getMethod() {
        return method;
    }
}
