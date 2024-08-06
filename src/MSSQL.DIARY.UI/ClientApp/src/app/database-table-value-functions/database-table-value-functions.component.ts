import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerProprty } from 'src/models/ModelClass'
@Component({
  selector: 'app-database-table-value-functions',
  templateUrl: './database-table-value-functions.component.html',
  styleUrls: ['./database-table-value-functions.component.css']
})
export class DatabaseTableValueFunctionsComponent implements OnInit {


  public all_TableValueFunctions: ServerProprty[];
  public dbName: string;
  public baseUrl: string;
  public TableValueFunctionName: string;
  public http: HttpClient;
  public query: any;
  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.query = "";
  }

  ngOnInit() {
    this.ngxLoader.start();
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.TableValueFunctionName = this.route.snapshot.params.dbName.split('/')[1]

    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetAllTableValueFunctionWithMsDescriptions', { params: { istrdbName: this.dbName } }).subscribe(result => {
      this.all_TableValueFunctions = result;
      for (let Property of this.all_TableValueFunctions) {
        Property.istrNevigation = this.dbName + "/" + Property.istrName;
      }
      this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());

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
            icon: "fa fa-folder"
          }

        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, () => this.ngxLoader.stop());


  }

}

