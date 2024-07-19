import { College } from "./college.model";

export interface Application {
  ApplicationId?: number;
  UserId?: number;
  CollegeId?: number;
  College?: College
  DegreeName: string;
  TwelfthPercentage: number;
  PreviousCollege: string;
  PreviousCollegeCGPA: number;
  Status: string;
  CreatedAt: string;
}
