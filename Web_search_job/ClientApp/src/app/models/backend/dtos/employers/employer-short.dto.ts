import { EmployerTagDTO } from './employer-tag.dto';
import {CommentStarsDTO} from "./comment-stars.dto";

export interface EmployerShortDTO {
  id?: number;
  companyName: string;
  companyIndustryDescription?: string;
  companyShortDescription?: string;
  companyChecked: boolean;
  companyURL?: string;
  companyImg: string;
  companyYear?: number;
  companyRating?: number;
  commentCount: number;
  employerCreatedAt: Date;
  comments?: CommentStarsDTO[];
  tags?: EmployerTagDTO[];
}
