import {Component, Input} from '@angular/core';
import {EmployerDTO} from "../../../models/backend/dtos/employers/employer-full.dto";

@Component({
  selector: 'app-comments-rating',
  templateUrl: './comments-rating.component.html',
  styleUrls: ['./comments-rating.component.scss']
})
export class CommentsRatingComponent {

  @Input() employer: EmployerDTO;

  numberComments: number = 0;
  rating: number = 0;
  numbersArray: number[] = [5, 4, 3, 2, 1];
  commentsArray: number[] = [0, 0, 0, 0, 0];

  ngOnInit(): void {
    this.numberComments = this.employer.commentCount;
    this.CommentsRating();
  }

  CommentsRating(){
    this.rating = this.employer.companyRating;
    const comments = this.employer.comments;

    comments.forEach(comment => {
      // Отримуємо оцінку коментаря
      const rating = comment.commentStars;
      let num = 0;
      this.numbersArray.forEach(stars => {
        if (rating === stars) {
          this.commentsArray[num] += 1;
        }
        num++;
      });
    });
  }


}
