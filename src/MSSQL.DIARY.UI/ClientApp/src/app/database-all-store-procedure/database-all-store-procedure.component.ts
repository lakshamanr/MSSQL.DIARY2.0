import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerProprty } from 'src/models/ModelClass'
@Component({
  selector: 'app-database-all-store-procedure',
  templateUrl: './database-all-store-procedure.component.html',
  styleUrls: ['./database-all-store-procedure.component.css']
})
export class DatabaseAllStoreProcedureComponent implements OnInit {

  public all_database_SP: ServerProprty[];
  public dbName: string;
  public baseUrl: string;
  public SP_Name: string;
  public query: any;
  public cols: any[];
  public http: HttpClient;
  public language = 'plsql';
  public searchInAll: boolean; 
  public searchInSSIS: boolean;
  public items: MenuItem[];
  public home: MenuItem;
  public serverName: string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService)
  {

    this.http = http;
    this.baseUrl = baseUrl;
    this.searchInAll = true;
  }

  ngOnInit()
  {


    this.cols = [
      { field: 'istrName', header: 'istrFullName' },
      { field: 'istrValue', header: 'istrValue' },
      //{ field: 'lstSSISpackageReferance', header: 'lstSSISpackageReferance' }
    ];

    this.ngxLoader.start();
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.SP_Name = this.route.snapshot.params.dbName.split('/')[1]
  
    this.LoadAllStoreProcInfo("false");

    this.http.get<string>(this.baseUrl + 'api/ServerInformation/GetServerInformation').subscribe(result => {
      this.serverName = result[0];
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
          label: "StoredProcedures",
          icon: "fa fa-folder"
        }
        
        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, error => this.ngxLoader.stop());
  }
  private LoadAllStoreProcInfo(blnSearchInSSISPackages:any) {
    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseSPInfomration/GetAllStoreprocedureDescription', { params: { istrdbName: this.dbName, iblnSearchInSSISPackages: blnSearchInSSISPackages } }).subscribe(result => {
            this.all_database_SP = result;
            for (let Property of this.all_database_SP) {
                Property.istrNevigation = this.dbName + "/" + Property.istrName;
            }
            this.ngxLoader.stop();
        }, error => this.ngxLoader.stop());
    }

  changeSuit(e) {
    this.searchInAll = false; 
    this.searchInSSIS = false;
    this.query = "";
    switch (e) {
      case "searchInAll":
        this.searchInAll = true;
        break; 
      case "searchInSSIS":
        this.searchInSSIS = true;
        this.LoadAllStoreProcInfo("true");
        break;
      default:
        this.searchInAll = true;
        break;

    }
  }

}

