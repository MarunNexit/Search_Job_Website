
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {UserData} from "../../models/backend/dtos/auth/dtos/user-data";
import {AuthenticationClient} from "../../models/backend/dtos/auth/auth-client";
import {ResumeDTO} from "../../models/backend/dtos/profiles/resume.dto";

@Injectable({
  providedIn: 'root'
})
export class ProfileDataService {
  private profileDataSubject: BehaviorSubject<ResumeDTO | null> = new BehaviorSubject<ResumeDTO | null>(null);

  constructor() {}

  setProfileData(resume: ResumeDTO): void {
    this.profileDataSubject.next(resume);
  }

  getProfileData(): Observable<ResumeDTO | null> {
    return this.profileDataSubject.asObservable();
  }

  isProfileDataEmpty(): boolean {
    console.log(this.profileDataSubject.getValue())
    return this.profileDataSubject.getValue() == null;
  }

  clearProfileData(): void {
    if(!this.isProfileDataEmpty()){
      this.profileDataSubject.next(null);
    }
  }

}
