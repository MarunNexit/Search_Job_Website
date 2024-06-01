import {Component, Input} from '@angular/core';
import {PopupPurpleComponent} from "../../popup/popup-purple/popup-purple.component";
import {PopupWhiteComponent} from "../../popup/popup-white/popup-white.component";
import {RouterHelperService} from "../../../services/router-helper.service";
import {Location} from "@angular/common";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {JobService} from "../../../services/backend/job.service";
import {MatDialog} from "@angular/material/dialog";
import {AuthService} from "../../../services/backend/auth/auth-service";
import {handleImageError} from "../../../functions/handleImageError";
import {animate, state, style, transition, trigger} from "@angular/animations";

@Component({
  selector: 'app-resume-section',
  templateUrl: './resume-section.component.html',
  styleUrls: ['./resume-section.component.scss'],
  animations: [
    trigger('openClose', [
      state('collapsed', style({ height: '100px', overflow: 'hidden' })),
      state('expanded', style({ height: '*' })),
      transition('collapsed <=> expanded', [
        animate('0.5s')
      ])
    ])
  ]
})
export class ResumeSectionComponent {
  @Input() sections: string[] = [];
  @Input() title: string = '';

  dataJob: any;

  showLess: boolean = false;
  isSmallScreen: boolean = false;
  isSaved: boolean = false;
  isProblemRequest: boolean = false;
  isPublicView: boolean = false;

  result: any;
  isLogin:boolean = false;
  isProblem:boolean = false;

  numberWatch: number = 231;
  numberCandidates: number = 15;


  languages = [{
    language: "Англійська",
    level: "Середня",
  },
    {
      language: "Німецька",
      level: "Початкова",
    },
    {
      language: "Українська",
      level: "Рідна",
    },
    {
      language: "Українська",
      level: "Рідна",
    },
    {
      language: "Українська",
      level: "Рідна",
    },
    {
      language: "Українська",
      level: "Рідна",
    },
  ]

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

  toggleShow(){
    this.showLess = !this.showLess;
  }

  protected readonly handleImageError = handleImageError;

}
