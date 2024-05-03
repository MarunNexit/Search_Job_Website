import {AfterViewInit, Component, ElementRef, HostListener, Renderer2, ViewChild} from '@angular/core';



@Component({
  selector: 'app-search-job-page',
  templateUrl: './search-job-page.component.html',
  styleUrls: ['./search-job-page.component.scss'],
})
export class SearchJobPageComponent implements AfterViewInit{


  showScrollButton: boolean = false;
  @ViewChild('myButtonDiv') myButtonDiv: ElementRef;
  divHeight: number;

  ngAfterViewInit() {
    setTimeout(() => {
      this.divHeight = this.myButtonDiv.nativeElement.offsetHeight;
    });
  }

  constructor(private renderer: Renderer2) {}


  @HostListener('window:scroll', [])
  onWindowScroll() {
    const windowHeight = window.innerHeight;
    const scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    this.divHeight = this.myButtonDiv.nativeElement.offsetTop;

    if (window.pageYOffset > 600 && scrollTop < this.divHeight - windowHeight) {
      this.showScrollButton = true;
    } else {
      this.showScrollButton = false;
    }
  }

  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }


}
