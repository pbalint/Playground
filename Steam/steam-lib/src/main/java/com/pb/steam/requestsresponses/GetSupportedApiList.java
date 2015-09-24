package com.pb.steam.requestsresponses;

import java.util.List;
import java.util.Map;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonRootName;

@JsonRootName( value = "apilist" )
public class GetSupportedApiList {
    public static SteamApiRequest getRequest( Map< String, String > parameters ) {
        return new SteamApiRequest( "ISteamWebAPIUtil", "GetSupportedAPIList", 1, SteamApiRequest.Type.GET, parameters );
    }

    public static class Interface {
        public static class Method {
            public static class Parameter {
                public String  name;
                public String  type;
                public boolean optional;
                public String  description;

            }

            public String            name;
            public int               version;
            @JsonProperty( "httpmethod" )
            public String            method;
            public List< Parameter > parameters;
            public String            description;

        }

        public String         name;
        public List< Method > methods;

    }

    public List< Interface > interfaces;

}
