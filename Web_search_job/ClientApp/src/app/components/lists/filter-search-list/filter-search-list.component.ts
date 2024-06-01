import {ChangeDetectorRef, Component} from '@angular/core';
import {Job} from "../../../models/backend/dtos/jobs/job";
import {JobService} from "../../../services/backend/job.service";
import {FilterService} from "../../../services/backend/filter.service";
import {AllFilters} from "../../../models/backend/dtos/filters/allFilters";
import {catchError} from "rxjs";
import {SearchParamService} from "../../../services/search-param.service";

@Component({
  selector: 'app-filter-search-list',
  templateUrl: './filter-search-list.component.html',
  styleUrls: ['./filter-search-list.component.scss']
})


export class FilterSearchListComponent {

  filtersData: AllFilters;
  isFiltersLoaded: boolean = false;
  hasError: boolean = false;
  constructor(
    private filterService: FilterService, private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit() {
    this.filterService
      .GetAllFilters()
      .pipe(
        catchError(() => {
          this.isFiltersLoaded = true;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: AllFilters) => {
        this.filtersData = result;
        this.isFiltersLoaded = true;
        this.hasError = false

        // Manually trigger change detection after async operation completes
        this.cdr.detectChanges();
      });
  }

  protected readonly Object = Object;
}
