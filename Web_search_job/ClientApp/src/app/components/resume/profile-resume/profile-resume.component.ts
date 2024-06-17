import {Component, Input} from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";
import {Location} from "@angular/common";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {JobService} from "../../../services/backend/job.service";
import {MatDialog} from "@angular/material/dialog";
import {AuthService} from "../../../models/backend/dtos/auth/auth-service";
import {PopupPurpleComponent} from "../../popup/popup-purple/popup-purple.component";
import {PopupWhiteComponent} from "../../popup/popup-white/popup-white.component";
import {handleImageError} from "../../../functions/handleImageError";
import {JobShortDTO} from "../../../models/backend/dtos/jobs/job-short.dto";
import {ResumeDTO} from "../../../models/backend/dtos/profiles/resume.dto";
import {UserData} from "../../../models/backend/dtos/auth/dtos/user-data";

class AvailableCard{
  type: string;
  active: boolean;
  required: boolean;
}

@Component({
  selector: 'app-profile-resume',
  templateUrl: './profile-resume.component.html',
  styleUrls: ['./profile-resume.component.scss']
})
export class ProfileResumeComponent {
  @Input() resume: ResumeDTO;
  @Input() userData: UserData | null;
  @Input() isMyProfile: boolean;

  ListAvailableLeftCards:AvailableCard[] = [];
  ListAvailableRightCards:AvailableCard[] = [];

  dataJob: any;

  isSmallScreen: boolean = false;
  isSaved: boolean = false;
  isProblemRequest: boolean = false;

  result: any;
  isLogin:boolean = false;
  isProblem:boolean = false;

  numberWatch: number = 231;
  numberCandidates: number = 15;

  constructor(
    private routerHelper: RouterHelperService,
    private location: Location,
    private screenSizeService: ScreenSizeService,
    private jobService: JobService,
    public dialog: MatDialog,
    private authService: AuthService,


  ) {
    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isSmallScreen = isSmall;
      console.log(isSmall);
    })
  }


  ngOnInit(){
  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }


  openDialog(): void {
    if(this.isLogin){
      const dialogRef = this.dialog.open(PopupPurpleComponent, {
        width: '1000px',
        data: {type: "JobRequest", job: this.dataJob, title: "Відгук на вакансію"}
      });

      dialogRef.afterClosed().subscribe(result => {
        this.result = result;
        //console.log(result);
        if(result != null){
          this.createJobRequest();
        }
      });
    }
    else{
      const reason:string = "NeedAuth";
      this.onCreateRequest(reason);
    }
  }


  createJobRequest(): void {
    let reason:string = "";
    this.isProblemRequest = false;

    /*if(!this.isLogin){
      this.isProblemRequest = true;
      reason = "NeedAuth";
    }*/

    if(!this.isProblem){
      this.isProblemRequest = true;
      reason = "ProblemSendRequest";
    }

    if(this.isProblemRequest){
      this.onCreateRequest(reason);
    }
    else{
      reason = "SuccessSendRequest";
      this.onCreateRequest(reason);
    }
    this.isProblemRequest = false;
  }

  onCreateRequest(reason: string){
    const problemDialog = this.dialog.open(PopupWhiteComponent, {
      width: '1000px',
      data: {type: reason}
    });

    problemDialog.afterClosed().subscribe(result => {
      this.result = result;
      //console.log(result);
      if(result == "TryAgain"){
        this.createJobRequest();
      }
    });
  }


/*  IsAuthUser(): void {
    this.authService.isLoggedIn().subscribe(isLoggedIn => {
      this.isLogin = isLoggedIn;
      if (isLoggedIn) {

      } else {
        // Користувач не увійшов у систему, виконуйте необхідні дії
      }
    });
  }*/

  hasSectionType(type: string): boolean {
    return this.resume && this.resume.activeResumeSection &&
      this.resume.activeResumeSection.some(section => section.resumeSectionType && section.resumeSectionType.sectionType === type);
  }

  protected readonly handleImageError = handleImageError;

}



