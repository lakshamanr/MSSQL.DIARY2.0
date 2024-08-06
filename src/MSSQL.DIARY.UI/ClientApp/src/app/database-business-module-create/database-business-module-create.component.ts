import { Component, Inject, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api';
import { MessageService } from 'primeng/api';

import svgPanZoom from 'svg-pan-zoom';
import { dropdownlists, TablePropertyInfo, Ms_Description } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-business-module-create',
  templateUrl: './database-business-module-create.component.html',
  styleUrls: ['./database-business-module-create.component.css']
})
export class DatabaseBusinessModuleCreateComponent implements OnInit, AfterViewInit {

  public home: MenuItem;
  http: HttpClient;
  baseUrl: string;
  dbName: any;
  serverName: any;
  dropdownList: dropdownlists[];
  selectedItems: TablePropertyInfo[];
  public all_database_table: TablePropertyInfo[];
  selectedTables: Array<string> = [''];
  svgPanZoom: SvgPanZoom.Instance = null;
  needsToInitSvgPanZoom = true;
  public sqlmoddule: string;
  @ViewChild('dataContainer', { static: false }) dataContainer: ElementRef;
  dropdownSettings = {};
  constructor(private routeTemp: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService,private messageService: MessageService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.serverName = this.route.snapshot.params.dbName.split('/')[1];
    this.http.get<TablePropertyInfo[]>(this.baseUrl + 'api/DatabaseTables/GetAllDatabaseTables', {
      params: {
        istrdbName: this.dbName,
        iblnSearchInSSISPackages: "false"
      }
    }).subscribe(result => {
      this.all_database_table = result;
      var counter = 0;
      for (let property of this.all_database_table) {
        property.id = (counter++) + "";
        property.itemName = property.istrFullName;
      }

      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());




    this.dropdownSettings = {
      singleSelection: false,
      text: "Select Table",
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableSearchFilter: true,
      classes: "myclass custom-class"
    };
  }
  onItemSelect(item: TablePropertyInfo) {
    console.log(item);

    var argstr = "";
    this.selectedTables.push(item.istrFullName);
    for (var table of this.selectedTables) {
      argstr += table + ";"
    }
    this.ExecuteGetMethods(argstr);
  }
  OnItemDeSelect(item: TablePropertyInfo) {
    console.log(item);
    var index = this.selectedTables.findIndex(x => x == item.istrFullName);
    if (index > -1) {
      this.selectedTables.splice(index, 1);
    }
    var argstr = "";
    for (var table of this.selectedTables) {
      argstr += table + ";"
    }
    this.ExecuteGetMethods(argstr);
  }
  SaveSqlModuleChanges() {
    if (this.sqlmoddule != undefined && this.sqlmoddule != "") {
      var argstr = "";
      for (var table of this.selectedTables) {
        argstr += table + ";"
      }
      this.ngxLoader.start();
      this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/SaveERDiagramWithSelectedTables',
        {
          params: {
            istrdbName: this.dbName,
            istrServerName: this.serverName,
            SelectedTables: argstr,
            istrsqlmodule: this.sqlmoddule
          }
        })
        .subscribe(result => {
          alert(result.desciption)
          this.ngxLoader.stop();
        }, error => this.ngxLoader.stop());

    }
    else {
      alert("Please give meaningful name to module");
    }

  }
  private ExecuteGetMethods(argstr: string) {
    this.ngxLoader.start();
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/GetERDiagramWithSelectedTables', { params: { istrdbName: this.dbName, istrServerName: this.serverName, istrSchemaName: "All", SelectedTables: argstr } })
      .subscribe(result => {
        this.dataContainer.nativeElement.innerHTML = result.desciption;
        document.getElementById('svgDatabaseDiagram').attributes['width'].nodeValue = "1310px";
        document.getElementById('svgDatabaseDiagram').attributes['height'].nodeValue = "560px";
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
  }

  onSelectAll(items: TablePropertyInfo[]) {
    this.ngxLoader.start();
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/GetERDiagram', { params: { istrdbName: this.dbName, istrServerName: this.serverName, istrSchemaName: "All" } })
      .subscribe(result => {
        this.dataContainer.nativeElement.innerHTML = result.desciption;
        document.getElementById('svgDatabaseDiagram').attributes['width'].nodeValue = "1310px";
        document.getElementById('svgDatabaseDiagram').attributes['height'].nodeValue = "560px";
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

  }
  onDeSelectAll(items: TablePropertyInfo[]) {
    console.log(items);

    this.dataContainer.nativeElement.innerHTML = "";
  }
  ngAfterViewInit(): void {

    //this.ngxLoader.start();
    //this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/GetERDiagram', { params: { istrdbName: this.dbName, istrServerName: this.serverName, istrSchemaName: "All" } })
    //  .subscribe(result => {
    //    this.dataContainer.nativeElement.innerHTML = result.desciption;
    //    document.getElementById('svgDatabaseDiagram').attributes['width'].nodeValue = "1310px";
    //    document.getElementById('svgDatabaseDiagram').attributes['height'].nodeValue = "560px";
    //    if (this.svgPanZoom) {
    //      this.svgPanZoom.destroy();
    //    }
    //    this.svgPanZoom = svgPanZoom('#svgDatabaseDiagram', {
    //      minZoom: 0.1,
    //      maxZoom: 10,
    //      zoomEnabled: true,
    //      controlIconsEnabled: true,
    //      fit: true,
    //      center: true
    //    });

    //    this.ngxLoader.stop();
    //  }, error => this.ngxLoader.stop());

  }
}
