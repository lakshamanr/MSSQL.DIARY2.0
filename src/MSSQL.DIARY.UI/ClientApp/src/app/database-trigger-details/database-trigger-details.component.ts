import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { TriggerDetails } from 'src/models/ModelClass'
@Component({
  selector: 'app-database-trigger-details',
  templateUrl: './database-trigger-details.component.html',
  styleUrls: ['./database-trigger-details.component.css']
})
export class DatabaseTriggerDetailsComponent implements OnInit {

  public query: any;
  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public TriggerName: string;
  public TriggerMsDescription: string; 
  public triggerDetails: TriggerDetails;

  public tiggersName: string;
  public tiggersDesc: string;
  public tiggersCreateScript: string;
  public tiggersCreatedDate: string;
  public tiggersModifyDate: string;
  public iblnShowEditBox: boolean;
  public language = 'plsql';

  public items: MenuItem[];
  public home: MenuItem;
    ms_description_old: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.iblnShowEditBox = false;
  }


  ngOnInit()
  {

    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.TriggerName = this.route.snapshot.params.dbName.split('/')[1]; 
    this.ngxLoader.start(); 
    this.http.get<TriggerDetails>(this.baseUrl + 'api/DatabaseTriggers/GetTriggerInfosByName',
      {
        params:
        {
          istrdbName: this.dbName,
          istrTriggerName:this.TriggerName
        }
      }).subscribe(result =>
      {
        this.triggerDetails = result;
        this.tiggersName = this.triggerDetails.tiggersName;
        this.tiggersDesc = this.triggerDetails.tiggersDesc;
         
        this.ms_description_old = this.tiggersDesc ;

        this.tiggersCreateScript = this.triggerDetails.tiggersCreateScript;
        this.tiggersCreatedDate = this.triggerDetails.tiggersCreatedDate;
        this.tiggersModifyDate = this.triggerDetails.tiggersModifyDate;
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
            label: "Database Trigger ",
            icon: "fa fa-folder",
            routerLink: "/DatabaseTriggers/" + this.dbName
        } ,
        {
          label: this.TriggerName 
        }
        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, () => this.ngxLoader.stop());

  }

  ShowModelPOP(): any {
    this.iblnShowEditBox = true;

  }
  SaveTableMsDesciption(): any {
    this.iblnShowEditBox = false;
    this.http.get<boolean>(this.baseUrl + 'api/DatabaseTriggers/CreateOrUpdateTriggerDescription', {
      params:
      {
        istrdbName: this.dbName,
        astrDescription_Value: this.tiggersDesc,
        astrTrigger_Name: this.TriggerName
      }
    }).subscribe(() => {

    }, error => console.error(error));

  }
  CancelTableMsDesciption(): any {
    this.tiggersDesc = this.ms_description_old;
    this.iblnShowEditBox = false;

  }

}
