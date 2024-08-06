import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';


@Component({
  selector: 'app-database-table-value-function-details',
  templateUrl: './database-table-value-function-details.component.html',
  styleUrls: ['./database-table-value-function-details.component.css']
})
export class DatabaseTableValueFunctionDetailsComponent implements OnInit {

  public FunctionName: string;
  public ms_description: string;
  public ms_descriptions: ServerProprty;
  public FunctionParametersInfo: FunctionParameters[];
  public showCreateScript: boolean
  public language = "plsql";
  public FunCreateScript: FunctionCreateScript;
  public functiodDependencies: FunctionDependencies[];
  public functionProperties: FunctionProperties[];
  iblnShowEditBox: boolean;
  ms_description_old: string;

  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.FunctionName = "";
    this.ms_description = "";
    this.showCreateScript = false;
  }

  ngOnInit() {

    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.FunctionName = this.route.snapshot.params.dbName.split('/')[1];
    this.showCreateScript = false;

    this.ngxLoader.start();
    this.http.get<FunctionDependencies[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetTableValueFunctionDependencies', {
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
    this.http.get<FunctionCreateScript>(this.baseUrl + 'api/DatabaseFunctionInformation/GetTableValueFunctionCreateScript', {
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
    this.http.get<FunctionProperties[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetTableValueFunctionProperties', {
      params: {
        astrFunctionName: this.FunctionName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.functionProperties = result;
      this.ngxLoader.stop();
    }, error => console.error(error));


    this.ngxLoader.start();
    this.http.get<FunctionParameters[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetTableValueFunctionParameters', {
      params: {
        astrFunctionName: this.FunctionName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.FunctionParametersInfo = result;
      this.ngxLoader.stop();
    }, error => console.error(error));

    this.ngxLoader.start();
    this.http.get<ServerProprty>(this.baseUrl + 'api/DatabaseFunctionInformation/GetTableValueFunctionMsDescriptions', {
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
            label: "Table-valued Functions",
            icon: "fa fa-folder",
            routerLink: "/TableValueFunctions/" + this.dbName
          },
          {
            label: this.FunctionName,
            icon: "fa fa-table fa-fw"
          }

        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, () => this.ngxLoader.stop());


  }
  SaveTableMsDesciption(): any {
    this.iblnShowEditBox = false;
    this.http.get<boolean>(this.baseUrl + 'api/DatabaseFunctionInformation/CreateOrUpdateTableValueFunctionDescription', {
      params:
      {
        istrdbName: this.dbName,
        astrDescription_Value: this.ms_description,
        astrFunctionName: this.FunctionName
      }
    }).subscribe(() => {

    }, error => console.error(error));

  }

  ShowModelPOP(): any {
    this.iblnShowEditBox = true;

  }
  CancelTableMsDesciption(): any {
    this.ms_description = this.ms_description_old;
    this.iblnShowEditBox = false;

  }

}
export class ServerProprty {
  public istrName: string;
  public istrValue: string;
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
