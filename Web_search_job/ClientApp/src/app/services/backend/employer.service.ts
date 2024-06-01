import { Injectable } from '@angular/core';
import {Job} from "../../models/backend/dtos/jobs/job";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {AllFilters} from "../../models/backend/dtos/filters/allFilters";
import {EmployerShortDTO} from "../../models/backend/dtos/employers/employer-short.dto";
import {EmployerDTO} from "../../models/backend/dtos/employers/employer-full.dto";

@Injectable({
  providedIn: 'root'
})
export class EmployerService {

  private url = "Employer";
  constructor(private http: HttpClient) { }

/*  public GetAllEmployers(): Observable< >{
    return this.http.get< >(`${environment.apiUrl}/${this.url}`)
  }*/

  public GetShortEmployers(page: number, pageSize: number): Observable<EmployerShortDTO[]>{
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<EmployerShortDTO[]>(`${environment.apiUrl}/${this.url}/short`, { params })
  }

  getEmployerById(id: number, userId: string | null): Observable<EmployerDTO> {
    return this.http.get<EmployerDTO>(`${environment.apiUrl}/${this.url}/${id}?userId=${userId}`);
  }

  public GetSixShortEmployers(): Observable<EmployerShortDTO[]>{
    return this.http.get<EmployerShortDTO[]>(`${environment.apiUrl}/${this.url}/short_6_limit`)
  }

  public GetCountEmployers(): Observable<number>{
    return this.http.get<number>(`${environment.apiUrl}/${this.url}/count`)
  }

}
