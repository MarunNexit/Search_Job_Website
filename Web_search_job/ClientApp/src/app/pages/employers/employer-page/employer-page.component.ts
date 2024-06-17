import {ChangeDetectorRef, Component, ElementRef, Input, ViewChild} from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";
import {Location} from "@angular/common";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {handleImageError} from "../../../functions/handleImageError";
import {EmployerDTO} from "../../../models/backend/dtos/employers/employer-full.dto";
import {ActivatedRoute} from "@angular/router";
import {EmployerService} from "../../../services/backend/employer.service";
import {UserData} from "../../../models/backend/dtos/auth/dtos/user-data";
import {UserService} from "../../../services/backend/user.service";
import {EmployerTagDTO} from "../../../models/backend/dtos/employers/employer-tag.dto";
import {catchError} from "rxjs";
import {AllFilters} from "../../../models/backend/dtos/filters/allFilters";

@Component({
  selector: 'app-employer-page',
  templateUrl: './employer-page.component.html',
  styleUrls: ['./employer-page.component.scss']
})
export class EmployerPageComponent {
  employer: EmployerDTO | undefined;
  userId: number = 1;
  @ViewChild('parentDiv') parentDiv: ElementRef;

  isLoaded = false;
  hasError = false;

  employer_checked: boolean = true;
  arrowPossition: boolean[] = [false, false]

  isSmallScreen: boolean = false;
  isSaved: boolean = false;
  isAppropriateness: boolean[] = [false, true, true, true];
  isAppropriatenessAll: boolean = false;

  numberWatch: number = 231;
  numberCandidates: number = 15;

  expandField: boolean = true;

  userData: UserData | null = null;


  constructor(
    private routerHelper: RouterHelperService,
    private location: Location,
    private screenSizeService: ScreenSizeService,
    private route: ActivatedRoute,
    private employerService: EmployerService,
    private userService: UserService,
    private cdr: ChangeDetectorRef,
  ) {
    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isSmallScreen = isSmall;
      console.log(isSmall);
    });

  }

  ngOnInit(): void {
    this.userService.getUserData().subscribe(userData => {
      this.userData = userData;
      const id = +this.route.snapshot.paramMap.get('id')!;
      if (this.userData) {
        this.getEmployer(id, this.userData.userId);
      }
      else {
        this.getEmployer(id, null);
      }
    });

    this.checkAllAppropriateness();
  }


  getEmployer(id: number, userId: string | null): void {

    this.employerService
      .getEmployerById(id, userId)
      .pipe(
        catchError(() => {
          this.isLoaded = false;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: EmployerDTO) => {
        this.employer = result;
        this.isLoaded = true;
        this.hasError = false

        console.log(this.employer);
        console.log(result);

        //this.employer.companyDescription = this.formatDescription(this.employer.companyDescription);
        // Manually trigger change detection after async operation completes
        this.cdr.detectChanges();
      });


  /*  this.employerService.getEmployerById(id, userId).subscribe(
      (employer) => this.employer = employer,
      (error) => console.error(error)
    );*/

  }


  checkAllAppropriateness() {
    this.isAppropriatenessAll = this.isAppropriateness.every(value => value);
  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  navigateToExternalUrl(url: string): void {
    window.open(url, '_blank');
  }

  goBack(): void {
    this.location.back();
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

  public itemHasIntensities = (item: EmployerTagDTO): boolean => item.tagType === 'about';

  hasProsTag(): boolean {
    var temp = this.employer && this.employer.tags && this.employer.tags.some(tag => tag.tagType === 'pros');
    if(temp){
      return temp;
    }
    return false;
  }

  protected readonly handleImageError = handleImageError;
}
