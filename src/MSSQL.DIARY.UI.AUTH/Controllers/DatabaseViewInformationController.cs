using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;

namespace MSSQL.DIARY.UI.AUTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseViewInformationController : ControllerBase
    {
        public DatabaseViewInformationController()
        {
            SrvDatabaseViews = new SrvDatabaseViews();
        }

        private SrvDatabaseViews SrvDatabaseViews { get; }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetAllViewsDetailsWithms_description(string istrdbName)
        {
            return SrvDatabaseViews.GetAllViewsDetailsWithms_description(istrdbName);
        }

        [HttpGet("[action]")]
        public List<ViewDependancy> GetView_Dependancies(string istrdbName, string astrViewName)
        {
            return SrvDatabaseViews.GetView_Dependancies(istrdbName, astrViewName);
        }

        [HttpGet("[action]")]
        public List<View_Properties> GetViewProperties(string istrdbName, string astrViewName)
        {
            return SrvDatabaseViews.GetViewProperties(istrdbName, astrViewName);
        }

        [HttpGet("[action]")]
        public List<ViewColumns> GetViewColumns(string istrdbName, string astrViewName)
        {
            return SrvDatabaseViews.GetViewColumns(istrdbName, astrViewName);
        }

        [HttpGet("[action]")]
        public ViewCreateScript GetViewCreateScript(string istrdbName, string astrViewName)
        {
            return SrvDatabaseViews.GetViewCreateScript(istrdbName, astrViewName);
        }

        [HttpGet("[action]")]
        public PropertyInfo GetViewNameWithMs_description(string istrdbName, string astrViewName)
        {
            return SrvDatabaseViews.GetViewNameWithMs_description(istrdbName, astrViewName);
        }
    }
}