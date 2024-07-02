import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private  http:HttpClient) { }

  apiUrl="https://8080-bdacbbefbbbd312790230becabaaeeffone.premiumproject.examly.io/api/login";

  login(username:string,password:string,email:string):Observable<{message:string}>
  {
    return this.http.post<{message:string}>(this.apiUrl, {username:username ,password:password,email:email} )
  }
  //Method to check if the user is logged in by checking the local storage
  isLoggedIn():boolean
  {
    if(localStorage.getItem('loggedIn') == 'true')
    {
      return true;
    }
    return false;
  }
}
