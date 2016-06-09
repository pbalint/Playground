using InputPlayback.Input.Native;
using InputPlayback.Input.Native.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace InputPlayback
{
    public class Utils
    {
        private static Type[] actionTypes;
        public static Type[] ActionTypes { get { return actionTypes; } }

        static Utils()
        {
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type assemblyType in assembly.GetTypes())
                {
                    if (typeof(Actions.Action).IsAssignableFrom(assemblyType) && !assemblyType.IsAbstract)
                    {
                        types.Add(assemblyType);
                    }
                }
            }

            actionTypes = types.ToArray();
        }

        public static void SerializeActions<T>(T obj, string file_name)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType(), actionTypes);
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter(file_name);
                serializer.Serialize(stream, obj);
            }
            catch (Exception e) { Console.Error.WriteLine(e); }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static void DeserializeActions<T>(ref T obj, string file_name)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType(), actionTypes);
            StreamReader stream = null;
            try
            {
                stream = new StreamReader(file_name);
                obj = (T)serializer.Deserialize(stream);
            }
            catch (Exception e) { Console.Error.WriteLine(e); }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static KEYBDINPUT GetKeyInputFromChar(char ch)
        {
            KEYBDINPUT keyboard = new KEYBDINPUT();
            keyboard.KeyCode = 0;
            keyboard.Scan = ch;
            keyboard.ExtraInfo = IntPtr.Zero;
            keyboard.Flags = KeyboardFlag.Unicode;
            return keyboard;
        }
    }
}
