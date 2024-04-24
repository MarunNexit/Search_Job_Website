import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HeaderStateService {
  private isHeaderSectionVisible: boolean = true;

  constructor() { }

  toggleHeaderSectionVisibilityMakeFalse() {
    this.isHeaderSectionVisible = false;
  }

  toggleHeaderSectionVisibilityMakeTrue() {
    this.isHeaderSectionVisible = true;
  }

  getIsHeaderSectionVisible(): boolean {
    return this.isHeaderSectionVisible;
  }
}
