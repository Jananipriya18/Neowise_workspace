import { PartyHall } from "./partyHall.model";
import { User } from "./user.model";

export class Booking {
  bookingId?: number;
  noOfPersons: number;
  fromDate: Date;
  toDate: Date;
  status: string;
  totalPrice: number;
  address: string;
  userId: number; // Nullable foreign key
  user?: User; // Nullable navigation property
  partyHallId: number; // Nullable foreign key
  partyHall?: PartyHall; // Nullable navigation property
}

//one