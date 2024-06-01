import {Component, Input} from '@angular/core';
import {EmployerTagDTO} from "../../../models/backend/dtos/employers/employer-tag.dto";

@Component({
  selector: 'app-about-employer',
  templateUrl: './about-employer.component.html',
  styleUrls: ['./about-employer.component.scss']
})
export class AboutEmployerComponent {
  @Input() aboutEmployerData: EmployerTagDTO[]
  @Input() isProfile: boolean = false;


}
