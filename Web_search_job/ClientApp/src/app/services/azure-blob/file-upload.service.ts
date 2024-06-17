import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  public azureBlobStorageUrl = 'https://websearchjob.blob.core.windows.net/files';
  private sasToken = 'sp=r&st=2024-06-06T08:29:21Z&se=2024-07-06T16:29:21Z&spr=https&sv=2022-11-02&sr=c&sig=7WYNNLHD2O4WI%2BU8bZ%2B%2Fp%2BAi4VM69BkyLxM2%2FDqD49U%3D';

  constructor(private http: HttpClient) {}

  uploadFileToBlob(file: File): Observable<any> {
    const blobUrl = `${this.azureBlobStorageUrl}/${file.name}?${this.sasToken}`;
    const headers = new HttpHeaders({
      'x-ms-blob-type': 'BlockBlob'
    });

    return this.http.put(blobUrl, file, { headers, observe: 'response' });
  }
}
