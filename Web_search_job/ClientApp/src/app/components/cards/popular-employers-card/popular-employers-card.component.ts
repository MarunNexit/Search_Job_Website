import { Component } from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";

@Component({
  selector: 'app-popular-employers-card',
  templateUrl: './popular-employers-card.component.html',
  styleUrls: ['./popular-employers-card.component.scss']
})
export class PopularEmployersCardComponent {
  stars: string[] = [];
  employer_checked: boolean = true;
  rating: number = 3.5;

  ngOnInit(): void {
    this.rating = 3.5; // ваше значення рейтингу
    const fullStars = Math.floor(this.rating);
    const halfStar = this.rating - fullStars >= 0.5 ? '../../../../assets/img/icons/stars/half_star.png' : '';
    const emptyStars = 5 - fullStars - (halfStar ? 1 : 0);

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
