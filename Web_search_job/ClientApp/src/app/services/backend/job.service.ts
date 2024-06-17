import { Injectable } from '@angular/core';
import {Job} from "../../models/backend/dtos/jobs/job";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {JobShortDTO} from "../../models/backend/dtos/jobs/job-short.dto";
import {EmployerDTO} from "../../models/backend/dtos/employers/employer-full.dto";
import {JobDTO} from "../../models/backend/dtos/jobs/job.dto";
import {JobRequestFieldsDTO} from "../../models/backend/dtos/jobs/job-request-fields.dto";
import {Validators} from "@angular/forms";
import {JobWithRequestDTO} from "../../models/backend/dtos/jobs/job-with-request.dto";
import {JobSearchResultDTO} from "../../models/backend/dtos/jobs/job-search-result.dto";

@Injectable({
  providedIn: 'root'
})
export class JobService {

  private url = "Job";
  constructor(private http: HttpClient) { }

  public getJobShortList(
    userId: string, page: number = 1, pageSize: number = 18, showLess: string[] = [], searchBarParam:string, sortingParam: string = 'NULL', filtersParam: string = 'NULL'
  ): Observable<JobSearchResultDTO>{

    let params = new HttpParams()
      .set('userId', userId ? userId : 'NULL')
      .set('page', page ? page.toString() : '1')
      .set('pageSize', pageSize ? pageSize.toString() : '18')

    if (showLess.length > 0) {
      params = params.set('showLess', showLess.join(','));
    } else {
      params = params.set('showLess', 'NULL');
    }

    params = params.set('searchBarParam', searchBarParam ? searchBarParam : 'NULL');
    params = params.set('sortingParam', sortingParam ? sortingParam : 'NULL');

    console.log('filtersParam', filtersParam)

    params = params.set('filtersParam', filtersParam ? filtersParam : 'NULL');

    return this.http.get<JobSearchResultDTO>(`${environment.apiUrl}/${this.url}/search_cards`, { params });
  }


  public getSimilarJobList(
    userId: string | null, showLess: string[] = [], searchBarParam:string, sortingParam: string = 'NULL', filtersParam: string = 'NULL', id: number = 0
  ): Observable<JobShortDTO[]>{

    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')

    if (showLess.length > 0) {
      params = params.set('showLess', showLess.join(','));
    } else {
      params = params.set('showLess', 'NULL');
    }

    params = params.set('searchBarParam', searchBarParam ? searchBarParam : 'NULL');
    params = params.set('sortingParam', sortingParam ? sortingParam : 'NULL');

    console.log('filtersParam', filtersParam)
    params = params.set('filtersParam', filtersParam ? filtersParam : 'NULL');

    params = params.set('id', id ? id : 0);
    console.log(params);

    return this.http.get<JobShortDTO[]>(`${environment.apiUrl}/${this.url}/similar_jobs`, { params });
  }

  updateSavedJob(userId: string | null, jobId: number, isSavedJob: boolean): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let UserId = userId ? userId : "NULL";
    let JobId = jobId ? jobId : 0;

    const body = {
      UserId,
      JobId,
      isSavedJob
    };
    return this.http.post(`${environment.apiUrl}/${this.url}/update_saved_job`, body, { headers });
  }


  public getSavedJobList(
    userId: string, page: number = 1, pageSize: number = 18
  ): Observable<JobShortDTO[]>{

    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')
      .set('page', page ? page.toString() : '1')
      .set('pageSize', pageSize ? pageSize.toString() : '18')

    return this.http.get<JobShortDTO[]>(`${environment.apiUrl}/${this.url}/saved_jobs`, { params });
  }


  public getRecommendJobShortList(
    userId: string, page: number = 1, pageSize: number = 18, showLess: string[] = []
  ): Observable<JobShortDTO[]>{

    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')
      .set('page', page ? page.toString() : '1')
      .set('pageSize', pageSize ? pageSize.toString() : '18')

    if (showLess.length > 0) {
      params = params.set('showLess', showLess.join(','));
    } else {
      params = params.set('showLess', 'NULL');
    }

    return this.http.get<JobShortDTO[]>(`${environment.apiUrl}/${this.url}/recommend_jobs`, { params });
  }


  getEmployerById(id: number, userId: string | null): Observable<EmployerDTO> {
    return this.http.get<EmployerDTO>(`${environment.apiUrl}/${this.url}/${id}?userId=${userId}`);
  }

  public getJobById(id: number, userId: string | null  ): Observable<JobDTO>{
    return this.http.get<JobDTO>(`${environment.apiUrl}/${this.url}/${id}?userId=${userId}`);
  }

  public getJobRequestFields(jobId: number): Observable<JobRequestFieldsDTO> {
    return this.http.get<JobRequestFieldsDTO>(`${environment.apiUrl}/${this.url}/job_request_fields/${jobId}`);
  }

  public getJobRequestList(
    userId: string, page: number = 1, pageSize: number = 18
  ): Observable<JobWithRequestDTO[]>{

    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')
      .set('page', page ? page.toString() : '1')
      .set('pageSize', pageSize ? pageSize.toString() : '18')

    return this.http.get<JobWithRequestDTO[]>(`${environment.apiUrl}/${this.url}/jobs-with-requests`, { params });
  }


  public getJobRequestListWithParam(
    userId: string, page: number = 1, pageSize: number = 18, type: string = 'active'
  ): Observable<JobWithRequestDTO[]>{

    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')
      .set('page', page ? page.toString() : '1')
      .set('pageSize', pageSize ? pageSize.toString() : '18')
      .set('type', type ? type : 'active')

    console.log(type)
    return this.http.get<JobWithRequestDTO[]>(`${environment.apiUrl}/${this.url}/jobs-with-requests-with-param`, { params });
  }

  public deleteJobRequest(jobId: number, userId: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/${this.url}/delete-job-request/${jobId}/${userId}`);
  }

  createJobRequest(jobRequest: any, jobId: number, userId: number | undefined): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let JobId = jobId ? jobId : 0;
    let UserId = userId ? userId : "NULL";
    let ResumeId = jobRequest.resume ? jobRequest.resume : null;
    let ResumeURL = jobRequest.filesUrl ? jobRequest.filesUrl : null;
    let CoverLetter = jobRequest.coverLetter ? jobRequest.coverLetter : null;
    let Positives = jobRequest.similarProjects ? jobRequest.similarProjects : null;
    let Projects = jobRequest.positiveTraits ? jobRequest.positiveTraits : null;
    let Status = "active";


    const body = {
      JobId,
      UserId,
      ResumeId,
      ResumeURL,
      CoverLetter,
      Positives,
      Projects,
      Status
    };

    return this.http.post(`${environment.apiUrl}/${this.url}/create-job-request`, body, { headers });
  }



  /*  public GetCountRecJobs(): Observable<number>{
      return this.http.get<number>(`${environment.apiUrl}/${this.url}/count_rec_jobs`)
    }

    public GetCountJobs(): Observable<number>{
      return this.http.get<number>(`${environment.apiUrl}/${this.url}/count_jobs`)
    }*/
}
