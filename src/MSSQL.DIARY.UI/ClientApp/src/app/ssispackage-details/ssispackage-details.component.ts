import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { PackageJsonHandler } from 'src/models/ModelClass';

@Component({
  selector: 'app-ssispackage-details',
  templateUrl: './ssispackage-details.component.html',
  styleUrls: ['./ssispackage-details.component.css']
})
export class SSISPackageDetailsComponent implements OnInit {
  public http: HttpClient;
  public baseUrl: string;
  public pkgHandler: PackageJsonHandler;

  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.pkgHandler = new PackageJsonHandler();
  }


  ngOnInit() {
    this.ngxLoader.start();
    this.http.get<PackageJsonHandler>(this.baseUrl + 'api/SSISPackageInfoHandler/GetPackageInfoByName',
      {
        params:
        {
          istrPackageName: this.route.snapshot.params.PkgName
        }
      }).subscribe(result => {
        this.pkgHandler = result;

        this.ngxLoader.stop();
      }, () => this.ngxLoader.stop());
  }

}

