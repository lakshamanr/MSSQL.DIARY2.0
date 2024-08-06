import { Component, Inject, OnInit, AfterViewInit, AfterContentInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MenuItem } from 'primeng/api/menuitem';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { ViewChild, ElementRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import svgPanZoom from 'svg-pan-zoom';
import { downloadFile } from 'file-saver';
import { ServerProprty, FileInfomration, SchemaReferanceInfo, Ms_Description } from 'src/models/ModelClass'

@Component({
  selector: 'app-database-info',
  templateUrl: './database-info.component.html',
  styleUrls: ['./database-info.component.css']
})
export class DatabaseInfoComponent implements OnInit, AfterViewInit {

  public GetDatabaseObjectTypes: string[];
  public GetdbPropertValues: ServerProprty[];
  public GetdbOptionValues: ServerProprty[];
  public GetdbFilesDetails: FileInfomration[];
  public schemReferance: SchemaReferanceInfo[];
  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public dbName1: string;
  public iblnShowObjects: boolean;
  public items: MenuItem[];
  public home: MenuItem;
  public serverName: string;
  svg: SafeHtml;
  svgPanZoom: SvgPanZoom.Instance = null;
  needsToInitSvgPanZoom = true;
  @ViewChild('dataContainer', { static: false }) dataContainer: ElementRef;
  bsModalRef: BsModalRef;
  constructor(private modalService: BsModalService, private sanitizer: DomSanitizer, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.iblnShowObjects = false;

  }
  openModalWithComponent() {
    const initialState = {
      list: [
        ''
      ],
      title: 'ER Diagram',
      istrServerName: this.serverName,
      istrdbName: this.dbName1,
      ModelschemReferance: this.schemReferance,
      istrFullPathName: this.dbName
    };
    this.bsModalRef = this.modalService.show(ModalContentComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
  }
  ngOnInit() {
    this.dbName = this.route.snapshot.params.dbName;
    this.dbName1 = this.dbName.split('/')[0];
    this.ngxLoader.start();
    this.http.get<string[]>(this.baseUrl + 'api/DatabaseInformation/GetDatabaseObjectTypes', { params: { istrdbName: this.dbName.split('/')[0] } }).subscribe(result => {
      this.GetDatabaseObjectTypes = result;
      this.iblnShowObjects = true;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());

    this.ngxLoader.start();
    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseInformation/GetdbPropertValues', { params: { istrdbName: this.dbName.split('/')[0] } }).subscribe(result => {
      this.GetdbPropertValues = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<ServerProprty[]>(this.baseUrl + 'api/DatabaseInformation/GetdbOptionValues', { params: { istrdbName: this.dbName.split('/')[0] } }).subscribe(result => {
      this.GetdbOptionValues = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
    this.ngxLoader.start();
    this.http.get<FileInfomration[]>(this.baseUrl + 'api/DatabaseInformation/GetdbFilesDetails', { params: { istrdbName: this.dbName.split('/')[0] } }).subscribe(result => {
      this.GetdbFilesDetails = result;
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());

    this.ngxLoader.start();
    this.http.get<SchemaReferanceInfo[]>(this.baseUrl + 'api/DatabaseSchema/GetListOfAllSchemaAndMsDescription',
      { params: { istrdbName: this.dbName.split('/')[0] } }).subscribe(result => {
        this.schemReferance = result;
        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());


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
            icon: "fa fa-folder"
          },
          {
            label: this.dbName.split('/')[0],
            icon: "fa fa-database fa-fw",
            routerLink: "/DatabaseInfo/" + this.dbName.split('/')[0]
          }
        ];
      this.ngxLoader.stop();
    }, error => this.ngxLoader.stop());
    this.home = { label: 'Project', icon: 'pi pi-home', routerLink: "/" };
  }
  LoadSVGBasedOnSchema(value: any): void {

    this.LoadAllSchemaDetails(value);
  }

  DownloadTheERDiagram(value: any): void {

    var selectedformat = value;
    let link = document.createElement("a");
    link.download = this.dbName + "ERDiagram";
    switch (selectedformat) {
      case "PNG":
        link.href = "/assets/www/" + this.dbName.split('/')[1] + "/" + this.dbName.split('/')[0] + "/" + this.dbName.split('/')[0] + ".png";

        break;
      case "JPEG":
        link.href = "/assets/www/" + this.dbName.split('/')[1] + "/" + this.dbName.split('/')[0] + "/" + this.dbName.split('/')[0] + ".jpg";

        break
      case "PDF":
        link.href = "/assets/www/" + this.dbName.split('/')[1] + "/" + this.dbName.split('/')[0] + "/" + this.dbName.split('/')[0] + ".pdf";

        break;
    }
    link.target = "_blank";
    link.click();

  }
  ngAfterViewInit(): void {

    //this.LoadAllSchemaDetails("All");


  }

  private LoadAllSchemaDetails(value: any) {
    this.dbName = this.route.snapshot.params.dbName;
    this.ngxLoader.start();
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/GetERDiagram', { params: { istrdbName: this.dbName, istrServerName: "", istrSchemaName: value } })
      .subscribe(result => {
        this.dataContainer.nativeElement.innerHTML = result.desciption;
        //if (this.needsToInitSvgPanZoom) {
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
        //  this.needsToInitSvgPanZoom = false;
        //}
        this.ngxLoader.stop();
      }, error => this.ngxLoader.stop());
  }

}
@Component({
  selector: 'modal-content',
  template: `<div class=" "> 
 <div class="modal-dialog">
    <div class="modal-content"> 
    <div class="modal-body grab" #div>
       <button  id='closebtn' type="button" class="close pull-right" aria-label="Close" (click)="bsModalRef.hide()">
        <span aria-hidden="true">&times;</span>
      </button>
     <div> 
      <label> Select Database Schema : </label>
      <select id="Loadschema" (change)="LoadSVGBasedOnSchema($event.target.value)" style="height: 100%;">
        <option value="All">Select All Schema</option>
        <option *ngFor="let SchemaName of ModelschemReferance" [value]="SchemaName.istrName">
          {{ SchemaName.istrName }}
        </option>
      </select> 
&emsp;
    <button class="btn"  (click)="downloadPDF()"><i class="fa fa-file-pdf-o "></i></button>
 
   

<div id="container" style="width: 1300px; height: 570px; border:1px solid black; ">
     <div #dataContainer></div>
    </div>
</div> 
</div>
</div>
</div>`,
  styles: [`
.modal-content
{
height: 100px !important;
}
.modal-content
{
    width: 1348px !important;
    height: 630px !important;;
    margin-left: -423px !important;
    margin-top: -38px !important;
}
`]
})

export class ModalContentComponent implements OnInit, AfterViewInit, AfterContentInit {

  title: string;
  closeBtnName: string;
  list: any[] = [];
  istrdbName: string;
  istrServerName: string;
  istrFullPathName: string;
  @ViewChild('dataContainer', { static: false }) dataContainer: ElementRef;
  public ModelschemReferance: SchemaReferanceInfo[];
  public http: HttpClient;
  public baseUrl: string;
  public dbName: string;
  public dbName1: string;

  svgPanZoom: SvgPanZoom.Instance = null;
  needsToInitSvgPanZoom = true;

  constructor(public bsModalRef: BsModalRef, private sanitizer: DomSanitizer, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/GetERDiagram', { params: { istrdbName: this.istrdbName, istrServerName: this.istrServerName, istrSchemaName: "All" } })
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

      }, error => { });
  }
  LoadSVGBasedOnSchema(value: any): void {
    this.http.get<Ms_Description>(this.baseUrl + 'api/DatabaseInformation/GetERDiagram', { params: { istrdbName: this.istrdbName, istrServerName: this.istrServerName, istrSchemaName: value } })
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

      }, error => { });
  }
  ngAfterContentInit(): void {
  }
  ngAfterViewInit(): void {
  }

  downloadPDF() {
    this.http.post<Blob>(this.baseUrl + 'api/DatabaseInformation/downloadPdf?istrdbName=' + this.istrdbName,
      { params: { istrdbName: this.istrdbName } }, { responseType: "Blob" as "json" }
    )
      .subscribe(
        blob => {
          var file = new Blob([blob], { type: 'application/pdf' });

          var fileURL = URL.createObjectURL(file);
          window.open(fileURL);
        },
        error => {
          console.log(error.error);

        });
  }
  downloadPNG() {
    this.http.post<Blob>(this.baseUrl + 'api/DatabaseInformation/downloadPdf?istrdbName=' + this.istrdbName,
      { params: { istrdbName: this.istrdbName } }, { responseType: "Blob" as "json" }
    ).subscribe(
      blob => {
        var file = new Blob([blob], { type: 'application/image' });

        var fileURL = URL.createObjectURL(file);
        window.open(fileURL);
      },
      error => {
        console.log(error.error);

      });
  }
}

