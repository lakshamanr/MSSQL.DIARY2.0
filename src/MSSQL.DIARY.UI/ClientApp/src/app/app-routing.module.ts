//import { NgModule } from '@angular/core';
//import { RouterModule, Routes }  from '@angular/router';
//import { ServerInformationComponent } from './server-information/server-information.component';
//import { DatabaseInfoComponent } from './database-info/database-info.component';
//import { DatabaseTableInfoComponent } from './database-table-info/database-table-info.component';
//import { DatabaseAllTableInfoComponent } from './database-all-table-info/database-all-table-info.component' 
//import { DatabaseAllViewsComponent} from './database-all-views/database-all-views.component'
//import { DatabaseViewsDetailsComponent} from './database-views-details/database-views-details.component'
//import { DatabaseAllStoreProcedureComponent} from './database-all-store-procedure/database-all-store-procedure.component'
//import { DatabaseStoreProcedureDetailsComponent } from './database-store-procedure-details/database-store-procedure-details.component'
//import { DatabaseTableValueFunctionsComponent} from './database-table-value-functions/database-table-value-functions.component'
//import { DatabaseTableValueFunctionDetailsComponent} from './database-table-value-function-details/database-table-value-function-details.component'
//import { DatabaseScalarFunctionsComponent} from './database-scalar-functions/database-scalar-functions.component'
//import { DatabaseScalarFunctionDetailsComponent} from './database-scalar-function-details/database-scalar-function-details.component'
//import { DatabaseTriggersComponent } from './database-triggers/database-triggers.component'
//import { DatabaseTriggerDetailsComponent } from './database-trigger-details/database-trigger-details.component'
//import { DatabaseUsedDefinedDataTypesComponent } from './database-used-defined-data-types/database-used-defined-data-types.component'
//import { DatabaseUsedDefinedDataTypeDetailsComponent} from './database-used-defined-data-type-details/database-used-defined-data-type-details.component'
//import { DatabasexmlSchemaCollectionsComponent } from './databasexml-schema-collections/databasexml-schema-collections.component'
//import { DatabasexmlSchemaCollectionDetailsComponent } from './databasexml-schema-collection-details/databasexml-schema-collection-details.component'
//import { BusinessworkFlowComponent } from './businesswork-flow/businesswork-flow.component'
//import { LoadSSISPackagesComponent } from './load-ssispackages/load-ssispackages.component'
//import { SSISPackageComponent } from './ssispackage/ssispackage.component';
//import { SSISPackageDetailsComponent } from './ssispackage-details/ssispackage-details.component'
//import { DatabaseSchemasComponent } from './database-schemas/database-schemas.component'
//import { DatabaseSchemaDetailsComponent } from './database-schema-details/database-schema-details.component'
//import { AdminloginComponent } from './adminlogin/adminlogin.component'
//import { from } from 'rxjs';
//const appRoutes: Routes = [ 
  
//  {
//      path: 'serverinformation',
//      component: ServerInformationComponent,
//  },           
//  {
//      path: 'Tables/:dbName',
//      component: DatabaseAllTableInfoComponent,
//      pathMatch: 'full'
//  },
//  {
//    path: 'TableDetails/:dbName',
//      component: DatabaseTableInfoComponent,
//      pathMatch: 'full'
//  },
//  {
//    path: 'AllViews/:dbName',
//      component: DatabaseAllViewsComponent,
//      pathMatch: 'full'
//  } ,
//  {
//    path: 'ViewsDetails/:dbName',
//      component: DatabaseViewsDetailsComponent,
//      pathMatch: 'full'
//  } ,
//  {
//    path: 'StoreProcedures/:dbName',
//      component: DatabaseAllStoreProcedureComponent,
//      pathMatch: 'full'
//  }  
//  ,
//  {
//    path: 'StoreProcedureDetails/:dbName',
//      component: DatabaseStoreProcedureDetailsComponent,
//      pathMatch: 'full'
//  } ,

//  {
//    path: 'DatabaseInfo/:dbName',
//      component: DatabaseInfoComponent,
//      pathMatch: 'full'
//  },
//    {
//        path: 'TableValueFunctions/:dbName',
//        component: DatabaseTableValueFunctionsComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'TableValueFunctionDetails/:dbName',
//        component: DatabaseTableValueFunctionDetailsComponent,
//        pathMatch: 'full'
//    }
//    ,
//    {
//        path: 'ScalarFunctions/:dbName',
//        component: DatabaseScalarFunctionsComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'ScalarFunctionDetails/:dbName',
//        component: DatabaseScalarFunctionDetailsComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'DatabaseTriggers/:dbName',
//        component: DatabaseTriggersComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'DatabaseTriggerDetails/:dbName',
//        component: DatabaseTriggerDetailsComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'UserDefinedDataTypes/:dbName',
//        component: DatabaseUsedDefinedDataTypesComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'UserDefinedDataTypeDetails/:dbName',
//        component: DatabaseUsedDefinedDataTypeDetailsComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'XmlSchemaCollections/:dbName',
//        component: DatabasexmlSchemaCollectionsComponent,
//        pathMatch: 'full'
//    },
//    {
//        path: 'XmlSchemaCollectionDetails/:dbName',
//        component: DatabasexmlSchemaCollectionDetailsComponent,
//        pathMatch: 'full'
//  },
//  {
//    path: 'WorkFlow/:dbName',
//    component: BusinessworkFlowComponent,
//    pathMatch: 'full'
//  },
//  {
//    path: 'LoadSSISPackage',
//    component: LoadSSISPackagesComponent,
//    pathMatch: 'full'
//  },
//  {
//    path: 'SSISPackage',
//    component: SSISPackageComponent,
//    pathMatch: 'full'
//  },
 
//  {
//    path: 'SSISPackagesDetails/:PkgName',
//    component: SSISPackageDetailsComponent,
//    pathMatch: 'full'
//  },

//  {
//    path: 'AllSchemas/:dbName',
//    component: DatabaseSchemasComponent,
//    pathMatch: 'full'
//  },
//  {
//    path: 'SchemasDetails/:dbName',
//    component: DatabaseSchemaDetailsComponent,
//    pathMatch: 'full'
//  }, 
//  {
//    path: 'AdminLogin',
//    component: AdminloginComponent,
//    pathMatch: 'full'
//  },
//  {
//        path: '',
//        component: ServerInformationComponent,
//        pathMatch: 'full'
//  } 
//];

//@NgModule({
//  imports: [
//    RouterModule.forRoot(
//      appRoutes,
//      { useHash: true}  
//    )
//  ],
//  exports: [
//    RouterModule
//  ]
//})
//export class AppRoutingModule { }
