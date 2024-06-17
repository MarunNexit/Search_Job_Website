import {JobRequirementDTO} from "./job-requirement.dto";

export interface JobRequestFieldsDTO {
  id: number;
  needAdditionalResume: boolean;
  needResume: boolean;
  positiveField: boolean;
  projectField: boolean;
}
