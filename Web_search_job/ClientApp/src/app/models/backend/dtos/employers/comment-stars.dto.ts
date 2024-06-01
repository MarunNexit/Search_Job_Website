import {CommentUserDTO} from "../users/comment-user.dto";

export interface CommentStarsDTO {
  id: number;
  user: CommentUserDTO | null;
  commentStars: number;
}
