import {ChangeDetectorRef, Component} from '@angular/core';
import {UserData} from "../../../../models/backend/dtos/auth/dtos/user-data";
import {ResumeShortDTO} from "../../../../models/backend/dtos/profiles/resume-short.dto";
import {ResumeDTO} from "../../../../models/backend/dtos/profiles/resume.dto";
import {LocationDataDTO} from "../../../../models/backend/dtos/other/location-data.dto";
import {JobService} from "../../../../services/backend/job.service";
import {AuthService} from "../../../../models/backend/dtos/auth/auth-service";
import {UserService} from "../../../../services/backend/user.service";
import {ProfileService} from "../../../../services/backend/profile.service";
import {ActivatedRoute} from "@angular/router";
import {RouterHelperService} from "../../../../services/router-helper.service";
import {ProfileDataService} from "../../../../services/backend/profile-data.service";
import {MatDialog} from "@angular/material/dialog";
import {OtherService} from "../../../../services/backend/other.service";
import {AuthenticationClient} from "../../../../models/backend/dtos/auth/auth-client";
import {PopupPurpleComponent} from "../../../../components/popup/popup-purple/popup-purple.component";
import {PopupWhiteComponent} from "../../../../components/popup/popup-white/popup-white.component";

@Component({
  selector: 'app-job-create-page',
  templateUrl: './job-create-page.component.html',
  styleUrls: ['./job-create-page.component.scss']
})
export class JobCreatePageComponent {
  userData:UserData | null;
  userProfileData:UserData | null;

  myProfile: boolean = true;
  resumeList: ResumeShortDTO[];
  resume: ResumeDTO;
  selectedOption: number | null = null;
  id: string;
  dataLoaded: boolean = false;
  resultAddSection: any;
  resultUserInfo: any;
  resultCreateResume: any;
  isUserInfoDataNull: boolean = true;
  isFirstOpening: boolean = true;
  locations: LocationDataDTO[];
  isFirstResumeData: boolean = true;

  constructor(
    private jobService: JobService,
    private authService: AuthService,
    private cdr: ChangeDetectorRef,
    private userService: UserService,
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private routerHelperService: RouterHelperService,
    private profileDataService: ProfileDataService,
    public dialog: MatDialog,
    private otherService: OtherService,
    private authClient: AuthenticationClient,
  ) {
  }

  ngOnInit(){
    this.profileDataService.clearProfileData();

    this.userService.getUserData().subscribe(userData => {
      this.userData = userData;
      this.onLoadUserData();
    });
  }

  onLoadUserData(){
    const id = this.route.snapshot.paramMap.get('id')!;
    this.id = id;
    console.log(id);
    if(this.userData && this.userData.id != null){
      this.isUserInfoDataNull = false;
      this.myProfile = id == this.userData.userId;
      if(!this.myProfile){
        this.userService.getAnotherUserData().subscribe(userAnotherData => {
          this.userProfileData = userAnotherData;
        });
      }
      else {
        this.userProfileData = this.userData;
      }
      this.getResumeList(id)
    }
    else{
      this.myProfile = false;
      this.userService.getAnotherUserData().subscribe(userAnotherData => {
        if(userAnotherData != null){
          this.userProfileData = userAnotherData;
          console.log(userAnotherData)
          console.log(this.userProfileData)
          this.getResumeList(id)
        }
      });
      if(this.userData){
        if(this.userData.id != null){
          this.getResumeList(id)
        }
        else {
          if(this.userData.userId != null && this.isFirstOpening){
            this.isFirstOpening = false;
            this.openDialogFillUserData();
          }
        }
      }
    }
  }

  getResumeList(id: string) {
    this.profileService.getResumeListNameByUserId(id).subscribe(resumeList => {
      this.resumeList = resumeList;
      console.log(this.resumeList);
      const selectedResume = this.resumeList.find(resume => resume.resumeActive);
      this.selectedOption = selectedResume ? selectedResume.id : this.resumeList[0].id

      if(this.id != null && this.isFirstResumeData){
        this.isFirstResumeData = false;
        this.getResumeData(this.id)
      }
    });
  }

  getResumeData(id: string) {
    console.log(id)
    if(id != null){
      this.profileService.getResumeByUserId(id).subscribe(resume => {
        this.resume = resume;
        this.profileDataService.setProfileData(resume);
        console.log(this.resume);
        this.dataLoaded = true;
      });
    }
  }


  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = +target.value;
    console.log(selectedValue);
    if(selectedValue != null){
      this.dataLoaded = false;
      this.isFirstResumeData = true;
      this.profileService.setActiveResume(this.id, selectedValue).subscribe(resumeList => {
        this.getResumeList(this.id);
      });

    }
  }



  openDialogAddSection(): void {
    if(this.userData){
      const dialogRef = this.dialog.open(PopupPurpleComponent, {
        width: '1000px',
        data: {type: "AddProfileSection", resume: this.resume, title: "Секції резюме"}
      });

      dialogRef.afterClosed().subscribe(result => {
        this.resultAddSection = result;
        console.log(result);

        if(result == 'changed'){
          this.getResumeData(this.id);
        }
      });
    }
    else{
      const reason:string = "NeedAuth";
      this.onCreateRequest(reason);
    }
  }



  openDialogFillUserData(): void {
    this.otherService.getLocations(this.id, '', null).subscribe(locations => {
      this.locations = locations;
      console.log(this.locations)

      if(this.userData?.id === null){
        console.log("A IM HERE")

        const dialogRef = this.dialog.open(PopupPurpleComponent, {
          width: '1000px',
          data: {type: "AddUserInfo", user: this.userData, title: "Особиста інформація", locations: this.locations}
        });

        dialogRef.afterClosed().subscribe(result => {
          if(result == null && this.isUserInfoDataNull){
            this.openDialogFillUserData();
          }
          this.resultUserInfo = result;
          console.log(result);
          this.authClient.setUserData(result).subscribe(newUserData => {
            console.log(newUserData)
            this.userService.setUserData(newUserData);
            this.userData = newUserData;
            this.userProfileData = newUserData;
          })
        });

      }
      else{
        this.isUserInfoDataNull = false;
      }
    });
  }


  openDialogCreateResume(): void {
    this.otherService.getLocations(this.id, '', null).subscribe(locations => {
      this.locations = locations;
      console.log(this.locations)

      if(this.userData?.id != null){
        console.log("A IM HERE")

        const dialogRef = this.dialog.open(PopupPurpleComponent, {
          width: '1000px',
          data: {type: "CreateResume", user: this.userData, title: "Створення резюме", locations: this.locations}
        });

        dialogRef.afterClosed().subscribe(result => {
          /*if(result == null && this.isUserInfoDataNull){
            this.openDialogFillUserData();
          }*/

          this.resultCreateResume = result;
          console.log(result);

        });

      }
      else{
        this.isUserInfoDataNull = false;
      }
    });
  }


  onCreateRequest(reason: string) {
    const problemDialog = this.dialog.open(PopupWhiteComponent, {
      width: '1000px',
      data: { type: reason }
    });
    problemDialog.afterClosed().subscribe(result => {
      /*
            this.result = result;
      */
      console.log(result);

      /*if (result == "TryAgain") {
        this.createJobRequest();
      }*/
    });
  }


}
