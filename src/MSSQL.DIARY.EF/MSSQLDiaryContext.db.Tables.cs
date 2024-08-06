﻿using Microsoft.EntityFrameworkCore;
using MoreLinq;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Helper;
using MSSQL.DIARY.COMN.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext
    {
        public List<TableIndexInfo> GetTableIndex(string istrtableName)
        {
            List<TableIndexInfo> tableIndex = new List<TableIndexInfo>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText = SqlQueryConstant.GetTableIndex.Replace("@tblName", "'" + istrtableName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableIndex.Add
                                (
                                    new TableIndexInfo
                                    {
                                        index_name = reader.SafeGetString(0),
                                        columns = reader.SafeGetString(1),
                                        index_type = reader.SafeGetString(2),
                                        unique = reader.SafeGetString(3),
                                        tableView = reader.SafeGetString(4),
                                        object_Type = reader.SafeGetString(5)
                                    }
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tableIndex;
        }

        public TableCreateScript GetTableCreateScript(string istrtableName)
        {
            string tableCreateScript = "";
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetTableCreateScript.Replace("@table_name", "'" + istrtableName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableCreateScript = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new TableCreateScript { createscript = tableCreateScript };
        }

        public List<Tabledependencies> GetAllTableDependencies(string istrtableName)
        {
            List<Tabledependencies> tabledependencies = new List<Tabledependencies>();

            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    try
                    {
                        commad.CommandText =
                            SqlQueryConstant.GetAllTabledependencies.Replace("@tblName", "'" + istrtableName + "'");

                        commad.CommandTimeout = 10 * 60;
                        Database.OpenConnection();
                        using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    tabledependencies.Add
                                    (
                                        new Tabledependencies
                                        {
                                            name = reader.SafeGetString(0),
                                            object_type = reader.SafeGetString(1)
                                        }
                                    );
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tabledependencies.DistinctBy(x => x.name).ToList();
        }

        public List<TableColumns> GetAllTablesColumn(string istrtableName)
        {
            List<TableColumns> tablecolumns = new List<TableColumns>();

            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetAllTablesColumn.Replace("@tblName", "'" + istrtableName + "'");

                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tablecolumns.Add
                                (
                                    new TableColumns
                                    {
                                        tablename = reader.SafeGetString(0),
                                        columnname = reader.SafeGetString(1),
                                        key = reader.SafeGetString(2),
                                        identity = reader.SafeGetString(3),
                                        data_type = reader.SafeGetString(4),
                                        max_length = reader.SafeGetString(5),
                                        allow_null = reader.SafeGetString(6),
                                        defaultValue = reader.SafeGetString(7),
                                        description = reader.SafeGetString(8)
                                    }
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tablecolumns;
        }

        public List<TableFKDependency> GetAllTableForeignKeys(string istrtableName)
        {
            List<TableFKDependency> tableFKcolumns = new List<TableFKDependency>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetAllTableForeignKeys.Replace("@tblName", "'" + istrtableName + "'");

                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableFKcolumns.Add
                                (
                                    new TableFKDependency
                                    {
                                        values = reader.SafeGetString(0),
                                        Fk_name = reader.SafeGetString(1),
                                        current_table_name = reader.SafeGetString(3),
                                        current_table_fk_columnName = reader.SafeGetString(4),
                                        fk_refe_table_name = reader.SafeGetString(5),
                                        fk_ref_table_column_name = reader.SafeGetString(6)
                                    }
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tableFKcolumns;
        }

        public List<TableKeyConstraint> GetAlllKeyConstraints(string istrtableName)
        {
            List<TableKeyConstraint> tableKeyConstraints = new List<TableKeyConstraint>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetAllKeyConstraints.Replace("@tblName", "'" + istrtableName + "'");

                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableKeyConstraints.Add
                                (
                                    new TableKeyConstraint
                                    {
                                        table_view = reader.SafeGetString(0),
                                        object_type = reader.SafeGetString(1),
                                        Constraint_type = reader.SafeGetString(2),
                                        Constraint_name = reader.SafeGetString(3),
                                        Constraint_details = reader.SafeGetString(4)
                                    }
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tableKeyConstraints;
        }

        public List<TablePropertyInfo> GetAllTableDescription()
        {
            List<TablePropertyInfo> getAllTbleDesc = new List<TablePropertyInfo>();
            List<string> getlstOfExtendedProps = new List<string>();

            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText = SqlQueryConstant.GetListOfExtendedPropList;
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                getlstOfExtendedProps.Add(reader.SafeGetString(0));
                            }
                        }
                    }
                }

                getlstOfExtendedProps.ForEach(x =>
                {
                    using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                    {
                        commad.CommandText =
                            SqlQueryConstant.GetAllTableDescriptionWithAll.Replace("@ExtendedProp", "'" + x + "'");
                        commad.CommandTimeout = 10 * 60;
                        Database.OpenConnection();
                        using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    getAllTbleDesc.Add(new TablePropertyInfo
                                    {
                                        istrName = reader.SafeGetString(0),
                                        istrFullName = reader.SafeGetString(1),
                                        istrValue = reader.SafeGetString(2),
                                        istrSchemaName = reader.SafeGetString(3)
                                        //tableColumns = GetAllTablesColumn(reader.SafeGetString(0))
                                    });
                                }
                            }
                        }
                    }
                });

                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText = SqlQueryConstant.GetAllTableWithOutDesc;
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                getAllTbleDesc.Add(new TablePropertyInfo
                                {
                                    istrName = reader.SafeGetString(0),
                                    istrFullName = reader.SafeGetString(1),
                                    istrValue = reader.SafeGetString(2),
                                    istrSchemaName = reader.SafeGetString(3)
                                });
                            }
                        }
                    }
                }

                getAllTbleDesc.ForEach(x =>
                {
                    x.tableColumns = GetAllTablesColumn(x.istrFullName).DistinctBy(x1 => x1.columnname).ToList();
                });
            }
            catch (Exception)
            {
                // ignored
            }

            return getAllTbleDesc;
        }

        public Ms_Description GetTableDescription(string istrtableName)
        {
            string msDesc = "";
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetTableDescription.Replace("@tblName", "'" + istrtableName + "'");

                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                msDesc = reader.SafeGetString(1);
                            }
                        }
                    }
                }

                return new Ms_Description { desciption = msDesc };
            }
            catch (Exception)
            {
                return new Ms_Description { desciption = "" };
            }
        }

        public void CreateOrUpdateTableDescription(string astrDescriptionValue, string astrSchemaName, string astrTableName)
        {
            try
            {
                UpdateTableDescription(astrDescriptionValue, astrSchemaName, astrTableName);
            }
            catch (Exception)
            {
                CreateTableDescription(astrDescriptionValue, astrSchemaName, astrTableName);
            }
        }

        public void CreateOrUpdateColumnDescription(string astrDescriptionValue, string astrSchemaName, string astrTableName, string astrColumnValue)
        {
            try
            {
                UpdateColumnDescription(astrDescriptionValue, astrSchemaName, astrTableName, astrColumnValue);
            }
            catch (Exception)
            {
                CreateColumnDescription(astrDescriptionValue, astrSchemaName, astrTableName, astrColumnValue);
            }
        }

        public List<TableFragmentationDetails> GetAllTableFragmentations()
        {
            List<TableFragmentationDetails> tableFrgamentation = new List<TableFragmentationDetails>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText = SqlQueryConstant.AllTableFragmentation;

                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableFrgamentation.Add
                                (
                                    new TableFragmentationDetails
                                    {
                                        TableName = reader.SafeGetString(0),
                                        IndexName = reader.SafeGetString(1),
                                        PercentFragmented = reader.GetInt32(2).ToString()
                                    }
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tableFrgamentation.Where(x => Convert.ToInt32(x.PercentFragmented) > 0).ToList();
        }

        private void UpdateTableDescription(string astrDescriptionValue, string astrSchemaName, string astrTableName)
        {
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                string tableName =
                    astrTableName.Replace(
                        astrTableName.Substring(0, astrTableName.IndexOf(".", StringComparison.Ordinal)) + ".", "");

                commad.CommandText = SqlQueryConstant
                    .UpdateTableExtendedProperty
                    .Replace("@Table_value", "'" + astrDescriptionValue + "'")
                    .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                    .Replace("@Table_Name", "'" + tableName + "'");

                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                commad.ExecuteNonQuery();
            }
        }

        private void CreateTableDescription(string astrDescriptionValue, string astrSchemaName, string astrTableName)
        {
            string tableName =
                astrTableName.Replace(
                    astrTableName.Substring(0, astrTableName.IndexOf(".", StringComparison.Ordinal)) + ".", "");
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                commad.CommandText = SqlQueryConstant
                    .InsertTableExtendedProperty
                    .Replace("@Table_value", "'" + astrDescriptionValue + "'")
                    .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                    .Replace("@Table_Name", "'" + tableName + "'");
                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                try
                {
                    commad.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void UpdateColumnDescription(string astrDescriptionValue, string astrSchemaName, string astrTableName, string astrColumnValue)
        {
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                string tableName =
                    astrTableName.Replace(
                        astrTableName.Substring(0, astrTableName.IndexOf(".", StringComparison.Ordinal)) + ".", "");
                commad.CommandText = SqlQueryConstant
                    .UpdateTableColumnExtendedProperty
                    .Replace("@Column_value", "'" + astrDescriptionValue + "'")
                    .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                    .Replace("@Table_Name", "'" + tableName + "'")
                    .Replace("@Column_Name", "'" + astrColumnValue + "'");

                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                commad.ExecuteNonQuery();
            }
        }

        private void CreateColumnDescription(string astrDescriptionValue, string astrSchemaName, string astrTableName, string astrColumnValue)
        {
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                string tableName =
                    astrTableName.Replace(
                        astrTableName.Substring(0, astrTableName.IndexOf(".", StringComparison.Ordinal)) + ".", "");
                commad.CommandText = SqlQueryConstant
                    .InsertTableColumnExtendedProperty
                    .Replace("@Column_value", "'" + astrDescriptionValue + "'")
                    .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                    .Replace("@Table_Name", "'" + tableName + "'")
                    .Replace("@Column_Name", "'" + astrColumnValue + "'");

                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                try
                {
                    commad.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}