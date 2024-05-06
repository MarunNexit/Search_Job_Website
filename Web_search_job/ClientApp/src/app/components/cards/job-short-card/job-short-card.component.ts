import {Component, ElementRef, HostListener, Input, ViewChild} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {HomeModule} from "../../../pages/home/home.module";
import {PipeModule} from "../../../pipes/pipes.module";
import {NgIf} from "@angular/common";
import {RouterLink} from "@angular/router";
import {RouterHelperService} from "../../../services/router-helper.service";
import {ChipsModule} from "../../chips/chips.module";



@Component({
  selector: 'app-job-short-card',
  templateUrl: './job-short-card.component.html',
  styleUrls: ['./job-short-card.component.scss'],
  standalone: true,
  imports: [MatButtonModule, MatMenuModule, MatIconModule, HomeModule, PipeModule, NgIf, RouterLink, ChipsModule],
})

export class JobShortCardComponent {
  @Input() dataJob: any;
  @Input() shortVersionJob: boolean = false;
  @Input() isRecommendation: boolean = false;

  constructor(
    private routerHelper: RouterHelperService,
  ) {

  }

  IsSaved: boolean = false;

  protected readonly handleImageError = handleImageError;
  protected readonly window = window;

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  saveJob() {
    this.IsSaved = !this.IsSaved;
  }


  handleButtonClick(event: MouseEvent) {
    event.stopPropagation();
  }

  protected readonly console = console;
}

interface DataJobInterface {
  id: number;
  title: string;
  salary:string;
  company:string;
  description: string;
  tags: string[];
  company_picture:string;
  banner_picture:string;
  hot_new_marks:boolean[];

}


