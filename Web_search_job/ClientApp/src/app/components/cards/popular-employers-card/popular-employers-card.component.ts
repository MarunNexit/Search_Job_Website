import {Component, Input} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {RouterHelperService} from "../../../services/router-helper.service";
import {EmployerShortDTO} from "../../../models/backend/dtos/employers/employer-short.dto";

@Component({
  selector: 'app-popular-employers-card',
  templateUrl: './popular-employers-card.component.html',
  styleUrls: ['./popular-employers-card.component.scss']
})
export class PopularEmployersCardComponent {
  employer_checked: boolean = true;

  stars: string[] = [];
  rating: number = 3.5;

  @Input() dataEmployer: EmployerShortDTO;

  constructor(
    private routerHelper: RouterHelperService,
  ) {

  }

  handleWebsite(event: MouseEvent, url: string | undefined) {
    event.stopPropagation();
    if(url){
      //console.log(url);
      window.open(url, '_blank');
    }
  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }



  ngOnInit(): void {
    this.rating = 0; // ваше значення рейтингу
    let fullStars = 0;


    if(this.dataEmployer && this.dataEmployer.companyRating){
      this.rating = this.dataEmployer.companyRating; // ваше значення рейтингу
      fullStars = Math.floor(this.rating);
    }

    const halfStar = this.rating - fullStars >= 0.5 ? '../../../../assets/img/icons/stars/half_star.png' : '';
    const emptyStars = 5 - fullStars - (halfStar ? 1 : 0);


    //DELETE THIS
    //console.log(emptyStars);

    for (let i = 0; i < fullStars; i++) {
      this.stars.push('../../../../assets/img/icons/stars/full_star.png');
    }

    if (halfStar) {
      this.stars.push(halfStar);
    }

    for (let i = 0; i < emptyStars; i++) {
      this.stars.push('../../../../assets/img/icons/stars/empty_star.png');
    }
  }
  protected readonly handleImageError = handleImageError;
}
