import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerProprty } from 'src/models/ModelClass'
@Component({
  selector: 'app-database-all-views',
  templateUrl: './database-all-views.component.html',
  styleUrls: ['./database-all-views.component.css']
})
export class DatabaseAllViewsComponent implements OnInit {


  public GetAllViewsDetails: ServerProprty[];

  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public query: any;
  public cols: any[];
  public items: MenuItem[];
  public home: MenuItem;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.cols = [
      { field: 'istrName', header: 'istrName' },
      { field: 'istrValue', header: 'istrValue' }
    ];


    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.ngxLoader.start();
    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseViewInformation/GetAllViewsDetailsWithms_description', { params: { istrdbName: this.dbName } }).subscribe(result => {
      this.GetAllViewsDetails = result;
      for (let Property of this.GetAllViewsDetails) {
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
            label: "Views",
            icon: "fa fa-folder"
          }

        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, error => this.ngxLoader.stop());

  }

}

