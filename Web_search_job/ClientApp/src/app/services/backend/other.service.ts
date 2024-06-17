
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {UserData} from "../../models/backend/dtos/auth/dtos/user-data";
import {HttpClient} from "@angular/common/http";
import {AllFilters} from "../../models/backend/dtos/filters/allFilters";
import {environment} from "../../../environments/environment";
import {Industry} from "../../models/backend/dtos/other/industry.dto";
import {LocationDataDTO} from "../../models/backend/dtos/other/location-data.dto";

@Injectable({
  providedIn: 'root'
})
export class OtherService {

  private url = "OtherInfo";
  constructor(private http: HttpClient) { }

  public getLocations(userId: string | null, anonymLocation: string = "" , employerLocation: any): Observable<LocationDataDTO[]> {
    console.log(employerLocation)
    console.log(employerLocation)
    console.log(employerLocation)
    console.log(employerLocation)


    return this.http.get<LocationDataDTO[]>(`${environment.apiUrl}/${this.url}/locations`, {
      params: {
        userId: userId ? userId : '',
        anonymLocation: anonymLocation == ""? "NULL" : anonymLocation,
        employerLocationCountry: employerLocation != null? employerLocation.location_country : "NULL",
        employerLocationRegion: employerLocation != null? employerLocation.location_region : "NULL",
        employerLocationCity: employerLocation != null? employerLocation.location_city : "NULL",
      }
    });
  }

  public getIndustries(anonymIndustry: string, employerIndustry: string): Observable<Industry[]> {
    return this.http.get<Industry[]>(`${environment.apiUrl}/${this.url}/industries`, {
      params: {
        anonymIndustry: anonymIndustry,
        employerIndustry: employerIndustry
      }
    });
  }
}
