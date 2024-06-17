import {JobRequestDTO} from "./job-request.dto";
import {JobShortDTO} from "./job-short.dto";

export interface JobWithRequestDTO {
  jobShortDTO: JobShortDTO;
  jobRequestDTO: JobRequestDTO;
}
