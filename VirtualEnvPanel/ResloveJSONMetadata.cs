using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace VirtualEnvPanel
{
    class ResloveJSONMetadata
    {
        public string ConfigurationJSON { get; set; }
        public string Description { get; set; }
        private StartupCommand JsonObjectToCommand(JObject o)
        {
            StartupCommand cmd = new StartupCommand();
            cmd.Title = (string)o["Title"];
            cmd.Name = (string)o["Name"];
            cmd.Executable = (string)o["Executable"];
            cmd.InitializeScript = (string)o["InitializeScript"];
            var paths = (JArray)o["Path"];
            if (o["IsAdmin"]!=null&&(bool)o["IsAdmin"]) 
            {
                cmd.IsAdmin = true;
            }else
            {
                cmd.IsAdmin = false;
            }
            if (paths != null)
            {
                cmd.Path = paths.Select(c => (string)c).ToList();
            }
            var args = (JArray)o["Args"];
            if (args != null)
            {
                cmd.Args = args.Select(c => (string)c).ToList();
            }
            return cmd;
        }
        public ResloveJSONMetadata(String f)
        {
            ConfigurationJSON = f;

            //// do some thing
        }
        public List<StartupCommand> StartupCommandGet()
        {
            var list = new List<StartupCommand>();
            JObject obj = null;
            using (StreamReader reader = File.OpenText(ConfigurationJSON))
            {
                obj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
            Description = (string)obj["Description"];
            JArray app = (JArray)obj["App"];
            if (app != null)
            {
                foreach(var o in app)
                {
                    var cmd = JsonObjectToCommand((JObject)o);
                    if (cmd != null)
                    {
                        list.Add(cmd);
                    }
                }
            }
            return list;
        }
    }
}
