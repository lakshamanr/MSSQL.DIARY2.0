import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api';
import { MessageService } from 'primeng/api'; 

@Component({
    selector: 'app-database-business-module',
    templateUrl: './database-business-module.component.html',
    styleUrls: ['./database-business-module.component.css']
})
export class DatabaseBusinessModuleComponent implements OnInit {
    public items: MenuItem[];
    public home: MenuItem;
    http: HttpClient;
    baseUrl: string;
    dbName: any;
    serverName: any;
    constructor(private routeTemp: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService, private messageService: MessageService) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
        this.dbName = this.route.snapshot.params.dbName.split('/')[0];
        this.loadbreadcrumb();

    }

    private loadbreadcrumb() {
        this.http.get<string>(this.baseUrl + 'api/ServerInformation/GetServerInformation').subscribe(result => {
            this.serverName = result[0];
            this.items =
                [
                    {
                        label: this.serverName,
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
                        label: "Bussines Modules",
                        icon: "fa fa-table fa-fw"
                    }
                ];
            this.home =
            {
                label: 'Project',
                icon: 'pi pi-home',
                routerLink: "/"
            };
            this.ngxLoader.stop();
        }, error => this.ngxLoader.stop());
    }
}
