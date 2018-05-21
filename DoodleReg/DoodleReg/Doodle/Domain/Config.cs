using Newtonsoft.Json;
using System;
using System.IO;

namespace DoodleReg.Doodle.Domain
{
    public class Configuration
    {
        private static readonly string DEFAULT_CONFIG_NAME = "config.json";

        public string UserNameToRegister { get; set; }

        public static Configuration getDefault()
        {
            Configuration config = new Configuration();
            config.UserNameToRegister = "Pámer Bálint";
            return config;
        }

        public static Configuration loadOrGetDefault()
        {

            Configuration config = null;
            try
            {
                config = JsonConvert.DeserializeObject<Configuration>( File.ReadAllText( DEFAULT_CONFIG_NAME ) );
            }
            catch ( Exception )
            {
                config = getDefault();
                string config_json = JsonConvert.SerializeObject( config );
                File.WriteAllText( DEFAULT_CONFIG_NAME, config_json );
            }
            return config;
        }
    }
}
