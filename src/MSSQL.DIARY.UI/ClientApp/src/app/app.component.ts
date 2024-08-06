import { Component, ViewChild, ElementRef, ChangeDetectionStrategy, AfterViewInit, OnDestroy, OnInit } from '@angular/core';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SplitComponent } from 'angular-split'; 
import { NgxUiLoaderService } from 'ngx-ui-loader';  
import { Inject } from '@angular/core';
import { Router } from '@angular/router';  
import { HttpClient } from '@angular/common/http'; 
import { MenuItem } from 'primeng/api/menuitem'; 
import { Cookie } from 'ng2-cookies/ng2-cookies';
@Component({
  selector: 'app-root',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    'class': 'split-example-page'
  },
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnDestroy, OnInit {
    iblnShowPages: boolean = false;
    isDisabled: boolean = true;
    useTransition: boolean = true;
    dblClickTime: number = 0;
    logMessages: Array<{ type: string, text: string }> = [];
    status: boolean = false; 
    @ViewChild('area1', { static: false }) mySplitEl1: SplitComponent
    @ViewChild('area2', { static: false }) mySplitEl2: SplitComponent
    public http: HttpClient;
    public baseUrl: string;
    public items: MenuItem[];
  public home: MenuItem;
  public istrlogiggedUserName: string;
  iblnHiddeLeftMenu: boolean;
  constructor(private route: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private ngxLoader: NgxUiLoaderService)
  {
    this.iblnHiddeLeftMenu = false;
    this.http = http; 
    this.baseUrl = baseUrl;
    this.istrlogiggedUserName=""
    
    }
    ngAfterViewInit()
    {

      var userName = Cookie.get('istrUserName');
      this.istrlogiggedUserName = userName;
      this.ngxLoader.start();
      this.http.get<any>(this.baseUrl + 'api/DatabaseLogin/IsAlreadyLoggedIn', { params: { istrUserName: userName } }).subscribe(result => {
        this.iblnShowPages = !result
        this.iblnHiddeLeftMenu = true;
        this.http.get<boolean>(this.baseUrl + 'api/DatabaseLogin/IsAdminUser', {
          params:
          {
            istrUserName: Cookie.get('istrUserName')
          }
        }).subscribe(result1 => {
          if (result1)
          {
            this.iblnHiddeLeftMenu = false;
            this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
              this.route.navigate(["/AdminLogin"]));
          }
          else { 
            
          }
        });

        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());

      /*
       *  this.route.navigateByUrl('/', { skipLocationChange: true }).then(() =>
            this.route.navigate(["/AdminLogin"]));
       */

    }
    log(type: string, e: { gutterNum: number, sizes: Array<number> }) {
        this.logMessages.push({ type, text: `${new Date()} > ${type} event > ${JSON.stringify(e)}` }); 
        switch (type)
        {
            case "gutterDblClick":
                {
                    this.status = !this.status;  
                }
                break;
            case "gutterClick":
                {
                    this.status = !this.status;
                }
                break;
            case "dragEnd":
                break;
            case "dragStart":
                break;
        } 
    }
    
     
    ngOnDestroy() {
         
    }
  ngOnInit()
  {
    this.home = { icon: 'pi pi-home', routerLink:"/" };
    this.items =
      [
      //{ label: 'Categories' },
      //{ label: 'Sports' },
      //{ label: 'Football' },
      //{ label: 'Countries' },
      //{ label: 'Spain' },
      //{ label: 'F.C. Barcelona' },
      //{ label: 'Squad' },
      //{ label: 'Lionel Messi', url: 'https://en.wikipedia.org/wiki/Lionel_Messi' }
     ];
   }
     
}
