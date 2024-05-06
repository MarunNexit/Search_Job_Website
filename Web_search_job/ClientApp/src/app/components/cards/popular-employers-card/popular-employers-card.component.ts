import {Component, Input} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {RouterHelperService} from "../../../services/router-helper.service";

@Component({
  selector: 'app-popular-employers-card',
  templateUrl: './popular-employers-card.component.html',
  styleUrls: ['./popular-employers-card.component.scss']
})
export class PopularEmployersCardComponent {
  employer_checked: boolean = true;

  stars: string[] = [];
  rating: number = 3.5;

  @Input() dataEmployer: any;

  constructor(
    private routerHelper: RouterHelperService,
  ) {

  }

  handleButtonClick(event: MouseEvent) {
    event.stopPropagation();
  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  ngOnInit(): void {
    this.rating = 3.5; // ваше значення рейтингу
    const fullStars = Math.floor(this.rating);
    const halfStar = this.rating - fullStars >= 0.5 ? '../../../../assets/img/icons/stars/half_star.png' : '';
    const emptyStars = 5 - fullStars - (halfStar ? 1 : 0);

    //DELETE THIS
    this.dataEmployer = 1;
    //DELETE THIS

    console.log(emptyStars);

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
