"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ServerProprty = /** @class */ (function () {
    function ServerProprty() {
    }
    return ServerProprty;
}());
exports.ServerProprty = ServerProprty;
var Users = /** @class */ (function () {
    function Users() {
    }
    return Users;
}());
exports.Users = Users;
var TablePropertyInfo = /** @class */ (function () {
    function TablePropertyInfo() {
    }
    return TablePropertyInfo;
}());
exports.TablePropertyInfo = TablePropertyInfo;
var dropdownlists = /** @class */ (function () {
    function dropdownlists() {
    }
    dropdownlists.prototype.dropdownlists = function (aid, aitemName) {
        this.id = aid;
        this.itemName = aitemName;
    };
    return dropdownlists;
}());
exports.dropdownlists = dropdownlists;
var FileInfomration = /** @class */ (function () {
    function FileInfomration() {
    }
    return FileInfomration;
}());
exports.FileInfomration = FileInfomration;
var Ms_Description = /** @class */ (function () {
    function Ms_Description() {
    }
    return Ms_Description;
}());
exports.Ms_Description = Ms_Description;
var SchemaReferanceInfo = /** @class */ (function () {
    function SchemaReferanceInfo() {
    }
    return SchemaReferanceInfo;
}());
exports.SchemaReferanceInfo = SchemaReferanceInfo;
var FunctionDependencies = /** @class */ (function () {
    function FunctionDependencies() {
    }
    return FunctionDependencies;
}());
exports.FunctionDependencies = FunctionDependencies;
var FunctionProperties = /** @class */ (function () {
    function FunctionProperties() {
    }
    return FunctionProperties;
}());
exports.FunctionProperties = FunctionProperties;
var FunctionParameters = /** @class */ (function () {
    function FunctionParameters() {
    }
    return FunctionParameters;
}());
exports.FunctionParameters = FunctionParameters;
var FunctionCreateScript = /** @class */ (function () {
    function FunctionCreateScript() {
    }
    return FunctionCreateScript;
}());
exports.FunctionCreateScript = FunctionCreateScript;
var SchemaCreateScript = /** @class */ (function () {
    function SchemaCreateScript() {
    }
    return SchemaCreateScript;
}());
exports.SchemaCreateScript = SchemaCreateScript;
var SP_Depencancy = /** @class */ (function () {
    function SP_Depencancy() {
    }
    return SP_Depencancy;
}());
exports.SP_Depencancy = SP_Depencancy;
var SP_Parameters = /** @class */ (function () {
    function SP_Parameters() {
    }
    return SP_Parameters;
}());
exports.SP_Parameters = SP_Parameters;
var SP_CreateScript = /** @class */ (function () {
    function SP_CreateScript() {
    }
    return SP_CreateScript;
}());
exports.SP_CreateScript = SP_CreateScript;
var ExecutionPlanInfo = /** @class */ (function () {
    function ExecutionPlanInfo() {
    }
    return ExecutionPlanInfo;
}());
exports.ExecutionPlanInfo = ExecutionPlanInfo;
var TableColumns = /** @class */ (function () {
    function TableColumns() {
    }
    return TableColumns;
}());
exports.TableColumns = TableColumns;
var TableIndexInfo = /** @class */ (function () {
    function TableIndexInfo() {
    }
    return TableIndexInfo;
}());
exports.TableIndexInfo = TableIndexInfo;
var TableCreateScript = /** @class */ (function () {
    function TableCreateScript() {
    }
    return TableCreateScript;
}());
exports.TableCreateScript = TableCreateScript;
var Tabledependencies = /** @class */ (function () {
    function Tabledependencies() {
    }
    return Tabledependencies;
}());
exports.Tabledependencies = Tabledependencies;
var TableFKDependency = /** @class */ (function () {
    function TableFKDependency() {
    }
    return TableFKDependency;
}());
exports.TableFKDependency = TableFKDependency;
var TableKeyConstraint = /** @class */ (function () {
    function TableKeyConstraint() {
    }
    return TableKeyConstraint;
}());
exports.TableKeyConstraint = TableKeyConstraint;
var TableFragmentationDetails = /** @class */ (function () {
    function TableFragmentationDetails() {
    }
    return TableFragmentationDetails;
}());
exports.TableFragmentationDetails = TableFragmentationDetails;
var TriggerDetails = /** @class */ (function () {
    function TriggerDetails() {
    }
    return TriggerDetails;
}());
exports.TriggerDetails = TriggerDetails;
var UserDefinedDataTypeDetails = /** @class */ (function () {
    function UserDefinedDataTypeDetails() {
    }
    return UserDefinedDataTypeDetails;
}());
exports.UserDefinedDataTypeDetails = UserDefinedDataTypeDetails;
var FileToUpload = /** @class */ (function () {
    function FileToUpload() {
        this.fileName = "";
        this.fileSize = 0;
        this.fileType = "";
        this.lastModifiedTime = 0;
        this.lastModifiedDate = null;
        this.fileAsBase64 = "";
    }
    return FileToUpload;
}());
exports.FileToUpload = FileToUpload;
var UserDefinedDataTypeReferance = /** @class */ (function () {
    function UserDefinedDataTypeReferance() {
    }
    return UserDefinedDataTypeReferance;
}());
exports.UserDefinedDataTypeReferance = UserDefinedDataTypeReferance;
var View_columns = /** @class */ (function () {
    function View_columns() {
    }
    return View_columns;
}());
exports.View_columns = View_columns;
var View_create_script = /** @class */ (function () {
    function View_create_script() {
    }
    return View_create_script;
}());
exports.View_create_script = View_create_script;
var View_Dependancy = /** @class */ (function () {
    function View_Dependancy() {
    }
    return View_Dependancy;
}());
exports.View_Dependancy = View_Dependancy;
var View_Properties = /** @class */ (function () {
    function View_Properties() {
    }
    return View_Properties;
}());
exports.View_Properties = View_Properties;
var ServerLogin = /** @class */ (function () {
    function ServerLogin(ServerName, DatabaseName, UserName, Password) {
        this.istrServerName = ServerName;
        this.istrDatabaseName = DatabaseName;
        this.istrUserName = UserName;
        this.istrPassword = Password;
    }
    return ServerLogin;
}());
exports.ServerLogin = ServerLogin;
var LoginModel = /** @class */ (function () {
    function LoginModel() {
    }
    return LoginModel;
}());
exports.LoginModel = LoginModel;
var PropertyInfo = /** @class */ (function () {
    function PropertyInfo() {
    }
    return PropertyInfo;
}());
exports.PropertyInfo = PropertyInfo;
var PackageJsonHandler = /** @class */ (function () {
    function PackageJsonHandler() {
    }
    return PackageJsonHandler;
}());
exports.PackageJsonHandler = PackageJsonHandler;
var ExecuteSQLTaskHandler = /** @class */ (function () {
    function ExecuteSQLTaskHandler() {
    }
    return ExecuteSQLTaskHandler;
}());
exports.ExecuteSQLTaskHandler = ExecuteSQLTaskHandler;
var ScriptTaskHandler = /** @class */ (function () {
    function ScriptTaskHandler() {
    }
    return ScriptTaskHandler;
}());
exports.ScriptTaskHandler = ScriptTaskHandler;
var VariableHandler = /** @class */ (function () {
    function VariableHandler() {
    }
    return VariableHandler;
}());
exports.VariableHandler = VariableHandler;
var ConnectionHandler = /** @class */ (function () {
    function ConnectionHandler() {
    }
    return ConnectionHandler;
}());
exports.ConnectionHandler = ConnectionHandler;
var ftpHandler = /** @class */ (function () {
    function ftpHandler() {
    }
    return ftpHandler;
}());
exports.ftpHandler = ftpHandler;
var emailHandler = /** @class */ (function () {
    function emailHandler() {
    }
    return emailHandler;
}());
exports.emailHandler = emailHandler;
var ChildPackageHandler = /** @class */ (function () {
    function ChildPackageHandler() {
    }
    return ChildPackageHandler;
}());
exports.ChildPackageHandler = ChildPackageHandler;
var FileSystemTaskHandler = /** @class */ (function () {
    function FileSystemTaskHandler() {
    }
    return FileSystemTaskHandler;
}());
exports.FileSystemTaskHandler = FileSystemTaskHandler;
var ScripTaskHandler = /** @class */ (function () {
    function ScripTaskHandler() {
    }
    return ScripTaskHandler;
}());
exports.ScripTaskHandler = ScripTaskHandler;
//# sourceMappingURL=ModelClass.js.map