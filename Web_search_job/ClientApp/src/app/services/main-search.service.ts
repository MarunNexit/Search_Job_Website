
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {ActivatedRoute, NavigationExtras, Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class MainSearchService {
  private readonly searchParamSubject: BehaviorSubject<any>;

  constructor(private router: Router, private route: ActivatedRoute) {
    const initialParam = this.getParamFromUrl();
    this.searchParamSubject = new BehaviorSubject<any>(initialParam);
    console.log("initialParam consructor")
  }

  setParams(sort: string[], filter: string[], request: string): void {
    const searchParam = {
      sort: sort.join(","),
      filter: filter.join(","),
      request: request,
    };
    this.updateUrl(searchParam);
  }

  setParamsSort(sort: string[]): void {
    const currentParams = this.route.snapshot.queryParams;
    const searchParam = {
      sort: sort.join(","),
      filter: currentParams['filter'] || '',
      request: currentParams['request'] || ''
    };
    this.updateUrl(searchParam);
  }

  setParamsFilter(filter: string[]): void {
    const currentParams = this.route.snapshot.queryParams;
    const searchParam = {
      sort: currentParams['sort'] || '',
      filter: filter.join(","),
      request: currentParams['request'] || ''
    };
    this.updateUrl(searchParam);
  }

  setParamsFilterOne(filter: string, type: string): void {
    const currentParams = this.route.snapshot.queryParams;
    if (filter && filter !== '') {
      let existingFilters = currentParams['filter'] ? currentParams['filter'].split(',') : [];
      const newFilter = `${type}:${filter}`;

      // Check if the new filter already exists
      if (!existingFilters.includes(newFilter)) {
        existingFilters.push(newFilter);

        const searchParam = {
          sort: currentParams['sort'] || '',
          filter: existingFilters.join(','),
          request: currentParams['request'] || ''
        };

        this.updateUrl(searchParam);
      }
    }
  }


  deleteParamsFilterOne(filter: string, type: string): void {
    const currentParams = this.route.snapshot.queryParams;
    let filters = currentParams['filter'] ? currentParams['filter'].split(',') : [];

    // Remove the filter if it exists
    if(type != 'chip'){
      const filterToRemove = `${type}:${filter}`;
      filters = filters.filter((f: string) => f !== filterToRemove);

      const searchParam = {
        sort: currentParams['sort'] || '',
        filter: filters.join(','),
        request: currentParams['request'] || ''
      };

      this.updateUrl(searchParam);
    }
    else {
      const filterToRemove = `${filter}`;
      filters = filters.filter((f: string) => !f.endsWith(":" + filterToRemove));

      const searchParam = {
        sort: currentParams['sort'] || '',
        filter: filters.join(','),
        request: currentParams['request'] || ''
      };

      this.updateUrl(searchParam);
    }
  }


  deleteAllParamsFilterOne(): void {
    const currentParams = this.route.snapshot.queryParams;

    const searchParam = {
      sort: currentParams['sort'] || '',
      filter: '',
      request: currentParams['request'] || ''
    };

    this.updateUrl(searchParam);
  }

  setParamsRequest(request: string): void {
    const currentParams = this.route.snapshot.queryParams;
    const searchParam = {
      sort: currentParams['sort'] || '',
      filter: currentParams['filter'] || '',
      request: request
    };
    this.updateUrl(searchParam);
  }

  getParamSort(): string {
    const queryParams = this.route.snapshot.queryParams;
    return queryParams['sort'] || '';
  }

  getParamFilter(): string {
    const queryParams = this.route.snapshot.queryParams;
    return queryParams['filter'] || '';
  }

  getParamRequest(): string {
    const queryParams = this.route.snapshot.queryParams;
    return queryParams['request'] || '';
  }

  private updateUrl(searchParam: any): void {
    const navigationExtras: NavigationExtras = {
      queryParams: searchParam
    };
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: searchParam
    }).then(() => {
      this.searchParamSubject.next(searchParam);
    });
  }

  private getParamFromUrl(): string {
    const queryParams = this.route.snapshot.queryParams;
    const sort = queryParams['sort'] || '';
    const filter = queryParams['filter'] || '';
    const request = queryParams['request'] || '';
    return `${sort}|${filter}|${request}`;
  }

  getParamObservable() {
    return this.searchParamSubject.asObservable();
  }

}
