import {Component, ElementRef, HostListener, Input, SimpleChanges, ViewChild} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {HomeModule} from "../../../pages/home/home.module";
import {PipeModule} from "../../../pipes/pipes.module";
import {CurrencyPipe, NgIf, NgOptimizedImage} from "@angular/common";
import {RouterLink} from "@angular/router";
import {RouterHelperService} from "../../../services/router-helper.service";
import {ChipsModule} from "../../chips/chips.module";
import {Job} from "../../../models/backend/dtos/jobs/job";
import {JobEmployerShortDTO} from "../../../models/backend/dtos/jobs/job-employer-short.dto";
import {EmployerDTO} from "../../../models/backend/dtos/employers/employer-full.dto";
import {JobShortDTO} from "../../../models/backend/dtos/jobs/job-short.dto";
import {UserData} from "../../../models/backend/dtos/auth/dtos/user-data";
import {JobService} from "../../../services/backend/job.service";
import {JobRequestDTO} from "../../../models/backend/dtos/jobs/job-request.dto";



@Component({
  selector: 'app-job-short-card',
  templateUrl: './job-short-card.component.html',
  styleUrls: ['./job-short-card.component.scss'],
  standalone: true,
  imports: [MatButtonModule, MatMenuModule, MatIconModule, PipeModule, NgIf, RouterLink, ChipsModule, NgOptimizedImage, CurrencyPipe],
})

export class JobShortCardComponent {
  @Input() job: JobShortDTO | any;
  @Input() shortVersionJob: boolean = false;
  @Input() isEmployerPage: boolean = false;
  @Input() isSearchPage: boolean = false;
  @Input() isHistoryPage: boolean = false;
  @Input() isRecommendation: boolean = false;
  @Input() isSavedPage: boolean = false;
  @Input() isSimilarPage: boolean = false;
  @Input() employer: EmployerDTO;
  @Input() userData: UserData | null = null;
  @Input() requestData: JobRequestDTO | null = null;
  @Input() requestDataFinished: boolean = false;

  constructor(
    private routerHelper: RouterHelperService,
    private jobService: JobService,
  ) {
  }

  ngOnInit(){
    console.log(this.requestData);
    this.isSaved = this.job.isSavedJob;
  }

  isSaved: boolean = false;

  protected readonly handleImageError = handleImageError;
  protected readonly window = window;

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  saveJob() {
    if (this.userData) {
      this.isSaved = !this.isSaved;
      this.jobService.updateSavedJob(this.userData.userId, this.job.id, this.isSaved).subscribe(
        response => {
          console.log('Успішно збережено', response);
        },
        error => {
          console.error('Помилка збереження', error);
        }
      );
    }
  }

  handleButtonClick(event: MouseEvent) {
    event.stopPropagation();
  }


  timeSince(dateApprove: Date | undefined): string {
    if(dateApprove){
      const date = new Date(dateApprove);
      const seconds = Math.floor((new Date().getTime() - date.getTime()) / 1000);

      let interval = Math.floor(seconds / 31536000);

      if (interval > 1) {
        return `${interval} роки`;
      }
      interval = Math.floor(seconds / 2592000);
      if (interval > 1) {
        if(interval > 5){
          return `${interval} місяців`;
        }
        else{
          return `${interval} місяці`;
        }
      }
      interval = Math.floor(seconds / 86400);
      if (interval > 1) {
        if(interval > 5){
          return `${interval} днів`;
        }
        else{
          return `${interval} дні`;
        }
      }
      interval = Math.floor(seconds / 3600);
      if (interval > 1) {
        if(interval > 5){
          return `${interval} годин`;
        }
        else{
          return `${interval} години`;
        }
      }
      interval = Math.floor(seconds / 60);
      if (interval > 1) {
        if(interval > 5){
          return `${interval} хвилин`;
        }
        else{
          return `${interval} хвилини`;
        }
      }
      return `${Math.floor(seconds)} секунди`;
    }
    return '';
  }

  CompanyNameButton(event: MouseEvent){
    event.stopPropagation();
    if (this.employer != null && this.employer.id != 0){
      this.goToURL('/employer/' + 1, true)
    }
    else{
      this.goToURL('/employer/' + 2, true)
    }
  }

  protected readonly console = console;
}

