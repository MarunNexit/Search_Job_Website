export interface CommentEmployerDTO {
  id?: number;
  userId?: number;
  commentStars: string;
  commentTitle: string;
  commentText: string;
  commentsToEmployerCreatedAt: Date;
}
