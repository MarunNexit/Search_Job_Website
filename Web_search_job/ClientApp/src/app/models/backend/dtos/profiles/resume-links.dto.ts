import {ActiveResumeSectionDTO} from "./active-resume-section.dto";

export interface ResumeLinksDTO {
  id: number | null;
  resumeId: number;
  type: string;
  link: string;
  accountName: string;
}
