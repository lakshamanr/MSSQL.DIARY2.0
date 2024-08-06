import { Component, Inject, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { DOCUMENT } from '@angular/common';
declare var QP
import { MenuItem, TreeNode } from 'primeng/api';
import { MessageService } from 'primeng/api'; 

import { ServerProprty, SP_CreateScript, Ms_Description, SP_Parameters, SP_Depencancy, ExecutionPlanInfo } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-store-procedure-details',
  templateUrl: './database-store-procedure-details.component.html',
  styleUrls: ['./database-store-procedure-details.component.css']
})
export class DatabaseStoreProcedureDetailsComponent implements OnInit, AfterViewInit {

  @ViewChild('expandingTree', { static: false })
  filesTree0: any;
  filesTree1: any;
  selectedFile0: TreeNode;
  selectedFile1: TreeNode;
  loading: boolean;
  public count: number;
  public all_database_SP: ServerProprty[];
  public sp_create_script: SP_CreateScript;
  public sp_ms_description: Ms_Description;
  public ms_description: string;
  public ms_description_old: string;
  public sp_parameters: SP_Parameters[];
  public sp_Depencancy: SP_Depencancy[];
  public executionplan: ExecutionPlanInfo[];
  public dbName: string;
  public baseUrl: string;
  public SP_Name: string;
  public language = 'plsql';
  public iblnShow: boolean;
  public iblnShow_sp_name: boolean;
  public iblnShow_sp_description: boolean;
  public iblncreatescript: boolean;
  public iblnShowEditBox: boolean;
  public items: MenuItem[];
  public home: MenuItem;

