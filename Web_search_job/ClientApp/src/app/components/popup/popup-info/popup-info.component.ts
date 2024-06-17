import {Component, Input} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";
import {PopupPurpleComponent} from "../popup-purple/popup-purple.component";
import {RouterHelperService} from "../../../services/router-helper.service";
import {PopupWhiteComponent} from "../popup-white/popup-white.component";

@Component({
  selector: 'app-popup-info',
  templateUrl: './popup-info.component.html',
  styleUrls: ['./popup-info.component.scss']
})
export class PopupInfoComponent {
  @Input() Data: any;
  @Input() Type: any;

  IsAppropriateness: boolean = false;
  selectorValue: string = "";
  resumeForm: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<PopupWhiteComponent>,
    private routerHelperService:RouterHelperService,
  ) {

  }

  /*onSubmit() {
    if (this.resumeForm.valid) {
      console.log(this.resumeForm.value);
    }
  }*/

  OnExit(){
    this.dialogRef.close();
  }

  OnOK(): void {
    this.dialogRef.close(null);
    this.routerHelperService.goToUrl('/auth-login', true);
  }

  OnCancel(){
    this.dialogRef.close('Cancel');
  }

  OnTryAgain(){
    this.dialogRef.close('TryAgain');
  }
  OnSuccessSendRequest(){
    this.dialogRef.close(null);
    this.routerHelperService.goToUrl('/chat', true);
  }

  OnCancelRequest(){
    this.dialogRef.close('CancelDialog');
  }

  OnExitProblem(){
    this.dialogRef.close('problemResumeList');
  }
}
