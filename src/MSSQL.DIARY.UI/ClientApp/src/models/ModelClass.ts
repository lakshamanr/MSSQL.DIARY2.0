export class ServerProprty {
  public istrName: string;
  public istrValue: string;
  public istrNevigation: string;
  public lstrCreateScript: string[];
  public lstSSISpackageReferance: string[];

}
export class Users {

  id: string;
  userName: string;
  password: string;
  serveR_NAME: string;
  IsAdmin: string;
  connectioN_STRING: string;
}
 
export class TablePropertyInfo {
  public istrName: string;
  public istrFullName: string;
  public istrValue: string;
  public istrNevigation: string;
  public lstSSISpackageReferance: string[];
  id: string;
  itemName: string;
  public tableColumns: TableColumns[];
}
export class dropdownlists {
  id: string;
  itemName: string;
  public dropdownlists(aid: string, aitemName: string) {
    this.id = aid;
    this.itemName = aitemName
  }
}

export class FileInfomration {
  public FileLocation: string;
  public FileSize: string;
  public FileType: string;
  public Name: string;
}
export class Ms_Description {
  public desciption: string;
}
export class SchemaReferanceInfo {
  public istrName: string;
  public SchemObjectType: string;
  public istrNevigation: string;
}

export class FunctionDependencies {

  public name: string;
  public type: string;
  public updated: string;
  public selected: string;
  public column_name: string;
  public NevigationLink: string;
}

export class FunctionProperties {
  public uses_ansi_nulls: string;
  public uses_quoted_identifier: string;
  public create_date: string;
  public modify_date: string;
}
export class FunctionParameters {
  public name: string;
  public type: string;
  public updated: string;
  public selected: string;
  public column_name: string;

}
export class FunctionCreateScript {
  public createFunctionscript: string;
}
export class SchemaCreateScript {
  public istrCreateScript: string;

} 
export class SP_Depencancy {
  public referencing_object_name: string;
  public referencing_object_type: string;
  public referenced_object_name: string;
}
export class SP_Parameters {
  public id: number;
  public HideEdit: boolean;
  public parameter_name: string;
  public Type: string;
  public Length: string;
  public Prec: string;
  public Scale: string;
  public Param_order: string;
  public extended_property: string;

}
 
export class SP_CreateScript {
  public desciption: string;

}
export class ExecutionPlanInfo {

  public queryPlanXML: string;
  public UseAccounts: string;
  public CacheObjectType: string;
  public Size_In_Byte: string;
  public SqlText: string;
}
 
export class TableColumns {
  public tablename: string
  public columnname: string
  public key: string
  public identity: string
  public data_type: string
  public max_length: string
  public allow_null: string
  public defaultValue: string
  public description: string
  public HideEdit: boolean;
  public id: number;


}
export class TableIndexInfo {
  public index_name: string;
  public columns: string;
  public index_type: string;
  public unique: string;
  public tableView: string;
  public object_Type: string;
}
export class TableCreateScript {
  public createscript: string;

}
 
export class Tabledependencies {
  public name: string;
  public object_type: string;
  public NevigationLink: string;


}
export class TableFKDependency {
  public values: string;
  public current_table_name: string;
  public Fk_name: string;
  public current_table_fk_columnName: string;
  public fk_refe_table_name: string;
  public fk_ref_table_column_name: string;
  NevigationLink: string;
}
export class TableKeyConstraint {
  public table_view: string;
  public object_type: string;
  public Constraint_type: string;
  public Constraint_name: string;
  public Constraint_details: string;
}
export class TableFragmentationDetails {
  public TableName: string;
  public IndexName: string;
  public PercentFragmented: string;
}
export class TriggerDetails {
  public tiggersName: string;
  public tiggersDesc: string;
  public tiggersCreateScript: string;
  public tiggersCreatedDate: string;
  public tiggersModifyDate: string;
}
export class UserDefinedDataTypeDetails {
  public name: string;
  public iblnallownull: boolean;
  public basetypename: string;
  public length: number;
  public createscript: string;
  public istrNevigation: string
}
export class FileToUpload {
  fileName: string = "";
  fileSize: number = 0;
  fileType: string = "";
  lastModifiedTime: number = 0;
  lastModifiedDate: Date = null;
  fileAsBase64: string = "";
}

