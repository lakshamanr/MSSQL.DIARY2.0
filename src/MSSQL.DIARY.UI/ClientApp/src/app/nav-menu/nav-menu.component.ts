 
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Cookie } from 'ng2-cookies/ng2-cookies';
import { MenuItem } from 'primeng/api/menuitem';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
    public http: HttpClient;
  public baseUrl: string;
  public items: MenuItem[];
  public home: MenuItem;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
        this.http = http;
        this.baseUrl = baseUrl;
    }
  isExpanded = false;

  collapse()
  {
    this.isExpanded = false;
  }

  toggle()
  {
    this.isExpanded = !this.isExpanded;
    }
    
  ngOnInit()
  {
  }
  LogOut(): any
  {
    Cookie.delete('istrUserName');
    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/DatabaseLogin/IsLogOutSuccessfully').subscribe(result => {
      window.location.reload();
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());


   
  }
}
