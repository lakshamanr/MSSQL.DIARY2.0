import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { TreeNode, MenuItem } from 'primeng/api';
import { MessageService } from 'primeng/api'; 
import { TableColumns, Ms_Description, TableIndexInfo, Tabledependencies, TableCreateScript, TableFKDependency, TableKeyConstraint, TableFragmentationDetails } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-table-info',
  templateUrl: './database-table-info.component.html',
  styleUrls: ['./database-table-info.component.css']
})
export class DatabaseTableInfoComponent implements OnInit {

  @ViewChild('expandingTree', { static: false })


  filesTree0: any;
  filesTree1: any;
  selectedFile0: TreeNode;
  selectedFile1: TreeNode;
  loading: boolean;
  public query: any;
  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public tblName: string;
  public language = 'plsql';
  public iblnShowEditBox: boolean;
  public TableColumnlst: TableColumns[];
  public Ms_Descrip: Ms_Description;
  public ms_description: string;
  public ms_description_old: string;
  public count: number;
  public tblIndexInfo: TableIndexInfo[]
  public TblCreateScript: TableCreateScript;
  public tbldependencies: Tabledependencies[];
  public TblFKDependency: TableFKDependency[];
  public TblKeyConstraint: TableKeyConstraint[];
  public TblFragmentationDtls: TableFragmentationDetails[];
  public showCreateScript: boolean;
  public FullFilePath: string;
  public items: MenuItem[];
  public home: MenuItem;
  public serverName: string;
  constructor(private routeTemp: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService, private messageService: MessageService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.tblName = "";
    this.iblnShowEditBox = false;
    this.ms_description = "";
    this.FullFilePath = "";
    this.showCreateScript = false;

  }

