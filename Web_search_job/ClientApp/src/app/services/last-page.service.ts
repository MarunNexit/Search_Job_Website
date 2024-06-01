import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LastPageService {
  private lastPage: string;
  private readonly localStorageKey = 'lastVisitedPage';

  constructor() { }

  public setLastPage(url: string): void {
    localStorage.setItem(this.localStorageKey, url);
  }

  public getLastPage(): string {
    return localStorage.getItem(this.localStorageKey) || '/';
  }
}
