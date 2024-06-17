import {Component, Input} from '@angular/core';
import {EmployerTagDTO} from "../../../models/backend/dtos/employers/employer-tag.dto";
import {ResumeTagsDTO} from "../../../models/backend/dtos/profiles/resume-tags.dto";
import {ResumeSkillsDTO} from "../../../models/backend/dtos/profiles/resume-skills.dto";

@Component({
  selector: 'app-about-employer',
  templateUrl: './about-employer.component.html',
  styleUrls: ['./about-employer.component.scss']
})
export class AboutEmployerComponent {
  @Input() aboutEmployerData: EmployerTagDTO[]
  @Input() ProfileTagsData: ResumeSkillsDTO[]
  @Input() isProfile: boolean = false;

  ngOnInit(){
    console.log(this.ProfileTagsData)
  }
}
