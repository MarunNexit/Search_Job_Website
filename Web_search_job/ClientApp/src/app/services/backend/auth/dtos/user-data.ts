import {Location} from "../../../../models/backend/dtos/other/location.dto";
import {LocationDataDTO} from "../../../../models/backend/dtos/other/location-data.dto";

export interface UserData {
  id: string;
  email: string;
  age: number;
  img: string;
  location: LocationDataDTO;
  phone: string;
}
