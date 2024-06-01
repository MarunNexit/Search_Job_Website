import { Component } from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";
import {Location} from "@angular/common";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {JobService} from "../../../services/backend/job.service";
import {MatDialog} from "@angular/material/dialog";
import {AuthService} from "../../../services/backend/auth/auth-service";
import {PopupPurpleComponent} from "../../popup/popup-purple/popup-purple.component";
import {PopupWhiteComponent} from "../../popup/popup-white/popup-white.component";
import {handleImageError} from "../../../functions/handleImageError";

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
    this.dataJob = this.dataJob1;

    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isSmallScreen = isSmall;
      console.log(isSmall);
    });

      this.ListAvailableLeftCards.push(
        {
          type: "Особиста інформація",
          active: true,
          required: true,
        },
        {
          type: "Контактна інформація",
          active: true,
          required: true,
        },
        {
          type: "Навички",
          active: true,
          required: false,
        },
        {
          type: "Мови",
          active: true,
          required: false,
        },
        {
          type: "Поєднані акаунти",
          active: true,
          required: false,
        },
      )


    this.ListAvailableRightCards.push(
      {
        type: "Бажана посада",
        active: true,
        required: true,
      },
      {
        type: "Про себе",
        active: true,
        required: false,
      },
      {
        type: "Історія роботи",
        active: true,
        required: false,
      },
      {
        type: "Освіта",
        active: true,
        required: false,
      },
      {
        type: "Портфоліо",
        active: true,
        required: true,
      },
    )

  }


  ngOnInit(){
    //this.IsAuthUser();

    /*    this.jobService
          .getJobAllData()
          .subscribe((result: Job[]) => (this.dataJob = result[0]));*/
  }

  dataJob1 = {
    id: 1,
    title: "Розробник програмного забезпечення",
    salary: "90000 USD",
    company: "ABC Inc.",
    description: "Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення",
    tags: ["IT", "Програмування", "Розробка"],
    company_picture: "../../../../assets/img/icons/cards/check_mark.png",
    banner_picture: "url/to/banner_picture_1.jpg",
    hot_new_marks: [true, true, false],

  };

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  saveJob() {
    this.isSaved = !this.isSaved;
  }

  goBack(): void {
    this.location.back();
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


  IsAuthUser(): void {
    this.authService.isLoggedIn().subscribe(isLoggedIn => {
      this.isLogin = isLoggedIn;
      if (isLoggedIn) {

      } else {
        // Користувач не увійшов у систему, виконуйте необхідні дії
      }
    });
  }



  protected readonly handleImageError = handleImageError;



}



