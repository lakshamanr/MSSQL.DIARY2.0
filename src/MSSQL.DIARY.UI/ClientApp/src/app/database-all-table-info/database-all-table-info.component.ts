import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MenuItem } from 'primeng/api/menuitem';
import { TableColumns, TablePropertyInfo } from 'src/models/ModelClass'
@Component({
  selector: 'app-database-all-table-info',
  templateUrl: './database-all-table-info.component.html',
  styleUrls: ['./database-all-table-info.component.css'],
  animations: [
    trigger('rowExpansionTrigger', [
      state('void', style({
        transform: 'translateX(-10%)',
        opacity: 0
      })),
      state('active', style({
        transform: 'translateX(0)',
        opacity: 1
      })),
      transition('* <=> *', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
    ])
  ]
})
export class DatabaseAllTableInfoComponent implements OnInit {

  public all_database_table: TablePropertyInfo[];
  public query: any;
  public dbName: string;
  public baseUrl: string;
  public http: HttpClient;
  public cols: any[];
  public marked = false;
  public searchInAll: boolean;
  public searchInTable: boolean;
  public searchInColumn: boolean;
  public searchInSSIS: boolean;
  public items: MenuItem[];
  public home: MenuItem;
  public serverName: string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.query = "";
    this.searchInAll = true;
  }

  ngOnInit() {


    this.cols = [
      { field: 'istrFullName', header: 'istrFullName' },
      { field: 'istrValue', header: 'istrValue' }
    ];


    this.ngxLoader.start();
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    var serverName = this.route.snapshot.params.dbName.split('/')[1];
    this.LoadServerInformations(serverName, "false");
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
            icon: "fa fa-folder",
            routerLink: "/Tables/" + this.dbName
          }
        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, error => this.ngxLoader.stop());
  }

  private LoadServerInformations(serverName: any, blnSearchInSSISPackages) {
    this.http.get<TablePropertyInfo[]>(this.baseUrl + 'api/DatabaseTables/GetAllDatabaseTables', {
      params: {
        istrdbName: this.dbName,
        iblnSearchInSSISPackages: blnSearchInSSISPackages
      }
    }).subscribe(result => {
      this.all_database_table = result;
      for (let property of this.all_database_table) {
        property.istrNevigation = this.dbName + "/" + property.istrFullName + "///" + serverName;
      }
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
  }

  changeSuit(e) {
    this.searchInAll = false;
    this.searchInColumn = false
    this.searchInTable = false;
    this.searchInSSIS = false;
    this.query = "";
    switch (e) {
      case "searchInAll":
        this.searchInAll = true;
        this.LoadServerInformations(this.serverName, "false");
        break;
      case "searchInColumn":
        this.searchInColumn = true
        this.LoadServerInformations(this.serverName, "false");
        break;
      case "searchInTable":
        this.searchInTable = true;
        this.LoadServerInformations(this.serverName, "false");
        break;
      case "searchInSSIS":
        this.searchInSSIS = true;
        this.LoadServerInformations(this.serverName, "true");
        break;
      default:
        this.searchInAll = true;
        break;

    }
  }

}
