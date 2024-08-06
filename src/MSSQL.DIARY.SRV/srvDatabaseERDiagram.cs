using MoreLinq;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using MSSQL.DIARY.ERDIAGRAM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSSQL.DIARY.SRV
{
    public class srvDatabaseERDiagram
    {
        public srvDatabaseERDiagram()
        {
            SrvDatabaseTable = new SrvDatabaseTable();
        }

        public SrvDatabaseTable SrvDatabaseTable { get; set; }

        public byte[] GetGraphHtmlString(string istrdbName, string FormatType, string istrSchemaName)
        {
            // adding sub graph
            GraphSVG GraphHtml = new GraphSVG();
            List<TableWithSchema> lstTablesAndColumns = new List<TableWithSchema>();
            //List<TableWithSchema> TablesAndColumns List<Dictionary<string, Dictionary<string, string>>> lstTablesAndColumns = new List<Dictionary<string, Dictionary<string, string>>>();
            if (istrSchemaName.IsNullOrEmpty())
            {
                SrvDatabaseTable.GetAllDatabaseTablesDescription(istrdbName).ForEach(x =>
                {
                    Dictionary<string, string> columnDictionary = new Dictionary<string, string>();
                    x.tableColumns.ForEach(x2 =>
                    {
                        columnDictionary.AddIfNotContainsKey(x2.columnname, x2.data_type);
                    });
                    TableWithSchema tableWithSchema = new TableWithSchema();
                    Dictionary<string, Dictionary<string, string>> TablesAndColumns = new Dictionary<string, Dictionary<string, string>>
                    {
                        { x.istrFullName, columnDictionary }
                    };

                    tableWithSchema.keyValuePairs = TablesAndColumns;
                    tableWithSchema.istrSchemaName = x.istrSchemaName;
                    lstTablesAndColumns.Add(tableWithSchema);
                });
            }
            else
            {
                SrvDatabaseTable.GetAllDatabaseTablesDescription(istrdbName)
                    .Where(x => x.istrSchemaName == istrSchemaName).ForEach(x =>
                    {
                        Dictionary<string, string> columnDictionary = new Dictionary<string, string>();
                        x.tableColumns.ForEach(x2 =>
                        {
                            columnDictionary.AddIfNotContainsKey(x2.columnname, x2.data_type);
                        });
                        TableWithSchema tableWithSchema = new TableWithSchema();
                        Dictionary<string, Dictionary<string, string>> TablesAndColumns = new Dictionary<string, Dictionary<string, string>>
                        {
                            { x.istrFullName, columnDictionary }
                        };

                        tableWithSchema.keyValuePairs = TablesAndColumns;
                        tableWithSchema.istrSchemaName = x.istrSchemaName;
                        lstTablesAndColumns.Add(tableWithSchema);
                    });
            }

            GraphHtml.SetListOfTables(lstTablesAndColumns, istrSchemaName);

            if (FormatType.Equals("pdf"))
            {
                return FileDotEngine.PDF(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName));
            }

            if (FormatType.Equals("png"))
            {
                return FileDotEngine.PNG(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName));
            }

            if (FormatType.Equals("jpg"))
            {
                return FileDotEngine.JPG(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName));
            }

            return FileDotEngine.SVG(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName));
        }

        public byte[] GetGraphHtmlString(string istrdbName, string FormatType, string istrSchemaName, List<string> alstOfSelectedTables)
        {
            GraphSVG GraphHtml = new GraphSVG();
            List<TableWithSchema> lstTablesAndColumns = new List<TableWithSchema>();
            List<TablePropertyInfo> lstTablePropertyInfo = new List<TablePropertyInfo>();

            if (istrSchemaName.IsNullOrEmpty())
            {
                SrvDatabaseTable.GetAllDatabaseTablesDescription(istrdbName).ForEach(x =>
                {
                    if (alstOfSelectedTables.Any(argtbl => argtbl.Equals(x.istrName)))
                    {
                        lstTablePropertyInfo.Add(x);
                    }
                });
                lstTablePropertyInfo.ForEach(x =>
                   {
                       SelectTableWithOutSchemaNames(x, lstTablesAndColumns);
                   });
            }
            else
            {
                SrvDatabaseTable.GetAllDatabaseTablesDescription(istrdbName).ForEach(x =>
                {
                    if (alstOfSelectedTables.Any(argtbl => argtbl.Equals(x.istrName)))
                    {
                        lstTablePropertyInfo.Add(x);
                    }
                });

                lstTablePropertyInfo
                     .Where(x => x.istrSchemaName == istrSchemaName)
                     .ForEach(x =>
                     {
                         SelecctTableWithSchemaNames(x, lstTablesAndColumns);
                     });
            }

            GraphHtml.SetListOfTables(lstTablesAndColumns, istrSchemaName);

            if (FormatType.Equals("pdf"))
            {
                return FileDotEngine.PDF(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName, alstOfSelectedTables));
            }

            if (FormatType.Equals("png"))
            {
                return FileDotEngine.PNG(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName, alstOfSelectedTables));
            }

            if (FormatType.Equals("jpg"))
            {
                return FileDotEngine.JPG(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName, alstOfSelectedTables));
            }

            return FileDotEngine.SVG(GraphHtml.GraphSVGHTMLString(istrdbName, istrSchemaName, alstOfSelectedTables));
        }

        private static void SelecctTableWithSchemaNames(TablePropertyInfo x, List<TableWithSchema> lstTablesAndColumns)
        {
            Dictionary<string, string> columnDictionary = new Dictionary<string, string>();
            x.tableColumns.ForEach(x2 =>
            {
                columnDictionary.AddIfNotContainsKey(x2.columnname, x2.data_type);
            });
            TableWithSchema tableWithSchema = new TableWithSchema();
            Dictionary<string, Dictionary<string, string>> TablesAndColumns = new Dictionary<string, Dictionary<string, string>>
            {
                { x.istrFullName, columnDictionary }
            };

            tableWithSchema.keyValuePairs = TablesAndColumns;
            tableWithSchema.istrSchemaName = x.istrSchemaName;
            lstTablesAndColumns.Add(tableWithSchema);
        }

        private static void SelectTableWithOutSchemaNames(TablePropertyInfo x, List<TableWithSchema> lstTablesAndColumns)
        {
            Dictionary<string, string> columnDictionary = new Dictionary<string, string>();
            x.tableColumns.ForEach(x2 =>
            {
                columnDictionary.AddIfNotContainsKey(x2.columnname, x2.data_type);
            });
            TableWithSchema tableWithSchema = new TableWithSchema();
            Dictionary<string, Dictionary<string, string>> TablesAndColumns = new Dictionary<string, Dictionary<string, string>>
            {
                { x.istrFullName, columnDictionary }
            };

            tableWithSchema.keyValuePairs = TablesAndColumns;
            tableWithSchema.istrSchemaName = x.istrSchemaName;
            lstTablesAndColumns.Add(tableWithSchema);
        }

        public class GraphSVG
        {
            private List<TableWithSchema> TablesAndColumns { get; set; }
            public string istrSchemaName { get; set; }

            public string GraphStart => "digraph ERDiagram {  splines=ortho   nodesep=0.8; size=50 ;";

            //  "digraph ERDiagram {  splines=ortho rankdir=LR;  size=50 ";
            public string GraphEnd => "}";

            public GraphNode graphNode { get; set; }
            public GraphEdge graphEdge { get; set; }
            public List<TableSVG> tableSVGs { get; set; }

            public void SetListOfTables(List<TableWithSchema> TablesAndColumn, string istrSchemaName = null)
            {
                TablesAndColumns = TablesAndColumn;
                this.istrSchemaName = istrSchemaName;
            }

            public string GraphSVGHTMLString(string istrdbName, string istrSchemaName)
            {
                string ReturnGraphDot = GenHtmlSting(istrSchemaName);

                ReturnGraphDot += GetAllTableRefernce(istrdbName, istrSchemaName);
                ReturnGraphDot += GraphEnd;

                return ReturnGraphDot;
            }

            public string GraphSVGHTMLString(string istrdbName, string istrSchemaName, List<string> alstOfSelectedTables)
            {
                string ReturnGraphDot = GenHtmlSting(istrSchemaName);

                ReturnGraphDot += GetAllTableRefernce(istrdbName, istrSchemaName, alstOfSelectedTables);
                ReturnGraphDot += GraphEnd;

                return ReturnGraphDot;
            }

            private string GenHtmlSting(string istrSchemaName)
            {
                GraphNode node = new GraphNode();
                GraphEdge edge = new GraphEdge();
                string ReturnGraphDot = "";
                ReturnGraphDot += GraphStart;
                ReturnGraphDot += node.istrGraphNode;
                ReturnGraphDot += edge.istrGraphEdge;
                int clusterIncrement = 0;
                if (istrSchemaName.IsNullOrEmpty())
                {
                    TablesAndColumns.Select(x => x.istrSchemaName).DistinctBy(x => x).ToList().ForEach(x1 =>
                    {
                        TablesAndColumns.Select(x => x.istrSchemaName).DistinctBy(x => x).ToList().ForEach(SchemaName =>
                        {
                            ReturnGraphDot += "subgraph cluster_" + clusterIncrement + " {\t label=" + SchemaName +
                                              ";\t";
                            ReturnGraphDot += "bgcolor=" + "\"" + RandomColor() + "\";";
                            TablesAndColumns.Where(x => x.istrSchemaName.Equals(SchemaName)).ForEach(x =>
                            {
                                x.keyValuePairs.ForEach(x2 =>
                                {
                                    ReturnGraphDot += new TableSVG(x2.Key, x2.Value).GetTableHtml();
                                });
                            });
                            ReturnGraphDot += "\t}\t";
                            clusterIncrement++;
                        });
                    });
                }
                else
                {
                    TablesAndColumns.Select(x => x.istrSchemaName.Equals(istrSchemaName)).DistinctBy(x => x).ToList()
                        .ForEach(x1 =>
                        {
                            TablesAndColumns.Select(x => x.istrSchemaName).DistinctBy(x => x).ToList().ForEach(
                                SchemaName =>
                                {
                                    ReturnGraphDot += "subgraph cluster_" + clusterIncrement + " {\t label=" +
                                                      SchemaName + ";\t";
                                    ReturnGraphDot += "bgcolor=" + "\"" + RandomColor() + "\";";
                                    TablesAndColumns.Where(x => x.istrSchemaName.Equals(SchemaName)).ForEach(x =>
                                    {
                                        x.keyValuePairs.ForEach(x2 =>
                                        {
                                            ReturnGraphDot += new TableSVG(x2.Key, x2.Value).GetTableHtml();
                                        });
                                    });
                                    ReturnGraphDot += "\t}\t";
                                    clusterIncrement++;
                                });
                        });
                }

                return ReturnGraphDot;
            }

            public string RandomColor()
            {
                string[] colorName =
                {
                    //"Azure" ,
                    "white"
                };
                Random random = new Random();
                int randomNumber = random.Next(0, colorName.Length - 1);
                return colorName[randomNumber];
            }

            private string GetAllTableRefernce(string istrdbName, string istrSchemaName)
            {
                string RefernceHTML = "";
                List<TableFKDependency> tableReference = new List<TableFKDependency>();
                if (istrSchemaName.IsNullOrEmpty())
                {
                    using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                    {
                        tableReference = dbSqldocContext.GetAllTableRefernce();
                    }
                }
                else
                {
                    using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                    {
                        tableReference = dbSqldocContext.GetAllTableRefernce(istrSchemaName);
                    }
                }

                tableReference.ForEach(x =>
                {
                    RefernceHTML += x.fk_refe_table_name + "\t" + "[fontcolor=block, label=<" + "" + ">, color =block]";
                });
                return RefernceHTML;
            }

            private string GetAllTableRefernce(string istrdbName, string istrSchemaName, List<string> alstOfSelectedTables)
            {
                string RefernceHTML = "";
                List<TableFKDependency> tableReference = new List<TableFKDependency>();
                if (istrSchemaName.IsNullOrEmpty())
                {
                    using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                    {
                        dbSqldocContext.GetAllTableRefernce()
                            .ForEach(x =>
                            {
                                string result = x.fk_refe_table_name.IsNull() ? x.current_table_name : x.fk_refe_table_name;
                                if (alstOfSelectedTables.Any(argtable => result.Contains(argtable)))
                                {
                                    //x.fk_refe_table_name=x.fk_refe_table_name ??"";
                                    //x.fk_refe_table_name = x.fk_refe_table_name.Replace(".", "_");
                                    //x.current_table_name = x.current_table_name ?? "";
                                    //x.current_table_name = x.current_table_name.Replace(".", "_");
                                    tableReference.Add(x);
                                }
                            }
                            );
                    }
                }
                else
                {
                    using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
                    {
                        dbSqldocContext.GetAllTableRefernce(istrSchemaName).Where(x => x.fk_refe_table_name.IsNotNull())
                            .ForEach(x =>
                            {
                                string result = x.fk_refe_table_name.IsNull() ? x.current_table_name : x.fk_refe_table_name;
                                if (alstOfSelectedTables.Any(argtable => result.Contains(argtable)))
                                {
                                    //x.fk_refe_table_name = x.fk_refe_table_name ?? "";
                                    //x.fk_refe_table_name = x.fk_refe_table_name.Replace(".", "_");
                                    //x.current_table_name = x.current_table_name ?? "";
                                    //x.current_table_name = x.current_table_name.Replace(".", "_");
                                    tableReference.Add(x);
                                }
                            }
                            );
                    }
                }

                tableReference.ForEach(x =>
                {
                    RefernceHTML += x.fk_refe_table_name + "\t" + "[fontcolor=block, label=<" + "" +
                                    ">, color =block]";
                });
                return RefernceHTML;
            }
        }

        public class GraphNode
        {
            public string istrGraphNode =>
                " node [shape=box, style=filled, color=dodgerblue2, fillcolor=aliceblue];"; //"node [shape=none, margin=0]";
        }

        public class GraphEdge
        {
            public string istrGraphEdge => " edge [color=blue4, arrowhead=normal];";
            // " edge [color=blue4, arrowhead=crow];";
            //" edge [arrowhead=normal, arrowtail=none, dir=both]";
        }

        public class TableSVG
        {
            public TableSVG(string istrTableName, Dictionary<string, string> keyValuePairs)
            {
                TableName = istrTableName;
                ColumnDescription = keyValuePairs;
            }

            private Dictionary<string, string> ColumnDescription { get; }
            public string TableName { get; set; }

            public string istrTablelLabelStartHtml => "[ shape =none ;label=<<table \tborder=" + "'0'" +
                                                      "\tcellborder=" + "'1'" + "\tcellspacing=" + "'0'" +
                                                      "\tcellpadding=" + "'4'" + ">";

            public string TableHTML =>
                " <tr><td bgcolor=" + "'pink'" + ">" + TableName.Split('.')[1] + "</td></tr>";

            // " <tr><td bgcolor=" + "'lightblue'" + ">" + TableName.Split('.')[1] + "</td></tr>";
            public string istrTablelLabelEndHTML => "</table> >]";

            public string GetTableHtml()
            {
                string TableHtml = "";
                TableHtml += TableName.Split('.')[1];
                TableHtml += istrTablelLabelStartHtml;
                TableHtml += TableHTML;
                ColumnDescription.ForEach(x =>
                {
                    TableHtml += " <tr><td align='left'>" + x.Key + ":" + x.Value + "</td></tr>";
                });
                TableHtml += istrTablelLabelEndHTML;
                return TableHtml;
            }
        }

        public class TableWithSchema
        {
            public Dictionary<string, Dictionary<string, string>> keyValuePairs { get; set; }
            public string istrSchemaName { get; set; }
        }
    }
}