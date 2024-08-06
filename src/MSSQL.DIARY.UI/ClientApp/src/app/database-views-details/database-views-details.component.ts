import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { View_Dependancy, View_Properties, View_columns, View_create_script, ServerProprty } from 'src/models/ModelClass';

@Component({
  selector: 'app-database-views-details',
  templateUrl: './database-views-details.component.html',
  styleUrls: ['./database-views-details.component.css']
})
export class DatabaseViewsDetailsComponent implements OnInit {

  public View_ms_description: ServerProprty;
  public viewColumns: View_columns[];
  public view_script: View_create_script;
  public view_dependancies: View_Dependancy[];
  public view_property: View_Properties[];
  public language = "plsql";
  public iblnShowEditBox: boolean;

  public dbName: string;
  public baseUrl: string;
  public View_Name: string;
  public http: HttpClient;

  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.iblnShowEditBox = false;
  }

  ngOnInit() {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.View_Name = this.route.snapshot.params.dbName.split('/')[1];
    this.ngxLoader.start();
    this.http.get<ServerProprty>(this.baseUrl + 'api/DatabaseViewInformation/GetViewNameWithMs_description', { params: { istrdbName: this.dbName, astrViewName: this.View_Name } }).subscribe(result => {
      this.View_ms_description = result;
      this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<View_create_script>(this.baseUrl + 'api/DatabaseViewInformation/GetViewCreateScript', { params: { istrdbName: this.dbName, astrViewName: this.View_Name } }).subscribe(result => {
      this.view_script = result;
      this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<View_columns[]>(this.baseUrl + 'api/DatabaseViewInformation/GetViewColumns', { params: { istrdbName: this.dbName, astrViewName: this.View_Name } }).subscribe(result => {
      this.viewColumns = result;
      for (let Property of this.viewColumns) {
        Property.NevigationLink = this.dbName + "/" + Property.name;
      }
      this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<View_Properties[]>(this.baseUrl + 'api/DatabaseViewInformation/GetViewProperties', { params: { istrdbName: this.dbName, astrViewName: this.View_Name } }).subscribe(result => {
      this.view_property = result;
      this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<View_Dependancy[]>(this.baseUrl + 'api/DatabaseViewInformation/GetView_Dependancies', { params: { istrdbName: this.dbName, astrViewName: this.View_Name } }).subscribe(result => {
      this.view_dependancies = result;
      for (let Prop of this.view_dependancies) {
        Prop.NevigationLink = this.dbName + "/" + Prop.name;
      }
      //NevigationLink
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
            label: "Views",
            icon: "fa fa-folder",
            routerLink: "/AllViews/" + this.dbName
          },
          {
            label: this.View_Name,
            style: "background:url(./assets/icons/View.png)"
          }

        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, () => this.ngxLoader.stop());
  }
  EditView(): any {
    this.iblnShowEditBox = true;
  }
  CancelViewMsDesciption(): any {
    this.iblnShowEditBox = false;
  }
  SaveViewMsDesciption(): any {
    this.iblnShowEditBox = false;
  }

}

