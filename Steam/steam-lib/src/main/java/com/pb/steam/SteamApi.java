package com.pb.steam;

import java.io.IOException;
import java.util.Map;
import java.util.Map.Entry;

import org.apache.commons.io.IOUtils;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.client.methods.RequestBuilder;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClientBuilder;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.pb.steam.requestsresponses.SteamApiRequest;

public class SteamApi {
    private static final String URL_BASE = "http://api.steampowered.com/";
    private static final String FORMAT   = "json";
    private CloseableHttpClient client;
    private ObjectMapper        mapper;

    public SteamApi() {
        this.client = HttpClientBuilder.create()/*.setProxy( new HttpHost( "localhost", 8888 ) )*/.build();
        this.mapper = new ObjectMapper().configure( DeserializationFeature.UNWRAP_ROOT_VALUE, true );
    }

    private String getRequestStringRaw( String apiInterface, String method, int version, SteamApiRequest.Type type, Map< String, String > parameters ) {
        StringBuilder builder = new StringBuilder();
        builder.append( URL_BASE )
               .append( apiInterface )
               .append( '/' ).append( method )
               .append( "/v" ).append( Integer.toString( version ) )
               .append( "/?format=" ).append( FORMAT );
        if ( parameters != null && type == SteamApiRequest.Type.GET ) {
            for ( Entry< String, String > parameter : parameters.entrySet() ) {
                if ( parameter.getValue() != null && !parameter.getValue().isEmpty() ) {
                    builder.append( '&' ).append( parameter.getKey() ).append( '=' ).append( parameter.getValue() );
                }
            }
        }
        return builder.toString();
    }

    public String getResponseRaw( String apiInterface, String method, int version, SteamApiRequest.Type type, Map< String, String > parameters ) {
        String requestString = getRequestStringRaw( apiInterface, method, version, type, parameters );
        String responseString = null;
        System.out.println( "Request: " + requestString );
        
        HttpUriRequest request;
        if ( type == SteamApiRequest.Type.GET ) {
            request = RequestBuilder.get().setUri( requestString ).build();
        }
        else {
            RequestBuilder postBuilder = RequestBuilder.post().setUri( requestString );
            
            for ( Entry< String, String > parameter : parameters.entrySet() ) {
                if ( parameter.getValue() != null && !parameter.getValue().isEmpty() ) {
                    postBuilder.addParameter( parameter.getKey(), parameter.getValue() );
                }
            }
            request = postBuilder.build();
        }
        try ( CloseableHttpResponse response = client.execute( request ) ) {
            responseString = IOUtils.toString( response.getEntity().getContent() );
            System.out.println( "Response: " + responseString );
            return responseString;
        }
        catch ( Exception e ) {
            e.printStackTrace();
            return null;
        }
    }

    public < T > T getResponse( SteamApiRequest request, Class< T > cls ) {
        String responseString = getResponseRaw( request.getInterface(), request.getMethod(), request.getVersion(), request.getType(), request.getParameters() );
        try {
            return mapper.readValue( responseString, cls );
        }
        catch ( IOException e ) {
            e.printStackTrace();
            return null;
        }
    }
}
