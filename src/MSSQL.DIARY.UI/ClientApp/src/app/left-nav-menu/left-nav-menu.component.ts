import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { LeftMenuTreeViewJson } from '../left-menu-tree-view-json'
import { SchemaEnums } from 'src/models/util/schema-enums';
import { HttpClient } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Cookie } from 'ng2-cookies/ng2-cookies';
import { DOCUMENT } from '@angular/common';
import { MenuItem } from 'primeng/api/menuitem';
@Component({
  selector: 'app-left-nav-menu',
  templateUrl: './left-nav-menu.component.html',
  styleUrls: ['./left-nav-menu.component.css']
})
export class LeftNavMenuComponent implements OnInit {

  public http: HttpClient;
  public baseUrl: string;
  public CurrentOpenedLink: string;
  leftmenujsonvalues: any;
  leftmenuSSISjsonvalues: any;
  public iblnSSISMenu: boolean;
  public istrlogiggedUserName: string;
  public items: MenuItem[];
  public home: MenuItem;
  public iblnHiddeLeftMenu: boolean;
  public SSISMode: boolean;
  constructor(private route: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private ngxLoader: NgxUiLoaderService, @Inject(DOCUMENT) private _document: Document) {
    this.http = http;
    this.baseUrl = baseUrl;
  }
  ngOnInit() {
    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/LeftMenu/GetTreeViewJsonDetails').subscribe(result => {
      this.leftmenujsonvalues = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());

    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/LeftMenu/GetSSISPackageJsonDetails').subscribe(result => {
      this.leftmenuSSISjsonvalues = result;

      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());

    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/LeftMenu/SSISMode').subscribe(result => {
      this.SSISMode = result;
      console.table(this.SSISMode);
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());


    this.http.get<boolean>(this.baseUrl + 'api/DatabaseLogin/IsAdminUser', {
      params:
      {
        istrUserName: Cookie.get('istrUserName')
      }
    }).subscribe(result1 => {
      if (result1) {
        this.iblnHiddeLeftMenu = true;
      }
      else {
        this.iblnHiddeLeftMenu = false;
      }
    });


    var userName = Cookie.get('istrUserName');
    this.istrlogiggedUserName = userName;
  }
  getNodeData(data: LeftMenuTreeViewJson) {

    if (this.CurrentOpenedLink == undefined) {
      this.CurrentOpenedLink = data.mdaIcon;
    }
    else if (!(this.CurrentOpenedLink === data.mdaIcon)) {

      this.CurrentOpenedLink = data.mdaIcon;
    }
    else {
      return;

    }
    if (data.mdaIcon == "User Database") {
      return;
    }
    var databaseName = data.link.split('/')[4];
    switch (data.SchemaEnums) {

      case SchemaEnums.ProjectInfo:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["serverinformation"])
          );
        }
        break;

      case SchemaEnums.DatabaseServer:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["serverinformation"])
          );
        }
        break;
      case SchemaEnums.AllDatabase:
        {
          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/DatabaseInfo", data.text + "/" + data.link.split('/')[2]]));
        }
        break;
      case SchemaEnums.AllTable:
        {
          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/Tables", databaseName + "/" + data.link]));
        }
        break;
      case SchemaEnums.Table:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/TableDetails", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.AllViews:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/AllViews", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.Views:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/ViewsDetails", databaseName + "/" + data.text]));
        }
        break;

      case SchemaEnums.AllStoreprocedure:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/StoreProcedures", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.Storeprocedure:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/StoreProcedureDetails", databaseName + "/" + data.text + "/" + data.link]));
        }
        break;
      case SchemaEnums.AllTableValueFunction:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/TableValueFunctions", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.TableValueFunction:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/TableValueFunctionDetails", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.AllScalarValueFunctions:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/ScalarFunctions", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.ScalarValueFunctions:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/ScalarFunctionDetails", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.AllAggregateFunciton:
        {

        }
        break;
      case SchemaEnums.AggregateFunciton:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/StoreProcedureDetails", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.AllTriggers:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/DatabaseTriggers", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.Triggers:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/DatabaseTriggerDetails", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.AllUserDefinedDataType:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/UserDefinedDataTypes", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.UserDefinedDataType:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/UserDefinedDataTypeDetails", databaseName + "/" + data.text]));
        }
        break;
      case SchemaEnums.AllXMLSchemaCollection:
        {

          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/XmlSchemaCollections", databaseName + "/" + data.text]));
        }
        break;

    }
  }
  getSSISNodeData(data: LeftMenuTreeViewJson) {


    var ServerName = data.link;
    switch (data.SchemaEnums) {
      case SchemaEnums.AllSSISPackages:
        {
          try {
            this.route.navigateByUrl('/', { skipLocationChange: true }).then(
              () =>
                this.route.navigate(["/SSISPackage"]));
          } catch (e) {
            console.log(e)
          }

        }
        break;
      case SchemaEnums.SSISPackages:
        {
          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/SSISPackagesDetails", data.text]));
        }
        break;

    }
  }
  RefreshCache(): any {
    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/LeftMenu/RefreshCache').subscribe(result => {
      this.ngxLoader.stop();
      this._document.defaultView.location.reload();
    }, error => this.ngxLoader.stop());
  }
  HomePage(): any {
    this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.route.navigate(["serverinformation"])
    );
  }
  AdminHomePage(): any {
    this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.route.navigate(["AdminLogin"])
    );
  }
  //AdminHomePage
  loadLeftMenu(): any {
    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/LeftMenu/GetTreeViewJsonDetails').subscribe(result => {
      this.leftmenujsonvalues = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
  }
  LogOut(): any {
    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/DatabaseLogin/IsLogOutSuccessfully', {
      params: { istrUserName: Cookie.get('istrUserName') }
    }).subscribe(result => {
      Cookie.delete('istrUserName');
      this.ngxLoader.stop();
      window.location.href = "";


    }, error => this.ngxLoader.stop());



  }
}
