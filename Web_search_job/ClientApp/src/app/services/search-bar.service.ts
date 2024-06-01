import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SearchBarService {
  private allItems: string[] = []; // Масив всіх елементів
  private filteredItemsIndustry: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]); // Поточні відфільтровані елементи
  private filteredItemsLocation: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]); // Поточні відфільтровані елементи


  constructor() {}

  addItem(item: string, topic:string): void {
    this.allItems.push(item);

    if(topic === 'Професійна сфера'){
      this.filteredItemsIndustry.next(this.allItems);
    }
    else if (topic === 'Розташування') {
      this.filteredItemsLocation.next(this.allItems);
    }
  }

  getFilteredItems(topic:string): Observable<string[]> {
    if(topic === 'Професійна сфера'){
      return this.filteredItemsIndustry.asObservable();
    }

    //if (topic === 'Розташування')
    return this.filteredItemsLocation.asObservable();

  }

  filterItems(query: string, topic:string): void {
    const filtered = this.allItems.filter(item => item.toLowerCase().includes(query.toLowerCase()));

    if(topic === 'Професійна сфера'){
      this.filteredItemsIndustry.next(filtered);
    }
    else if (topic === 'Розташування') {
      this.filteredItemsLocation.next(filtered);
    }

  }



  // Метод для встановлення початкового масиву елементів
  /*  setItems(items: string[]): void {
      this.allItems = items;
      this.filteredItems.next(items);
    }*/
}
