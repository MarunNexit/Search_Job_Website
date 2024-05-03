import {Component, Input} from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";
import {Location} from "@angular/common";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {handleImageError} from "../../../functions/handleImageError";

@Component({
  selector: 'app-employer-page',
  templateUrl: './employer-page.component.html',
  styleUrls: ['./employer-page.component.scss']
})
export class EmployerPageComponent {
  dataEmployer: any;
  employer_checked: boolean = true;
  arrowPossition: boolean[] = [false, false]

  isSmallScreen: boolean = false;
  isSaved: boolean = false;
  isAppropriateness: boolean[] = [false, true, true, true];
  isAppropriatenessAll: boolean = false;
  numberWatch: number = 231;
  numberCandidates: number = 15;
  expandField: boolean = true;



  dataEmployer1 = {
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
    this.dataEmployer = this.dataEmployer1;

    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isSmallScreen = isSmall;
      console.log(isSmall);
    });

  }

  ngOnInit(): void {
    this.checkAllAppropriateness();
  }

  checkAllAppropriateness() {
    this.isAppropriatenessAll = this.isAppropriateness.every(value => value);
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

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = target.value;
    console.log('Selected value:', selectedValue);
  }



  protected readonly handleImageError = handleImageError;
}
