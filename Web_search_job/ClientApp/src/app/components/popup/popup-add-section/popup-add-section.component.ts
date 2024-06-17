import {Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {ResumeDTO} from "../../../models/backend/dtos/profiles/resume.dto";
import {ProfileService} from "../../../services/backend/profile.service";
import {ResumeSectionTypeDTO} from "../../../models/backend/dtos/profiles/resume-section-type.dto";
import {MatDialogRef} from "@angular/material/dialog";
import {PopupPurpleComponent} from "../popup-purple/popup-purple.component";

@Component({
  selector: 'app-popup-add-section',
  templateUrl: './popup-add-section.component.html',
  styleUrls: ['./popup-add-section.component.scss']
})
export class PopupAddSectionComponent {
  @Input() Data: any;
  sections: ResumeSectionTypeDTO[];
  @Output() isDataChanged = new EventEmitter<any>();

  constructor(
    private profileService: ProfileService
  ) {
  }
  ngOnInit(){
    this.profileService.getResumeSections().subscribe(sections => {
      this.sections = sections;
    });
    console.log(this.Data)
  }

  clickActionCB(event: any) {
    if(event.value){
      this.profileService.addActiveSection(this.Data.resume.id , event.event).subscribe(result => {
        console.log(result)
      });
    }
    else{
      this.profileService.removeActiveSection(this.Data.resume.id , event.event).subscribe(result => {
        console.log(result)
      });
    }

    this.isDataChanged.emit({value:true});
  }
}
