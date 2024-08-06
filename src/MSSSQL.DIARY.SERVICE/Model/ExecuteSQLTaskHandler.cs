namespace MSSSQL.DIARY.SERVICE.Model
{
    public class ExecuteSQLTaskHandler
    {
        public bool IsStoredProcedure { get; set; }
        public string ConnectionName { get; set; }
        public string SqlStatementSource { get; set; }
    }
}
