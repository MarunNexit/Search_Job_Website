import {Component, ElementRef, inject, ViewChild} from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatChipInputEvent, MatChipsModule} from "@angular/material/chips";
import {MatIconModule} from "@angular/material/icon";
import {MatAutocompleteModule, MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {AsyncPipe} from "@angular/common";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {map, Observable, startWith} from "rxjs";
import { CommonModule } from '@angular/common';

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

  isFiltersExists: boolean = true;

  @ViewChild('fruitInput') fruitInput: ElementRef<HTMLInputElement>;

/*
  announcer = inject(LiveAnnouncer);
*/

  constructor() {
    this.filteredFruits = this.fruitCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) => (fruit ? this._filter(fruit) : this.allFruits.slice())),
    );
  }




  /*OLD CODE*/

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our fruit
    if (value) {
      this.fruits.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();

    this.fruitCtrl.setValue(null);
  }

  remove(fruit: string): void {
    const index = this.fruits.indexOf(fruit);

    if (index >= 0) {
      this.fruits.splice(index, 1);

/*
      this.announcer.announce(`Removed ${fruit}`);
*/
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.fruits.push(event.option.viewValue);
    this.fruitInput.nativeElement.value = '';
    this.fruitCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allFruits.filter(fruit => fruit.toLowerCase().includes(filterValue));
  }



  /*MY CODE*/

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

  remove_chip(fruit: string): void {
    const index = this.fruits.indexOf(fruit);

    if (index >= 0) {
/*
      this.fruits.splice(index, 1);
*/
      this.fruits = this.fruits.filter(item => item !== fruit);

      if(this.fruits.length == 0){
        this.isFiltersExists = false;
      }

      console.log(this.fruits)
      console.log(this.filteredFruits)

/*
      this.announcer.announce(`Removed ${fruit}`);
*/
    }
  }


  remove_all_chip(): void {
    this.fruits = []; // Присвоєння порожнього масиву для видалення всіх елементів

    this.isFiltersExists = false; // Опціонально, якщо необхідно скинути прапор isFiltersExists

      console.log(this.fruits)
      console.log(this.filteredFruits)
  }


  selected_chip(event: MatAutocompleteSelectedEvent): void {
    this.fruits.push(event.option.viewValue);
    this.fruitInput.nativeElement.value = '';
    this.fruitCtrl.setValue(null);
  }

  private _filter_chip(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allFruits.filter(fruit => fruit.toLowerCase().includes(filterValue));
  }

}
