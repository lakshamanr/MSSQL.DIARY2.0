import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Cookie } from 'ng2-cookies/ng2-cookies';
import { DOCUMENT } from '@angular/common';
import { MenuItem } from 'primeng/api/menuitem';
import { ServerLogin, LoginModel } from 'src/models/ModelClass';
@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  public istrServerName: string;
  public istrDatabaseName: string;
  public istrUserName: string;
  public istrPassword: string;
  public istrErrorMessagese: string
  public serverLogin: ServerLogin;
  public http: HttpClient;
  public baseUrl: string;
  public items: MenuItem[];
  public home: MenuItem;
  constructor(private route: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private ngxLoader: NgxUiLoaderService, @Inject(DOCUMENT) private _document: Document) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.istrErrorMessagese = "";

  }

  ngOnInit() {

  }
  Submit() {

    this.ngxLoader.start();
    this.http.get<LoginModel>(this.baseUrl + 'api/DatabaseLogin/IsLoginSuccessfully', {
      params: {
        istrUserName: this.istrUserName,
        istrPassword: this.istrPassword,
        istrServerName: this.istrServerName
      }
    }).subscribe(result => {

      if (result.iblnIsLoginSucessFully) {
        Cookie.set('istrUserName', this.istrUserName, 10);
        this.http.get<any>(this.baseUrl + 'api/LeftMenu/RefreshCache').subscribe(result => {
          this._document.defaultView.location.reload();
          this.ngxLoader.stop();
        }, error => this.ngxLoader.stop());


      }
      else {
        this.istrErrorMessagese = "Faild to login Please varify User Name / password";
        this.ngxLoader.stop();
      }
    }, error => console.log(""));
  }
}

