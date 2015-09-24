#pragma once

#include <stddef.h>

#pragma warning( push )
#pragma warning( disable: 4127 ) // conditional expression is constant

namespace BLib
{

template< typename Type, bool Array = false >
class RefCountedPtr
{
protected:
    template< typename Type, bool Array >
    class Holder
    {
    friend class RefCountedPtr;

    protected:
        Type*   ptr;
        int     reference_count;
        bool    read_only;

    public:
        Holder( Type* ptr, bool read_only = false ) { Set( ptr, read_only ); }
        ~Holder()           { Release(); }
        int Use()           { return ++reference_count; }
        int Release()       
        {
            if ( reference_count == 0 ) return reference_count;

            if ( --reference_count == 0 )
            {
                if ( !read_only )
                {
                    if ( Array ) delete[] ptr;
                    else         delete ptr;
                }
                ptr = 0;
            }
            return reference_count;
        }
        void Set( Type* ptr, bool read_only = false )
        {
            this->ptr        = ptr;
            this->read_only  = read_only;
            reference_count  = 1;
        }
    };

    Holder< Type, Array >*  holder;

public:

    RefCountedPtr( bool read_only = false )
    {
        holder   = new Holder< Type, Array >( 0, read_only );
    }
    
    RefCountedPtr( Type* ptr, bool read_only = false )
    {
        holder   = new Holder< Type, Array >( ptr, read_only );
    }

    RefCountedPtr( const RefCountedPtr &other )
    {
        holder = other.holder;
        holder->Use();
    }

    ~RefCountedPtr()
    {
        if ( holder->Release() == 0 ) delete holder;
    }

    RefCountedPtr< Type, Array >& operator =( const RefCountedPtr< Type, Array > &other )
    {
        if ( holder != other.holder )
        {
            if ( holder->Release() == 0 ) delete holder;
            holder = other.holder;
            holder->Use();
        }

        return *this;
    }

    RefCountedPtr< Type, Array >& operator =( const Type* ptr )
    {
        if ( holder->ptr != ptr )
        {
            if ( holder->Release() == 0 ) holder->Set( (Type*)ptr, true );
            else holder = new Holder< Type, Array >( (Type*)ptr, true );
        }
        return *this;
    }

    RefCountedPtr< Type, Array >& operator =( Type* ptr )
    {
        if ( holder->ptr != ptr )
        {
            if ( holder->Release() == 0 ) holder->Set( (Type*)ptr, false );
            else holder = new Holder< Type, Array >( (Type*)ptr, false );
        }
        return *this;
    }

    RefCountedPtr< Type, Array >& Set( const Type* ptr, bool read_only = false )
    {
        if ( holder->ptr != ptr )
        {
            if ( holder->Release() == 0 ) holder->Set( (Type*)ptr, read_only );
            else holder = new Holder< Type, Array >( (Type*)ptr, read_only );
        }

        return *this;
    }

    template < class NewType > NewType* As() { return (NewType*)holder->ptr; }
    template < class NewType > const NewType* As() const { return (const NewType*)holder->ptr; }

    operator Type*&() const { return holder->ptr; }
    Type& operator*() { return *(holder->ptr); }
    Type*& operator->() { return holder->ptr; }
};

#pragma warning( pop )

}