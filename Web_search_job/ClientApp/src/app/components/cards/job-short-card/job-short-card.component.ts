import {Component, Input} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";

@Component({
  selector: 'app-job-short-card',
  templateUrl: './job-short-card.component.html',
  styleUrls: ['./job-short-card.component.scss'],
})
export class JobShortCardComponent {
  @Input() dataJob: any;

  //TEMPORARY CHANGE
  IsNewJob: boolean = true;
  IsHotJob: boolean = true;


  protected readonly handleImageError = handleImageError;
}
