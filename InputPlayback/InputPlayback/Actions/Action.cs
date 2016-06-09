using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;

namespace InputPlayback.Actions
{
    [Serializable]
    public abstract class Action
    {
        private static TypeConverter converter = new TypeConverter();

        virtual
        public void Invoke(Worker.State state) {}

        override
        public string ToString()
        {
            return GetType().Name;
        }

        public IList<ParameterContainer> GetParameters()
        {
            SortedList< uint, ParameterContainer> parameters = new SortedList<uint, ParameterContainer>();

            FieldInfo[] fields = GetType().GetFields( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
            foreach ( FieldInfo info in fields )
            {
                Attribute parameterAttribute = info.GetCustomAttribute( typeof( Parameter ) );
                if ( parameterAttribute == null ) continue;

                Parameter parameter = ( (Parameter)parameterAttribute );
                parameters.Add( parameter.Order, new ParameterContainer( parameter.Name, info.GetValue( this ), info.FieldType ) );
            }
            return parameters.Values;
        }

        public void SetParameters( Dictionary<string, object> parameters )
        {
            FieldInfo[] fields = GetType().GetFields( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach ( FieldInfo info in fields )
            {
                Attribute parameterAttribute = info.GetCustomAttribute( typeof( Parameter ) );
                if ( parameterAttribute == null ) continue;

                Parameter parameter = ( (Parameter)parameterAttribute );
                TypeConverter converter = TypeDescriptor.GetConverter( info.FieldType );
                info.SetValue( this, converter.ConvertFrom( parameters[ parameter.Name ] ) );
            }
        }
    }
}
