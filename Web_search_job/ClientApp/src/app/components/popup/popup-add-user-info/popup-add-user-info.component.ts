import {Component, Input} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import { BlobServiceClient } from '@azure/storage-blob';
import {MatDialogRef} from "@angular/material/dialog";
import {PopupPurpleComponent} from "../popup-purple/popup-purple.component";
import {handleImageError} from "../../../functions/handleImageError";
import {v4 as uuidv4} from "uuid";
import {environment} from "../../../../environments/environment.prod";
import {AzureBlobStorageService} from "../../../services/azure-blob/azure-blob-storage.service";

@Component({
  selector: 'app-popup-add-user-info',
  templateUrl: './popup-add-user-info.component.html',
  styleUrls: ['./popup-add-user-info.component.scss']
})
export class PopupAddUserInfoComponent {
  @Input() Data: any;
  userForm: FormGroup;
  imageUrl: string | ArrayBuffer | null = null;
  blobServiceClient: BlobServiceClient;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private dialogRef: MatDialogRef<PopupPurpleComponent>,
    private blobService: AzureBlobStorageService,
  ) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: [''],
      otherGender: [''],
      day: [''],
      month: [''],
      year: [''],
      city: [''],
      phoneNumber: [''],
      userImg: [null]
    });

/*    const sasToken = 'YOUR_SAS_TOKEN';
    const blobServiceClient = new BlobServiceClient(`https://<your-storage-account-name>.blob.core.windows.net/?${sasToken}`);
    this.blobServiceClient = blobServiceClient;*/

  }

  ngOnInit(): void {
    if(this.Data.resume){
      // ADD if resume exist
    }
    else{
      console.log(this.Data)
    }
  }

  months = [
    { id: 1, name: 'Січень' },
    { id: 2, name: 'Лютий' },
    { id: 3, name: 'Березень' },
    { id: 4, name: 'Квітень' },
    { id: 5, name: 'Травень' },
    { id: 6, name: 'Червень' },
    { id: 7, name: 'Липень' },
    { id: 8, name: 'Серпень' },
    { id: 9, name: 'Вересень' },
    { id: 10, name: 'Жовтень' },
    { id: 11, name: 'Листопад' },
    { id: 12, name: 'Грудень' }
  ];


  onSubmitThis() {

  }

  async onSubmit(): Promise<void>{
    if(this.userForm.valid){

      var dateOfBirth: any;
      if(this.userForm.get('year')?.value && this.userForm.get('month')?.value && this.userForm.get('day')?.value){
        const year = this.userForm.get('year')?.value;
        const month = this.userForm.get('month')?.value;
        const day = this.userForm.get('day')?.value;

        const date = new Date(year, month - 1, day); // Місяць у Date починається з 0

        // Форматування дати у строку YYYY-MM-DD
        dateOfBirth = date.toISOString().split('T')[0];
      }


      const formData = {
        userId: this.Data.user.userId,
        firstName: this.userForm.get('firstName')?.value,
        lastName: this.userForm.get('lastName')?.value,
        gender: this.userForm.get('gender')?.value === 'Other' ? this.userForm.get('otherGender')?.value : this.userForm.get('gender')?.value,
        dateOfBirth: dateOfBirth ? dateOfBirth : '',
        locationId: this.userForm.get('city')?.value,
        phoneNumber: this.userForm.get('phoneNumber')?.value,
        userImg: this.userForm.get('userImg')?.value,
      };

      if (formData.userImg) {
        const uniqueFileName = `${uuidv4()}_${formData.userImg.name}`;
        const sasToken = environment.sas; // Замість того, щоб отримувати SAS-токен як параметр
        this.blobService.uploadImage(sasToken, formData.userImg, uniqueFileName, () => {
          const fileUrl = `${this.blobService.filesUrl}${uniqueFileName}`;
          formData.userImg = fileUrl;
          this.OnOK(formData);
        });
      } else {
        this.OnOK(formData);
      }
    }
  }


  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.userForm.patchValue({
        userImg: file
      });

      const reader = new FileReader();
      reader.onload = () => {
        this.imageUrl = reader.result;
      };

      console.log(this.imageUrl);
      reader.readAsDataURL(file);
    }
  }

  OnExit(){
    this.dialogRef.close(null);
  }

  OnOK(formData: any): void {
    console.log(formData)
    if(formData){
      this.dialogRef.close(formData);
    }
    else{
      this.dialogRef.close('error');
    }
  }
  protected readonly document = document;
  protected readonly handleImageError = handleImageError;
}
