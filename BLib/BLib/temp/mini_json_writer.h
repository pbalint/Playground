#pragma once

#include <string>
#include <list>

using namespace std;

/* small class to create a json string (e.g. for javascript)
 * (originally created to send booking data to vsc)
 */

class JSONValue  // base class
{
protected:
    string name;

    static string DoubleToString( double value );
    JSONValue( string name = "" ) { this->name = name; }

public:
    virtual ~JSONValue() {}

    virtual JSONValue* AddValue( JSONValue* value ) { return NULL; }
    virtual void AddNull( string name = "" ) {}
    virtual void AddBool( bool value ) {}
    virtual void AddBool( string name, bool value ) {}
    virtual void AddString( string value ) {}
    virtual void AddString( double value ) {}   // "1.2"
    virtual void AddString( string name, string value ) {}
    virtual void AddString( string name, double value ) {} 
    virtual void AddNumber( double value ) {}   // 1.2
    virtual void AddNumber( string name, double value ) {}
    virtual JSONValue* AddObject( string name = "" ) { return NULL; }
    virtual JSONValue* AddArray( string name = "" ) { return NULL; }

    string GetName() { return name; }
	virtual bool GetBool() { return false; }
	virtual string GetString() { return ""; }
	virtual double GetNumber() { return 0.0; }
    
    void SetName( string name ) { this->name = name; }
	virtual void SetBool( bool value ) {}
	virtual void SetString( string value ) {}
	virtual void SetNumber( double value ) {}

	virtual int GetCount() { return 0; }

    virtual JSONValue* GetValue( int index ) { return NULL; }
    virtual JSONValue* GetValue( string name, bool let_children_search = false ) { return NULL; }

    virtual JSONValue* Clone() = 0;

    virtual JSONValue* RemoveValue( int index ) { return NULL; }
    virtual JSONValue* RemoveValue( string name, bool let_children_search = false ) { return NULL; }

    virtual bool DeleteValue( int index ) { return false; }
    virtual bool DeleteValue( string name, bool let_children_search = false ) { return false; }

    virtual void Output( string &output_string, int tab_count = 0 );
};

class JSONNull : public JSONValue
{
public:
    JSONNull( string name = "" ): JSONValue( name ) {}
    virtual JSONValue* Clone() { return new JSONNull( name ); }
    virtual void Output( string &output_string, int tab_count = 0 );
};

class JSONBool : public JSONValue
{
protected:
    bool value;

public:
    JSONBool( bool value, string name = "" ): JSONValue( name ) { this->value = value; }
    virtual JSONValue* Clone() { return new JSONBool( value, name ); }
	virtual bool GetBool() { return value; }
	virtual void SetBool( bool value ) { this->value = value; }
    virtual void Output( string &output_string, int tab_count = 0 );
};

class JSONString : public JSONValue
{
protected:
    string value;

public:
    JSONString( string &value, string name = "" ): JSONValue( name ) { this->value = value; }
    JSONString( double value, string name = "" ): JSONValue( name ) { this->value = DoubleToString( value ); }
	virtual string GetString() { return value; }
	virtual void SetString( string value ) { this->value = value; }
    virtual JSONValue* Clone() { return new JSONString( value, name ); }
    virtual void Output( string &output_string, int tab_count = 0 );
};

class JSONNumber : public JSONValue
{
protected:
    double value;

public:
    JSONNumber( double value, string name = "" ): JSONValue( name ) { this->value = value; }
    virtual JSONValue* Clone() { return new JSONNumber( value, name ); }
	virtual double GetNumber() { return value; }
	virtual void SetNumber( double value ) { this->value = value; }
    virtual void Output( string &output_string, int tab_count = 0 );
};

class JSONObject : public JSONValue
{
protected:
    list< JSONValue* > values;

public:

    JSONObject( string name = "" ): JSONValue( name ) {}
    virtual JSONValue* Clone();
    virtual ~JSONObject();

    virtual JSONValue* AddValue( JSONValue* value ) { if ( value ) values.push_back( value ); return value; }
    virtual void AddNull( string name = "" );
    virtual void AddBool( bool value );
    virtual void AddBool( string name, bool value );
    virtual void AddString( string value );
    virtual void AddString( double value );
    virtual void AddString( string name, string value );
    virtual void AddString( string name, double value );
    virtual void AddNumber( double value );
    virtual void AddNumber( string name, double value );
    virtual JSONValue* AddObject( string name = "" );
    virtual JSONValue* AddArray( string name = "" );

    virtual int GetCount() { return values.size(); }

    virtual JSONValue* GetValue( int index );
    virtual JSONValue* GetValue( string name, bool let_children_search = false );

    virtual JSONValue* RemoveValue( int index );
    virtual JSONValue* RemoveValue( string name, bool let_children_search = false );

    virtual bool DeleteValue( int index );
    virtual bool DeleteValue( string name, bool let_children_search = false );

    virtual void Output( string &output_string, int tab_count = 0 );
};

class JSONArray : public JSONObject
{
public:
    JSONArray( string name = "" ): JSONObject( name ) {}
    virtual JSONValue* Clone();
    virtual void Output( string &output_string, int tab_count = 0 );
};

