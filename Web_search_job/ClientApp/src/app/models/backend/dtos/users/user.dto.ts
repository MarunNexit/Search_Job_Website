
import {Location} from "../other/location.dto";

export interface UserDTO {
  id: number;
  userId: string;
  userName: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  userCreatedAt: Date;
  userImg: string;
  location: Location | null;
}
