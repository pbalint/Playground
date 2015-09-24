#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QFileSystemWatcher>
#include <QTimer>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT
    
public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();
    
private:
    static const int     timer_tick = 1000;

    Ui::MainWindow      *ui;
    QFileSystemWatcher   watcher;
    QTimer               timer;
    QString              log_file_name;
    QString              window_title;
    bool                 read_log_file;

private slots:
    void OpenLogClicked();
    void OpenFilterClicked();
    void LogChanged( const QString & path );
    void ReadLog();
};

#endif // MAINWINDOW_H
