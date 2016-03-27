using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VirtualEnvPanel
{
    class EnvironmentValueResolve
    {
        private string GetEnvironmentValueT(string k)
        {
            if (k.Equals("OWNER", StringComparison.InvariantCultureIgnoreCase))
            {
                return System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            }
            var v = Environment.GetEnvironmentVariable(k);
            return v == null ? "" : v;
        }
        public string ReplaceEnvironmentValue(string s)
        {
            string pattern = @"\$\{\w+\}";
            Regex regex = new Regex(pattern);
            Match m = regex.Match(s);
            if (!m.Success)
                return s;
            while (m.Success)
            {
                ///// Resolve
            }
            return s;
        }
    }
}
