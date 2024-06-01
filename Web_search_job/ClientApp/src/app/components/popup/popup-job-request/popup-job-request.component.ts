import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";
import {PopupPurpleComponent} from "../popup-purple/popup-purple.component";

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

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<PopupPurpleComponent>
  ) {
    this.resumeForm = this.fb.group({
      resume: ['option1', Validators.required],
      coverLetter: ['', [Validators.required, Validators.maxLength(100)]],
      similarProjects: ['', [Validators.maxLength(100)]],
      positiveTraits: ['', [Validators.maxLength(100)]]
    });
  }

  onChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.resumeForm.patchValue({ resume: selectElement.value });
  }

  onSubmit() {
    if (this.resumeForm.valid) {
      console.log(this.resumeForm.value);
    }
  }

  OnExit(){
    this.dialogRef.close(null);
  }

  OnOK(): void {
    this.dialogRef.close(this.resumeForm);
  }

}
