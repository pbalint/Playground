package com.pb.steam;

import java.util.HashMap;
import java.util.Map;

import com.pb.steam.requestsresponses.GetSupportedApiList;
import com.pb.steam.requestsresponses.GetSupportedApiList.Interface;
import com.pb.steam.requestsresponses.GetSupportedApiList.Interface.Method;

public class Main {
    private static final String API_KEY = "C2A64941E5ED4F2AF806463CD6E4FB3A";
    // private static final String API_DOMAIN = "po.no-ip.org";

    public static void main( String[] args ) {
        new Main().doStuff();
    }

    private void doStuff() {
        SteamApi api = new SteamApi();
        Map<String, String> parameters = new HashMap<>();
        parameters.put( "key", API_KEY );
        GetSupportedApiList apiList = api.getResponse( GetSupportedApiList.getRequest( parameters ), GetSupportedApiList.class );

        for ( Interface steamInterface : apiList.interfaces ) {
            for ( Method method : steamInterface.methods ) {
                System.out.println( steamInterface.name + "." + method.name + ": " + method.description );
            }
        }
    }
}
