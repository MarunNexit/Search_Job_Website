import {AfterViewInit, Component, ElementRef, HostListener, Renderer2, ViewChild} from '@angular/core';
import {JobShortDTO} from "../../models/backend/dtos/jobs/job-short.dto";
import {SearchParamService} from "../../services/search-param.service";
import {MainSearchService} from "../../services/main-search.service";



@Component({
  selector: 'app-search-job-page',
  templateUrl: './search-job-page.component.html',
  styleUrls: ['./search-job-page.component.scss'],
})
export class SearchJobPageComponent implements AfterViewInit{

  jobs: JobShortDTO[] = [];
  showScrollButton: boolean = false;
  @ViewChild('myButtonDiv') myButtonDiv: ElementRef;
  divHeight: number;
  sortedValue: string[] = ['new'];
  selectedOption: string = 'new';

  ngAfterViewInit() {
    setTimeout(() => {
      this.divHeight = this.myButtonDiv.nativeElement.offsetHeight;
    });
  }
  constructor(
    private renderer: Renderer2,
    private searchParamService: SearchParamService,
    private mainSearchService: MainSearchService,
  ) {}

  ngOnInit(){
    let temporarySearchParam = this.mainSearchService.getParamSort();
    let splitSearchParam = temporarySearchParam.split(',');
    if(splitSearchParam[0] == '' || splitSearchParam[0] == ' '){
      this.selectedOption = 'new';
    }
    else{
      this.selectedOption = splitSearchParam[0];
    }
  }

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

  onJobsOutput(data: JobShortDTO[]): void {
    this.jobs = data;
  }

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = target.value;
    this.sortedValue[0] = target.value;
    this.selectedOption = target.value;

    this.mainSearchService.setParamsSort(this.sortedValue)
    //this.searchParamService.setParamSorting(this.sortedValue);
  }
}
