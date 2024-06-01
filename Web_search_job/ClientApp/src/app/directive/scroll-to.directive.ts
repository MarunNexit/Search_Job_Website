import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[scrollTo]'
})
export class ScrollToDirective {
  @Input() scrollTo: ElementRef;

  constructor(private elementRef: ElementRef) {}

  scrollToElement() {
    if (this.scrollTo) {
      this.scrollTo.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
    } else {
      this.elementRef.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}
