using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VirtualEnvPanel
{
    public class StartupCommand
    {
        [DllImport("kernel32.dll",
                   SetLastError = true,
                   CharSet = CharSet.Auto)]
        static extern uint SearchPath(string lpPath,
                         string lpFileName,
                         string lpExtension,
                         int nBufferLength,
                         [MarshalAs ( UnmanagedType.LPTStr )]
                 StringBuilder lpBuffer,
                         out IntPtr lpFilePart);

        String SearchFile(String s)
        {
            StringBuilder sb = new StringBuilder(260);
            IntPtr ptr = new IntPtr();
            SearchPath(null,s,null,sb.Capacity,sb,out ptr);
            return sb.ToString();
        }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Executable { get; set; }
        public string InitializeScript { get; set; }
        public string Icon { get; set; }
        public bool IsAdmin { get; set; }
        public List<string> Path { get; set; }
        public List<string> Args { get; set; }
        public StartupCommand()
        {
            //// do some thing
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            if (InitializeScript==null)
            {
                return NativeExecute() == 0;
            }
            if (InitializeScript.EndsWith(".ps1"))
            {
                return PowerShellExecute() == 0;
            }else if (InitializeScript.EndsWith(".cmd") || InitializeScript.EndsWith(".bat"))
            {
                return CmdExecute() == 0;
            }
            return NativeExecute() == 0;
        }
        /// <summary>
        /// File is a executable file
        /// </summary>
        /// <returns></returns>
        private int NativeExecute()
        {
            ///
            //Process process = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            var path = System.Environment.GetEnvironmentVariable("PATH");
            foreach (var e in Path)
            {
                var p = e.Replace(@"\\", @"\");
                path += ";" + p;
            }
            pi.EnvironmentVariables["PATH"] = path;
            pi.FileName = Executable;
            if (IsAdmin)
            {
                pi.Verb = "runas";
            }
            pi.UseShellExecute = false;
            if (System.IO.File.Exists(Executable))
            {
                //// search

            }
            Process.Start(pi);
            return 0;
        }
        /// <summary>
        ///  InitializeScript is endwith .ps1
        /// </summary>
        /// <returns></returns>
        private int PowerShellExecute()
        {
            return 0;
        }
        /// <summary>
        ///  InitializeScript is cmd
        /// </summary>
        /// <returns></returns>
        private int CmdExecute()
        {
            ///
            //var command = "cmd.exe";
            string filename =System.IO.Path.GetTempFileName()+".bat";
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine("@echo off");
            string path="";
            foreach(var s in Path)
            {
                path += s+";";
            }
            sw.WriteLine("SET PATH={0};%PATH%", path);
            sw.WriteLine("call {0}", InitializeScript);
            sw.WriteLine("start /w {0}", Executable,Args);
            sw.Close();
            Process ps=null;
            if (IsAdmin)
            {
                var psi = new ProcessStartInfo(filename)
                {
                    UseShellExecute = true,
                    Verb = "runas"
                };
                ps = Process.Start(psi);
            }
            else
            {
                var psi = new ProcessStartInfo(filename);
                ps = Process.Start(psi);
            }
            File.Delete(filename);
            return 0;
        }
    }
}
