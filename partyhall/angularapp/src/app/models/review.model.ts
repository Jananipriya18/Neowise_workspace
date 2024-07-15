import { User } from "./user.model";

export class Review {
    reviewId?: number;
    userId: number;
    subject: string;
    body: string;
    rating: number;
    dateCreated: Date;
    user?: User; // Assuming User model is also defined
  }
  