import {ChangeDetectorRef, Component} from "@angular/core";
import {catchError} from "rxjs";
import {JobShortDTO} from "../../models/backend/dtos/jobs/job-short.dto";
import {UserData} from "../../models/backend/dtos/auth/dtos/user-data";
import {JobService} from "../../services/backend/job.service";
import {AuthService} from "../../models/backend/dtos/auth/auth-service";
import {UserService} from "../../services/backend/user.service";
import {ActivatedRoute} from "@angular/router";
import {JobWithRequestDTO} from "../../models/backend/dtos/jobs/job-with-request.dto";

@Component({
  selector: 'app-request-history-page',
  templateUrl: './request-history-page.component.html',
  styleUrls: ['./request-history-page.component.scss']
})
export class RequestHistoryPageComponent {
  isLoaded: boolean = false;
  hasError: boolean = false;
  dataJobs: JobWithRequestDTO[] = [];
  showLess: string[]
  pageSize: number = 18;
  isLogin: boolean = false;

  userData: UserData | null;

  dataJobsCurrent: JobWithRequestDTO[] = [];
  totalElements: number = 0;
  elementsPerPage: number = 5;
  currentPage: number = 1;
  countJobs: number = 0;

  dataJobsFinished: JobWithRequestDTO[] = [];
  totalElementsFinished: number = 0;
  elementsPerPageFinished: number = 5;
  currentPageFinished: number = 1;
  countJobsFinished: number = 0;


  handlePageChange(newPage: number) {
    this.currentPage =  newPage;
  }

  handlePageChangeFinished(newPage: number) {
    this.currentPageFinished =  newPage;
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
      this.GetHistoryJobsData(userData.userId, id, this.pageSize);
    } else {
      this.GetHistoryJobsData("", id, this.pageSize);
    }
  }

  async GetHistoryJobsData(userId: string, page: number = 1, pageSize: number = 18) {
    this.isLoaded = false;
    this.jobService
      .getJobRequestListWithParam(userId, page, pageSize, 'active')
      .pipe(
        catchError(() => {
          this.isLoaded = true;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: JobWithRequestDTO[]) => {
        this.isLoaded = true;
        this.hasError = false

        console.log(result);

        const now = new Date();

        this.dataJobsCurrent = result;

        this.totalElements = this.dataJobsCurrent.length;
        this.countJobs = this.dataJobsCurrent.length > 0 ? Math.ceil(this.dataJobsCurrent.length / this.elementsPerPage) : 0;

        this.cdr.detectChanges();
      });


    this.jobService
      .getJobRequestListWithParam(userId, page, pageSize, 'finished')
      .pipe(
        catchError(() => {
          this.isLoaded = true;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: JobWithRequestDTO[]) => {
        this.isLoaded = true;
        this.hasError = false

        console.log(result);

        const now = new Date();

        this.dataJobsFinished = result;
        this.totalElementsFinished = this.dataJobsFinished.length;
        this.countJobsFinished = this.dataJobsFinished.length > 0 ? Math.ceil(this.dataJobsFinished.length / this.elementsPerPageFinished) : 0;

        this.cdr.detectChanges();
      });
  }

  IsAuthUser(): void {
    this.authService.isLoggedIn().subscribe(isLoggedIn => {
      this.isLogin = isLoggedIn;
    });
  }

}
