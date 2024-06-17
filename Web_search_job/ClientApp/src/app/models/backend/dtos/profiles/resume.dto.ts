
import {UserDTO} from "../users/user.dto";
import {ActiveResumeSectionDTO} from "./active-resume-section.dto";
import {ResumeEducationDTO} from "./resume-education.dto";
import {ResumeLanguageDTO} from "./resume-language.dto";
import {ResumeLinksDTO} from "./resume-links.dto";
import {ResumePortfolioDTO} from "./resume-portfolio.dto";
import {ResumeSkillsDTO} from "./resume-skills.dto";
import {ResumeTagsDTO} from "./resume-tags.dto";
import {ResumeAboutMeDTO} from "./resume-about-me.dto";
import {ResumeHistoryWorkDTO} from "./resume-history-work.dto";



export interface ResumeDTO {
  id: number | null;
  userId: number | null;
  resumeName: string;
  resumeDescription: string | null;
  resumeEmail: string | null;
  resumePhone: string | null;
  wantedSalary: string | null;
  resumeTags: ResumeTagsDTO[];
  resumeActive: boolean;
  userInfo: UserDTO | null;
  resumeAboutMe: ResumeAboutMeDTO | null;
  activeResumeSection: ActiveResumeSectionDTO[];
  resumeEducation: ResumeEducationDTO[];
  resumeLanguage: ResumeLanguageDTO[];
  resumeLinks: ResumeLinksDTO[];
  resumePortfolio: ResumePortfolioDTO[];
  resumeSkills: ResumeSkillsDTO[];
  resumeHistoryWork: ResumeHistoryWorkDTO[];
}
