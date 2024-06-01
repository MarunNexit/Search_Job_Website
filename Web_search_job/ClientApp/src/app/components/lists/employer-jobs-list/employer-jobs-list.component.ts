import {Component, Input} from '@angular/core';
import {JobEmployerShortDTO} from "../../../models/backend/dtos/jobs/job-employer-short.dto";
import {EmployerDTO} from "../../../models/backend/dtos/employers/employer-full.dto";
import {UserService} from "../../../services/backend/user.service";
import {UserData} from "../../../services/backend/auth/dtos/user-data";


interface DataJob {
  id: number;
  title: string;
  salary:string;
  company:string;
  description: string;
  tags: string[];
  company_picture:string;
  banner_picture:string;
  hot_new_marks:boolean[];

}

@Component({
  selector: 'app-employer-jobs-list',
  templateUrl: './employer-jobs-list.component.html',
  styleUrls: ['./employer-jobs-list.component.scss']
})
export class EmployerJobsListComponent {

  @Input() dataEmployerJobs: JobEmployerShortDTO[];
  @Input() employer: EmployerDTO;
  userData: UserData | null;

  constructor(
    private userService: UserService
  ) {
  }

  ngOnInit(){
    this.userService.getUserData().subscribe(userData => {
      this.userData = userData;
    });
  }

}
