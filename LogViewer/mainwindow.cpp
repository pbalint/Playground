#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QFileDialog>

MainWindow::MainWindow( QWidget *parent ) :
    QMainWindow( parent ),
    ui( new Ui::MainWindow ),
    window_title( "LogViewer" ),
    read_log_file( false )
{
    ui->setupUi(this);

    QStringList header_labels;
    header_labels.append( "Entry" );
    header_labels.append( "Time" );
    ui->tw_log->setHeaderLabels( header_labels );
    //ui->tw_log->header()->resizeSection();

    connect( &watcher, SIGNAL( fileChanged( QString ) ), this, SLOT( LogChanged( QString ) ) );
    connect( &timer, SIGNAL( timeout() ), this, SLOT( ReadLog() ) );

    timer.setSingleShot( false );
    timer.setInterval( timer_tick );
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::OpenLogClicked()
{
    QString file_name = QFileDialog::getOpenFileName( this, "Select log file" );
    if ( file_name != "" )
    {
        if ( !log_file_name.isEmpty() )
        {
            watcher.removePath( log_file_name );
        }
        log_file_name = file_name;
        watcher.addPath( log_file_name );
        this->setWindowTitle( window_title + " - " + log_file_name );

        read_log_file = true;
        ReadLog();
        timer.start();
    }
}


void MainWindow::OpenFilterClicked()
{
}

void MainWindow::LogChanged( const QString & )
{
    read_log_file = true;
}

void MainWindow::ReadLog()
{
    if ( log_file_name != "" && !watcher.files().contains( log_file_name ) ) watcher.addPath( log_file_name );

    if ( !read_log_file ) return;
    read_log_file = false;

    ui->tw_log->addTopLevelItem( new QTreeWidgetItem() );
}
