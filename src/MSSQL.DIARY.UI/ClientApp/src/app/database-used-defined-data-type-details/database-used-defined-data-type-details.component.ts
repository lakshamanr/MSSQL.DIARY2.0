
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { UserDefinedDataTypeReferance, UserDefinedDataTypeDetails, Ms_Description } from 'src/models/ModelClass';

@Component({
  selector: 'app-database-used-defined-data-type-details',
  templateUrl: './database-used-defined-data-type-details.component.html',
  styleUrls: ['./database-used-defined-data-type-details.component.css']
})
export class DatabaseUsedDefinedDataTypeDetailsComponent implements OnInit {


  public items: MenuItem[];
  public home: MenuItem;
  iblnShowEditBox: boolean;
  public http: HttpClient;
  public baseUrl: string;
  public language = 'plsql';

  public dbName: string;
  public userDefinedDataTypeName: string;
  lstUserDefinedDataTypeDetails: UserDefinedDataTypeDetails;
  lstUserDefinedDataTypeReferance: UserDefinedDataTypeReferance[];
  ms_des: Ms_Description;
  ms_desc_clone: Ms_Description
  tblName: string;
  serverName: string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.lstUserDefinedDataTypeDetails = new UserDefinedDataTypeDetails();
    this.ms_des = new Ms_Description();
    this.ms_desc_clone = new Ms_Description();
    this.iblnShowEditBox = false;
  }
  ngOnInit() {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.userDefinedDataTypeName = this.route.snapshot.params.dbName.split('/')[1];

    this.ngxLoader.start();

    this.http.get<UserDefinedDataTypeDetails>(this.baseUrl + 'api/DatabaseUserDefinedDataTypes/GetUserDefinedDataTypeDetails',
      {
        params: {
          istrdbName: this.dbName,
          istrTypeName: this.userDefinedDataTypeName
        },

      }).subscribe(result => {
        this.lstUserDefinedDataTypeDetails = result;

        this.ngxLoader.stop();
      }, () => this.ngxLoader.stop());

    this.ngxLoader.start();

    this.http.get<UserDefinedDataTypeReferance[]>(this.baseUrl + 'api/DatabaseUserDefinedDataTypes/GetUsedDefinedDataTypeReferance',
      {
        params: {
          istrdbName: this.dbName,
          istrTypeName: this.userDefinedDataTypeName
        },

      }).subscribe(result => {
        this.lstUserDefinedDataTypeReferance = result;
        //
        // this.lstUserDefinedDataTypeDetails.istrNevigation = this.dbName + "/" + this.lstUserDefinedDataTypeDetails.name;
        for (let Property of this.lstUserDefinedDataTypeReferance) {
          Property.istrNevigation = this.dbName + "/" + Property.objectname;
        }
        this.ngxLoader.stop();
      }, () => this.ngxLoader.stop());

    this.ngxLoader.start();

    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseUserDefinedDataTypes/GetUsedDefinedDataTypeExtendedProperties',
      {
        params: {
          istrdbName: this.dbName,
          istrTypeName: this.userDefinedDataTypeName
        },

      }).subscribe(result => {
        this.ms_des = result;
        this.ms_desc_clone = this.ms_des
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
          },
          {

            label: this.lstUserDefinedDataTypeDetails.name,
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
    }, () => this.ngxLoader.stop());

  }
  ShowModelPOP(): any {
    this.iblnShowEditBox = true;

  }
  SaveTableMsDesciption(): any {
    this.iblnShowEditBox = false;
    this.http.get<any>(this.baseUrl + 'api/DatabaseUserDefinedDataTypes/CreateOrUpdateUsedDefinedDataTypeExtendedProperties', {
      params:
      {
        istrdbName: this.dbName,
        istrTypeName: this.userDefinedDataTypeName,
        istrdescValue: this.ms_des.desciption

      }
    }).subscribe(() => {

    }, error => console.error(error));

  }
  CancelTableMsDesciption(): any {
    this.ms_des = this.ms_desc_clone;
    this.iblnShowEditBox = false;

  }

}
