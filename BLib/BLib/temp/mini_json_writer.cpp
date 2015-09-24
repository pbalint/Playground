#include "mini_json_writer.h"
#include <math.h>

#define TAB   {for ( int i=0; i<tab_count; i++ ) output_string += "  ";}

void JSONValue::Output( string &output_string, int tab_count )
{
    TAB;
    if ( name.length() > 0 ) output_string += "\"" + name  + "\": ";
}

string JSONValue::DoubleToString( double value )
{
    char buff[128];
    double fractional_part, whole_part;
    fractional_part = modf( value, &whole_part );
    if ( fractional_part == 0 )
    {
        _snprintf( buff, 128, "%i", (int)value );
    }
    else
    {
        _snprintf( buff, 128, "%.2f", value );
    }

    return (string)buff;
}

void JSONNull::Output( string &output_string, int tab_count )
{
    JSONValue::Output( output_string, tab_count );
    output_string += "null";
}

void JSONBool::Output( string &output_string, int tab_count )
{
    JSONValue::Output( output_string, tab_count );
    output_string += value? "true": "false";
}

void JSONString::Output( string &output_string, int tab_count )
{
    JSONValue::Output( output_string, tab_count );
    output_string += "\"" + value + "\"";
}

void JSONNumber::Output( string &output_string, int tab_count )
{
    JSONValue::Output( output_string, tab_count );
    output_string += DoubleToString( value );
}

void JSONObject::Output( string &output_string, int tab_count )
{
    if ( name != "" )
    {
        JSONValue::Output( output_string, tab_count );
        output_string += "\n";
    }

    TAB;
    output_string += "{\n";
    tab_count++;
    if ( values.size() > 0 )
    {
        list< JSONValue* >::iterator it = values.begin();
        for ( unsigned int i=0; i < values.size() - 1; i++ )
        {
            (*it)->Output( output_string, tab_count );
            output_string += ",\n";
            ++it;
        }
        (*it)->Output( output_string, tab_count );
        output_string += "\n";
    }
    tab_count--;
    TAB;
    output_string += "}";
}

JSONObject::~JSONObject()
{
    JSONValue* value_to_delete;
    for (   list< JSONValue* >::iterator it = values.begin();
            it != values.end();
            )
    {
        value_to_delete = *it;
        ++it;
        delete value_to_delete;
    }
}

void JSONArray::Output( string &output_string, int tab_count )
{
    if ( name != "" )
    {
        JSONValue::Output( output_string, tab_count );
        output_string += "\n";
    }

    TAB;
    output_string += "[\n";
    tab_count++;
    if ( values.size() > 0 )
    {
        list< JSONValue* >::iterator it = values.begin();
        for ( unsigned int i=0; i < values.size() - 1; i++ )
        {
            (*it)->Output( output_string, tab_count );
            output_string += ",\n";
            ++it;
        }
        (*it)->Output( output_string, tab_count );
        output_string += "\n";
    }
    tab_count--;
    TAB;
    output_string += "]";
}

void JSONObject::AddNull( string name )                 { AddValue( new JSONNull( name ) ); }
void JSONObject::AddBool( bool value )                  { AddValue( new JSONBool( value ) ); }
void JSONObject::AddBool( string name, bool value )     { AddValue( new JSONBool( value, name ) ); }
void JSONObject::AddString( string value )              { AddValue( new JSONString( value ) ); }
void JSONObject::AddString( double value )              { AddValue( new JSONString( value ) ); }
void JSONObject::AddString( string name, string value ) { AddValue( new JSONString( value, name ) ); }
void JSONObject::AddString( string name, double value ) { AddValue( new JSONString( value, name ) ); } 
void JSONObject::AddNumber( double value )              { AddValue( new JSONNumber( value ) ); }
void JSONObject::AddNumber( string name, double value ) { AddValue( new JSONNumber( value, name ) ); }
JSONValue* JSONObject::AddObject( string name )         { return AddValue( new JSONObject( name ) ); }
JSONValue* JSONObject::AddArray( string name )          { return AddValue( new JSONArray( name ) ); }

JSONValue* JSONObject::GetValue( int index )
{
    list< JSONValue* >::iterator it = values.begin();
    while ( it != values.end() )
    {
        if ( index-- == 0 ) return *it;
        ++it;
    }
    return NULL;
}

JSONValue* JSONObject::GetValue( string name, bool let_children_search )
{
    // first search children
    list< JSONValue* >::iterator it = values.begin();
    while ( it != values.end() )
    {
        if ( (*it)->GetName() == name ) return *it;
        ++it;
    }

    if ( let_children_search )
    {
        // then let the children search theirs
        it = values.begin();
        while ( it != values.end() )
        {
            JSONValue* value = (*it)->GetValue( name, true );
            if ( value != NULL ) return value;
            ++it;
        }
    }
    return NULL;
}

bool JSONObject::DeleteValue( int index )
{
    JSONValue* obj = RemoveValue( index );
    if ( obj )
    {
        delete obj;
        return true;
    }
    return false;
}

bool JSONObject::DeleteValue( string name, bool let_children_search )
{
    JSONValue* obj = RemoveValue( name, let_children_search );
    if ( obj )
    {
        delete obj;
        return true;
    }
    return false;
}

JSONValue* JSONObject::RemoveValue( int index )
{
    
    list< JSONValue* >::iterator it = values.begin();
    while ( it != values.end() )
    {
        if ( index-- == 0 )
        {
            JSONValue* return_value = *it;
            values.erase( it );
            return return_value;
        }
        ++it;
    }

    return NULL;
}

JSONValue* JSONObject::RemoveValue( string name, bool let_children_search )
{
    // first search children
    list< JSONValue* >::iterator it = values.begin();
    while ( it != values.end() )
    {
        if ( (*it)->GetName() == name )
        {
            JSONValue* return_value = *it;
            values.erase( it );
            return return_value;
        }
        ++it;
    }

    if ( let_children_search )
    {
        // then let the children search theirs
        it = values.begin();
        while ( it != values.end() )
        {
            JSONValue* return_value = (*it)->RemoveValue( name, true );
            if ( return_value != NULL ) return return_value;
            ++it;
        }
    }
    return NULL;
}

JSONValue* JSONObject::Clone()
{
    JSONValue* clone = new JSONObject( name );
    list< JSONValue* >::iterator it = values.begin();
    while ( it != values.end() )
    {
        clone->AddValue( (*it)->Clone() );
        ++it;
    }
    return clone;
}

JSONValue* JSONArray::Clone()
{
    JSONValue* clone = new JSONArray( name );
    list< JSONValue* >::iterator it = values.begin();
    while ( it != values.end() )
    {
        clone->AddValue( (*it)->Clone() );
        ++it;
    }
    return clone;
}

