import {ChangeDetectorRef, Component} from '@angular/core';
import {AuthService} from "../../services/backend/auth/auth-service";
import {UserService} from "../../services/backend/user.service";
import {UserData} from "../../services/backend/auth/dtos/user-data";
import {RouterHelperService} from "../../services/router-helper.service";
import {Location} from "@angular/common";
import {ScreenSizeService} from "../../services/screen-size.sevice";
import {JobService} from "../../services/backend/job.service";
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute} from "@angular/router";
import {catchError, Subscription} from "rxjs";
import {JobDTO} from "../../models/backend/dtos/jobs/job.dto";
import {JobShortDTO} from "../../models/backend/dtos/jobs/job-short.dto";
import {EmployerShortDTO} from "../../models/backend/dtos/employers/employer-short.dto";


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
  selector: 'app-recommendation-searcher-page',
  templateUrl: './recommendation-searcher-page.component.html',
  styleUrls: ['./recommendation-searcher-page.component.scss']
})
export class RecommendationSearcherPageComponent {

  isLoaded: boolean = false;
  hasError: boolean = false;
  dataJobs: JobShortDTO[] = [];
  showLess: string[]
  pageSize: number = 18;
  isLogin: boolean = false;

  userData: UserData | null;

  totalElements: number = 0;
  elementsPerPage: number = 9;
  currentPage: number = 1;
  countJobs: number = 0;

  handlePageChange(newPage: number) {
    this.currentPage =  newPage;
  }

  constructor(
    private jobService: JobService,
    private authService: AuthService,
    private cdr: ChangeDetectorRef,
    private userService: UserService,
    private route: ActivatedRoute,

  ) {

  }

  ngOnInit(){
    this.IsAuthUser();
    this.GetJobsData();
  }

  private GetJobsData(){
    this.userService.getUserData().subscribe(userData => {
      this.userData = userData;
      const id = +this.route.snapshot.paramMap.get('id')!;
      this.GetJobsDataBasedOnUser(this.userData, id);
    });
  }

  private GetJobsDataBasedOnUser(userData: UserData | null, id: number) {
    if (userData) {
      this.GetRecJobsData(userData.id, id, this.pageSize, this.showLess);
    } else {
      this.GetRecJobsData("", id, this.pageSize, this.showLess);
    }
  }

  async GetRecJobsData(userId: string, page: number = 1, pageSize: number = 18, showLess: string[] = []) {
    this.isLoaded = false;
    this.jobService
      .getRecommendJobShortList(userId, page, pageSize, showLess)
      .pipe(
        catchError(() => {
          this.isLoaded = true;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: JobShortDTO[]) => {
        this.dataJobs = result;
        this.isLoaded = true;
        this.hasError = false

        this.totalElements = result.length;
        if(result.length != 0){
          this.countJobs = Math.ceil(result.length / this.elementsPerPage);
        }
        else{
          this.countJobs = 0;
        }

        this.cdr.detectChanges();
      });
  }

  IsAuthUser(): void {
    this.authService.isLoggedIn().subscribe(isLoggedIn => {
      this.isLogin = isLoggedIn;
    });
  }


}
