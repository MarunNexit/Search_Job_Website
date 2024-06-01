
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {UserData} from "./auth/dtos/user-data";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userDataSubject: BehaviorSubject<UserData | null> = new BehaviorSubject<UserData | null>(null);

  constructor() {}

  setUserData(userData: UserData): void {
    this.userDataSubject.next(userData);
  }

  getUserData(): Observable<UserData | null> {
    return this.userDataSubject.asObservable();
  }

  isUserDataEmpty(): boolean {
    console.log(this.userDataSubject.getValue())
    return this.userDataSubject.getValue() == null;
  }

  clearUserData(): void {
    this.userDataSubject.next(null);
  }
}
