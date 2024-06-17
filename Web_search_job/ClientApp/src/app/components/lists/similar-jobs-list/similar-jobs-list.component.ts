import {ChangeDetectorRef, Component, Input} from '@angular/core';
import {JobShortDTO} from "../../../models/backend/dtos/jobs/job-short.dto";
import {JobDTO} from "../../../models/backend/dtos/jobs/job.dto";
import {UserData} from "../../../models/backend/dtos/auth/dtos/user-data";
import {catchError} from "rxjs";
import {ActivatedRoute} from "@angular/router";
import {JobService} from "../../../services/backend/job.service";
import {MainSearchService} from "../../../services/main-search.service";

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
  selector: 'app-similar-jobs-list',
  templateUrl: './similar-jobs-list.component.html',
  styleUrls: ['./similar-jobs-list.component.scss']
})
export class SimilarJobsListComponent {

  @Input() job: JobDTO;
  @Input() userData: UserData | null = null;

  similarJobs: JobShortDTO[];

  isLoaded: boolean = false;
  hasError: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private jobService: JobService,
    private cdr: ChangeDetectorRef,
    private mainSearchService: MainSearchService,
  ) {

  }

  ngOnInit(){
    console.log(this.job)
    if(this.job != null){
      if (this.userData) {
        this.getSimilarJob(this.userData.userId);
      }
      else {
        this.getSimilarJob(null);
      }
    }
  }

  showMore(){
    let sortingParam: string[] = ['popular'];
    let filtersParam = 'industry:' + this.job.industry.industry_name + ',' + 'location:' + this.job.location.location_region;
    let filtersParamArray = filtersParam.split(',');
    let searchBarParam = '';
    this.mainSearchService.setParams(sortingParam, filtersParamArray, searchBarParam)
  }

  getSimilarJob(userId: string | null): void {
    let sortingParam = 'popular';
    let filtersParam = 'industry:' + this.job.industry.industry_name + ',' + 'location:' + this.job.location.location_region;
    let searchBarParam = '';
    let showLess: string[] = [];
    let id = this.job.id;
    this.jobService
      .getSimilarJobList(
        userId, showLess, searchBarParam, sortingParam, filtersParam, id
      )
      .pipe(
        catchError(() => {
          this.isLoaded = false;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: JobShortDTO[]) => {
        this.similarJobs = result;
        this.isLoaded = true;
        this.hasError = false

        console.log(this.similarJobs);
        console.log(result);

        this.cdr.detectChanges();
      });


    /*  this.employerService.getEmployerById(id, userId).subscribe(
        (employer) => this.employer = employer,
        (error) => console.error(error)
      );*/

  }
}




