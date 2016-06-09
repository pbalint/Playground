using System;

namespace InputPlayback.Actions
{
    [AttributeUsage( AttributeTargets.Field )]
    class Parameter : Attribute
    {
        private string name;
        private uint order;

        public string Name { get { return name; } }
        public uint Order { get { return order; } }

        public Parameter( string name, uint order )
        {
            this.name = name;
            this.order = order;
        }
    }

    public class ParameterContainer
    {
        private string name;
        private object value;
        private Type type;

        public string Name { get { return name; } }
        public object Value { get { return value; } }
        public Type Type { get { return type; } }

        public ParameterContainer(string name, object value, Type type)
        {
            this.name = name;
            this.value = value;
            this.type = type;
        }
    }
}
