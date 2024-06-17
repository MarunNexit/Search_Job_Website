import {ResumeShortDTO} from "../profiles/resume-short.dto";

export interface JobRequestDTO {
  id: number;
  jobId: number;
  userId: number;
  resume?: ResumeShortDTO;
  resumeURL?: string;
  coverLetter?: string;
  positives?: string;
  projects?: string;
  status?: string;
  createdAt: Date;
}
