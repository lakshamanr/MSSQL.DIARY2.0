using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;

namespace MSSQL.DIARY.UI.Controllers
{
    [Route("api/[controller]")]
    public class ServerInformationController : ControllerBase
    {
        public ServerInformationController()
        {
            SrvServerInfo = new SrvDatabaseServerInfo();
        }

        private SrvDatabaseServerInfo SrvServerInfo { get; }

        [HttpGet("[action]")]
        public List<string> GetServerInformation()
        {
            return SrvServerInfo.GetServerName();
        }

        [HttpGet("[action]")]
        public List<string> GetDatabaseNames()
        {
            return SrvServerInfo.GetDatabaseNames();
        }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetServerProperties()
        {
            return SrvServerInfo.GetServerProperties();
        }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetAdvancedServerSettingsInfo()
        {
            return SrvServerInfo.GetAdvancedServerSettingsInfo();
        }
    }
}