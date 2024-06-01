import {Component, Input} from '@angular/core';
import {CommentDTO} from "../../../models/backend/dtos/employers/Comment.dto";

@Component({
  selector: 'app-comment-card',
  templateUrl: './comment-card.component.html',
  styleUrls: ['./comment-card.component.scss']
})
export class CommentCardComponent {
  @Input() dataComment: CommentDTO;
  @Input() isRightSide: boolean;

  stars: string[] = [];
  rating: number = 0;
  isShortView: boolean = true;

  ngOnInit(): void {
    console.log(this.dataComment)

    this.rating = this.dataComment.commentStars; // ваше значення рейтингу
    const fullStars = Math.floor(this.dataComment.commentStars);
    const halfStar = this.dataComment.commentStars - fullStars >= 0.5 ? '../../../../assets/img/icons/stars/half_star.png' : '';
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
}
