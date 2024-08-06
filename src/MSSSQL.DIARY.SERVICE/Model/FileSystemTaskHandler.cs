namespace MSSSQL.DIARY.SERVICE.Model
{
    public class FileSystemTaskHandler
    {
        public string TaskOperationType { get; set; }
        public string OperationName { get; set; }
        public string TaskOverwriteDestFile { get; set; }
        public string TaskSourcePath { get; set; }
        public string TaskIsSourceVariable { get; set; }
        public string TaskDestinationPath { get; set; }
        public string TaskIsDestinationVariable { get; set; }
        public string TaskFileAttributes { get; set; }
        public string TaskPreservedAttributes { get; set; }

    }
}
