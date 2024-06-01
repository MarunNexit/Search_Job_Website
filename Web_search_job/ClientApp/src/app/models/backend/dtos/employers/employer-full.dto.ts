import {EmployerTagDTO} from "./employer-tag.dto";
import {JobEmployerShortDTO} from "../jobs/job-employer-short.dto";
import {CommentDTO} from "./Comment.dto";
import {Location} from "../other/location.dto";
import {UserDTO} from "../users/user.dto";
import {LocationDataDTO} from "../other/location-data.dto";
import {Industry} from "../other/industry.dto";

export interface EmployerDTO {
  id: number;
  companyName: string;
  companyShortDescription: string;
  companyIndustryDescription: string;
  companyChecked: boolean;
  companyURL: string;
  companyYear: number;
  companyRating: number;
  companyImg: string;
  companyBackgroundImg: string;
  employerCreatedAt: Date;
  jobsCount: number;
  commentCount: number;
  numberWorkers: number;
  companyDescription: string;
  companyStatus: string;
  comments: CommentDTO[];
  industry: Industry;
  tags: EmployerTagDTO[];
  location: LocationDataDTO;
  user: UserDTO[];
  jobs: JobEmployerShortDTO[];
}
