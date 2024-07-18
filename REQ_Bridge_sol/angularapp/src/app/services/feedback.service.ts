import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feedback } from '../models/feedback.model';
import { apiUrl } from 'src/apiconfig';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  public apiUrl = apiUrl;

  constructor(private http: HttpClient) { }

  // Methods Overview:
// addFeedback(feedback: Feedback): Observable<Feedback>:
// Use this method to add a new feedback. It sends a POST request to the '/api/feedback' endpoint with the feedback data provided as the body and the authorization token prefixed with 'Bearer' stored in localStorage.
// getAllfeedbacksByUserId(userId: string): Observable<Feedback[]>:
// This method is used to get all feedbacks by user ID. It sends a GET request to the '/api/feedback/user/{userId}' endpoint with the user ID provided as a parameter and the authorization token prefixed with 'Bearer' stored in localStorage.
// deleteFeedback(feedbackId: string): Observable<void>:
// Use this method to delete a feedback. It sends a DELETE request to the '/api/feedback/{feedbackId}' endpoint with the feedback ID provided as a parameter and the authorization token prefixed with 'Bearer' stored in localStorage.
// getFeedbacks(): Observable<Feedback[]>:
// This method is used to get all feedbacks. It sends a GET request to the '/api/feedback' endpoint with the authorization token prefixed with 'Bearer' stored in localStorage.



  sendFeedback(feedback: Feedback): Observable<Feedback> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Feedback>(`${this.apiUrl}/api/feedback`, feedback, { headers });
  }

  getAllfeedbacksByUserId(userId: string): Observable<Feedback[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Feedback[]>(`${this.apiUrl}/api/feedback/user/${userId}`, { headers });
  }

  deleteFeedback(feedbackId: string): Observable<void> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<void>(`${this.apiUrl}/api/feedback/${feedbackId}`, { headers });
  }


 getFeedbacks(): Observable<Feedback[]> {
  const headers = new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  });
  return this.http.get<Feedback[]>(`${this.apiUrl}/api/feedback`, { headers });
}

}

