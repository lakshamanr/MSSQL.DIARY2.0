import { Component, Inject, OnInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MessageService } from 'primeng/api'; 
import svgPanZoom from 'svg-pan-zoom';
import { Ms_Description } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-business-module-details',
  templateUrl: './database-business-module-details.component.html',
  styleUrls: ['./database-business-module-details.component.css']
})
export class DatabaseBusinessModuleDetailsComponent implements OnInit {

  http: HttpClient;
  baseUrl: string;
  dbName: any;
  serverName: any;
  databasemodules: string[];
  selecteddatamodule: string;
  @ViewChild('dataContainer2', { static: false }) dataContainer: ElementRef;
  svgPanZoom: SvgPanZoom.Instance = null;
  needsToInitSvgPanZoom = true;

  constructor(private routeTemp: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService,  private messageService: MessageService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }
  ngOnInit() {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.serverName = this.route.snapshot.params.dbName.split('/')[1];

    this.LoadMoules();
  }

  private LoadMoules() {
    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/DatabaseInformation/LoadAllModuless', { params: { istrdbName: this.dbName, istrServerName: this.serverName } }).subscribe(result => {
      this.databasemodules = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
  }
  refreshmodule() {
    this.LoadMoules();
  }
  removemodule() {
    this.ngxLoader.start();
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/deleteERDiagramWithSelectedTables',
      { params: { istrdbName: this.dbName, istrServerName: this.serverName, istrsqlmodule: this.selecteddatamodule } })
      .subscribe(result => {
        this.dataContainer.nativeElement.innerHTML = "";
        this.selecteddatamodule = "";
        this.LoadMoules();
        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());

    this.ngxLoader.stop();
  }
  filterDatabaseModule(selectedWorkflowName: any): any {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.selecteddatamodule = selectedWorkflowName;
    this.ngxLoader.start();
    if (selectedWorkflowName === "All") {
      this.ngxLoader.stop();
      this.selecteddatamodule = "";
      return;
    }

    this.ngxLoader.start();
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/LoadERDiagramWithSelectedTables',
      { params: { istrdbName: this.dbName, istrServerName: this.serverName, istrsqlmodule: selectedWorkflowName } })
      .subscribe(result => {
        this.dataContainer.nativeElement.innerHTML = result.desciption;
        document.getElementById('svgDatabaseDiagram').attributes['width'].nodeValue = "1310px";
        document.getElementById('svgDatabaseDiagram').attributes['height'].nodeValue = "500px";
        if (this.svgPanZoom) {
          this.svgPanZoom.destroy();
        }
        this.svgPanZoom = svgPanZoom('#svgDatabaseDiagram', {
          minZoom: 0.1,
          maxZoom: 10,
          zoomEnabled: true,
          controlIconsEnabled: true,
          fit: true,
          center: true
        });

        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());

    this.ngxLoader.stop();
  }
}