  ngOnInit() {

    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.tblName = this.route.snapshot.params.dbName.split('/')[1];
    this.loading = true;
    this.ngxLoader.start();
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

            label: "Tables",
            routerLink: "/Tables/" + this.dbName,
            icon: "fa fa-folder"
          },
          {

            label: this.tblName,
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
    }, () => this.ngxLoader.stop());

    this.http.get<any>(this.baseUrl + 'api/DatabaseTables/GetDependancyTree',
      {
        params:
        {
          istrdbName: this.dbName,
          istrtableName: this.tblName,
        }
      }
    )
      .toPromise()
      .then(res => {
        this.filesTree1 = res.data;
        this.loading = false;
      });

    this.ngxLoader.start();
    this.http.get<TableColumns[]>(this.baseUrl + 'api/DatabaseTables/GetAllTablesColumn',
      {
        params:
        {
          istrtableName: this.tblName,
          istrdbName: this.dbName
        }
      }).subscribe(result => {
        this.TableColumnlst = result;
        this.count = 0;
        for (let Property of this.TableColumnlst) {
          Property.id = this.count;
          Property.HideEdit = false;
          this.count++;

        }

        this.ngxLoader.stop();
      }, error => console.error(error));

    //this.ngxLoader.start();
    this.http.get<TableIndexInfo[]>(this.baseUrl + 'api/DatabaseTables/GetTableIndex', {
      params: {
        istrtableName: this.tblName,
        istrdbName: this.dbName
      }
    }).subscribe(result => {
      this.tblIndexInfo = result;

    }, error => console.error(error));
    //this.ngxLoader.start();
    this.http.get<TableCreateScript>(this.baseUrl + 'api/DatabaseTables/GetTableCreateScript', {
      params: {
        istrtableName: this.tblName,
        istrdbName: this.dbName
      }
    }).subscribe((result: any) => {
      this.TblCreateScript = result;
      this.showCreateScript = true;

    }, error => console.error(error));

    //this.ngxLoader.start();
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseTables/GetTableDescription', {
      params: {
        istrtableName: this.tblName,
        istrdbName: this.dbName
      }
    }).subscribe((result: any) => {
      this.Ms_Descrip = result;
      this.ms_description = this.Ms_Descrip.desciption;
      this.ms_description_old = this.ms_description;
      //this.ngxLoader.stop();
    }, error => console.error(error));

    //this.ngxLoader.start();
    this.http.get<Tabledependencies>(this.baseUrl + 'api/DatabaseTables/GetAllTabledependencies', {
      params: {
        istrtableName: this.tblName,
        istrdbName: this.dbName
      }
    }).subscribe((result: any) => {
      this.tbldependencies = result;
      for (let Property of this.tbldependencies) {
        Property.NevigationLink = this.dbName + "/" + Property.name;
      }
      //this.ngxLoader.stop();
    }, error => console.error(error));


    //this.ngxLoader.start();
    this.http.get<TableFKDependency>(this.baseUrl + 'api/DatabaseTables/GetAllTableForeignKeys', {
      params: {
        istrtableName: this.tblName,
        istrdbName: this.dbName
      }
    }).subscribe((result: any) => {
      this.TblFKDependency = result;
      for (let Property of this.TblFKDependency) {
        Property.NevigationLink = this.dbName + "/" + Property.fk_refe_table_name;
      }
      //this.ngxLoader.stop();
    }, error => console.error(error));


    //this.ngxLoader.start();
    this.http.get<TableKeyConstraint>(this.baseUrl + 'api/DatabaseTables/GetAlllKeyConstraints', {
      params: {
        istrtableName: this.tblName,
        istrdbName: this.dbName
      }
    }).subscribe((result: any) => {
      this.TblKeyConstraint = result;
    }, error => console.error(error));
    //TblFragmentationDtls
    this.http.get<TableFragmentationDetails>(this.baseUrl + 'api/DatabaseTables/TableFragmentationDetails', {
      params: {
        istrtableName: this.tblName,
        istrdbName: this.dbName
      }
    }).subscribe((result: any) => {
      this.TblFragmentationDtls = result;
    }, error => console.error(error));



  }

  ShowModelPOP(): any {
    this.iblnShowEditBox = true;

  }
  SaveTableMsDesciption(): any {
    this.iblnShowEditBox = false;
    this.http.get<boolean>(this.baseUrl + 'api/DatabaseTables/CreateOrUpdateTableDescription', {
      params:
      {
        istrdbName: this.dbName,
        astrDescription_Value: this.ms_description,
        astrTableName: this.tblName
      }
    }).subscribe(() => {

    }, error => console.error(error));

  }
  CancelTableMsDesciption(): any {
    this.ms_description = this.ms_description_old;
    this.iblnShowEditBox = false;

  }
  EditGridRow(InputRowId: any) {
    this.TableColumnlst[InputRowId].HideEdit = true;
  }
  SaveGridRow(InputRowId: any) {
    this.TableColumnlst[InputRowId].HideEdit = false;

    this.http.get<boolean>(this.baseUrl + 'api/DatabaseTables/CreateOrUpdateColumnDescription', {
      params:
      {
        istrdbName: this.dbName,
        astrDescription_Value: this.TableColumnlst[InputRowId].description,
        astrTableName: this.tblName,
        astrColumnName: this.TableColumnlst[InputRowId].columnname
      }
    }).subscribe(() => {

    }, error => console.error(error));
  }
  CancelGridRow(InputRowId: any) {
    this.TableColumnlst[InputRowId].HideEdit = false;
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
  }

  expandAll() {
    this.filesTree0.forEach(node => {
      this.expandRecursive(node, true);
    });
  }

  collapseAll() {
    this.filesTree0.forEach(node => {
      this.expandRecursive(node, false);
    });
  }
  samePageNevigation(tableName: any) {

    this.routeTemp.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.routeTemp.navigate(["/TableDetails", this.dbName + "/" + tableName]));

  }

  private expandRecursive(node: TreeNode, isExpand: boolean) {
    node.expanded = isExpand;
    if (node.children) {
      node.children.forEach(childNode => {
        this.expandRecursive(childNode, isExpand);
      });
    }
  }
}

