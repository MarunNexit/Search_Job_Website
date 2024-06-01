import {Component, Input} from '@angular/core';
import {MatCheckboxChange, MatCheckboxModule} from "@angular/material/checkbox";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AsyncPipe, NgClass, NgForOf, NgIf} from "@angular/common";
import {JsonPipe} from '@angular/common';
import {expandCollapse} from "./animation_filter_card";
import {FiltersCheckedService} from "../../../services/filters-checked.service";
import {Observable, Subscription} from "rxjs";
import {SearchParamService} from "../../../services/search-param.service";
import {SearchBarService} from "../../../services/search-bar.service";
import {async} from "@angular/core/testing";
import {filter} from "rxjs/operators";

interface Filter {
  name: string;
  // Інші поля, які можуть бути у вашому об'єкті фільтру
}

@Component({
  selector: 'app-filter-search-card',
  templateUrl: './filter-search-card.component.html',
  styleUrls: ['./filter-search-card.component.scss'],
  standalone: true,
  imports: [MatCheckboxModule, FormsModule, NgForOf, ReactiveFormsModule, JsonPipe, NgIf, NgClass, AsyncPipe],
  animations: [expandCollapse] // Додайте анімацію до списку анімацій
})
export class FilterSearchCardComponent {
  @Input() dataFilters: any;
  @Input() addingSearchField: boolean;
  @Input() nameTopic: string;

  type: string = '';

  private subscription: Subscription;
  filteredItems$: Observable<string[]>;

  expandFilterCard: boolean = true;
  filters_status = this._formBuilder.group({});

  constructor(
    private _formBuilder: FormBuilder,
    private filtersCheckedService: FiltersCheckedService,
    private searchBarService: SearchBarService,

  ) {}

  GetType(){
    if(this.nameTopic == 'Характер роботи') {
      this.type = "character"
    }
    else if(this.nameTopic == 'Професійна сфера'){
      this.type = "industry"
    }
    else if(this.nameTopic == 'Розташування'){
      this.type = "location"
    }
    else if(this.nameTopic == 'Рівень зарплати'){
      this.type = "salary"
    }
    else if(this.nameTopic == 'Рівень необхідного досвіду'){
      this.type = "experience"
    }
    else if(this.nameTopic == 'Тип зайнятості'){
      this.type = "type-work"
    }
    else if(this.nameTopic == 'Пошуковий запит'){
      this.type = "search-request"
    }
    else
    {
      this.type = "other"
    }
  }

  ngOnInit(): void {
    this.GetType()

    if (this.dataFilters) {
      this.dataFilters.forEach((filter: { name: string, checked: boolean }) => {
        this.filters_status.addControl(this.getFilterFormControlName(filter), this._formBuilder.control(false));

        const checked = this.filtersCheckedService.getCheckboxValue(this.getFilterFormControlName(filter));
        this.filtersCheckedService.setCheckboxValue(this.getFilterFormControlName(filter), checked, this.type);

      });
    }

    this.subscription = this.filtersCheckedService.checkboxValuesChange.subscribe((checkboxValues) => {
      this.updateFiltersStatus(checkboxValues);
    });

    if (this.dataFilters) {
      this.dataFilters.forEach((filter: Filter) => {
        this.searchBarService.addItem(this.getFilterFormControlName(filter), this.nameTopic);
      });
    }

    this.filteredItems$ = this.searchBarService.getFilteredItems(this.nameTopic);

  }

  ngOnDestroy(): void {
    // Відписуємося від підписки під час знищення компонента
    this.subscription.unsubscribe();
  }

  private CreateFilters(checkboxValues: { [key: string]: boolean }): void {
    /*// Ітеруємось по ключам об'єкта checkboxValues
    for (const key in checkboxValues) {
      if (checkboxValues.hasOwnProperty(key)) {
        // Додаємо елемент до сервісу за допомогою кожного ключа
        this.searchBarService.addItem(key);
      }
    }*/
  }

  private updateFiltersStatus(checkboxValues: { [key: string]: boolean }): void {
    Object.keys(checkboxValues).forEach((key) => {
      const control = this.filters_status.get(key);
      if (control) {
        control.setValue(checkboxValues[key]);
      }
    });
  }

  getFilterFormControlName(filter: any): any {
    if(filter != undefined) {

      if (filter.location_region) {
        return filter.location_region.toString();
      } else if (filter.industry_name) {
        return filter.industry_name.toString();
      } else if (filter.filter_name) {
        return filter.filter_name.toString();
      }
      return '...';
    }
  }

  CheckData(filterString: string):boolean {
    return this.dataFilters.some((filter: Filter) => this.getFilterFormControlName(filter) === filterString);
  }


  onChange($event: MatCheckboxChange, filter: any, isFiltered: boolean = false)  {
    const checkboxValue = $event.checked; // Отримання значення чекбокса з $event
    // Оновлення значення чекбокса та збереження його у сервісі
    const control = this.filters_status.get(this.getFilterFormControlName(filter));
    if (control !== null) {
      control.setValue(checkboxValue);
    }

    if(isFiltered){
      this.filtersCheckedService.setCheckboxValue(filter, checkboxValue, this.type);
    }
    else{
      this.filtersCheckedService.setCheckboxValue(this.getFilterFormControlName(filter), checkboxValue, this.type);
    }

    //console.log(this.filtersCheckedService.getAllCheckboxValues())

  }
  onInput(query: string): void {
    this.searchBarService.filterItems(query, this.nameTopic);

  }

}
