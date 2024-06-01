import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-popup-white',
  templateUrl: './popup-white.component.html',
  styleUrls: ['./popup-white.component.scss']
})
export class PopupWhiteComponent {

  constructor(
    public dialogRef: MatDialogRef<PopupWhiteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }


}
