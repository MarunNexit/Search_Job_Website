import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class ChatsService {

  sortParam: string;
  filterParam: string;
  param: string;

  private readonly localStorageKey = 'ChatParam';

  constructor() { }

  public setParam(sort: string[], filter: string[]): void {
    this.sortParam = sort.join(",");
    this.filterParam = filter.join(",");
    this.param = this.sortParam + "|" + this.filterParam;
    localStorage.setItem(this.localStorageKey, this.param);
  }

  public setParamSorting(sort: string[]): void {
    this.filterParam = this.getParamFilters();
    this.sortParam = sort.join(",");
    this.param = this.sortParam + "|" + this.filterParam;
    localStorage.setItem(this.localStorageKey, this.param);
  }

  public setParamFilters(filter: string[]): void {
    this.sortParam = this.getParamSorting();
    this.filterParam = filter.join(",");
    this.param = this.sortParam + "|" + this.filterParam;
    localStorage.setItem(this.localStorageKey, this.param);
  }

  public getParamSorting(): string {
    let searchParam = this.getParam();
    if(searchParam != ''){
      let searchParamList = searchParam.split('|')
      return searchParamList[0];
    }
    else{
      return '';
    }
  }

  public getParamFilters(): string {
    let searchParam = this.getParam();
    if(searchParam != ''){
      let searchParamList = searchParam.split('|')
      return searchParamList[1];
    }
    else{
      return '';
    }
  }


  public getParam(): string {
    return localStorage.getItem(this.localStorageKey) || '';
  }
}
