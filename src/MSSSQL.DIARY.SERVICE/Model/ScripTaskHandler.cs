namespace MSSSQL.DIARY.SERVICE.Model
{
    public class ScripTaskHandler
    {
        public string ReadWriteVariables { get; set; }
        public string ReadOnlyVariables { get; set; }
        public string DefaultActiveItem { get; }
        public string ProjectTemplatePath { get; }
        public string ScriptLanguage { get; set; }
        public string EntryPoint { get; set; }
        public string ScriptProjectName { get; set; }
        public bool ScriptLoaded { get; }
        public bool DebugMode { get; set; }
        public bool SuspendRequired { get; set; }
    }
}
