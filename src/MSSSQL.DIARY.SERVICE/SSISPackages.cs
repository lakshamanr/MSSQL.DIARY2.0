using MSSSQL.DIARY.SERVICE.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;

namespace MSSSQL.DIARY.SERVICE
{
    public partial class SSISPackages : ServiceBase
    {
      //  private Timer Schedular;
        public SSISPackages()
        {
            InitializeComponent();
            fileSystemWatcher1.Changed += OnChanged;

        }

        protected override void OnStart(string[] args)
        {
            this.WriteToFile("Simple Service started {0}"); 
            fileSystemWatcher1.Path= Settings.Default.SSISPackageSourceLocation; ; 
            base.OnStart(args);

        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {

            PackageInfoRetriver.LoadPackageFiles(e.FullPath.Replace(e.Name, " ").Trim())
           .ForEach(x =>
            { 
                try
                {
                    PackageInfoRetriver packageHandler = new PackageInfoRetriver();
                    packageHandler.LoadPackage(x.FullName);
                    packageHandler.LoadPackageDetails();
                    packageHandler.GenerateJsonFile(x, packageHandler);
                    packageHandler.deleteTheFile(x);
                }
                catch (Exception ex)
                {
                    WriteToFile(ex.ToString());
                }

            });
            
        } 
        protected override void OnStop()
        {
            this.WriteToFile("Simple Service stopped {0}");
        }
        private void WriteToFile(string text)
        { 
 
            if (!Directory.Exists(Settings.Default.ServiceLog))
            {
                Directory.CreateDirectory(Settings.Default.ServiceLog);
            } 
            if (!System.IO.File.Exists(Settings.Default.ServiceLog+ "\\ServiceLog.txt"))
            {
                System.IO.File.Create(Settings.Default.ServiceLog + "\\ServiceLog.txt");
            }
            using (StreamWriter writer = new StreamWriter(Settings.Default.ServiceLog + "\\ServiceLog.txt", true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
    }
}
