import {Component, Input} from '@angular/core';
import {PopupPurpleComponent} from "../../popup/popup-purple/popup-purple.component";
import {PopupWhiteComponent} from "../../popup/popup-white/popup-white.component";
import {RouterHelperService} from "../../../services/router-helper.service";
import {DatePipe, Location} from "@angular/common";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {JobService} from "../../../services/backend/job.service";
import {MatDialog} from "@angular/material/dialog";
import {AuthService} from "../../../models/backend/dtos/auth/auth-service";
import {handleImageError} from "../../../functions/handleImageError";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {ResumeDTO} from "../../../models/backend/dtos/profiles/resume.dto";
import {UserService} from "../../../services/backend/user.service";
import {UserData} from "../../../models/backend/dtos/auth/dtos/user-data";
import { differenceInYears, differenceInMonths, addYears } from 'date-fns';
import {ResumeEducationDTO} from "../../../models/backend/dtos/profiles/resume-education.dto";

@Component({
  selector: 'app-resume-section',
  templateUrl: './resume-section.component.html',
  styleUrls: ['./resume-section.component.scss'],
  providers: [DatePipe],
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

  @Input() resume: ResumeDTO;
  @Input() userData: UserData | null;
  @Input() isMyProfile: boolean = false;

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

  wantedSalary: string[] | undefined;

  links: string[] = ['','','',''];


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
    private datePipe: DatePipe,
  ) {
    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isSmallScreen = isSmall;
      console.log(isSmall);
    });
  }


  ngOnInit(){
    console.log('this.userData', this.userData)

    this.resume.resumeLinks
      .forEach(link =>
        this.setDataResume(link.link, link.accountName, link.type)
    );

    this.wantedSalary = this.resume.wantedSalary?.split(':')
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


  hasLinkType(type: string): boolean {
    return this.resume && this.resume.activeResumeSection &&
      this.resume.resumeLinks.some(link => link.type && link.type === type);
  }

  hasLinkName(type: string): boolean {
    return this.resume && this.resume.activeResumeSection &&
      this.resume.resumeLinks.some(link => link.type && link.type === type && link.accountName != null && link.accountName != '');
  }

  setDataResume(url: string, name: string, type: string): boolean {
    if(name){
      if(type == 'dribble'){
        this.links[0] = name;
      }
      else if(type == 'behance'){
        this.links[1] = name;
      }
      else if(type == 'facebook'){
        this.links[2] = name;
      }
      else if(type == 'linkedin'){
        this.links[3] = name;
      }
      return true;
    }
    else{
      if(type == 'dribble'){
        this.links[0] = '';
      }
      else if(type == 'behance'){
        this.links[1] = '';
      }
      else if(type == 'facebook'){
        this.links[2] = '';
      }
      else if(type == 'linkedin'){
        this.links[3] = '';
      }
      return false;
    }
  }

  navigateToExternalUrlLink(type: string): void {
    this.resume.resumeLinks
      .filter(link => link.type === type)  // Фільтруємо лінки за типом
      .forEach(link => window.open(link.link, '_blank'));  // Відкриваємо кожне посилання в новій вкладці
  }


  formatDescription(text: string | undefined): string {
    if(text != undefined){
      text = text.replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>'); // Заміна жирного тексту
      text = text.replace(/\*(.*?)\*/g, '<em>$1</em>'); // Заміна курсивного тексту
      // Додаткові правила для інших форматувань можуть бути додані, якщо потрібно

      text = text.replace(/\\n/g, '<br>');
      return text;
    }
    else{
      return ''
    }
  }
  navigateToExternalUrl(url: string | undefined) {
    if(url != undefined){
      window.open(url, '_blank');
    }
  }


  getDifferenceInYearsAndMonths(startDate: Date, endDate: Date): string {
    // Різниця в роках
    const years = differenceInYears(endDate, startDate);

    // Дата через кількість років від початкової дати
    const dateAfterYears = addYears(startDate, years);

    // Різниця в місяцях
    const months = differenceInMonths(endDate, dateAfterYears);

    let year_str = '';
    let month_str = '';

    if(years > 0){
      if(years == 1){
        year_str = `${years} рік`;
      }
      else if(years < 5){
        year_str = `${years} роки`;
      }
      else {
        year_str = `${years} років`;
      }
    }

    if(months > 0){
      if(months == 1){
        month_str = `${months} місяць`;
      }
      else if(months < 5){
        month_str = `${months} місяці`;
      }
      else {
        month_str = `${months} місяців`;
      }
    }
    return `${year_str} ${month_str}`;
  }

  formatDate(date: Date): string {
    return this.datePipe.transform(date, 'yyyy.MM.dd') || '';
  }

  getEducationData(type: string, education: string){
    const splitEducation = education.split(':')
    if(type === 'place'){
      return splitEducation[0]
    }
    else {
      return splitEducation[1]
    }
  }
  protected readonly handleImageError = handleImageError;

  protected readonly Number = Number;
}
