using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEnvPanel
{
    class PanelMetadata
    {
        public String Description { get; set; }
        public List<StartupCommand> App { get; set; }
        public PanelMetadata() {
            /// TO DO
            /// 
            App = new List<StartupCommand>();
        }
        static public PanelMetadata Builder(String file)
        {
            PanelMetadata panelMetadata=null;
            using (StreamReader reader = File.OpenText(file))
            {
                panelMetadata = JsonConvert.DeserializeObject<PanelMetadata>(reader.ReadToEnd());
            }
            return panelMetadata;
        }
    }
}
