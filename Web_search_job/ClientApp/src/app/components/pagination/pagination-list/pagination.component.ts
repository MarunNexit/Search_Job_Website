import {Component, EventEmitter, Input, Output} from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";
import {IdFromUrlService} from "../../../services/id-from-url.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-pagination-list',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent {
  @Input() numberElement: number = 0;
  @Input() maxElementOnPage: number = 0;
  @Output() currentPage: EventEmitter<number> = new EventEmitter<number>();
  numberPages: number = 0;
  pages: number[] = [];
  spaceDots: boolean[] = [false, false, false, false, false];

  currentPageValue: number = 0;

  constructor(
    private routerHelper: RouterHelperService,
    private idFromUrl: IdFromUrlService,
    private route: ActivatedRoute,
    private router: Router
  ) {

  }

  ngOnInit(){
    this.numberPages = Math.ceil(this.numberElement / this.maxElementOnPage);
    console.log(this.numberPages, this.numberElement, this.maxElementOnPage)

    this.route.queryParams.subscribe(params => {
      const page = params['page'] || 1;
      this.currentPage.emit(page);
      this.generatePages(page); // Отримання параметра page, за замовчуванням 1
    });
  }

  clearDots(){
    for (let i = 0; i < this.spaceDots.length; i++) {
      this.spaceDots[i] = false;
    }
  }

  generatePages(page: number) {
    this.pages = [];
    page = +page;

    this.currentPageValue = page;

    if(this.numberPages < 2){
      this.pages = [1];
      this.clearDots();
    }
    else if(this.numberPages < 3){
      this.pages = [1, 2];
      this.clearDots();
    }
    else if(page < 2){
      this.pages = [1, 2, this.numberPages];
      this.clearDots();
      this.spaceDots[1] = true;
    }
    else if(page >= this.numberPages){
      this.pages = [1, this.numberPages - 1,  this.numberPages];
      this.clearDots();
      this.spaceDots[0] = true;
    }
    else if(page == 2){
      this.pages = [1, 2, 3, this.numberPages];
      this.clearDots();
      this.spaceDots[2] = true;
    }
    else if(page == (this.numberPages - 1)){
      this.pages = [1, this.numberPages - 2, this.numberPages - 1,  this.numberPages ];
      this.clearDots();
      this.spaceDots[0] = true;
    }
    else{
      this.pages = [1, page - 1,  page, page + 1, this.numberPages];
      this.clearDots();
      this.spaceDots[0] = true;
      this.spaceDots[3] = true;
    }
  }

  goToPage(page: number): void {
    this.router.navigate([], { queryParams: { page } }).then(() => {
      window.scrollTo(0, 0); // Прокрутка до верху після навігації
    });;
    this.generatePages(page);
  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }
}
