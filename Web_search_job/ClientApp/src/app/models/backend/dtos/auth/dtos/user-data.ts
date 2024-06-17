import {Location} from "../../other/location.dto";
import {LocationDataDTO} from "../../other/location-data.dto";

export interface UserData {
  id: number;
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  userAge: number;
  userImg: string;
  dateOfBirth: Date;
  location: LocationDataDTO;
  phoneNumber: string;
}
