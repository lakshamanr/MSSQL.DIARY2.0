 
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { PropertyInfo } from 'src/models/ModelClass';
@Component({
  selector: 'app-ssispackage',
  templateUrl: './ssispackage.component.html',
  styleUrls: ['./ssispackage.component.css']
})
export class SSISPackageComponent implements OnInit {
  public dbName: string;
  public baseUrl: string; 
  public http: HttpClient;

  public progress: number;
  public message: string;

  public GetAllSSISPackages: PropertyInfo[];
  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private ngxLoader: NgxUiLoaderService)
  {
    this.http = http;
    this.baseUrl = baseUrl;
  }

    

  ngOnInit()
  {
    this.ngxLoader.start();
    this.http.get<PropertyInfo[]>(this.baseUrl + 'api/LeftMenu/GetSSISPackagDetails').subscribe(result => {
      this.GetAllSSISPackages = result; 
      this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());
  }

}
