﻿namespace MSSQL.DIARY.COMN.Models
{
    public class TableColumns
    {
        public string tablename { get; set; }
        public string columnname { get; set; }
        public string key { get; set; }
        public string identity { get; set; }
        public string data_type { get; set; }
        public string max_length { get; set; }
        public string allow_null { get; set; }
        public string defaultValue { get; set; }
        public string description { get; set; }
    }
}