import {ChangeDetectorRef, Component, EventEmitter, Output} from '@angular/core';
import {JobService} from "../../../services/backend/job.service";
import {Job} from "../../../models/backend/dtos/jobs/job";
import {JobShortDTO} from "../../../models/backend/dtos/jobs/job-short.dto";
import {UserService} from "../../../services/backend/user.service";
import {UserData} from "../../../services/backend/auth/dtos/user-data";
import {ActivatedRoute} from "@angular/router";
import {SearchParamService} from "../../../services/search-param.service";
import {catchError, debounceTime, Subscription} from "rxjs";
import {SearchBarService} from "../../../services/search-bar.service";
import {MainSearchService} from "../../../services/main-search.service";
import {EmployerShortDTO} from "../../../models/backend/dtos/employers/employer-short.dto";



@Component({
  selector: 'app-job-short-list',
  templateUrl: './job-short-list.component.html',
  styleUrls: ['./job-short-list.component.scss']
})
export class JobShortListComponent {
  dataJobs: JobShortDTO[] = [];
  @Output() dataJobsOutput = new EventEmitter<JobShortDTO[]>();
  userData: UserData | null = null;

  pageSize: number = 18;

  showLess: string[] = [];
  sortingParam: string = '';
  filtersParam: string = '';
  fullSearchRequest: string = '';
  searchParam: string = '';
  searchParamList: string[];
  onLoadedJobs: boolean = false;
  private searchParamSubscription: Subscription;
  searchBarParam: string = '';
  private paramSubscription: Subscription;

  employerData: EmployerShortDTO[];
  countEmployers: number = 0;
  hasError: boolean = false;

  //CHANGE DATA
  totalElements: number = 0;
  elementsPerPage: number = 9;
  currentPage: number = 1;
  countJobs: number = 0;

  handlePageChange(newPage: number) {
    this.currentPage =  newPage;
  }

  constructor(
    private jobService: JobService, private userService: UserService, private route: ActivatedRoute,
    private searchParamService: SearchParamService,
    private mainSearchService: MainSearchService,
    private cdr: ChangeDetectorRef
  ) {
  }

  ngOnInit() {
    this.userService.getUserData().subscribe(userData => {
      this.userData = userData;
      this.SearchParamData();

    })
  }

  private GetJobsDataArray(){
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.GetJobsDataBasedOnUser(this.userData, id);
  }

  private GetJobsDataBasedOnUser(userData: UserData | null, id: number) {
    if (userData) {
      this.GetJobsData(userData.id, id, this.pageSize, this.showLess, this.searchBarParam, this.sortingParam, this.filtersParam);
    } else {
      this.GetJobsData("", id, this.pageSize, this.showLess, this.searchBarParam, this.sortingParam, this.filtersParam);
    }
  }

  async GetJobsData(userId: string, page: number = 1, pageSize: number = 18, showLess: string[] = [], searchBarParam: string, sortingParam: string = 'NULL', filtersParam: string = 'NULL') {
    this.onLoadedJobs = false;

      this.jobService
        .getJobShortList(userId, page, pageSize, showLess, searchBarParam, sortingParam, filtersParam
    ).subscribe((result: JobShortDTO[]) => {
        if (result) {
          this.dataJobs = result;
          this.dataJobsOutput.emit(result);
          this.onLoadedJobs = true;

          this.totalElements = result.length;
          if(result.length != 0){
            this.countJobs = Math.ceil(result.length / this.elementsPerPage);
          }
          else{
            this.countJobs = 0;
          }
        }
      });
  }


  SearchParamData(){
    this.paramSubscription = this.mainSearchService.getParamObservable()
      .pipe(debounceTime(300))  // Wait for 300ms pause in events
      .subscribe(params => {
        this.onLoadedJobs = false;
        this.searchParamList = params || '';
        this.filtersParam = params.filter;
        this.sortingParam = params.sort || '';
        this.searchBarParam = params.request || '';
        const id = +this.route.snapshot.paramMap.get('id')!;
        //this.GetJobsDataBasedOnUser(this.userData, id);
        this.GetJobsDataArray();
      });
  }

  ngOnDestroy() {
    this.paramSubscription.unsubscribe();
  }

}
