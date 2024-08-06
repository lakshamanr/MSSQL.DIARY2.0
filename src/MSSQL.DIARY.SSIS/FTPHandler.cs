namespace MSSQL.DIARY.SSIS
{
    public class FTPHandler
    {
        public dynamic Connection { get; set; }
        public dynamic LocalPath { get; set; }
        public dynamic Operation { get; set; }
        public dynamic RemotePath { get; set; }
    }
}