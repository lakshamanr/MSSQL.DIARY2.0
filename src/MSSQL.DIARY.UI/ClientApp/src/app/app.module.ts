import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PrismModule } from '@ngx-prism/core'; // <----- Here
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AmexioDataModule } from 'amexio-ng-extensions';
import { AmexioWidgetModule } from 'amexio-ng-extensions';
import { NgxUiLoaderModule, NgxUiLoaderHttpModule, NgxUiLoaderConfig, POSITION, SPINNER, PB_DIRECTION } from 'ngx-ui-loader';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalModule, BsModalRef } from 'ngx-bootstrap/modal';
import { AmexioChartsModule } from 'amexio-ng-extensions';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AmexioEnterpriseModule } from 'amexio-ng-extensions';
import { AngularSplitModule } from 'angular-split';
import { TreeDragDropService } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { AccordionModule } from 'primeng/accordion';
import { TableModule } from 'primeng/table';
import { ProgressBarModule } from "angular-progress-bar"
import { CommonModule } from '@angular/common';
import { TreeModule } from 'primeng/tree';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { ContextMenuModule } from 'primeng/contextmenu';
import { TabViewModule } from 'primeng/tabview';
import { CodeHighlighterModule } from 'primeng/codehighlighter';
import { LeftNavMenuComponent } from './left-nav-menu/left-nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ServerInformationComponent } from './server-information/server-information.component';
import { DatabaseInfoComponent, ModalContentComponent } from './database-info/database-info.component';
import { Configuration } from 'src/app/app.constants';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';

//import { AppRoutingModule } from './app-routing.module';
import { DatabaseTableInfoComponent } from './database-table-info/database-table-info.component';
import { DatabaseAllTableInfoComponent } from './database-all-table-info/database-all-table-info.component';
import { DatabaseAllViewsComponent } from './database-all-views/database-all-views.component';
import { DatabaseViewsDetailsComponent } from './database-views-details/database-views-details.component';
import { DatabaseAllStoreProcedureComponent } from './database-all-store-procedure/database-all-store-procedure.component';
import { DatabaseStoreProcedureDetailsComponent } from './database-store-procedure-details/database-store-procedure-details.component';
import { DatabaseTableValueFunctionsComponent } from './database-table-value-functions/database-table-value-functions.component';
import { DatabaseTableValueFunctionDetailsComponent } from './database-table-value-function-details/database-table-value-function-details.component';
import { DatabaseScalarFunctionsComponent } from './database-scalar-functions/database-scalar-functions.component';
import { DatabaseScalarFunctionDetailsComponent } from './database-scalar-function-details/database-scalar-function-details.component';
import { DatabaseTriggersComponent } from './database-triggers/database-triggers.component';
import { DatabaseTriggerDetailsComponent } from './database-trigger-details/database-trigger-details.component';
import { DatabaseUsersComponent } from './database-users/database-users.component';
import { DatabaseUserDetailsComponent } from './database-user-details/database-user-details.component';
import { DatabaseRolesComponent } from './database-roles/database-roles.component';
import { DatabaseRoleDetailsComponent } from './database-role-details/database-role-details.component';
import { DatabaseSchemasComponent } from './database-schemas/database-schemas.component';
import { DatabaseSchemaDetailsComponent } from './database-schema-details/database-schema-details.component';
import { DatabaseUsedDefinedDataTypesComponent } from './database-used-defined-data-types/database-used-defined-data-types.component';
import { DatabaseUsedDefinedDataTypeDetailsComponent } from './database-used-defined-data-type-details/database-used-defined-data-type-details.component';
import { DatabasexmlSchemaCollectionsComponent } from './databasexml-schema-collections/databasexml-schema-collections.component';
import { DatabasexmlSchemaCollectionDetailsComponent } from './databasexml-schema-collection-details/databasexml-schema-collection-details.component';
import { SearchPipe } from './search.pipe';
import { HighlightPipe } from './highlight.pipe';
import { LoginPageComponent } from './login-page/login-page.component';
import { BusinessworkFlowComponent } from './businesswork-flow/businesswork-flow.component';
import { SSISPackageComponent } from './ssispackage/ssispackage.component';
import { SSISPackageDetailsComponent } from './ssispackage-details/ssispackage-details.component';
import { LoadSSISPackagesComponent } from './load-ssispackages/load-ssispackages.component';
import { AdminloginComponent } from './adminlogin/adminlogin.component'
import { Routes, RouterModule } from '@angular/router';
import { DatabaseBusinessModuleComponent } from './database-business-module/database-business-module.component';
import { DatabaseBusinessModuleCreateComponent } from './database-business-module-create/database-business-module-create.component';
import { DatabaseBusinessModuleDetailsComponent } from './database-business-module-details/database-business-module-details.component';

