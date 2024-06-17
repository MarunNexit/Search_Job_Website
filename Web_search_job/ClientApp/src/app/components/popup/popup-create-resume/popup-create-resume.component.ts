import {Component, Input} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ProfileService} from "../../../services/backend/profile.service";
import {v4 as uuidv4} from "uuid";
import {environment} from "../../../../environments/environment.prod";
import {AzureBlobStorageService} from "../../../services/azure-blob/azure-blob-storage.service";
import {MatDialogRef} from "@angular/material/dialog";
import {PopupPurpleComponent} from "../popup-purple/popup-purple.component";

@Component({
  selector: 'app-popup-create-resume',
  templateUrl: './popup-create-resume.component.html',
  styleUrls: ['./popup-create-resume.component.scss']
})
export class PopupCreateResumeComponent {
  @Input() Data: any;
  @Input() Type: any;

  uploadJob: boolean = false;
  fileUrl: any;
  fileName: string;
  resumeName: string = '';

  constructor(
    private dialogRef: MatDialogRef<PopupPurpleComponent>,
    private http: HttpClient,
    private profileService: ProfileService,
    private blobService: AzureBlobStorageService,
    ) {}

  onUploadFileButton(){
    this.uploadJob = !this.uploadJob;
  }

  onCreateOnMyOwn(){
    this.dialogRef.close({status: 'onMyOwn', name: this.resumeName});
  }

  selectedFile: File | null = null;

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    if(this.selectedFile && this.selectedFile.name){
      this.fileName = this.selectedFile.name;
    }
  }

  onUpload() {
    console.log("TRY")
    if (this.selectedFile) {
      const uniqueFileName = `${uuidv4()}_${this.selectedFile.name}`;
      const sasToken = environment.sas; // Замість того, щоб отримувати SAS-токен як параметр
      this.blobService.uploadImage(sasToken, this.selectedFile, uniqueFileName, () => {
        const fileUrl = `${this.blobService.filesUrl}${uniqueFileName}`;
        this.fileUrl = fileUrl;
        this.OnOK();
      });
    }


  }

  OnOK(){
    if (this.fileUrl) {
      this.profileService.uploadResumeFile(this.fileUrl)
        .then(result => {
          console.log('Parsed resume:', result);
          this.dialogRef.close({status: 'withData', name: this.resumeName, result: result} );
        })
        .catch(error => {
          console.error('Error parsing resume:', error);
        });
    }
    else{
      this.onCreateOnMyOwn();
    }
  }

}
