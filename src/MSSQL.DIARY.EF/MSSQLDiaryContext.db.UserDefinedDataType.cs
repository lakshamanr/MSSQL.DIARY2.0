﻿using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Helper;
using MSSQL.DIARY.COMN.Models;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext
    {
        public List<UserDefinedDataTypeDetails> GetAllUserDefinedDataTypes()
        {
            List<UserDefinedDataTypeDetails> userdefineddatatypedetails = new List<UserDefinedDataTypeDetails>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.GetAllUserDefinedDataTypes;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userdefineddatatypedetails.Add(new UserDefinedDataTypeDetails
                                {
                                    name = reader.SafeGetString(0),
                                    iblnallownull = reader.GetBoolean(1),
                                    basetypename = reader.SafeGetString(2),
                                    length = reader.GetInt16(3),
                                    createscript = reader.SafeGetString(4)
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }

            return userdefineddatatypedetails;
        }

        public UserDefinedDataTypeDetails GetUserDefinedDataTypeDetails(string istrTypeName)
        {
            UserDefinedDataTypeDetails userdefineddatatypedetails = new UserDefinedDataTypeDetails();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText =
                        SqlQueryConstant.GetUserDefinedDataTypeDetails.Replace("@TypeName", "'" + istrTypeName + "'");
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userdefineddatatypedetails = new UserDefinedDataTypeDetails
                                {
                                    name = reader.SafeGetString(0),
                                    iblnallownull = reader.GetBoolean(1),
                                    basetypename = reader.SafeGetString(2),
                                    length = reader.GetInt16(3),
                                    createscript = reader.SafeGetString(4)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return userdefineddatatypedetails;
        }

        public List<UserDefinedDataTypeReferance> GetUsedDefinedDataTypeReferance(string istrTypeName)
        {
            List<UserDefinedDataTypeReferance> userdefineddatatypeRefe = new List<UserDefinedDataTypeReferance>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText =
                        SqlQueryConstant.GetUsedDefinedDataTypeReferance.Replace("@TypeName", "'" + istrTypeName + "'");
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userdefineddatatypeRefe.Add(new UserDefinedDataTypeReferance
                                {
                                    objectname = reader.SafeGetString(0),
                                    typeofobject = reader.SafeGetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return userdefineddatatypeRefe;
        }

        public Ms_Description GetUsedDefinedDataTypeExtendedProperties(string istrTypeName)
        {
            Ms_Description userdefineddatatypeExtendedPropertyInfo = new Ms_Description();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.GetUsedDefinedDataTypeExtendedProperties
                        .Replace("@SchemaName", "'" + istrTypeName.Split('.')[0] + "'")
                        .Replace("@TypeName", "'" + istrTypeName.Split('.')[1] + "'");
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userdefineddatatypeExtendedPropertyInfo = new Ms_Description
                                {
                                    desciption = reader.SafeGetString(0)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return userdefineddatatypeExtendedPropertyInfo;
        }

        public void CreateUsedDefinedDataTypeExtendedProperties(string istrTypeName, string istrdescValue)
        {
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.AddUserDefinedDataTypeExtendedProperty
                        .Replace("@desc", "'" + istrdescValue + "'")
                        .Replace("@SchemaName", "'" + istrTypeName.Split('.')[0] + "'")
                        .Replace("@TypeName", "'" + istrTypeName.Split('.')[1] + "'");
                    Database.OpenConnection();
                    commad.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
            }
        }

        public void UpdateUsedDefinedDataTypeExtendedProperties(string istrTypeName, string istrdescValue)
        {
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.UpdateUserDefinedDataTypeExtendedProperty
                        .Replace("@desc", "'" + istrdescValue + "'")
                        .Replace("@SchemaName", "'" + istrTypeName.Split('.')[0] + "'")
                        .Replace("@TypeName", "'" + istrTypeName.Split('.')[1] + "'");
                    Database.OpenConnection();
                    commad.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CreateUsedDefinedDataTypeExtendedProperties
    }
}