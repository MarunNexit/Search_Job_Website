import {LanguageLevelDTO} from "./language-level.dto";

export interface ResumeLanguageDTO {
  id: number | null;
  resumeId: number;
  languale: string;
  levelId: number;
  languageLevel: LanguageLevelDTO | null;
}
