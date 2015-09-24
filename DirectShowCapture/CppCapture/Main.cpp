#include <stdio.h>
#include <windows.h>
#include <dshow.h>

#include "DeviceEnumerator.h"
#include "FilterGraph.h"

using namespace std;

bool SaveImage( const char* szPathName, void* lpBits, int w, int h )
{
    //Create a new file for writing
    FILE *pFile = nullptr;
    fopen_s( &pFile, szPathName, "wb" );
    if ( pFile == NULL )
    {
        return false;
    }

    BITMAPINFOHEADER BMIH;
    BMIH.biSize = sizeof( BITMAPINFOHEADER );
    BMIH.biSizeImage = w * h * 3;
    // Create the bitmap for this OpenGL context
    BMIH.biSize = sizeof( BITMAPINFOHEADER );
    BMIH.biWidth = w;
    BMIH.biHeight = h;
    BMIH.biPlanes = 1;
    BMIH.biBitCount = 24;
    BMIH.biCompression = BI_RGB;
    BMIH.biSizeImage = w * h * 3;
    BITMAPFILEHEADER bmfh;
    int nBitsOffset = sizeof( BITMAPFILEHEADER ) + BMIH.biSize;
    LONG lImageSize = BMIH.biSizeImage;
    LONG lFileSize = nBitsOffset + lImageSize;
    bmfh.bfType = 'B' + ( 'M' << 8 );
    bmfh.bfOffBits = nBitsOffset;
    bmfh.bfSize = lFileSize;
    bmfh.bfReserved1 = bmfh.bfReserved2 = 0;
    //Write the bitmap file header
    UINT nWrittenFileHeaderSize = fwrite( &bmfh, 1, sizeof( BITMAPFILEHEADER ), pFile );
    //And then the bitmap info header

    UINT nWrittenInfoHeaderSize = fwrite( &BMIH, 1, sizeof( BITMAPINFOHEADER ), pFile );

    //Finally, write the image data itself 
    //-- the data represents our drawing

    UINT nWrittenDIBDataSize = fwrite( lpBits, 1, lImageSize, pFile );
    fclose( pFile );

    return true;
}

void cb( void* context, double time, unsigned char* buffer, long length )
{
    static int i = 0;
    string file_name = "image";
    file_name += to_string( i++ );
    file_name += ".bmp";
    SaveImage( file_name.c_str(), buffer, 640, 480 );
}

void test()
{
    DeviceEnumerator* enumerator = new DeviceEnumerator();
    //vector< shared_ptr< AudioDevice > > audio_devices = enumerator->GetAudioDevices();
    vector< shared_ptr< VideoDevice > > video_devices = enumerator->GetVideoDevices();
    shared_ptr< SampleGrabber > sample_grabber = enumerator->CreateSampleGrabber();
    shared_ptr< NullRenderer > null_renderer = enumerator->CreateNullRenderer();
    delete enumerator;

    sample_grabber->SetCallBack( nullptr, &cb );
    shared_ptr< FilterGraph > filter_graph = std::make_shared< FilterGraph >();
    filter_graph->Add( sample_grabber );
    filter_graph->Add( video_devices[ 1 ] );
    filter_graph->Add( null_renderer );
    filter_graph->Connect( video_devices[ 1 ]->GetOutputPins()[ 0 ], sample_grabber->GetInputPins()[ 0 ] );
    filter_graph->Connect( null_renderer->GetInputPins()[ 0 ], sample_grabber->GetOutputPins()[ 0 ] );
    filter_graph->Run();
    for ( int i = 0; i < 5; i++ ) {
        Sleep( 1000 );
    }
}

int main( int argc, char* argv[] )
{
    if ( CoInitializeEx( 0, COINIT_MULTITHREADED ) != S_OK )
    {
        printf( "Error" );
    }
    /*while ( true )
    {*/
        test();
    //}


    CoUninitialize();
    return 0;
}
