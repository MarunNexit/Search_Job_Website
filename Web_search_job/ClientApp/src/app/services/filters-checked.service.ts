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

  }

  private updateCheckboxValues(filterParam: string): void {
    const tempSearchParam = filterParam.split(',');
    const checkboxValues: { [key: string]: boolean } = {};

    tempSearchParam.forEach(item => {
      const [type, value] = item.split(':');
      if (value) {
        this.checkboxValues[value] = true;
      }
    });
    this.checkboxValuesChange.next(this.checkboxValues);
  }


  setCheckboxValue(name: string, value: boolean, type: string): void {
    if(value){
      this.mainSearchService.setParamsFilterOne(name, type);
      //this.searchParamService.setParamFiltersOne(name, type);
    }
    else{
      this.mainSearchService.deleteParamsFilterOne(name, type);
      //this.searchParamService.deleteParamFiltersOne(name, type);
    }

    console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", name, value, type)
    this.checkboxValues[name] = value;
    console.log(this.checkboxValues)
    this.checkboxValuesChange.next(this.checkboxValues);
  }

  setCheckboxAllValuesToFalse(type: string): void {
    Object.keys(this.checkboxValues).forEach(key => {
      this.checkboxValues[key] = false;
    });
    this.checkboxValuesChange.next(this.checkboxValues);

    this.mainSearchService.deleteAllParamsFilterOne();
  }

  getCheckboxValue(name: string): boolean {
    return this.checkboxValues[name] || false;
  }

  getAllCheckboxValues(): { [key: string]: boolean } {
    return this.checkboxValues;
  }
}
