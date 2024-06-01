
export class Job {
  id?: number;

  employer_id?: number;
  company_name? = "";
  company_picture?="";
  company_banner_picture? = "";

  job_title? = "";
  job_img?= "";
  job_background_img?= "";

  job_tags_marks?: any[];
  job_tags_pros?: any[];

  job_salary_min: number;
  job_salary_max: number;
  job_salary_currency: string;

  job_industry_categoty?= "";

  job_description?= "";

  creater_user_id?: number;
  industry? = "";
  city? = "";
  country? = "";

  number_candidates? : number;
  number_view? : number;

  requirement_id? : number;

  date_ending?: Date;
  date_last_editing?: Date | null;
  date_approving?: Date | null;
  status?= "";
  created_at?: Date;
}



