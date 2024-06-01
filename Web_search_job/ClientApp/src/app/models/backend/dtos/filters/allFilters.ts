import {RegionLocation} from "./regionLocation";
import {Filter} from "./filter";
import {Industry} from "../other/industry.dto";

export class AllFilters {
  filters?: {
    [key: string]: Filter[];
  };
  industry?: Industry[];
  locations?: RegionLocation[];
}
