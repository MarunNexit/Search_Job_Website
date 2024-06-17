import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-popup-purple',
  templateUrl: './popup-purple.component.html',
  styleUrls: ['./popup-purple.component.scss']
})
export class PopupPurpleComponent {

  isChanged: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<PopupPurpleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
  }

  ngOnInit() {
    console.log(this.data)
  }

  onNoClick(): void {
    if(this.isChanged){
      this.dialogRef.close("changed");
    }
    else{
      this.dialogRef.close(null);
    }
  }


  isDataChanged(event: any) {
    this.isChanged = !!event.value;
  }

  protected readonly event = event;
}
