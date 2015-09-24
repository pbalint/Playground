#include <windows.h>
#include <stdio.h>
#include "BLib/UI/Label.h"
#include "BLib/UI/Button.h"
#include "BLib/Time.h"
#include "BLib/Image.h"
#include "BLib/UI/Icon.h"
#include "Blib/UI/TrayIcon.h"

using namespace BLib;

class MainWindow
{
private:
    Label* l;
    Button* b;
    Window* window;

public:
    MainWindow()
    {

        window = new Window( "Test", 0, 0, 640, 480 );

        String str = "I'm a label";
        l = new Label( window, str, 0, 0 );
        b = new Button( window, "Click me!", 0, 16 );
        b->SetSize( 200, 32 );

        l->click = new Window::Callback<MainWindow>( this, &MainWindow::LabelClicked );
        l->mouse_down = new Window::Callback1<MainWindow, Mouse&>( this, &MainWindow::LabelClickedWithMouse );
        b->mouse_down = new Window::Callback1<MainWindow, Mouse&>( this, &MainWindow::HoverOverButton );
        
        Icon icon( "D:\\icon.ico" );
        TrayIcon* tray_icon = new TrayIcon( window, icon );

        /*PAINTSTRUCT ps;
        HDC dc = GetDC( NULL );
        Icon icon( "D:\\icon.ico" );
        DrawIconEx( dc, 10, 100, icon, 0, 0, 0, 0, DI_MASK | DI_IMAGE );
        ReleaseDC( NULL, dc );*/
    }

    virtual ~MainWindow()
    {
        delete window;
    }

    void LabelClicked()
    {
        OutputDebugString( L"haha\n" );
    }

    void LabelClickedWithMouse( Mouse& /*mouse*/ )
    {
        OutputDebugString( L"down" );
    }

    void HoverOverButton( Mouse& /*mouse*/ )
    {
        b->SetText( b->GetText() += "a" );
    }

};

int WINAPI WinMain( HINSTANCE, HINSTANCE, LPSTR, int )
{
    Window::Init();

    MainWindow* main_window = new MainWindow();

    Window::DoMessageLoop();

    delete main_window;

    Window::Finish();
    //CreatePopupMenu
    return 0;
}

