import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerProprty } from 'src/models/ModelClass'
@Component({
  selector: 'app-database-schemas',
  templateUrl: './database-schemas.component.html',
  styleUrls: ['./database-schemas.component.css']
})
export class DatabaseSchemasComponent implements OnInit {
  public query: any;
  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public lstSchemas: ServerProprty[];
  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }


  ngOnInit() {

    this.dbName = this.route.snapshot.params.dbName.split('/')[0];

    this.ngxLoader.start();
    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseSchema/GetListOfAllSchemaAndMsDescription',
      { params: { istrdbName: this.dbName } }).subscribe(result => {
        this.lstSchemas = result;
        for (let Property of this.lstSchemas) {
          Property.istrNevigation = this.dbName + "/" + Property.istrName;
        }
        this.ngxLoader.stop();
      }, () => this.ngxLoader.stop());


  }

}
