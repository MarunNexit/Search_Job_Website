import {Component, Input} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {RouterHelperService} from "../../../services/router-helper.service";
import { Location } from '@angular/common';
import {ScreenSizeService} from "../../../services/screen-size.sevice";


@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.scss'],
})
export class JobDetailComponent {
  dataJob: any;

  isSmallScreen: boolean = false;
  isSaved: boolean = false;

  numberWatch: number = 231;
  numberCandidates: number = 15;


  dataJob1 = {
    id: 1,
    title: "Розробник програмного забезпечення",
    salary: "90000 USD",
    company: "ABC Inc.",
    description: "Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробникаОпис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення",
    tags: ["IT", "Програмування", "Розробка"],
    company_picture: "../../../../assets/img/icons/cards/check_mark.png",
    banner_picture: "url/to/banner_picture_1.jpg",
    hot_new_marks: [true, true, false]
  };

  constructor(
    private routerHelper: RouterHelperService,
    private location: Location,
    private screenSizeService: ScreenSizeService,
  ) {
    this.dataJob = this.dataJob1;

    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isSmallScreen = isSmall;
      console.log(isSmall);
    });

  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  saveJob() {
    this.isSaved = !this.isSaved;
  }

  goBack(): void {
    this.location.back();
  }


  protected readonly handleImageError = handleImageError;
}
