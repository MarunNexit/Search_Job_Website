import {Component, Input} from '@angular/core';
import {MatCheckboxModule} from "@angular/material/checkbox";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgClass, NgForOf, NgIf} from "@angular/common";
import {JsonPipe} from '@angular/common';
import {expandCollapse} from "./animation_filter_card";


@Component({
  selector: 'app-filter-search-card',
  templateUrl: './filter-search-card.component.html',
  styleUrls: ['./filter-search-card.component.scss'],
  standalone: true,
  imports: [MatCheckboxModule, FormsModule, NgForOf, ReactiveFormsModule, JsonPipe, NgIf, NgClass],
  animations: [expandCollapse] // Додайте анімацію до списку анімацій
})
export class FilterSearchCardComponent {
  @Input() dataFilters: any;
  @Input() addingSearchField: boolean;
  expandFilterCard: boolean = true;

  constructor(private _formBuilder: FormBuilder) {}
  filters_status = this._formBuilder.group({});

  ngOnInit(): void {
    // Створіть FormGroup
    // Додайте FormControl для кожного фільтра
    this.dataFilters.filters.forEach((filter: { name: string, checked: boolean }) => {
      this.filters_status.addControl(filter.name, this._formBuilder.control(filter.checked));
    });
  }
}
