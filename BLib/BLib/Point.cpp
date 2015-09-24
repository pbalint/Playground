#include "Point.h"

namespace BLib
{

Point::Point( int x, int y )
{
    this->x = x;
    this->y = y;
}

int& Point::X()
{
    return x;
}

int& Point::Y()
{
    return y;
}

Rectangle::Rectangle( Point top_left, Point bottom_right )
    :
    top_left( top_left ),
    bottom_right( bottom_right )
{
}

Point& Rectangle::TopLeft()
{
    return top_left;
}

Point& Rectangle::BottomRight()
{
    return bottom_right;
}

int Rectangle::Top()
{
    return top_left.Y();
}

int Rectangle::Left()
{
    return top_left.X();
}

int Rectangle::Bottom()
{
    return bottom_right.Y();
}

int Rectangle::Right()
{
    return bottom_right.X();
}

int Rectangle::Width()
{
    return Right() - Left();
}

int Rectangle::Height()
{
    return Bottom() - Top();
}


}
