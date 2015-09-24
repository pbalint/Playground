package com.pb;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;

public final class Utils {
    public static < T > void serializeToXmlFile( T object, String fileName ) {
        try {
            JAXBContext context = JAXBContext.newInstance( object.getClass() );
            try ( FileOutputStream outStream = new FileOutputStream( fileName ) ) {
                context.createMarshaller().marshal( object, outStream );
            }
        }
        catch ( JAXBException | IOException e ) {
        }
    }

    @SuppressWarnings( "unchecked" )
    public static < T > T deserializeFromXmlFile( T defaultObject, String fileName ) {
        T object = defaultObject;
        try {
            JAXBContext context = JAXBContext.newInstance( defaultObject.getClass() );
            try ( FileInputStream inStream = new FileInputStream( fileName ) ) {
                object = (T)context.createUnmarshaller().unmarshal( inStream );
            }
        }
        catch ( JAXBException | IOException e ) {
        }
        return object;
    }
}
