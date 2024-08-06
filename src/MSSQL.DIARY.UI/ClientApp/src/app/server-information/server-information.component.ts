import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerProprty } from 'src/models/ModelClass';
@Component({
  selector: 'app-server-information',
  templateUrl: './server-information.component.html',
  styleUrls: ['./server-information.component.css']
})
export class ServerInformationComponent implements OnInit {

  public ServerNames: string[];
  public DatabaseNames: string[];
  public serverProprties: ServerProprty[];
  public serverAdvanceProprties: ServerProprty[];
  public http: HttpClient;
  public baseUrl: string;
  public items: MenuItem[];
  public home: MenuItem;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;

  }
  ngOnInit() {
    this.ngxLoader.start();
    this.http.get<string[]>(this.baseUrl + 'api/ServerInformation/GetServerInformation').subscribe(result => {
      this.ServerNames = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<string[]>(this.baseUrl + 'api/ServerInformation/GetDatabaseNames').subscribe(result => {
      this.DatabaseNames = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<ServerProprty[]>(this.baseUrl + 'api/ServerInformation/GetServerProperties').subscribe(result => {
      this.serverProprties = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());

    this.ngxLoader.start();
    this.http.get<ServerProprty[]>(this.baseUrl + 'api/ServerInformation/GetAdvancedServerSettingsInfo').subscribe(result => {
      this.serverAdvanceProprties = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());

    this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };
    this.items =
      [

      ];
  }
} 
