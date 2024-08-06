using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL.DIARY.SSIS
{
    public class LoadSSISFiles
    {
        public Dictionary<string, PackageHandler> SSISPackages_Details = new Dictionary<string, PackageHandler>();
        string istrSSISPath { get; set; }
        public LoadSSISFiles(string istrFolderPath)
        {
            this.istrSSISPath = istrFolderPath;
        }
        public List<FileInfo> LoadSSISPackageFiles()
        {
            string[] lstPackgeExtension = { "*.dtsx" };
            List<FileInfo> PackageFileDetails = new List<FileInfo>();
            DirectoryInfo CurrentFileDirectory = new DirectoryInfo(istrSSISPath);
            foreach (string ext in lstPackgeExtension)
            {
                FileInfo[] folder = CurrentFileDirectory.GetFiles(ext, SearchOption.AllDirectories);
                foreach (FileInfo file in folder)
                {
                    if ((file.Attributes & FileAttributes.Directory) != 0)
                        continue;
                    PackageFileDetails.Add(file);
                }
            }
            return PackageFileDetails;
        }
        public void LoadpackageDetails()
        {
            
            LoadSSISPackageFiles().Take(1).ToList().ForEach(x =>
            {
                PackageHandler packageHandler = new PackageHandler();
                packageHandler.LoadPackageDetailsByName(x.FullName);
                packageHandler.IdtListOfConnections = packageHandler.LoadConnectionString();
                packageHandler.IdtListOfVariables = packageHandler.LoadVariablesData();
                packageHandler.PackageExecutables();
                SSISPackages_Details.Add(x.Name, packageHandler);

            });
        }
    }
}