export class UserDefinedDataTypeReferance {
  public objectname: string;
  public typeofobject: string;
  public istrNevigation: string;
}
export class View_columns {
  public name: string;
  public type: string;
  public updated: string;
  public selected: string;
  public column_name: string;
  public NevigationLink: string;
}
export class View_create_script {
  public createViewScript: string;
}
export class View_Dependancy {
  public name: string;
  public type: string;
  public updated: string;
  public selected: string;
  public column_name: string;
  public NevigationLink: string;
}
export class View_Properties {
  public uses_ansi_nulls: string;
  public uses_quoted_identifier: string;
  public create_date: string;
  public modify_date: string;

}
export class ServerLogin {
  public istrServerName: string;
  public istrDatabaseName: string;
  public istrUserName: string;
  public istrPassword: string;
  constructor(ServerName: string, DatabaseName: string, UserName: string, Password: string) {
    this.istrServerName = ServerName;
    this.istrDatabaseName = DatabaseName;
    this.istrUserName = UserName;
    this.istrPassword = Password;

  }
}
export class LoginModel {
  iblnIsLoginSucessFully: boolean;
  iblnIsAdminUser: boolean;
}
export class PropertyInfo {
  public istrName: string;
  public istrValue: string;
}
export class PackageJsonHandler {
  public PackageLocation: string;
  public ExecuteSQLTask: ExecuteSQLTaskHandler[];
  public Variable: VariableHandler[];
  public ftpTasks: ftpHandler[];
  public Connections: ConnectionHandler[];
  public EmailsTasks: emailHandler[];
  public FileSystemTask: FileSystemTaskHandler[];
  public ChildPackages: ChildPackageHandler[];
  public ScripTasks: ScripTaskHandler[];
}
export class ExecuteSQLTaskHandler {
  public IsStoredProcedure: boolean// { get; set; }
  public ConnectionName: string// { get; set; }
  public SqlStatementSource: string// { get; set; }
}
export class ScriptTaskHandler {
}
export class VariableHandler {
  public Name: string //{ get; set; }
  public Value: any//{ get; set; }
}
export class ConnectionHandler {
  public Name: string// { get; set; }
  public ConnectionString: string// { get; set; }
}
export class ftpHandler {
  public Connection: string//{ get; set; }
  public LocalPath: string//{ get; set; }
  public Operation: string//{ get; set; }
}
export class emailHandler {
  public CCline: string// { get; set; }
  public FormLine: string// { get; set; }
  public Subject: string// { get; set; }
  public ToLine: string// { get; set; }

}
export class ChildPackageHandler {
  public Name: string// { get; set; }
  public ChildPackageLocation: string// { get; set; }
}

export class FileSystemTaskHandler {
  public TaskOperationType: string //{ get; set; }
  public OperationName: string //{ get; set; }
  public TaskOverwriteDestFile: string //{ get; set; }
  public TaskSourcePath: string //{ get; set; }
  public TaskIsSourceVariable: string //{ get; set; }
  public TaskDestinationPath: string //{ get; set; }
  public TaskIsDestinationVariable: string // { get; set; }
  public TaskFileAttributes: string //{ get; set; }
  public TaskPreservedAttributes: string //{ get; set; }

}
export class ScripTaskHandler {
  public ReadWriteVariables: string // { get; set; }
  public ReadOnlyVariables: string // { get; set; }
  public DefaultActiveItem: string // { get; }
  public ProjectTemplatePath: string // { get; }
  public ScriptLanguage: string //{ get; set; }
  public EntryPoint: string // { get; set; }
  public ScriptProjectName: string // { get; set; }
  public ScriptLoaded: boolean// { get; }
  public DebugMode: boolean// { get; set; }
  public SuspendRequired: boolean//{ get; set; }
}
