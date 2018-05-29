using System.Reflection;
using Newtonsoft.Json;
using System;
using System.IO;
using log4net;

namespace DoodleReg.Doodle.Domain
{
    public class Configuration
    {
        private static readonly ILog LOG = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
        private static readonly string DEFAULT_CONFIG_NAME = "config.json";

        [JsonProperty("userNameToRegister")] public string UserNameToRegister { get; set; }

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
            catch ( Exception e )
            {
                LOG.ErrorFormat( "{0} Exception caught: ", e );
                config = getDefault();
                string config_json = JsonConvert.SerializeObject( config );
                File.WriteAllText( DEFAULT_CONFIG_NAME, config_json );
            }
            return config;
        }
    }
}
