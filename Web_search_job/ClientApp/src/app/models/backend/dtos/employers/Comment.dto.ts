import {CommentUserDTO} from "../users/comment-user.dto";

export interface CommentDTO {
  id: number;
  user: CommentUserDTO | null;
  commentStars: number;
  commentTitle: string;
  commentText: string;
  commentsToEmployerCreatedAt: Date;
}
