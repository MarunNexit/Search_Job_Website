import {Component, Input, OnInit} from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";

@Component({
  selector: 'app-chips-job-priority',
  templateUrl: './chips-job-priority.component.html',
  styleUrls: ['./chips-job-priority.component.scss']
})
export class ChipsJobPriorityComponent implements OnInit {
  @Input() dataJob: any;

  IsNewJob: boolean = false;
  IsHotJob: boolean = false;
  IsRecommendJob: boolean = false;

  constructor(
    private routerHelper: RouterHelperService
  ) { }


  ngOnInit(): void {
    if (this.dataJob && this.dataJob.jobTagsMarks) {
      this.IsNewJob = this.dataJob.jobTagsMarks.tagNew;
      this.IsHotJob = this.dataJob.jobTagsMarks.tagHot;
      this.IsRecommendJob = this.dataJob.jobTagsMarks.tagRecommend
    }
  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

}
