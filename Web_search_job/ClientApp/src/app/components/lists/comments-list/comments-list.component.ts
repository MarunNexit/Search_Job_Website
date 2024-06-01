import {ChangeDetectorRef, Component, Input} from '@angular/core';
import {EmployerDTO} from "../../../models/backend/dtos/employers/employer-full.dto";

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.scss']
})
export class CommentsListComponent {
  @Input() isRightSide: boolean = false;
  @Input() scrollTo: any = "";
  @Input() employer: EmployerDTO;
  @Input() isSmallScreen: boolean = false;

  currentNumberComments: number = 2;

  OnClick(){
    if(this.isRightSide){
      if (this.scrollTo && this.scrollTo != ""){
        this.scrollTo.scrollToElement();
      }
    }
    else{
      this.currentNumberComments += 4;
    }

  }
  constructor() {

  }

  ngOnInit(){

  }



}
