import {Location} from "../other/location.dto";
import {JobTagsMarksDTO} from "./job-tags-marks.dto";
import {Industry} from "../other/industry.dto";
import {EmployerShortDTO} from "../employers/employer-short.dto";
import {JobTagsProsDTO} from "./job-tags-pros.dto";

export interface JobShortDTO {
  id: number;
  jobTitle: string;
  jobImg: string;
  JobBackgroundImg: string;
  jobSalaryMin: number;
  jobSalaryMax: number;
  jobSalaryCurrency: string;
  jobDescription: string;
  numberCandidates: number;
  numberView: number;
  dateEnding: Date;
  dateLast_editing: Date;
  dateApproving: Date;
  status: string;
  createdAt: Date;
  isSavedJob: boolean;
  location: Location;
  industry: Industry;
  jobTagsMarks: JobTagsMarksDTO;
  jobTagsPros: JobTagsProsDTO[];
  employer: EmployerShortDTO;
}
