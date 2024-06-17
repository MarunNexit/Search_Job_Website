import {JobShortDTO} from "./job-short.dto";

export interface JobSearchResultDTO {
  jobs: JobShortDTO[];
  totalElements: number;
}
