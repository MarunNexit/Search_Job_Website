import {ResumeSectionTypeDTO} from "./resume-section-type.dto";

export interface ActiveResumeSectionDTO {
  id: number | null;
  resumeId: number;
  sectionId: number;
  resumeSectionType: ResumeSectionTypeDTO | null;
}
