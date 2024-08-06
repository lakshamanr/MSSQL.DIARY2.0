import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { LeftMenuTreeViewJson } from '../left-menu-tree-view-json' 
import { HttpClient } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent
{
    public http: HttpClient;
    public baseUrl: string;
    constructor(private route: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private ngxLoader: NgxUiLoaderService) {
        this.http = http;
        this.baseUrl = baseUrl;
    }
     
}
