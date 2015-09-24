#pragma once

#include <stddef.h>
#include <new>

namespace BLib
{

template< typename TYPE >
class Array
{
protected:
    static const size_t     default_grow_amount = 4;
    static const size_t     max_grow_amount     = 1024;
    TYPE*                   elements;
    size_t                  element_count;
    size_t                  capacity;
    size_t                  grow_amount;

public:
    Array( size_t grow_amount = default_grow_amount )
    {
        this->element_count = 0;
        this->capacity      = 0;
        this->grow_amount   = grow_amount;
        this->elements      = NULL;
    }

    ~Array()
    {
        Clear();
    }

    void Clear()
    {
        for ( size_t i=0; i<element_count; i++ )
        {
            elements[ i ].~TYPE();
        }
        free( elements );
        element_count   = 0;
        capacity        = 0;
        elements        = NULL;
        grow_amount     = default_grow_amount;
    }

    const TYPE& operator[] ( size_t index ) const
    {
        return elements[ index ];
    }

    TYPE& operator[] ( size_t index )
    {
        return elements[ index ];
    }

    size_t GetSize() { return element_count; }
    size_t GetCapacity() { return capacity; }

    bool AddLast( const TYPE& element )
    {
        if ( element_count >= capacity )
        {
            const size_t new_capacity = capacity + grow_amount;
            grow_amount *= 2;
            if ( grow_amount > max_grow_amount )
            {
                grow_amount = max_grow_amount;
            }

            if ( elements != NULL )
            {
                TYPE* new_elements = (TYPE*)realloc( elements, new_capacity * sizeof( TYPE ) );
                if ( !new_elements ) return false;
                elements = new_elements;
            }
            else
            {
                elements = (TYPE*)malloc( new_capacity * sizeof( TYPE ) );
            }
            new ( &elements[ element_count ] ) (TYPE);

            capacity = new_capacity;
        }
        elements[ element_count++ ] = element;
        return true;
    }

    TYPE& RemoveLast()
    {
        TYPE& element = elements[ --element_count ];
        return element;
    }

    void DeleteLast()
    {
        RemoveLast().~TYPE();
    }

    bool MakeExactSize()
    {
        if ( element_count == capacity ) return;

        TYPE* new_elements = realloc( elements, element_count * sizeof( TYPE ) );
        if ( !new_elements ) return false;

        elements = new_elements
        capacity = element_count;
        return true;
    }
};

}