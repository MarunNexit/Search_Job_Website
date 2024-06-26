import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthTestingClient {
  constructor(private http: HttpClient) {}

  public getTextForAll(): Observable<string> {
    return this.http.get(environment.apiUrl2 + '/authTesting/anonymous', {
      responseType: 'text',
    });
  }

  public getTextForRegistered(): Observable<string> {
    return this.http.get(environment.apiUrl2 + '/authTesting/registered', {
      responseType: 'text',
    });
  }

  public getTextForAdmins(): Observable<string> {
    return this.http.get(environment.apiUrl2 + '/authTesting/adminOnly', {
      responseType: 'text',
    });
  }
}
