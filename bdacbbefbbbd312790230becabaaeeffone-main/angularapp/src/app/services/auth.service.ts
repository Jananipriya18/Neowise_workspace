import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private  http:HttpClient) { }

  apiUrl="https://8080-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/api/register";

  register(username:string,password:string,email:string):Observable<{message:string}>
  {
    return this.http.post<{message:string}>(this.apiUrl, {username:username ,password:password,email:email} )
  }
  //Method to check if the user is logged in by checking the local storage
  isRegisteredIn():boolean
  {
    if(localStorage.getItem('registeredIn') == 'true')
    {
      return true;
    }
    return false;
  }
}
