import { Injectable } from '@angular/core';
import {Subject, Subscription} from "rxjs";
import {SearchParamService} from "./search-param.service";
import {MainSearchService} from "./main-search.service";

@Injectable({
  providedIn: 'root'
})
export class FiltersCheckedService {
  private checkboxValues: { [key: string]: boolean } = {};
  checkboxValuesChange: Subject<{ [key: string]: boolean }> = new Subject();
  searchParam: any;
  temporarySearchParam: Subscription;

  constructor(
    private searchParamService: SearchParamService,
    private mainSearchService: MainSearchService,
  ) {
    this.initialize();
  }

  private initialize() {
    this.GetSearchParam();
/*    this.checkboxValuesChange.subscribe(values => {
    });*/
  }


  GetSearchParam(){
    this.temporarySearchParam = this.mainSearchService.getParamObservable().subscribe(params => {
      this.searchParam = params.filter || '';
      //console.log('AAAA', this.searchParam)
      this.updateCheckboxValues(this.searchParam);
    });


/*    let temporarySearchParam = this.mainSearchService.getParamObservable().subscribe((searchParam: string) => {
      console.log("Received search parameters:", searchParam);
      this.searchParam = searchParam.filter || '';
    });*/

/*
    let tempSearchParam = temporarySearchParam.split(',');
    let splitSearchParam = tempSearchParam.map(item => item.split(':').pop() ?? '');

    if (splitSearchParam.length > 0 && splitSearchParam[0].trim() !== '') {
      splitSearchParam.forEach(param => {
        const trimmedParam = param.trim();
        // Якщо checkboxValues вже містить параметр, встановлюємо значення true
        if (this.checkboxValues.hasOwnProperty(trimmedParam)) {
          this.checkboxValues[trimmedParam] = true;
        } else {
          // Якщо параметр відсутній, додаємо його і встановлюємо значення true
          this.checkboxValues[trimmedParam] = true;
        }
      });
      // Оновлюємо Subject з новими значеннями checkboxValues
      this.checkboxValuesChange.next(this.checkboxValues);
    } else {
      // Якщо параметри відсутні або порожні, можна зробити додаткові дії за потреби
      console.log('No search parameters found.');
    }*/
  }

  private updateCheckboxValues(filterParam: string): void {
    const tempSearchParam = filterParam.split(',');
    const checkboxValues: { [key: string]: boolean } = {};

    tempSearchParam.forEach(item => {
      const [type, value] = item.split(':');
      if (value) {
        checkboxValues[value] = true;
      }
    });

    this.checkboxValuesChange.next(checkboxValues);
  }


  /*GetSearchParam(){
    let temporarySearchParam = this.searchParamService.getParamFilters();
    let tempSearchParam = temporarySearchParam.split(',');
    let splitSearchParam = tempSearchParam.map(item => item.split(':').pop() ?? '');

    if (splitSearchParam.length > 0 && splitSearchParam[0].trim() !== '') {
      splitSearchParam.forEach(param => {
        const trimmedParam = param.trim();
        // Якщо checkboxValues вже містить параметр, встановлюємо значення true
        if (this.checkboxValues.hasOwnProperty(trimmedParam)) {
          this.checkboxValues[trimmedParam] = true;
        } else {
          // Якщо параметр відсутній, додаємо його і встановлюємо значення true
          this.checkboxValues[trimmedParam] = true;
        }
      });
      // Оновлюємо Subject з новими значеннями checkboxValues
      this.checkboxValuesChange.next(this.checkboxValues);
    } else {
      // Якщо параметри відсутні або порожні, можна зробити додаткові дії за потреби
      console.log('No search parameters found.');
    }
  }*/

  setCheckboxValue(name: string, value: boolean, type: string): void {
    if(value){
      this.mainSearchService.setParamsFilterOne(name, type);
      //this.searchParamService.setParamFiltersOne(name, type);
    }
    else{
      this.mainSearchService.deleteParamsFilterOne(name, type);
      //this.searchParamService.deleteParamFiltersOne(name, type);
    }
    this.checkboxValues[name] = value;
    this.checkboxValuesChange.next(this.checkboxValues);
  }

  setCheckboxAllValuesToFalse(type: string): void {
    Object.keys(this.checkboxValues).forEach(key => {
      this.setCheckboxValue(key, false, type);
    });

    this.mainSearchService.deleteAllParamsFilterOne();
  }

  getCheckboxValue(name: string): boolean {
    return this.checkboxValues[name] || false;
  }

  getAllCheckboxValues(): { [key: string]: boolean } {
    return this.checkboxValues;
  }
}
