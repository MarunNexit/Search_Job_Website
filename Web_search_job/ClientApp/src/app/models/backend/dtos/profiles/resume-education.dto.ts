import {Industry} from "../other/industry.dto";
import {LocationDataDTO} from "../other/location-data.dto";
import {ActiveResumeSectionDTO} from "./active-resume-section.dto";

export interface ResumeEducationDTO {
  id: number | null;
  resumeId: number;
  educationProffesion: string;
  industryId: number | null;
  locationId: number | null;
  educationYear: number;
  industry: Industry | null;
  location: LocationDataDTO | null;
}
