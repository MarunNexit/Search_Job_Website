import {ActiveResumeSectionDTO} from "./active-resume-section.dto";

export interface ResumePortfolioDTO {
  id: number | null;
  resumeId: number;
  portfolioName: string;
  portfolioImg: string;
  portfolioLink: string;
}
