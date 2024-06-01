import {Location} from "../other/location.dto";
import {JobTagsMarksDTO} from "./job-tags-marks.dto";
import {Industry} from "../other/industry.dto";

export interface JobEmployerShortDTO {
  id: number;
  jobTitle: string;
  jobSalaryMin: number;
  jobSalaryMax: number;
  jobSalaryCurrency: string;
  dateEnding: Date;
  dateLastEditing: Date;
  dateApproving: Date;
  status: string;
  createdAt: Date;
  isSaved: boolean;
  jobTagsMarksDTO: JobTagsMarksDTO[];
  industry: Industry;
  location: Location;

}
