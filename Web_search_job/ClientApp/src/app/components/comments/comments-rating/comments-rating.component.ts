import { Component } from '@angular/core';

@Component({
  selector: 'app-comments-rating',
  templateUrl: './comments-rating.component.html',
  styleUrls: ['./comments-rating.component.scss']
})
export class CommentsRatingComponent {
  numberComments: number = 53;
  rating: number = 3.5;
  numbersArray: number[] = [5, 4, 3, 2, 1];
  commentsArray: number[] = [10, 20, 15, 5, 3];

  ngOnInit(): void {
  }


}
