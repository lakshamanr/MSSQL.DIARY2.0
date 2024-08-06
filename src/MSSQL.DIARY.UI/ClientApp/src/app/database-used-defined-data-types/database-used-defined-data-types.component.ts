 
 
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { UserDefinedDataTypeDetails } from 'src/models/ModelClass'
@Component({
  selector: 'app-database-used-defined-data-types',
  templateUrl: './database-used-defined-data-types.component.html',
  styleUrls: ['./database-used-defined-data-types.component.css']
})
export class DatabaseUsedDefinedDataTypesComponent implements OnInit {

  public items: MenuItem[];
  public home: MenuItem;

  public http: HttpClient;
  public baseUrl: string;

  public dbName: string;

  public lstUserDefinedDataTypeDetails: UserDefinedDataTypeDetails[];
    serverName: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }


  ngOnInit()
  {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];

    this.ngxLoader.start();

    this.http.get<UserDefinedDataTypeDetails[]>(this.baseUrl + 'api/DatabaseUserDefinedDataTypes/GetAllUserDefinedDataTypes',
      { params: { istrdbName: this.dbName } }).subscribe(result =>
      {
        this.lstUserDefinedDataTypeDetails = result;
        for (let Property of this.lstUserDefinedDataTypeDetails) {
          Property.istrNevigation = this.dbName + "/" + Property.name;
        }
        this.ngxLoader.stop();
      }, () => this.ngxLoader.stop());


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

            label: "User Defined Data Types",
            routerLink: "/UserDefinedDataTypes/" + this.dbName,
            icon: "fa fa-folder"
          }
        ];
      this.home =
      {

        label: 'Project',
        icon: 'pi pi-home',
        routerLink: "/"
      };
      this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());
  }

}
