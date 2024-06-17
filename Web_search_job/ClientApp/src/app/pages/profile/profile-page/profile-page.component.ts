import {ChangeDetectorRef, Component} from '@angular/core';
import {JobService} from "../../../services/backend/job.service";
import {AuthService} from "../../../models/backend/dtos/auth/auth-service";
import {UserService} from "../../../services/backend/user.service";
import {ActivatedRoute, Router} from "@angular/router";
import {RouterHelperService} from "../../../services/router-helper.service";
import {UserData} from "../../../models/backend/dtos/auth/dtos/user-data";
import {ProfileService} from "../../../services/backend/profile.service";
import {ResumeShortDTO} from "../../../models/backend/dtos/profiles/resume-short.dto";
import {ResumeDTO} from "../../../models/backend/dtos/profiles/resume.dto";
import {ProfileDataService} from "../../../services/backend/profile-data.service";
import {PopupPurpleComponent} from "../../../components/popup/popup-purple/popup-purple.component";
import {MatDialog} from "@angular/material/dialog";
import {PopupWhiteComponent} from "../../../components/popup/popup-white/popup-white.component";
import {OtherService} from "../../../services/backend/other.service";
import {LocationDataDTO} from "../../../models/backend/dtos/other/location-data.dto";
import {AuthenticationClient} from "../../../models/backend/dtos/auth/auth-client";
import {catchError, of} from "rxjs";

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent {
  userData:UserData | null;
  userProfileData:UserData | null;

  myProfile: boolean = true;
  resumeList: ResumeShortDTO[];
  resume: ResumeDTO;
  selectedOption: number | null = null;
  id: string = '';
  dataLoaded: boolean = false;
  resultAddSection: any;
  resultUserInfo: any;
  resultCreateResume: any;
  isUserInfoDataNull: boolean = true;
  isFirstOpening: boolean = true;
  locations: LocationDataDTO[];
  isFirstResumeData: boolean = true;
  dialogOpened: boolean = false;

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
    private router: Router,
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
      if(id){
        this.getResumeList(id)
      }
    }
    else if(this.userData){
      if(this.userData.userId != null && this.isFirstOpening){
        this.isFirstOpening = false;
        this.openDialogFillUserData();
      }
    }
    else{
      this.myProfile = false;
      this.userService.getAnotherUserData().subscribe(userAnotherData => {
        if(userAnotherData != null){
          this.userProfileData = userAnotherData;
          console.log(userAnotherData)
          console.log(this.userProfileData)
          if(id){
            this.getResumeList(id)
          }
        }
      });
      /*if(this.userData){
        if(this.userData.id != null && id != null){
          this.getResumeList(id)
        }
        else {
          if(this.userData.userId != null && this.isFirstOpening){
            this.isFirstOpening = false;
            this.openDialogFillUserData();
          }
        }
      }*/
    }
  }

  getResumeList(id: string) {
    this.profileService.getResumeListNameByUserId(id).pipe(
      catchError(error => {
        console.error('Error fetching resume list:', error);
        // Вживаємо необхідних дій при помилці, наприклад, показуємо повідомлення користувачу
        if(!this.myProfile){
          this.onInfoField('resumeListEmpty');
        }
        else{
          if (!this.dialogOpened) { // Перевіряємо, чи вже відкрито діалогове вікно
            this.openDialogCreateResume();
            this.dialogOpened = true; // Встановлюємо прапорець, щоб відзначити, що діалогове вікно вже відкрите
          }
        }
        // Повертаємо порожній список або обробляємо помилку іншим чином
        return of([]);
      })
    ).subscribe(resumeList => {
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
            window.location.reload();
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
          this.dialogOpened = false;
          this.resultCreateResume = result;
          console.log(result);

          if(result != null){
            if(result.status == 'onMyOwn'){
              this.onCreateEmpty(result);
            }
            else {
              this.onCreateResumeWithData(result);
            }
          }
        });

      }
      else{
        this.isUserInfoDataNull = false;
      }
    });
  }


  onCreateResumeWithData(result: any): void {
    console.log(result)
    result.UserId = this.userData?.userId;
    console.log(result)

    if(result){
      this.profileService.createResume(result).subscribe(
        response => {
          console.log('Resume created successfully:', response);
          window.location.reload();
        },
        error => {
          console.error('Error creating resume:', error);
        }
      );
    }
  }

  onCreateEmpty(result: any): void {
    console.log(result)
    this.profileService.createEmptyResume(this.userData?.userId, result.name).subscribe(
      response => {
        console.log('Resume created successfully:', response);
        console.log('Resume created successfully:', response);
        console.log('Resume created successfully:', response);
        window.location.reload();
        this.openDialogAddSection()
      },
      error => {
        console.error('Error creating resume:', error);
      }
    );
  }

  onCreateRequest(reason: string) {
    const problemDialog = this.dialog.open(PopupWhiteComponent, {
      width: '1000px',
      data: { type: reason }
    });
    problemDialog.afterClosed().subscribe(result => {
      console.log(result);
    });
  }


  onInfoField(reason: string) {
    const problemDialog = this.dialog.open(PopupWhiteComponent, {
      width: '1000px',
      data: { type: reason }
    });
    problemDialog.afterClosed().subscribe(result => {
      console.log(result);
      if(result === 'TryAgain'){
        this.router.navigate(['/']);
      }
      else if(!this.myProfile && result === 'problemResumeList'){
        this.router.navigate(['/']);
      }
    });
  }

}
