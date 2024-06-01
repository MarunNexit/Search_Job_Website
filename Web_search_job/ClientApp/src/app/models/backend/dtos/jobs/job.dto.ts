import {JobTagsMarksDTO} from "./job-tags-marks.dto";
import {JobTagsProsDTO} from "./job-tags-pros.dto";
import {EmployerDTO} from "../employers/employer-full.dto";
import {UserDTO} from "../users/user.dto";
import {LocationDataDTO} from "../other/location-data.dto";
import {Industry} from "../other/industry.dto";
import {JobRequirementDTO} from "./job-requirement.dto";
import {JobRequestFieldsDTO} from "./job-request-fields.dto";

export interface JobDTO {
  id: number;
  jobTitle: string;
  jobImg: string;
  jobBackgroundImg: string;
  jobSalaryMin: number;
  jobSalaryMax: number;
  jobSalaryCurrency: string;
  jobDescription: string;
  numberCandidates: number;
  numberView: number;
  dateEnding: Date;
  dateLastEditing: Date;
  dateApproving: Date;
  status: string;
  createdAt: Date;
  isSavedJob: boolean;
  location: LocationDataDTO;
  industry: Industry;
  jobRequirement: JobRequirementDTO;
  jobRequestFields: JobRequestFieldsDTO;
  jobTagsMarks: JobTagsMarksDTO;
  jobTagsPros: JobTagsProsDTO[];
  employer: EmployerDTO;
  user: UserDTO[];
}
