import {ChangeDetectorRef, Component, Input} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {RouterHelperService} from "../../../services/router-helper.service";
import { Location } from '@angular/common';
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {JobService} from "../../../services/backend/job.service";
import {Job} from "../../../models/backend/dtos/jobs/job";
import {MatDialog} from "@angular/material/dialog";
import {PopupPurpleComponent} from "../../../components/popup/popup-purple/popup-purple.component";
import {PopupWhiteComponent} from "../../../components/popup/popup-white/popup-white.component";
import {AuthService} from "../../../services/backend/auth/auth-service";
import {catchError} from "rxjs";
import {EmployerDTO} from "../../../models/backend/dtos/employers/employer-full.dto";
import {JobDTO} from "../../../models/backend/dtos/jobs/job.dto";
import {UserService} from "../../../services/backend/user.service";
import {UserData} from "../../../services/backend/auth/dtos/user-data";
import {ActivatedRoute} from "@angular/router";


@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.scss'],
})
export class JobDetailComponent {
  job: JobDTO;

  isSmallScreen: boolean = false;
  isSaved: boolean = false;
  isProblemRequest: boolean = false;
  isLoaded: boolean = false;
  hasError: boolean = false;
  result: any;
  isLogin:boolean = false;
  isProblem:boolean = false;
  firstRequest:boolean = true;

  userData: UserData | null = null;
  numberWatch: number = 231;
  numberCandidates: number = 15;

  constructor(
    private routerHelper: RouterHelperService,
    private location: Location,
    private screenSizeService: ScreenSizeService,
    private jobService: JobService,
    public dialog: MatDialog,
    private authService: AuthService,
    private cdr: ChangeDetectorRef,
    private userService: UserService,
    private route: ActivatedRoute,

  ) {
    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isSmallScreen = isSmall;
      console.log(isSmall);
    });

  }


  ngOnInit(){
    this.IsAuthUser();
    this.userService.getUserData().subscribe(userData => {
      this.userData = userData;
      const id = +this.route.snapshot.paramMap.get('id')!;
      if(this.firstRequest){
        this.firstRequest = false;
        if (this.userData) {
          this.getJob(id, this.userData.id);
        }
        else {
          this.getJob(id, null);
        }
      }
    });
  }

  getJob(id: number, userId: string | null): void {
    this.jobService
      .getJobById(id, userId)
      .pipe(
        catchError(() => {
          this.isLoaded = false;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: JobDTO) => {
        this.job = result;
        this.isSaved = this.job.isSavedJob;
        console.log(this.isSaved)
        console.log(this.job.isSavedJob)

        this.isLoaded = true;
        this.hasError = false

        console.log(this.job);
        console.log(result);

        this.cdr.detectChanges();
      });


    /*  this.employerService.getEmployerById(id, userId).subscribe(
        (employer) => this.employer = employer,
        (error) => console.error(error)
      );*/

  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  saveJob() {
    if(this.userData){
      this.firstRequest = true;
      this.isSaved = !this.isSaved;
      this.jobService.updateSavedJob(this.userData.id , this.job.id, this.isSaved).subscribe(
        response => {
          console.log('Успішно збережено', response);
        },
        error => {
          console.error('Помилка збереження', error);
        }
      );
    }

  }

  goBack(): void {
    this.location.back();
  }

  openDialog(): void {
    if(this.isLogin){
      const dialogRef = this.dialog.open(PopupPurpleComponent, {
        width: '1000px',
        data: {type: "JobRequest", job: this.job, title: "Відгук на вакансію"}
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
      console.log(result);
      if(result == "TryAgain"){
        this.createJobRequest();
      }
    });
  }


  IsAuthUser(): void {
    this.authService.isLoggedIn().subscribe(isLoggedIn => {
      this.isLogin = isLoggedIn;
    });
  }

  goToURLCompany() {
    var idCompany = this.job.employer.id;
    console.log(idCompany)
    this.routerHelper.goToUrl('/employer/'+ idCompany, true);
  }

  navigateToExternalUrl(url: string): void {
    window.open(url, '_blank');
  }

  formatDescription(text: string): string {
    // Заміна форматованих тегів на відповідний текст і збереження стилів
    text = text.replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>'); // Заміна жирного тексту
    text = text.replace(/\*(.*?)\*/g, '<em>$1</em>'); // Заміна курсивного тексту
    // Додаткові правила для інших форматувань можуть бути додані, якщо потрібно

    // Заміна нових рядків на HTML теги для переносу рядків
    text = text.replace(/\\n/g, '<br>');

    return text;
  }

  protected readonly handleImageError = handleImageError;
}
