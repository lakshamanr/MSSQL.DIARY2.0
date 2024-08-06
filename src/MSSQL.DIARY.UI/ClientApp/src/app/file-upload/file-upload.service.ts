import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
 

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
 public baseUrl :string;  
 public http: HttpClient;
  public HttpOptions =
    {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl:string) 
  {
    this.baseUrl = baseUrl +"api/SSISPackageUploader";
    this.http=http;
  }
  uploadFile(theFile: FileToUpload) :
    Observable<any>
  {

    return this.http.post<FileToUpload>(this.baseUrl, theFile, this.HttpOptions);
 }


}
export class FileToUpload {
  fileName: string = "";
  fileSize: number = 0;
  fileType: string = "";
  lastModifiedTime: number = 0;
  lastModifiedDate: Date = null;
  fileAsBase64: string = "";
}
