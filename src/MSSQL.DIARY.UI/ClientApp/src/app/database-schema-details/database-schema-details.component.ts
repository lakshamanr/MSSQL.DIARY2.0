import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { Ms_Description, SchemaReferanceInfo} from 'src/models/ModelClass'

@Component({
  selector: 'app-database-schema-details',
  templateUrl: './database-schema-details.component.html',
  styleUrls: ['./database-schema-details.component.css']
})
export class DatabaseSchemaDetailsComponent implements OnInit {
  public query: any;
  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public dbSchemaName: string;
  public ms_description: Ms_Description;
  public schemReferance: SchemaReferanceInfo[];
  public items: MenuItem[];
  public home: MenuItem;
  serverName: string;
  tblName: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {

    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.dbSchemaName = this.route.snapshot.params.dbName.split('/')[1];

    this.ngxLoader.start();

    this.http.get<SchemaReferanceInfo[]>(this.baseUrl + 'api/DatabaseSchema/GetSchemaReferanceInfo',
      {
        params: {

          istrdbName: this.dbName,
          astrSchema_Name: this.dbSchemaName
        }
      }).subscribe(result => {
        this.schemReferance = result;
        for (let Property of this.schemReferance) {
          Property.istrNevigation = this.dbName + "/" + this.dbSchemaName + "." + Property.istrName;
        }
        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());


    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseSchema/GetSchemaMsDescription',
      {
        params: {

          istrdbName: this.dbName,
          astrSchema_Name: this.dbSchemaName
        }
      }).subscribe(result => {
        this.ms_description = result;
        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());

    this.http.get<string>(this.baseUrl + 'api/ServerInformation/GetServerInformation').subscribe(result => {
      this.serverName = result[0];
      this.items =
        [
          {

            label: this.serverName,
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

            label: "Tables",
            routerLink: "/Tables/" + this.dbName,
            icon: "fa fa-folder"
          },
          {

            label: this.tblName,
            icon: "fa fa-table fa-fw"

          }
        ];
      this.home =
      {

        label: 'Project',
        icon: 'pi pi-home',
        routerLink: "/"
      };
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());

  }

}


//public class SchemaReferanceInfo {
//  public string istrName { get; set; }
//        public string SchemObjectType { get; set; }
//    }

