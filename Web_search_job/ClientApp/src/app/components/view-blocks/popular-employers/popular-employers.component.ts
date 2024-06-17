import {ChangeDetectorRef, Component, Input} from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";
import {AllFilters} from "../../../models/backend/dtos/filters/allFilters";
import {FilterService} from "../../../services/backend/filter.service";
import {catchError, of} from "rxjs";
import {EmployerShortDTO} from "../../../models/backend/dtos/employers/employer-short.dto";
import {EmployerService} from "../../../services/backend/employer.service";
import {ActivatedRoute, Router} from "@angular/router";
import {IdFromUrlService} from "../../../services/id-from-url.service";


@Component({
  selector: 'app-popular-employers',
  templateUrl: './popular-employers.component.html',
  styleUrls: ['./popular-employers.component.scss']
})
export class PopularEmployersComponent {
  @Input() isPage: boolean = false;

  employerData: EmployerShortDTO[];
  countEmployers: number = 0;
  isLoaded: boolean = false;
  hasError: boolean = false;

  //CHANGE DATA
  totalElements: number = 0;
  elementsPerPage: number = 9;
  currentPage: number = 1;

  handlePageChange(newPage: number) {
    this.currentPage =  newPage;
  }

  constructor(
    private employerService: EmployerService, private cdr: ChangeDetectorRef, private routerHelper: RouterHelperService,
  ) {
  }

  ngOnInit() {
    if(this.isPage){
      this.employerService
        .GetCountEmployers()
        .pipe(
          catchError(() => {
            this.hasError = true;
            return [] // Повертаємо Observable зі значенням null
          })
        )
        .subscribe((result: number) => {
          if (result !== null) {
            this.totalElements = result;
            this.countEmployers = Math.ceil(result / 9);
            this.hasError = false;
            // Manually trigger change detection after async operation completes
            this.cdr.detectChanges();

            this.employerService
              .GetShortEmployers(this.currentPage, 18)
              .pipe(
                catchError(() => {
                  this.isLoaded = true;
                  this.hasError = true;
                  return []
                })
              )
              .subscribe((result: EmployerShortDTO[]) => {
                this.employerData = result;
                this.isLoaded = true;
                this.hasError = false

                // Manually trigger change detection after async operation completes
                this.cdr.detectChanges();
              });
          }
        });
    }
    else{
      this.employerService
        .GetSixShortEmployers()
        .pipe(
          catchError(() => {
            this.isLoaded = true;
            this.hasError = true;
            return []
          })
        )
        .subscribe((result: EmployerShortDTO[]) => {
          this.employerData = result;
          this.isLoaded = true;
          this.hasError = false

          // Manually trigger change detection after async operation completes
          this.cdr.detectChanges();
        });
    }

  }


  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }


}
