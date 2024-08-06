import { Component, OnInit, Inject, AfterViewInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { DOCUMENT } from '@angular/common';
import { Cookie } from 'ng2-cookies/ng2-cookies';
import { Users } from 'src/models/ModelClass'
 @Component({
  selector: 'app-adminlogin',
  templateUrl: './adminlogin.component.html',
  styleUrls: ['./adminlogin.component.css']
})
export class AdminloginComponent implements OnInit, AfterViewInit {

 

  public http: HttpClient;
  public baseUrl: string;
  public lstservers: Users[];
  public lstServerBaseUsers: Users[];
  public lstServerBaseUsersClone: Users[];
  public iblnAddNewUser: boolean;
  public iblnDeleteUser: boolean;
  public iblnUpdateUser: boolean;
  public iblnShowPanel: boolean;
  public iblnUserPanel: boolean;
  public iblnUserName: boolean; 
  public iblnPassword: boolean; 
  public iblnSERVER_NAME: boolean; 
  public iblnCONNECTION_STRING: boolean; 
  public iblnIsAdmin: boolean;

  public users: Users;
  public cols: any[];
  public CurrentSelectedServer: string;
  constructor(private route: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private ngxLoader: NgxUiLoaderService, @Inject(DOCUMENT) private _document: Document) {

    this.http = http;
    this.baseUrl = baseUrl;
    this.iblnShowPanel = false;
    this.iblnUserPanel = false;
    this.users = new Users();
    this.CurrentSelectedServer = "";
  }

  ngOnInit()
  {
 

    this.ngxLoader.start();
    this.http.get<boolean>(this.baseUrl + 'api/DatabaseLogin/IsAdminUser', {
      params:
      {
        istrUserName: Cookie.get("istrUserName")
      }
    }).subscribe(result =>
    {
      if (result)
      {
        //load Dropdown values
        this.http.get<Users[]>(this.baseUrl + 'api/DatabaseLogin/GetUserDetails', {
          params:
          {
            istrUserName: Cookie.get("istrUserName")
          }
        }).subscribe(result1 =>
        {
          this.lstservers = result1;
          console.table(this.lstservers);
          this.ngxLoader.stop();
        });
      }
      else
      {
        window.location.href = "";
        this.route.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.route.navigate(["serverinformation"]);

        });
      }
    });
  }
  ngAfterViewInit(): void
  {
     
  }
  OnSaveClick(value:any)
  {

    if (this.users.userName === undefined || this.users.userName === "") {
      this.iblnUserName = true;
    }
    else {
      this.iblnUserName = false
    }
    if (this.users.password === "" || this.users.password ===undefined) {
      this.iblnPassword = true
    }
    else {
      this.iblnPassword = false
    }
    if (this.users.connectioN_STRING === "" || this.users.connectioN_STRING ===undefined) {
      this.iblnCONNECTION_STRING = true
    }
    else {
      this.iblnCONNECTION_STRING = false
    }
    if (this.users.serveR_NAME === "" || this.users.serveR_NAME ===undefined) {
      this.iblnSERVER_NAME = true
    }
    else {
      this.iblnSERVER_NAME = false
    }
    
    if ( this.iblnSERVER_NAME || this.iblnCONNECTION_STRING || this.iblnPassword || this.iblnUserName) {
      return;
    }
    else
    {
      var iblnIsAdminser = false;
      
      this.ngxLoader.start();
      this.http.get<boolean>(this.baseUrl + 'api/DatabaseLogin/CreateDbUser', {
        params:
        {
          istrUserName: Cookie.get("istrUserName"),
          istrNewUserName: this.users.userName,
          istrPassword: this.users.password,
          istrSERVER_NAME: this.users.serveR_NAME,
          istrCONNECTION_STRING: this.users.connectioN_STRING,
          iblnIsAdmin: this.users.IsAdmin
        }
      }).subscribe(result =>
      {
        this.iblnShowPanel = false;
        this.iblnUserPanel = true;

        if (result)
        { 
          this.http.get<Users[]>(this.baseUrl + 'api/DatabaseLogin/GetUserDetailsByServerName', {
            params:
            {
              istrUserName: Cookie.get("istrUserName"),
              istrServerName: this.CurrentSelectedServer
            }
          }).subscribe(result1 => {
            this.lstServerBaseUsers = result1;
            this.users = new Users();
            
          });
              
        }
        else {
        }
        this.ngxLoader.stop();
      });
     

    }
    
  }
  OnCancelClick()
  {
    this.users = new Users();
    this.iblnUserName = false;
    this.iblnPassword = false;
    this.iblnCONNECTION_STRING = false;
    this.iblnSERVER_NAME = false;
    this.iblnShowPanel = false;
  }
  ShowAddUserPlanel()
  {
    this.iblnShowPanel = true;
  }
  public filterServer(value: any): void
  {
    this.CurrentSelectedServer = value;
    this.iblnUserPanel = true;
    this.ngxLoader.start(); 
    this.http.get<Users[]>(this.baseUrl + 'api/DatabaseLogin/GetUserDetailsByServerName', {
      params:
      {
        istrUserName: Cookie.get("istrUserName"),
        istrServerName: value
      }
    }).subscribe(result1 => {
      this.lstServerBaseUsers = result1;
      this.lstServerBaseUsersClone = this.lstServerBaseUsers;
      this.ngxLoader.stop();
    });
   }
   onRowEditInit(user: Users)
   {
     //this.lstServerBaseUsersClone[user.Id] = { ...car };
   }
   onRowEditSave(user: Users)
   {

     this.ngxLoader.start();
     this.http.get<boolean>(this.baseUrl + 'api/DatabaseLogin/UpdateDbUser', {
       params:
       {
         istrUserName: Cookie.get("istrUserName"),
         id: user.id,
         istrDbUserName: user.userName,
         istrPassword: user.password,
         istrSERVER_NAME: user.serveR_NAME,
         istrCONNECTION_STRING: user.connectioN_STRING,
         iblnIsAdmin: user.IsAdmin
       }
     }).subscribe(result =>
     {

       this.ngxLoader.stop();
     });
   }
   onRowEditCancel(user: Users, index: number)
   {
     //this.lstServerBaseUsersClone[user.Id] = { ...car };
   }
   onRowEditDelete(user: Users, index: number)
   {
     this.ngxLoader.start();
     this.http.get<boolean>(this.baseUrl + 'api/DatabaseLogin/DeleteDbUser', {
       params:
       {
         istrUserName: Cookie.get("istrUserName"),
         id: user.id 
       }
     }).subscribe(result => {
       this.http.get<Users[]>(this.baseUrl + 'api/DatabaseLogin/GetUserDetailsByServerName', {
         params:
         {
           istrUserName: Cookie.get("istrUserName"),
           istrServerName: this.CurrentSelectedServer
         }
       }).subscribe(result1 => {
         this.lstServerBaseUsers = result1;
        

       });
       this.ngxLoader.stop();
     });
   }
}

