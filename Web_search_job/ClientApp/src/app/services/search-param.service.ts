import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})

export class SearchParamService {
  // OLD CODE

  /*sortParam: string;
  filterParam: string;
  param: string;

  private readonly localStorageKey = 'SearchParam';
  private readonly localStorageSearchBarKey = 'SearchBar';

  private searchBarItems: BehaviorSubject<string> = new BehaviorSubject<string>(''); // Поточні відфільтровані елементи
  private searchParamSubject: BehaviorSubject<string>;

  initial(): void {
    let item = localStorage.getItem(this.localStorageSearchBarKey);
    this.addSearchBarItem(item);
  }

  getSearchBarItemObservable() {
    return this.searchBarItems.asObservable();
  }

  addSearchBarItem(item: string | null): void {
    if(item != null && item != ' ' && item != ''&& item != 'NULL'){
      this.searchBarItems.next(item);
      localStorage.setItem(this.localStorageSearchBarKey, item);
    }
  }

  getParamObservable() {
    return this.searchParamSubject.asObservable();
  }

  constructor() {
    const initialParam = this.getParam();
    this.searchParamSubject = new BehaviorSubject<string>(initialParam);
    this.initial();
  }

/!*  public setParams(sort: string[], filter: string[]): void {
    this.sortParam = sort.join(",");
    this.filterParam = filter.join(",");
    this.param = this.sortParam + "|" + this.filterParam;
    localStorage.setItem(this.localStorageKey, this.param);
    this.searchParamSubject.next(this.param);
  }*!/

  public setParamSorting(sort: string[]): void {
    this.filterParam = this.getParamFilters();
    this.sortParam = sort.join(",");
    this.param = this.sortParam + "|" + this.filterParam;
    localStorage.setItem(this.localStorageKey, this.param);
    this.searchParamSubject.next(this.param);
  }

/!*  public setParamFilters(filter: string[]): void {
    this.sortParam = this.getParamSorting();
    this.filterParam = filter.join(",");
    this.param = this.sortParam + "|" + this.filterParam;
    localStorage.setItem(this.localStorageKey, this.param);
    this.searchParamSubject.next(this.param);
  }*!/

  public setParamFiltersOne(filter: string, type: string): void {
    this.sortParam = this.getParamSorting();
    this.filterParam = this.getParamFilters();

    let filters = this.filterParam.split(',')
    console.log(filters);

    if(filter != null && filter != ""){
      if(!filters.includes(type + ':' + filter)){
        if(this.filterParam == "" || this.filterParam == null || this.filterParam == " "){
          this.filterParam = type + ':' + filter;
        }
        else{
          this.filterParam = this.filterParam + "," + type + ':' + filter;
        }
      }
    }
    this.param = this.sortParam + "|" + this.filterParam;
    localStorage.setItem(this.localStorageKey, this.param);
    this.searchParamSubject.next(this.param);
  }

  public deleteParamFiltersOne(filter: string, type: string): void {
    this.sortParam = this.getParamSorting();
    this.filterParam = this.getParamFilters();

    if (filter != null && filter != "") {
      // Розділяємо filterParam на масив
      let filterArray = this.filterParam.split(',');

      console.log(filter, type, filterArray)


      // Знаходимо індекс елемента для видалення
      let index = filterArray.indexOf(type + ":" + filter);

      if(type == 'all' || type == 'chip'){
        console.log('all + chip')
        index = filterArray.findIndex(item => {
          const parts = item.split(':');
          console.log(parts[1], filter)
          return parts.length > 1 && parts[1] === filter;
        });
      }

      // Якщо елемент знайдено, видаляємо його
      if (index > -1) {
        filterArray.splice(index, 1);
      }

      // З'єднуємо масив назад у рядок через кому
      this.filterParam = filterArray.join(',');
    }

    // Створюємо новий параметр з відфільтрованими значеннями
    this.param = this.sortParam + "|" + this.filterParam;

    // Зберігаємо новий параметр у localStorage
    localStorage.setItem(this.localStorageKey, this.param);
    this.searchParamSubject.next(this.param);
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
    return localStorage.getItem(this.localStorageKey) || '|';
  }*/
}
