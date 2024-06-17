
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {UserData} from "../../models/backend/dtos/auth/dtos/user-data";
import {AuthenticationClient} from "../../models/backend/dtos/auth/auth-client";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userDataSubject: BehaviorSubject<UserData | null> = new BehaviorSubject<UserData | null>(null);
  private anotherUserDataSubject: BehaviorSubject<UserData | null> = new BehaviorSubject<UserData | null>(null);


  constructor(
    private authClient: AuthenticationClient
  ) {}

  setUserData(userData: UserData): void {
    this.userDataSubject.next(userData);
  }

  setAnotherUserData(id: string): void {
    this.authClient.getUserById(id).subscribe(userData => {
      this.anotherUserDataSubject.next(userData);
    });
  }

  getUserData(): Observable<UserData | null> {
    return this.userDataSubject.asObservable();
  }

  getAnotherUserData(): Observable<UserData | null> {
    return this.anotherUserDataSubject.asObservable();
  }

  isUserDataEmpty(): boolean {
    console.log(this.userDataSubject.getValue())
    return this.userDataSubject.getValue() == null;
  }

  isAnotherUserDataEmpty(): boolean {
    console.log(this.userDataSubject.getValue())
    return this.anotherUserDataSubject.getValue() == null;
  }

  clearUserData(): void {
    this.userDataSubject.next(null);
  }

  clearAnotherUserData(): void {
    if(!this.isAnotherUserDataEmpty()){
      this.anotherUserDataSubject.next(null);
    }
  }




}
