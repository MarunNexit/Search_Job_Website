import {Component, Input} from '@angular/core';
import {expandCollapse} from "../filter-search-card/animation_filter_card";
import {JobDTO} from "../../../models/backend/dtos/jobs/job.dto";

@Component({
  selector: 'app-appropriateness-job',
  templateUrl: './appropriateness-job.component.html',
  styleUrls: ['./appropriateness-job.component.scss'],
  animations: [expandCollapse]
})
export class AppropriatenessJobComponent {

  @Input() job: JobDTO;

  isAppropriateness: boolean[] = [false, true, true, true];
  isAppropriatenessAll: boolean = false;
  expandField: boolean = true;

  ngOnInit(): void {
    this.checkAllAppropriateness();
  }

  checkAllAppropriateness() {
    this.isAppropriatenessAll = this.isAppropriateness.every(value => value);
  }
}
