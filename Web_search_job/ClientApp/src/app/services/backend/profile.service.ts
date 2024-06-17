import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {ResumeDTO} from "../../models/backend/dtos/profiles/resume.dto";
import {environment} from "../../../environments/environment";
import {ResumeShortDTO} from "../../models/backend/dtos/profiles/resume-short.dto";
import {JobShortDTO} from "../../models/backend/dtos/jobs/job-short.dto";
import {ResumeSectionTypeDTO} from "../../models/backend/dtos/profiles/resume-section-type.dto";
@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private url = "Resume";
  constructor(private http: HttpClient) {}

  getResumeByUserId(userId: string): Observable<ResumeDTO> {

    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')

    console.log('GET RESUME');
    return this.http.get<ResumeDTO>(`${environment.apiUrl}/${this.url}/active_resume`, { params });
  }


  createResume(createResumeRequest: any): Observable<ResumeDTO> {
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);
    console.log(createResumeRequest);

    return this.http.post<ResumeDTO>(`${environment.apiUrl}/${this.url}/create_resume_with_data`, createResumeRequest);
  }

  createEmptyResume(_userId: string | undefined, resumeName: string): Observable<ResumeDTO> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const userId = _userId ? _userId : '';
    const body = { userId, resumeName };

    return this.http.post<ResumeDTO>(`${environment.apiUrl}/${this.url}/create_empty_resume`, body, { headers });
  }

  getResumeListNameByUserId(userId: string): Observable<ResumeShortDTO[]> {

    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')

    return this.http.get<ResumeShortDTO[]>(`${environment.apiUrl}/${this.url}/resume_name_list`, { params });
  }


  getResumeSections(): Observable<ResumeSectionTypeDTO[]> {
    return this.http.get<ResumeSectionTypeDTO[]>(`${environment.apiUrl}/${this.url}/sections_list`);
  }


  setActiveResume(userId: string, resumeId: number): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let UserId = userId ? userId : "NULL";
    let ResumeId = resumeId ? resumeId : 0;

    const body = {
      UserId,
      ResumeId
    };

    return this.http.post(`${environment.apiUrl}/${this.url}/set_active_resume`, body, { headers });
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

  addActiveSection(resumeId: number, sectionId: number): Observable<void> {
    const request: AddSectionRequest = { resumeId, sectionId };
    console.log(request)
    return this.http.post<void>(`${environment.apiUrl}/${this.url}/add_active_section`, request);
  }

  removeActiveSection(resumeId: number, sectionId: number): Observable<void> {
    const request: RemoveSectionRequest = { resumeId, sectionId };
    console.log(request)
    return this.http.request<void>('delete', `${environment.apiUrl}/${this.url}/remove_active_section`, { body: request });
  }


  uploadResumeFile(fileUrl: string): Promise<any> {
    const apiUrl = `https://api.apilayer.com/resume_parser/url?url=${fileUrl}`;
    const apiKey = 'ZYmkCUSoJy3iZpj25wu72A8A6nTgGSgM';

    return fetch(apiUrl, {
      method: 'GET',
      headers: {
        'apikey': apiKey
      }
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.json(); // or response.text() if you need plain text
      })
      .then(result => {
        console.log(result);
        return result;
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
        throw error;
      });
  }

/*  public uploadResumeFile(fileUrl: string): Observable<any> {
    /!*
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    *!/
    console.log(fileUrl);
    return this.http.post(`${environment.apiUrl}/${this.url}/upload-resume-parser?fileUrl=${fileUrl}`, null, {
      headers: {
        // Note: Do not set Content-Type header manually here; the browser will set it automatically to multipart/form-data
      }
    });
  }*/

/*  public uploadResumeFile(fileUrl: string): Observable<any> {
    /!*
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    *!/
    console.log(fileUrl);
    return this.http.post(`${environment.apiUrl}/${this.url}/upload-resume-parser?fileUrl=${fileUrl}`, null, {
      headers: {
        // Note: Do not set Content-Type header manually here; the browser will set it automatically to multipart/form-data
      }
    });
  }*/


  /*  updateSavedJob(userId: string | null, jobId: number, isSavedJob: boolean): Observable<any> {
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      let UserId = userId ? userId : "NULL";
      let JobId = jobId ? jobId : 0;

      const body = {
        UserId,
        JobId,
        isSavedJob
      };
      return this.http.post(`${environment.apiUrl}/${this.url}/update_saved_job`, body, { headers });
    }*/

}


interface AddSectionRequest {
  resumeId: number;
  sectionId: number;
}

interface RemoveSectionRequest {
  resumeId: number;
  sectionId: number;
}
