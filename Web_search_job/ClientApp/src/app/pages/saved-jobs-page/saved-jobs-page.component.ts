import {ChangeDetectorRef, Component} from '@angular/core';
import {JobShortDTO} from "../../models/backend/dtos/jobs/job-short.dto";
import {UserData} from "../../models/backend/dtos/auth/dtos/user-data";
import {JobService} from "../../services/backend/job.service";
import {AuthService} from "../../models/backend/dtos/auth/auth-service";
import {UserService} from "../../services/backend/user.service";
import {ActivatedRoute} from "@angular/router";
import {catchError} from "rxjs";

@Component({
  selector: 'app-saved-jobs-page',
  templateUrl: './saved-jobs-page.component.html',
  styleUrls: ['./saved-jobs-page.component.scss']
})
export class SavedJobsPageComponent {

  isLoaded: boolean = false;
  hasError: boolean = false;
  dataJobs: JobShortDTO[] = [];
  showLess: string[]
  pageSize: number = 18;
  isLogin: boolean = false;

  userData: UserData | null;

  dataJobsCurrent: JobShortDTO[] = [];
  totalElements: number = 0;
  elementsPerPage: number = 5;
  currentPage: number = 1;
  countJobs: number = 0;

  dataJobsFinished: JobShortDTO[] = [];
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
      this.GetSavedJobsData(userData.userId, id, this.pageSize, this.showLess);
    } else {
      this.GetSavedJobsData("", id, this.pageSize, this.showLess);
    }
  }

  async GetSavedJobsData(userId: string, page: number = 1, pageSize: number = 18, showLess: string[] = []) {
    this.isLoaded = false;
    this.jobService
      .getSavedJobList(userId, page, pageSize)
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


        const now = new Date();

        this.dataJobsCurrent = this.dataJobs.filter(job => !job.dateEnding || new Date(job.dateEnding) > now);
        this.dataJobsFinished = this.dataJobs.filter(job => job.dateEnding && new Date(job.dateEnding) <= now);

        this.totalElements = this.dataJobsCurrent.length;
        this.totalElementsFinished = this.dataJobsFinished.length;

        this.countJobs = this.dataJobsCurrent.length > 0 ? Math.ceil(this.dataJobsCurrent.length / this.elementsPerPage) : 0;
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
