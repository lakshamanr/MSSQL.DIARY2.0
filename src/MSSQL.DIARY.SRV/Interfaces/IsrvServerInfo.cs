using MSSQL.DIARY.COMN.Models;
using System.Collections.Generic;

namespace MSSQL.DIARY.SRV.Interfaces
{
    public interface ISrvServerInfo
    {
        List<PropertyInfo> GetAdvancedServerSettingsInfo();

        List<string> GetDatabaseNames();

        List<string> GetServerName();

        List<PropertyInfo> GetServerProperties();
    }
}