const appRoutes: Routes = [

  {
    path: 'serverinformation',
    component: ServerInformationComponent,
  },
  {
    path: 'Tables/:dbName',
    component: DatabaseAllTableInfoComponent,
    pathMatch: 'full'
  },
  {
    path: 'TableDetails/:dbName',
    component: DatabaseTableInfoComponent,
    pathMatch: 'full'
  },
  {
    path: 'AllViews/:dbName',
    component: DatabaseAllViewsComponent,
    pathMatch: 'full'
  },
  {
    path: 'ViewsDetails/:dbName',
    component: DatabaseViewsDetailsComponent,
    pathMatch: 'full'
  },
  {
    path: 'StoreProcedures/:dbName',
    component: DatabaseAllStoreProcedureComponent,
    pathMatch: 'full'
  }
  ,
  {
    path: 'StoreProcedureDetails/:dbName',
    component: DatabaseStoreProcedureDetailsComponent,
    pathMatch: 'full'
  },

  {
    path: 'DatabaseInfo/:dbName',
    component: DatabaseInfoComponent,
    pathMatch: 'full'
  },
  {
    path: 'TableValueFunctions/:dbName',
    component: DatabaseTableValueFunctionsComponent,
    pathMatch: 'full'
  },
  {
    path: 'TableValueFunctionDetails/:dbName',
    component: DatabaseTableValueFunctionDetailsComponent,
    pathMatch: 'full'
  }
  ,
  {
    path: 'ScalarFunctions/:dbName',
    component: DatabaseScalarFunctionsComponent,
    pathMatch: 'full'
  },
  {
    path: 'ScalarFunctionDetails/:dbName',
    component: DatabaseScalarFunctionDetailsComponent,
    pathMatch: 'full'
  },
  {
    path: 'DatabaseTriggers/:dbName',
    component: DatabaseTriggersComponent,
    pathMatch: 'full'
  },
  {
    path: 'DatabaseTriggerDetails/:dbName',
    component: DatabaseTriggerDetailsComponent,
    pathMatch: 'full'
  },
  {
    path: 'UserDefinedDataTypes/:dbName',
    component: DatabaseUsedDefinedDataTypesComponent,
    pathMatch: 'full'
  },
  {
    path: 'UserDefinedDataTypeDetails/:dbName',
    component: DatabaseUsedDefinedDataTypeDetailsComponent,
    pathMatch: 'full'
  },
  {
    path: 'XmlSchemaCollections/:dbName',
    component: DatabasexmlSchemaCollectionsComponent,
    pathMatch: 'full'
  },
  {
    path: 'XmlSchemaCollectionDetails/:dbName',
    component: DatabasexmlSchemaCollectionDetailsComponent,
    pathMatch: 'full'
  },
  {
    path: 'WorkFlow/:dbName',
    component: BusinessworkFlowComponent,
    pathMatch: 'full'
  },
  {
    path: 'LoadSSISPackage',
    component: LoadSSISPackagesComponent,
    pathMatch: 'full'
  },
  {
    path: 'SSISPackage',
    component: SSISPackageComponent,
    pathMatch: 'full'
  },

  {
    path: 'SSISPackagesDetails/:PkgName',
    component: SSISPackageDetailsComponent,
    pathMatch: 'full'
  },

  {
    path: 'AllSchemas/:dbName',
    component: DatabaseSchemasComponent,
    pathMatch: 'full'
  },
  {
    path: 'SchemasDetails/:dbName',
    component: DatabaseSchemaDetailsComponent,
    pathMatch: 'full'
  },
  //DatabaseBusinessModuleComponent

  {
    path: 'AdminLogin',
    component: AdminloginComponent,
    pathMatch: 'full'
  },
  {
    path: '',
    component: ServerInformationComponent,
    pathMatch: 'full'
  },
  {
    path: 'BusinessModule/:dbName',
    component: DatabaseBusinessModuleComponent,
    pathMatch: 'full'
  },
  {
    path: 'BusinessModuleCreate/:dbName',
    component: DatabaseBusinessModuleCreateComponent,
    pathMatch: 'full'
  },
  {
    path: 'BusinessModuleDetails/:dbName',
    component: DatabaseBusinessModuleDetailsComponent,
    pathMatch: 'full'
  },
  //DatabaseBusinessModuleComponent
];
const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  //bgsColor: 'red',
  //bgsPosition: POSITION.bottomCenter,
  //bgsSize: 40,
  //bgsType: SPINNER.rectangleBounce, // background spinner type
  //fgsType: SPINNER.chasingDots, // foreground spinner type
  //pbDirection: PB_DIRECTION.leftToRight, // progress bar direction
  //pbThickness: 5, // progress bar thickness

  "bgsColor": "#1c749a",
  "bgsOpacity": 0.7,
  "bgsPosition": "center-center",
  "bgsSize": 80,
  "bgsType": "ball-spin-clockwise",
  "blur": 5,
  "delay": 0,
  "fgsColor": "#1c749a",
  "fgsPosition": "center-center",
  "fgsSize": 60,
  "fgsType": "ball-spin-clockwise",
  "gap": 10,
  "logoPosition": "center-center",
  "logoSize": 120,
  "logoUrl": "",
  "masterLoaderId": "master",
  "overlayBorderRadius": "0",
  "overlayColor": "rgba(40, 40, 40, 0.8)",
  "pbColor": "red",
  "pbDirection": "ltr",
  "pbThickness": 1,
  "hasProgressBar": true,
  "text": "Loading",
  "textColor": "#FFFFFF",
  "textPosition": "center-center",
  "maxTime": -1,
  "minTime": 300
};
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LeftNavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ServerInformationComponent,
    DatabaseInfoComponent,
    DatabaseTableInfoComponent,
    DatabaseAllTableInfoComponent,
    DatabaseAllViewsComponent,
    DatabaseViewsDetailsComponent,
    DatabaseAllStoreProcedureComponent,
    DatabaseStoreProcedureDetailsComponent,
    DatabaseTableValueFunctionsComponent,
    DatabaseTableValueFunctionDetailsComponent,
    DatabaseScalarFunctionsComponent,
    DatabaseScalarFunctionDetailsComponent,
    DatabaseTriggersComponent,
    DatabaseTriggerDetailsComponent,
    DatabaseUsersComponent,
    DatabaseUserDetailsComponent,
    DatabaseRolesComponent,
    DatabaseRoleDetailsComponent,
    DatabaseSchemasComponent,
    DatabaseSchemaDetailsComponent,
    DatabaseUsedDefinedDataTypesComponent,
    DatabaseUsedDefinedDataTypeDetailsComponent,
    DatabasexmlSchemaCollectionsComponent,
    DatabasexmlSchemaCollectionDetailsComponent,
    SearchPipe,
    HighlightPipe,
    LoginPageComponent,
    BusinessworkFlowComponent,
    SSISPackageComponent,
    SSISPackageDetailsComponent,
    LoadSSISPackagesComponent,
    AdminloginComponent,
    ModalContentComponent,
    DatabaseBusinessModuleComponent,
    DatabaseBusinessModuleCreateComponent,
    DatabaseBusinessModuleDetailsComponent

  ],
  entryComponents: [ModalContentComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AmexioDataModule,
    AmexioWidgetModule,
    AmexioChartsModule,
    AmexioEnterpriseModule,
    AngularFontAwesomeModule,
    BrowserAnimationsModule,
    AngularSplitModule.forRoot(),
    RouterModule.forRoot(appRoutes, { useHash: true }),
    PrismModule,
    TreeModule,
    CommonModule,
    ToastModule,
    ButtonModule,
    ContextMenuModule,
    TabViewModule,
    CodeHighlighterModule,
    AccordionModule,
    TableModule,
    NgbModule,
    ProgressBarModule,
    BreadcrumbModule,
    HttpClientModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderHttpModule,
    ModalModule.forRoot(),
    AngularMultiSelectModule

  ],
  providers:
    [
      {
        provide: LocationStrategy,
        useClass: HashLocationStrategy
      },
      Configuration,
      TreeDragDropService,
      MessageService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
