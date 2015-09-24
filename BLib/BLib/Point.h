#pragma once

namespace BLib
{

class Point
{
private:
    int x;
    int y;

public:
    Point( int x, int y );
    int& X();
    int& Y();
};

class Rectangle
{
private:
    Point top_left;
    Point bottom_right;

public:
    Rectangle( Point top_left, Point bottom_right );
    Rectangle( int x0, int y0, int x1, int y1 );
    Point& TopLeft();
    Point& BottomRight();
    int Top();
    int Left();
    int Bottom();
    int Right();
    int Width();
    int Height();
};

}
