import { environment } from '../../../../../environments/environment';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginResponseDto } from './dtos/login-response-dto';
import { RefreshTokenResponseDto } from './dtos/refresh-token-response-dto';
import {UserData} from "./dtos/user-data";
import {ResumeShortDTO} from "../profiles/resume-short.dto";

@Injectable({
  providedIn: 'root',
})
export class AuthenticationClient {

  private url = "User";

  constructor(private http: HttpClient) {}

  public login(
    username: string,
    password: string
  ): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(
      environment.apiUrl2 + '/auth/login',
      {
        username: username,
        password: password,
      }
    );
  }

  public socialLogin(
    email: string,
    provider: string,
    accessToken: string
  ): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(
      environment.apiUrl2 + '/auth/social-login',
      {
        email: email,
        provider: provider,
        accessToken: accessToken,
      }
    );
  }

  public register(email: string, password: string) {
    return this.http.post(environment.apiUrl2 + '/auth/register', {
      email: email,
      password: password,
    });
  }

  public registerEmployer(email: string, password: string) {
    return this.http.post(environment.apiUrl2 + '/auth/register-employer', {
      email: email,
      password: password,
    });
  }

  public refreshToken(
    accessToken: string,
    refreshToken: string
  ): Observable<RefreshTokenResponseDto> {
    return this.http.post<RefreshTokenResponseDto>(
      environment.apiUrl2 + '/auth/refresh-token',
      {
        accessToken: accessToken,
        refreshToken: refreshToken,
      }
    );
  }


  getUserData(): Observable<UserData> {
    const url = environment.apiUrl2 + '/auth/user-data';
    const token = localStorage.getItem('accessToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<UserData>(url,
      { headers })
  }


  setUserData(user: any): Observable<UserData> {
    const url = environment.apiUrl2 + '/auth/set-user-data';
/*  const token = localStorage.getItem('accessToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);*/

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    let userId = user.userId ? user.userId : null;
    let dateOfBirth = user.dateOfBirth && user.dateOfBirth != '--' ? user.dateOfBirth : null;
    let locationId = user.locationId ? user.locationId : null;
    let firstName = user.firstName ? user.firstName : null;
    let lastName = user.lastName ? user.lastName : null;
    let phoneNumber = user.phoneNumber ? user.phoneNumber : null;
    let userImg = user.userImg ? user.userImg : null;
    let gender = user.gender ? user.gender : null;

    const userData = {
      userId,
      dateOfBirth,
      locationId,
      firstName,
      lastName,
      phoneNumber,
      userImg,
      gender
    };


    console.log(userData);
    return this.http.post<UserData>(url, userData, { headers })
  }



  public getUserId(): Observable<any> {
    const url = environment.apiUrl2 + '/auth/user-id';
    return this.http.get<UserData>(url);
  }


  getUserById(userId: string ): Observable<UserData> {
    let params= new HttpParams()
      .set('userId', userId ? userId : 'NULL')

    return this.http.get<UserData>(`${environment.apiUrl}/${this.url}/user_data_by_id`, { params });
  }


  getStatisticsSearcher(userId: string): Observable<any> {
    const url = `${environment.apiUrl}/${this.url}/get_statistics_searcher?userId=${userId}`;
    return this.http.get<any>(url);
  }


}
