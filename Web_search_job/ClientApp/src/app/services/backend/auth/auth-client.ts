import { environment } from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginResponseDto } from './dtos/login-response-dto';
import { RefreshTokenResponseDto } from './dtos/refresh-token-response-dto';
import {UserData} from "./dtos/user-data";

@Injectable({
  providedIn: 'root',
})
export class AuthenticationClient {
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



  public getUserId(): Observable<any> {
    const url = environment.apiUrl2 + '/auth/user-id';
    return this.http.get<UserData>(url);
  }

}
