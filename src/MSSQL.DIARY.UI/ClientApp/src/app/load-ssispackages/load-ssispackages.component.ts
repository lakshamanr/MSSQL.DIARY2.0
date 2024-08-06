
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { FileUploadService, FileToUpload } from '../file-upload/file-upload.service';
import { MenuItem } from 'primeng/api/menuitem';
 

@Component({
  selector: 'app-load-ssispackages',
  templateUrl: './load-ssispackages.component.html',
  styleUrls: ['./load-ssispackages.component.css']
})
export class LoadSSISPackagesComponent implements OnInit {

  public dbName: string;
  public baseUrl: string;
  public http: HttpClient;
  public theFiles: any = null;
  messages: string[] = [];
  public progress: number;
  public message: string;
  MAX_SIZE: number = 1048576;
  public items: MenuItem[];
  public home: MenuItem;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private uploadService: FileUploadService, private ngxLoader: NgxUiLoaderService) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  onFileChange(event)
  { 
    if (event.target.files &&  event.target.files.length > 0)
    {
      this.theFiles = event.target.files;
    } 
  }
  uploadFile(): void
  {
    this.ngxLoader.start();
    for (let index = 0; index < this.theFiles.length; index++)
    {
      this.readAndUploadFile(this.theFiles[index]);
    }
    this.ngxLoader.stop();
  }
  private readAndUploadFile(theFile: any) {
    let file = new FileToUpload();
         
    file.fileName = theFile.name;
    file.fileSize = theFile.size;
    file.fileType = theFile.type;
    file.lastModifiedTime = theFile.lastModified;
    file.lastModifiedDate = theFile.lastModifiedDate;
   
    let reader = new FileReader();  
    // Setup onload event for reader
    reader.onload = () => {
      // Store base64 encoded representation of file
      file.fileAsBase64 = reader.result.toString();
        
      // POST to server
      this.uploadService.uploadFile(file)
        .subscribe(() =>
        {
          this.messages.push(file.fileName);
        });
    }
         
    reader.readAsDataURL(theFile);
  }
  
  

  ngOnInit() {
  }
}
