import {Component, Input} from '@angular/core';
import {EmployerTagDTO} from "../../../models/backend/dtos/employers/employer-tag.dto";
import {JobTagsMarksDTO} from "../../../models/backend/dtos/jobs/job-tags-marks.dto";
import {JobTagsProsDTO} from "../../../models/backend/dtos/jobs/job-tags-pros.dto";

@Component({
  selector: 'app-chip-job-list',
  templateUrl: './chip-job-list.component.html',
  styleUrls: ['./chip-job-list.component.scss']
})
export class ChipJobListComponent {
  @Input() Tags: EmployerTagDTO[];
  @Input() TagsPros: JobTagsProsDTO[];
  @Input() IsBig: boolean;
  @Input() IsSearchPage: boolean = false;

  ngOnInit(){

  }
}
