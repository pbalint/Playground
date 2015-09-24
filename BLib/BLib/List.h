#pragma once

#include <stddef.h>

namespace BLib
{

template< typename TYPE >
class List
{
public:
    struct Node
    {
        TYPE      element;
        Node*     next;
        Node*     prev;

        Node( const TYPE& element )
            :
            element( element )
        { 
            next          = NULL;
            prev          = NULL;
        }

        Node( const TYPE& element, Node* prev )
            :
            element( element )
        {
            this->prev    = prev;
            this->next    = prev->next;
            prev->next    = this;
        }

        Node( const TYPE& element, Node* prev, Node* next )
            :
            element( element )
        {
            this->prev = prev; prev->next = this;
            this->next = next; next->prev = this;
        }
    };

protected:
    Node* first;
    Node* last;
    size_t  size;

public:
    List()
    {
        first = 0;
        last  = 0;
        size  = 0;
    }

    List( const List< TYPE >& other )
    {
        size = other.size;
        if ( size == 0 ) return;

        first = new Node( other.first->element );
        last  = first;
        Node* ptr = other.first->next;
        while ( ptr != NULL )
        {
            AddLast( ptr->element );
            ptr = ptr->next;
        }
    }

    ~List()
    {
        Clear();
    }

    List< TYPE >& operator =( const List< TYPE >& other )
    {
        if ( this == &other ) return;

        Clear();

        size = other.size;
        if ( size == 0 ) return;

        first = new Node( other.first->element );
        last  = first;
        Node* ptr = other.first->next;
        while ( ptr != NULL )
        {
            AddLast( ptr->element );
            ptr = ptr->next;
        }
    }

    void Clear()
    {
        while ( first )
        {
            last = first->next;
            delete first;
            first = last;
        }
        size = 0;
    }

    void AddFirst( const TYPE& element )
    {   
        if ( size == 0 )
        {
            first = new Node( element );
            last = first;
        }
        else
        {
            Node* ptr = first;
            first = new Node( element );
            first->next = ptr;
            ptr->prev = first;
        }
        size++;
    }

    void AddLast( const TYPE& element )
    {   
        if ( size == 0 )
        {
            first = new Node( element );
            last = first;
        }
        else
        {
            last = new Node( element, last );
        }
        size++;
    }

    Node* GetFirst() { return first; }
    Node* GetLast() { return last; }
    size_t GetSize() const { return size; }

    Node* GetNode( size_t index )
    {
        Node* ptr = first;
        while ( index > 0 && ptr )
        {
            ptr = ptr->next;
            index--;
        }
        return ptr;
    }

    TYPE& GetElement( size_t index )
    {
        return GetNode( index )->element;
    }

    TYPE Remove( size_t index )
    {
        Node* ptr = GetNode( index );

        if ( ptr->prev ) ptr->prev->next = ptr->next;
        if ( ptr->next ) ptr->next->prev = ptr->prev;
        if ( ptr == first ) first = ptr->next;
        if ( ptr == last ) last = ptr->prev;

        TYPE element = ptr->element;
        delete ptr;
        size--;

        return element;
    }

    TYPE RemoveFirst()
    {
        TYPE element = first->element;

        first = first->next;
        if ( size == 1 )
        {
            delete first;
            first = NULL;
            last = first;
        }
        else
        {
            Node* ptr = first;
            first = first->next;
            delete ptr;
        }

        size--;
        return element;
    }

    TYPE RemoveLast()
    {
        TYPE element = last->element;
        if ( size == 1 )
        {
            delete first;
            first = NULL;
            last = first;
        }
        else
        {
            Node* ptr = last;
            last = last->prev;
            delete ptr;
        }

        size--;
        return element;
    }
};

}