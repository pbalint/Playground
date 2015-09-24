#pragma once

#include "RefCountedPtr.h"

namespace BLib
{

class Buffer
{
friend class String;
protected:
    size_t                                  size;
    RefCountedPtr< unsigned char, true >    ptr;

public:
    Buffer();
    Buffer( size_t size );
    Buffer( const void* ptr, size_t size = 0 );
    Buffer( const Buffer& other );

    void CopyFrom( const void* src,     size_t size,     ptrdiff_t src_offset = 0, ptrdiff_t dst_offset = 0 );
    void CopyFrom( const Buffer& other, size_t size = 0, ptrdiff_t src_offset = 0, ptrdiff_t dst_offset = 0 );
    void CopyTo(   const void* dst,     size_t size = 0, ptrdiff_t src_offset = 0, ptrdiff_t dst_offset = 0 ) const;
    void CopyTo(   Buffer& other,       size_t size = 0, ptrdiff_t src_offset = 0, ptrdiff_t dst_offset = 0 ) const;
    unsigned char* GetPtr( size_t offset = 0 ) const { return ptr + offset; }
    size_t GetSize() const { return size; }
    void Map(       void* ptr, size_t size = 0 );
    void Map( const void* ptr, size_t size = 0 );
    void Map( const Buffer& other );

    void SetSize( size_t size );

    bool Save( const String& file_name );
    bool Load( const String& file_name );

    Buffer& operator =( const Buffer& other );
    Buffer& operator =( const void* ptr );
    operator void*() const { return GetPtr(); }
    operator unsigned char*() const { return GetPtr(); }

    void SHA1( Buffer& sha1 );
};

}