package com.pb.steam.requestsresponses;

import java.util.Map;

public class SteamApiRequest {
    public enum Type {
        GET, POST;
    }

    private final Map< String, String > parameters;
    private final String                apiInterface;
    private final String                method;
    private final int                   version;
    private final Type                  type;

    public SteamApiRequest( String apiInterface, String method, int version, Type type, Map< String, String > parameters ) {
        this.parameters = parameters;
        this.apiInterface = apiInterface;
        this.method = method;
        this.version = version;
        this.type = type;
    }

    public String getInterface() {
        return apiInterface;
    }

    public String getMethod() {
        return method;
    }

    public int getVersion() {
        return version;
    }

    public Type getType() {
        return type;
    }

    public Map< String, String > getParameters() {
        return parameters;
    }
}
