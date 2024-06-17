import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";
import {PopupPurpleComponent} from "../popup-purple/popup-purple.component";
import {JobService} from "../../../services/backend/job.service";
import {ProfileService} from "../../../services/backend/profile.service";
import {JobRequestFieldsDTO} from "../../../models/backend/dtos/jobs/job-request-fields.dto";
import {ResumeShortDTO} from "../../../models/backend/dtos/profiles/resume-short.dto";
import {FileUploadService} from "../../../services/azure-blob/file-upload.service";
import {AzureBlobStorageService} from "../../../services/azure-blob/azure-blob-storage.service";
import {environment} from "../../../../environments/environment.prod";
import { v4 as uuidv4 } from 'uuid'; // Використовуйте uuid для генерації унікальних ідентифікаторів

@Component({
  selector: 'app-popup-job-request',
  templateUrl: './popup-job-request.component.html',
  styleUrls: ['./popup-job-request.component.scss']
})
export class PopupJobRequestComponent {
  @Input() Data: any;
  @Input() Type: any;
  IsAppropriateness: boolean = false;
  selectorValue: string = "";
  resumeForm: FormGroup;
  jobFields: JobRequestFieldsDTO;
  resumeList: ResumeShortDTO[]
  fileName = '';
  sas: string = '';


  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<PopupPurpleComponent>,
    private jobService: JobService,
    private profileService: ProfileService,
    private blobService: AzureBlobStorageService,
  ) {
    this.resumeForm = this.fb.group({
      resume: ['option1', Validators.required],
      coverLetter: ['', [Validators.required, Validators.maxLength(100)]],
      similarProjects: ['', [Validators.maxLength(100)]],
      positiveTraits: ['', [Validators.maxLength(100)]],
      file: [null]
    });
  }

  ngOnInit(){
    console.log(this.Data)

    if(this.Data.job.id){
      this.jobService.getJobRequestFields(this.Data.job.id).subscribe(fields => {
        this.jobFields = fields;
        console.log(this.jobFields)

        this.profileService.getResumeListNameByUserId(this.Data.user.userId).subscribe(resumeList => {
          this.resumeList = resumeList;
          console.log(this.resumeList)
        })
      })
    }

  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.fileName = file.name;
      this.resumeForm.patchValue({ file: file });
    }
  }


  onChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.resumeForm.patchValue({ resume: selectElement.value });

  }

  onSubmitThis() {
    if (this.resumeForm.valid) {
      const formData = this.resumeForm.value;
      if (formData.file) {
        const uniqueFileName = `${uuidv4()}_${formData.file.name}`;
        const sasToken = environment.sas; // Замість того, щоб отримувати SAS-токен як параметр
        this.blobService.uploadImage(sasToken, formData.file, uniqueFileName, () => {
          const fileUrl = `${this.blobService.filesUrl}${uniqueFileName}`;
          formData.fileUrl = fileUrl;
          this.OnOK(formData);
        });
      } else {
        this.OnOK(formData);
      }
    }
  }

  OnExit(){
    this.dialogRef.close(null);
  }

  OnOK(formData: any): void {
    console.log('AAAAAAA')
    console.log(formData)
    if(formData){
      this.dialogRef.close(formData);
    }
    else{
      this.dialogRef.close('error');
    }
  }


  saveJobRequest(formData: any) {
    // Logic to save job request, e.g., calling a service method
    console.log('Job request saved', formData);
  }

}
