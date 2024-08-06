using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.COMN.Cache;
using MSSQL.DIARY.SRV;
using MSSQL.DIARY.UI.Models;
using Newtonsoft.Json;

namespace MSSQL.DIARY.UI.AUTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSISPackageInfoHandlerController : ControllerBase
    {
        public static NaiveCache<List<PackageJsonHandler>> SSISPkgeCache = new NaiveCache<List<PackageJsonHandler>>();
        private static IHostingEnvironment _hostingEnv;

        public SSISPackageInfoHandlerController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            srvServerInfo = new SrvDatabaseServerInfo();
            GetAllSSISPackages();
        }

        public SrvDatabaseServerInfo srvServerInfo { get; set; }

        [HttpGet("[action]")]
        public PackageJsonHandler GetPackageInfoByName(string istrPackageName)
        {
            istrPackageName = istrPackageName.Replace(".json", string.Empty);
            var result = GetAllSSISPackages().Where(x => x.PackageLocation.Contains(istrPackageName)).FirstOrDefault();
            return result;
        }

        public static List<PackageJsonHandler> GetAllSSISPackages(string istrPaths = null)
        {
            var serverName = new SrvDatabaseServerInfo().GetServerName().FirstOrDefault();
            return SSISPkgeCache.GetOrCreate(serverName, () => LoadAllSSISPackageJsons(serverName, istrPaths));
        }

        private static List<PackageJsonHandler> LoadAllSSISPackageJsons(string istrServerName, string istrPaths = null)
        {
            var lstSSISPackages = new List<PackageJsonHandler>();
            List<PackageJsonHandler> SSISPackages;
            if (SSISPkgeCache.Cache.TryGetValue(istrServerName, out SSISPackages))
            {
                if (SSISPackages.Count() <= 0)
                {
                    SSISPkgeCache.Cache.Remove(istrServerName);
                    try
                    {
                        LoadSSISPackageDetails(istrServerName, istrPaths, lstSSISPackages);
                        SSISPkgeCache.Cache.Add(istrServerName, lstSSISPackages);
                        return lstSSISPackages;
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    return SSISPackages;
                }
            }
            else
            {
                LoadSSISPackageDetails(istrServerName, istrPaths, lstSSISPackages);
                SSISPkgeCache.Cache.Add(istrServerName, lstSSISPackages);
            }

            return lstSSISPackages;
        }

        private static void LoadSSISPackageDetails(string istrServerName, string istrPaths,
            List<PackageJsonHandler> lstSSISPackages)
        {
            var Paths = "";
            Paths = istrPaths != null ? istrPaths : _hostingEnv.WebRootPath;
            var SSISPath = Path.Combine(Paths, istrServerName + "\\Json");
            if (Directory.Exists(SSISPath))
            {
                string[] lstPackgeExtension = {"*.json"};
                var PackageFileDetails = new List<FileInfo>();
                var CurrentFileDirectory = new DirectoryInfo(SSISPath);
                foreach (var ext in lstPackgeExtension)
                {
                    var folder = CurrentFileDirectory.GetFiles(ext, SearchOption.AllDirectories);
                    foreach (var file in folder)
                    {
                        if ((file.Attributes & FileAttributes.Directory) != 0) continue;

                        PackageFileDetails.Add(file);
                    }
                }

                foreach (var ssisPackageFile in PackageFileDetails)
                    try
                    {
                        lstSSISPackages.Add(
                            JsonConvert.DeserializeObject<PackageJsonHandler>(
                                System.IO.File.ReadAllText(ssisPackageFile.FullName)));
                    }
                    catch (Exception)
                    {
                    }
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] FileToUpload theFile)
        {
            var SSISPath = Path.Combine(_hostingEnv.WebRootPath,
                srvServerInfo.GetServerName().FirstOrDefault() + "\\Json");
            var filePathName = SSISPath
                               + Path.GetFileNameWithoutExtension(theFile.FileName)
                               + "-"
                               + DateTime.Now.ToString().Replace("/", "")
                                   .Replace(":", "").Replace(" ", "")
                               + Path.GetExtension(theFile.FileName);
            using (var fs = new FileStream(filePathName, FileMode.CreateNew))
            {
                fs.Write(theFile.FileAsByteArray, 0,
                    theFile.FileAsByteArray.Length);
            }

            return Ok();
        }
    }
}