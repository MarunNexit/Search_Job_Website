import {Component, Input, OnInit} from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
    if (this.dataJob && this.dataJob.hot_new_marks) {
      this.IsNewJob = this.dataJob.hot_new_marks[0];
      this.IsHotJob = this.dataJob.hot_new_marks[1];
      this.IsRecommendJob = this.dataJob.hot_new_marks[2];
    }
  }

}
