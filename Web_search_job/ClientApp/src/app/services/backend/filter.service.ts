import { Injectable } from '@angular/core';
import {Job} from "../../models/backend/dtos/jobs/job";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {AllFilters} from "../../models/backend/dtos/filters/allFilters";
import {LocationDataDTO} from "../../models/backend/dtos/other/location-data.dto";

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  private url = "Filter";
  constructor(private http: HttpClient) { }


  public GetAllFilters(userId:string="", anonymLocation:string = "", anonymindustry:string = ""): Observable<AllFilters>{

    return this.http.get<AllFilters>(`${environment.apiUrl}/${this.url}`,{
      params: {
        userId: userId ? userId : '',
        anonymLocation: anonymLocation == ""? "NULL" : anonymLocation,
        anonymindustry: anonymindustry == ""? "NULL" : anonymindustry,
    }
  })
  }
}
