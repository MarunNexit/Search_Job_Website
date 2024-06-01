import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-popup-purple',
  templateUrl: './popup-purple.component.html',
  styleUrls: ['./popup-purple.component.scss']
})
export class PopupPurpleComponent {
  constructor(
    public dialogRef: MatDialogRef<PopupPurpleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }


}
