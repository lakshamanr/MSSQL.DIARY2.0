using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseSchema
    {
        public List<PropertyInfo> GetListOfAllSchemaAndMsDescription(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetListOfAllSchemaAndMsDescription();
            }
        }

        public void CreateOrUpdateSchemaMsDescription(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.GetListOfAllSchemaAndMsDescription();
            }
        }

        public void CreateOrUpdateSchemaMsDescription(string istrdbName, string astrDescription_Value,
            string astrSchema_Name)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.CreateOrUpdateSchemaMsDescription(astrDescription_Value, astrSchema_Name);
            }
        }

        public List<SchemaReferanceInfo> GetSchemaReferanceInfo(string istrdbName, string astrSchema_Name)
        {
            List<SchemaReferanceInfo> result = new List<SchemaReferanceInfo>();
            try
            {
                using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                {
                    result = dbSqldocContext.GetSchemaReferanceInfo(astrSchema_Name);
                }
            }
            catch (Exception)
            {
            }

            return result;
        }

        public Ms_Description GetSchemaMsDescription(string istrdbName, string astrSchema_Name)
        {
            Ms_Description result = new Ms_Description();
            try
            {
                using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                {
                    result = dbSqldocContext.GetSchemaMsDescription(astrSchema_Name);
                }
            }
            catch (Exception)
            {
            }

            return result;
        }
    }
}