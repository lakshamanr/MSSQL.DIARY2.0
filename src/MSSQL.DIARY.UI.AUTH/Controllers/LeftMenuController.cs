using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MSSQL.DIARY.COMN.Cache;
using MSSQL.DIARY.COMN.Enums;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;
using MSSQL.DIARY.UI.Models;
using Newtonsoft.Json;

namespace MSSQL.DIARY.UI.AUTH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeftMenuController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        public LeftMenuController(IHostingEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;

            SrvServerInfo = new SrvDatabaseServerInfo();
        }

        private SrvDatabaseServerInfo SrvServerInfo { get; }

        [HttpGet("[action]")]
        public string GetTreeViewJsonDetails()
        {
            return SrvTreeViewJsonGen.cacheLeftMenu.GetOrCreate("LeftMenu", GetLeftMenuJsonvalue);
        }

        [HttpGet("[action]")]
        public void RefreshCache()
        {
            SrvTreeViewJsonGen.cacheLeftMenu = new NaiveCache<string>();
            SrvDatabaseTable.CacheThatDependsOn = new NaiveCache<string>();
            SrvDatabaseTable.CacheAllTableDetails = new NaiveCache<List<TablePropertyInfo>>();
            SrvDatabaseTable.CacheAllTableFragmentationDetails = new NaiveCache<List<TableFragmentationDetails>>();
            SrvDatabaseStoreProc.StoreprocedureDescription = new NaiveCache<List<SP_PropertyInfo>>();
            SrvDatabaseStoreProc.cacheThatDependsOn = new NaiveCache<string>();
            SrvDatabaseStoreProc.CacheExecutionPlan = new NaiveCache<List<ExecutionPlanInfo>>();
            SrvDatabaseViews.cacheViewDetails = new NaiveCache<List<PropertyInfo>>();
            SSISPackageInfoHandlerController.SSISPkgeCache = new NaiveCache<List<PackageJsonHandler>>();
        }

        private string GetLeftMenuJsonvalue()
        {
            return @"{""data"":" +
                   JsonConvert.SerializeObject(new SrvTreeViewJsonGen(_env.ContentRootPath).GetLeftMenuJson()) + "}";
        }


        public List<TreeViewJson> GetLeftMenuJson()
        {
            var data = new List<TreeViewJson>
            {
                new TreeViewJson
                {
                    text = "All SSIS Packages",
                    icon = "fa fa-home fa-fw",
                    mdaIcon = "SSIS Package",
                    selected = true,
                    expand = true,
                    link = SrvServerInfo.GetServerName().FirstOrDefault(),
                    SchemaEnums = SchemaEnums.AllSSISPackages,
                    children = loadSSISPackageDetails()
                }
            };
            return data;
        }

        private IList<TreeViewJson> loadSSISPackageDetails()
        {
            var lstSSISPackageName = new List<TreeViewJson>();
            List<FileInfo> PackageFileDetails;
            try
            {
                LoadSSISPackageDetails(out lstSSISPackageName, out PackageFileDetails);
                PackageFileDetails.ForEach(x =>
                {
                    lstSSISPackageName.Add
                    (
                        new TreeViewJson
                        {
                            text = x.Name,
                            icon = "fa fa-folder",
                            mdaIcon = "SSIS Package",
                            selected = true,
                            link = SrvServerInfo.GetServerName().FirstOrDefault(),
                            expand = true,
                            badge = 12,
                            SchemaEnums = SchemaEnums.SSISPackages
                        }
                    );
                });
            }
            catch (Exception)
            {
            }

            return lstSSISPackageName;
        }

        private void LoadSSISPackageDetails(out List<TreeViewJson> lstSSISPackageName,
            out List<FileInfo> PackageFileDetails)
        {
            lstSSISPackageName = new List<TreeViewJson>();
            var SSISPath = Path.Combine(_env.WebRootPath, SrvServerInfo.GetServerName().FirstOrDefault() + "\\Json");

            string[] lstPackgeExtension = {"*.json"};
            PackageFileDetails = new List<FileInfo>();
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
        }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetSSISPackagDetails()
        {
            var propertyInfos = new List<PropertyInfo>();
            List<TreeViewJson> lstSSISPackageName;
            List<FileInfo> PackageFileDetails;
            LoadSSISPackageDetails(out lstSSISPackageName, out PackageFileDetails);
            PackageFileDetails.ForEach(x => { propertyInfos.Add(new PropertyInfo {istrName = x.Name}); });
            return propertyInfos;
        }

        private string GetSSISPackageJsonvalues()
        {
            return @"{""data"":" + JsonConvert.SerializeObject(GetLeftMenuJson()) + "}";
        }

        [HttpGet("[action]")]
        public string GetSSISPackageJsonDetails()
        {
            var result = SSISMode();
            if (result)
                return GetSSISPackageJsonvalues();
            return "";
        }

        [HttpGet("[action]")]
        public bool SSISMode()
        {
            try
            {
                return _configuration.GetSection("MySettings").GetSection("SSISMode").Value.Equals("True") ||
                       _configuration.GetSection("MySettings").GetSection("DbConnection").Value.Equals("true")
                    ? true
                    : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}