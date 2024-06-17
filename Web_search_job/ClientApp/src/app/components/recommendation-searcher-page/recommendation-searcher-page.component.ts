import {ChangeDetectorRef, Component} from '@angular/core';
import {AuthService} from "../../models/backend/dtos/auth/auth-service";
import {UserService} from "../../services/backend/user.service";
import {UserData} from "../../models/backend/dtos/auth/dtos/user-data";
import {JobService} from "../../services/backend/job.service";
import {ActivatedRoute} from "@angular/router";
import {catchError, Subscription} from "rxjs";
import {JobShortDTO} from "../../models/backend/dtos/jobs/job-short.dto";



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
  elementsPerPage: number = 5;
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
      this.GetRecJobsData(userData.userId, id, this.pageSize, this.showLess);
    } else {
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
