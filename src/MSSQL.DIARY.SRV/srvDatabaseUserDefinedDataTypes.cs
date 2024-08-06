using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.SRV
{
    public class srvDatabaseUserDefinedDataTypes
    {
        public List<UserDefinedDataTypeDetails> GetAllUserDefinedDataTypes(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAllUserDefinedDataTypes();
            }
        }

        public UserDefinedDataTypeDetails GetUserDefinedDataTypeDetails(string istrdbName, string istrTypeName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetUserDefinedDataTypeDetails(istrTypeName);
            }
        }

        public List<UserDefinedDataTypeReferance> GetUsedDefinedDataTypeReferance(string istrdbName,
            string istrTypeName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetUsedDefinedDataTypeReferance(istrTypeName);
            }
        }

        public Ms_Description GetUsedDefinedDataTypeExtendedProperties(string istrdbName, string istrTypeName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetUsedDefinedDataTypeExtendedProperties(istrTypeName);
            }
        }

        public void CreateOrUpdateUsedDefinedDataTypeExtendedProperties(string istrdbName, string istrTypeName,
            string istrdescValue)
        {
            try
            {
                using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                {
                    dbSqldocContext.UpdateUsedDefinedDataTypeExtendedProperties(istrTypeName, istrdescValue);
                }
            }
            catch (Exception)
            {
                using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                {
                    dbSqldocContext.CreateUsedDefinedDataTypeExtendedProperties(istrTypeName, istrdescValue);
                }
            }

            //
        }
    }
}