import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { TreeNode } from 'primeng/api/treenode';
import { MenuItem } from 'primeng/api/menuitem';

@Component({
  selector: 'app-businesswork-flow',
  templateUrl: './businesswork-flow.component.html',
  styleUrls: ['./businesswork-flow.component.css']
})
export class BusinessworkFlowComponent implements OnInit {

  public query: any;
  public dbName: string;
  public baseUrl: string;
  public http: HttpClient;
  public cols: any[];
  filesTree0: any;
  filesTree1: any;
  selectedFile0: TreeNode;
  selectedFile1: TreeNode;
  showTreeView: boolean;
  workflowList: string[];
  selectedWorkflow: string;
  public items: MenuItem[];
  public home: MenuItem;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.query = "";
    this.showTreeView = false;
    this.selectedWorkflow = "";
  }

  ngOnInit() {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.ngxLoader.start();
    this.http.get<any>(this.baseUrl + 'api/BusinessWorkFlow/GetWorkList',
      { params: { istrdbName: this.dbName } }).subscribe(result => {
        this.workflowList = result;

        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());
  }

  filterWorkFlow(selectedWorkflowName: any): any {
    this.dbName = this.route.snapshot.params.dbName.split('/')[0];
    this.selectedWorkflow = selectedWorkflowName;
    this.ngxLoader.start();
    if (selectedWorkflowName === "All") {
      this.ngxLoader.stop();
      this.selectedWorkflow = "";
      this.showTreeView = false;
      return;

    }
    this.http.get<any>(this.baseUrl + 'api/BusinessWorkFlow/GetWorkDetailsbyName',
      {
        params:
        {
          istrdbName: this.dbName,
          istrWorkFlowName: selectedWorkflowName
        }
      }
    )
      .toPromise()
      .then(res => {
        this.showTreeView = true;
        this.filesTree1 = res.data;
        this.ngxLoader.stop();
      });

  }

}
