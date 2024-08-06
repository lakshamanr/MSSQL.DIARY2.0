using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;

namespace MSSQL.DIARY.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTriggersController : ControllerBase
    {
        public DatabaseTriggersController()
        {
            srvDatabaseTrigger = new srvDatabaseTrigger();
        }

        private srvDatabaseTrigger srvDatabaseTrigger { get; }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetAllDatabaseTrigger(string istrdbName)
        {
            return srvDatabaseTrigger.GetAllDatabaseTrigger(istrdbName);
        }

        [HttpGet("[action]")]
        public TriggerInfo GetTriggerInfosByName(string istrdbName, string istrTriggerName)
        {
            return srvDatabaseTrigger.GetTriggerInfosByName(istrdbName, istrTriggerName).FirstOrDefault();
        }

        [HttpGet("[action]")]
        public void CreateOrUpdateTriggerDescription(string istrdbName, string astrDescription_Value,
            string astrTrigger_Name)
        {
            srvDatabaseTrigger.CreateOrUpdateTriggerDescription(istrdbName, astrDescription_Value, astrTrigger_Name);
        }
    }
}