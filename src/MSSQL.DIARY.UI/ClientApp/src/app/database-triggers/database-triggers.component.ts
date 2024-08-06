import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerProprty } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-triggers',
  templateUrl: './database-triggers.component.html',
  styleUrls: ['./database-triggers.component.css']
})
export class DatabaseTriggersComponent implements OnInit {
  public query: any;
  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public TriggerName: string;
  public lstTrigger: ServerProprty[];
  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }


  ngOnInit() {

    this.dbName = this.route.snapshot.params.dbName.split('/')[0];

    this.ngxLoader.start();

    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseTriggers/GetAllDatabaseTrigger',
      { params: { istrdbName: this.dbName } }).subscribe(result => {
        this.lstTrigger = result;
        for (let Property of this.lstTrigger) {
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
            label: " Database Trigger ",
            icon: "fa fa-folder"
          }
        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, () => this.ngxLoader.stop());

  }

}
