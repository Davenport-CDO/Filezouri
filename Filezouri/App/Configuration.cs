using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Filezouri.App
{
    public class Configuration
    {
        private Configuration() { }

        private static Configuration _current;

        public static Configuration Current
        {
            get
            {
                if (_current != null) return _current;

                if (!File.Exists(ConfigurationPath))
                {
                    new Configuration().SaveChanges();
                }

                return _current = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(ConfigurationPath));
            }
        }

        public static string ConfigurationPath => Path.Combine("filezouri_settings.json");

        public void SaveChanges()
        {
            File.WriteAllText(ConfigurationPath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        //Start actual config options
        public string Username { get; set; }
        public string Password { get; set; }
        public string DirectoryRoot { get; set; }
    }
}
