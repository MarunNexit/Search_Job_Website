import {Component, ElementRef, inject, ViewChild} from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatChipInputEvent, MatChipsModule} from "@angular/material/chips";
import {MatIconModule} from "@angular/material/icon";
import {MatAutocompleteModule, MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {AsyncPipe} from "@angular/common";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {map, Observable, startWith, takeUntil} from "rxjs";
import { CommonModule } from '@angular/common';
import {FiltersCheckedService} from "../../../services/filters-checked.service";
import {SearchParamService} from "../../../services/search-param.service";

@Component({
  selector: 'app-chip-search-job',
  templateUrl: './chip-search-job.component.html',
  styleUrls: ['./chip-search-job.component.scss'],
  standalone: true,
  imports: [
    CommonModule,

    FormsModule,
    MatFormFieldModule,
    MatChipsModule,
    MatIconModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    AsyncPipe,
  ],
})
export class ChipSearchJobComponent {

  separatorKeysCodes: number[] = [ENTER, COMMA];
  fruitCtrl = new FormControl('');
  filteredFruits: Observable<string[]>;
  fruits: string[] = ['Apple', 'Lemon', 'Lime', 'Orange', 'Strawberry', 'a111112', 'a111113', 'a111114', 'a111115', 'a111116', 'a111117', 'a111118', 'a111119', 'a111120', 'a111121'];
  allFruits: string[] = ['Apple', 'Lemon', 'Lime', 'Orange', 'Strawberry'];

  showInput: boolean = false;

  filters_true = this.filtersCheckedService.getAllCheckboxValues();
  isFiltersExists: boolean = false;

  constructor(
    private filtersCheckedService: FiltersCheckedService,

  ) {
    this.filteredFruits = this.fruitCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) => (fruit ? this._filter(fruit) : this.allFruits.slice())),
    );
  }

  ngOnInit(){
    this.filters_true = this.filtersCheckedService.getAllCheckboxValues();

    this.filtersCheckedService.checkboxValuesChange.subscribe(values => {
      this.filters_true = values;
      console.log(values)
      this.checkIfAnyTrue();
    });

    this.checkIfAnyTrue();
  }

  private checkIfAnyTrue(): void {
    this.isFiltersExists = Object.values(this.filters_true).some(value => value === true);
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.fruits.push(event.option.viewValue);
    this.fruitCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allFruits.filter(fruit => fruit.toLowerCase().includes(filterValue));
  }


  add_chip(event: any): void {
    const value = (event.value || '').trim();
    // Add our fruit
    if (value) {
      this.fruits.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();

    if(!this.isFiltersExists){
      this.isFiltersExists = true;
    }

    this.fruitCtrl.setValue(null);
  }

  remove_chip(fruit: any): void {
    console.log(fruit.key)
    this.filtersCheckedService.setCheckboxValue(fruit.key, false, 'chip');
  }


  remove_all_chip(): void {
    this.filtersCheckedService.setCheckboxAllValuesToFalse('chip')

    this.isFiltersExists = false; // Опціонально, якщо необхідно скинути прапор isFiltersExists
  }

}
