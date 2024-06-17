import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ActiveResumeSectionDTO} from "../../../models/backend/dtos/profiles/active-resume-section.dto";
import {ResumeSectionTypeDTO} from "../../../models/backend/dtos/profiles/resume-section-type.dto";

@Component({
  selector: 'app-popup-add-section-card',
  templateUrl: './popup-add-section-card.component.html',
  styleUrls: ['./popup-add-section-card.component.scss']
})
export class PopupAddSectionCardComponent {
  @Input() section: ResumeSectionTypeDTO;
  @Input() activeSections: ActiveResumeSectionDTO[];
  @Output() clickActionCB = new EventEmitter<any>();

  isSectionAdded: boolean = false;

  ngOnInit(){
    this.activeSections.forEach(item => {
      if (item.resumeSectionType?.id === this.section.id) {
        this.isSectionAdded = true;
      }
    });
  }
  clickAction(id: number | null) {
    if(id){
      this.isSectionAdded = !this.isSectionAdded;
      this.clickActionCB.emit({ event:id, value: this.isSectionAdded });
    }
  }

}
