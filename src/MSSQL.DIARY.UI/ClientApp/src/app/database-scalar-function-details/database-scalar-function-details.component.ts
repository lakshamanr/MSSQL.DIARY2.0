import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerProprty, FunctionParameters, FunctionCreateScript, FunctionProperties,FunctionDependencies } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-scalar-function-details',
  templateUrl: './database-scalar-function-details.component.html',
  styleUrls: ['./database-scalar-function-details.component.css']
})
export class DatabaseScalarFunctionDetailsComponent implements OnInit {

  public FunctionName: string;
  public ms_description: string;
  public ms_descriptions: ServerProprty;
  public FunctionParametersInfo: FunctionParameters[];
  public showCreateScript: boolean
  public language = "plsql";
  public FunCreateScript: FunctionCreateScript;
  public functiodDependencies: FunctionDependencies[];
  public functionProperties: FunctionProperties[];

  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  iblnShowEditBox: boolean;
  ms_description_old: string;
  public items: MenuItem[];
  public home: MenuItem;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.FunctionName = "";
    this.ms_description = "";
    this.showCreateScript = false;
    this.iblnShowEditBox = false;
    this.ms_description = "";
  }

  ngOnInit() {

    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.FunctionName = this.route.snapshot.params.dbName.split('/')[1];
    this.showCreateScript = false;

    this.ngxLoader.start();
    this.http.get<FunctionDependencies[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetScalerFunctionDependencies', {
      params: {
        astrFunctionName: this.FunctionName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.functiodDependencies = result;
      //NevigationLink
      for (let Prop of this.functiodDependencies) {
        Prop.NevigationLink = this.dbName + "/" + Prop.name;
      }

      this.ngxLoader.stop();
    }, error => console.error(error));



    this.ngxLoader.start();
    this.http.get<FunctionCreateScript>(this.baseUrl + 'api/DatabaseFunctionInformation/GetScalerFunctionCreateScript', {
      params: {
        astrFunctionName: this.FunctionName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.FunCreateScript = result;
      this.showCreateScript = true;

      this.ngxLoader.stop();
    }, error => console.error(error));



    this.ngxLoader.start();
    this.http.get<FunctionProperties[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetScalerFunctionProperties', {
      params: {
        astrFunctionName: this.FunctionName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.functionProperties = result;

      this.ngxLoader.stop();
    }, error => console.error(error));


    this.ngxLoader.start();
    this.http.get<FunctionParameters[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetScalerFunctionParameters', {
      params: {
        astrFunctionName: this.FunctionName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.FunctionParametersInfo = result;
      this.ngxLoader.stop();
    }, error => console.error(error));

    this.ngxLoader.start();
    this.http.get<ServerProprty>(this.baseUrl + 'api/DatabaseFunctionInformation/GetScalarFunctionMsDescriptions', {
      params: {
        astrFunctionName: this.FunctionName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.ms_descriptions = result;
      this.ms_description = this.ms_descriptions.istrValue;
      this.ms_description_old = this.ms_description;
      this.ngxLoader.stop();
    }, error => console.error(error));

    this.http.get<string>(this.baseUrl + 'api/ServerInformation/GetServerInformation').subscribe(result => {
      this.items =
        [

          {
            label: result[0],
            icon: "fa  fa-desktop fa-fw",
            routerLink: "/serverinformation"
          },
          {
            label: 'User Databases',
            icon: "fa fa-folder",
          },
          {
            label: this.dbName,
            icon: "fa fa-database fa-fw",
            routerLink: "/DatabaseInfo/" + this.dbName
          },
          {
            label: "Programmability",
            icon: "fa fa-folder"
          },
          {
            label: "Functions",
            icon: "fa fa-folder"
          },
          {
            label: "Scalar-valued Functions",
            icon: "fa fa-folder",
            routerLink: "/TableValueFunctions/" + this.dbName
          },
          {
            label: this.FunctionName
            //,
            //icon: "fa fa-table fa-fw"
          }

        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, error => this.ngxLoader.stop());


  }
  ShowModelPOP($event): any {
    this.iblnShowEditBox = true;

  }
  SaveTableMsDesciption($event): any {
    this.iblnShowEditBox = false;
    this.http.get<boolean>(this.baseUrl + 'api/DatabaseFunctionInformation/CreateOrUpdateScalerFunctionDescription', {
      params:
      {
        istrdbName: this.dbName,
        astrDescription_Value: this.ms_description,
        astrFunctionName: this.FunctionName
      }
    }).subscribe((result: any) => {

    }, error => console.error(error));

  }
  CancelTableMsDesciption($event): any {
    this.ms_description = this.ms_description_old;
    this.iblnShowEditBox = false;

  }
}
