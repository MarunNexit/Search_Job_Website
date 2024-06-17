import {EmployerHistoryWorkShortDTO} from "../employers/employer-history-work-short.dto";

export interface ResumeHistoryWorkDTO {
  id: number | null;
  resumeId: number;
  workName: string;
  employer: EmployerHistoryWorkShortDTO | null;
  startWorkDate: Date;
  endWorkDate: Date;
  workText: string;
}
