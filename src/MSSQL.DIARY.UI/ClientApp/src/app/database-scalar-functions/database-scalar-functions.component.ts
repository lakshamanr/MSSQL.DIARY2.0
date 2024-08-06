import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem'; 
import { ServerProprty } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-scalar-functions',
  templateUrl: './database-scalar-functions.component.html',
  styleUrls: ['./database-scalar-functions.component.css']
})
export class DatabaseScalarFunctionsComponent implements OnInit {

  public all_ScalarFunctions: ServerProprty[];
  public dbName: string;
  public baseUrl: string;
  public ScalatFunctionName: string;
  public http: HttpClient;
  public query: any;

  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.ngxLoader.start();
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.ScalatFunctionName = this.route.snapshot.params.dbName.split('/')[1]

    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseFunctionInformation/GetAllScalarFunctionWithMsDescriptions', { params: { istrdbName: this.dbName } }).subscribe(result => {
      this.all_ScalarFunctions = result;
      for (let Property of this.all_ScalarFunctions) {
        Property.istrNevigation = this.dbName + "/" + Property.istrName;
      }
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
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
            icon: "fa fa-folder"
          }
        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, error => this.ngxLoader.stop());

  }

}
 