  public http: HttpClient;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService, private messageService: MessageService) {

    this.http = http;
    this.baseUrl = baseUrl;
    this.iblnShow = false;
    this.iblnShow_sp_name = false;
    this.iblnShowEditBox = false;
    this.iblnShow_sp_description = false;
    this.iblncreatescript = false;
    this.ms_description = "";
    this.SP_Name = "";

  }

  ngOnInit() {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.SP_Name = this.route.snapshot.params.dbName.split('/')[1]
    this.iblnShow_sp_name = true;
    this.ngxLoader.start();

    this.loading = true;
    this.http.get<any>(this.baseUrl + 'api/DatabaseSPInfomration/GetDependancyTree',
      {
        params:
        {
          istrdbName: this.dbName,
          StoreprocName: this.SP_Name,
        }
      }
    )
      .toPromise()
      .then(res => {
        this.filesTree1 = res.data;
        this.loading = false;
      });


    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseSPInfomration/GetAllStoreprocedureDescription', { params: { istrdbName: this.dbName } }).subscribe(result => {
      this.all_database_SP = result;
      //this.ngxLoader.stop();
    }, () => this.ngxLoader.stop());


    // this.ngxLoader.start();
    this.http.get<SP_CreateScript>(this.baseUrl + 'api/DatabaseSPInfomration/GetCreateScriptOfStoreProc', { params: { istrdbName: this.dbName, StoreprocName: this.SP_Name } }).subscribe(result => {
      this.sp_create_script = result;
      //this.ngxLoader.stop();
      this.iblncreatescript = true;
    }, () => this.ngxLoader.stop());


    // this.ngxLoader.start();
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseSPInfomration/GetStoreProcMsDescription', { params: { istrdbName: this.dbName, StoreprocName: this.SP_Name } }).subscribe(result => {
      this.sp_ms_description = result;
      this.ms_description = this.sp_ms_description.desciption;
      this.ms_description_old = this.ms_description;
      this.iblnShow_sp_description = true;
      //this.ngxLoader.stop();
    }, error => console.log(error));


    // this.ngxLoader.start();
    this.http.get<SP_Parameters[]>(this.baseUrl + 'api/DatabaseSPInfomration/GetStoreProcParameters', { params: { istrdbName: this.dbName, StoreprocName: this.SP_Name } }).subscribe(result => {
      this.sp_parameters = result;
      this.count = 0;
      for (let property of this.sp_parameters) {
        property.id = this.count;
        property.HideEdit = false;
        this.count++;

      }
      this.ngxLoader.stop();
    }, error => console.log(error));


    // this.ngxLoader.start();
    this.http.get<SP_Depencancy[]>(this.baseUrl + 'api/DatabaseSPInfomration/GetStoreProcDependancy', { params: { istrdbName: this.dbName, StoreprocName: this.SP_Name } }).subscribe(result => {
      this.sp_Depencancy = result;
      //this.ngxLoader.stop();
    }, error => console.log(error));


    this.http.get<string>(this.baseUrl + 'api/ServerInformation/GetServerInformation').subscribe(result => {

      this.items =
        [

          {
            label: result[0],
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
            label: "Programmability",
            icon: "fa fa-folder"
          },
          {
            label: "StoredProcedures",
            icon: "fa fa-folder",
            routerLink: "/StoreProcedures/" + this.dbName
          },
          {
            label: this.SP_Name,
            //  icon: "fa fa-table fa-fw" 
          }

        ];
      this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };

    }, () => this.ngxLoader.stop());


  }
  ngAfterViewInit(): void {
    this.http.get<ExecutionPlanInfo[]>(this.baseUrl + 'api/DatabaseSPInfomration/GetCachedExecutionPlan', { params: { istrdbName: this.dbName, StoreprocName: this.SP_Name } }).subscribe(result => {
      this.executionplan = result;
      if (this.executionplan != undefined) {
        this.iblnShow = true;
        if (this.executionplan.length > 0) {
          var renderstring = this.executionplan[0].queryPlanXML
          this.iblnShow = true;
          QP.showPlan(document.getElementById("container"), `${renderstring}`, { jsTooltips: false });
        }

      }
    }, error => console.error(error));

  }
  nodeSelect(event) {
    this.messageService.add({ severity: 'info', summary: 'Node Selected', detail: event.node.label });
  }

  nodeUnselect(event) {
    this.messageService.add({ severity: 'info', summary: 'Node Unselected', detail: event.node.label });
  }

  nodeExpandMessage(event) {
    this.messageService.add({ severity: 'info', summary: 'Node Expanded', detail: event.node.label });
  }

  nodeExpand(event) {
    if (event.node) {
      ////in a real application, make a call to a remote url to load children of the current node and add the new nodes as children
      //this.nodeService.getLazyFiles().then(nodes => event.node.children = nodes);
    }
  }

  viewFile(file: TreeNode) {
    this.messageService.add({ severity: 'info', summary: 'Node Selected with Right Click', detail: file.label });
  }

  unselectFile() {
    // this.selectedFile2 = null;
  }

  expandAll() {
    //this.filesTree10.forEach(node => {
    //    this.expandRecursive(node, true);
    //});
  }

  collapseAll() {
    //this.filesTree10.forEach(node => {
    //    this.expandRecursive(node, false);
    //});
  }
  ShowEdit(): any {
    this.iblnShowEditBox = true;

  }
  SaveStoreProcMsDesciption(): any {
    this.iblnShowEditBox = false;
    this.http.get<boolean>(this.baseUrl + 'api/DatabaseSPInfomration/CreateOrUpdateStoreProcDescription', {
      params:
      {
        istrdbName: this.dbName,
        astrDescription_Value: this.ms_description,
        astrSP_Name: this.SP_Name
      }
    }).subscribe(() => {

    }, error => console.error(error));

  }
  CancelStoreProcMsDesciption(): any {
    this.ms_description = this.ms_description_old;
    this.iblnShowEditBox = false;

  }
  EditGridRow(InputRowId: any) {
    this.sp_parameters[InputRowId].HideEdit = true;
  }
  SaveGridRow(InputRowId: any) {
    this.sp_parameters[InputRowId].HideEdit = false;

    this.http.get<boolean>(this.baseUrl + 'api/DatabaseSPInfomration/CreateOrUpdateStoreProcParameterDescription', {
      params:
      {
        istrdbName: this.dbName,
        astrDescription_Value: this.sp_parameters[InputRowId].extended_property,
        astrSP_Name: this.SP_Name,
        astrSP_Parameter_Name: this.sp_parameters[InputRowId].parameter_name
      }
    }).subscribe(() => {

    }, error => console.error(error));
  }
  CancelGridRow(InputRowId: any) {
    this.sp_parameters[InputRowId].HideEdit = false;
  }


}
