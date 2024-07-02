import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/register.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  newLogin:Login = new Login();
  username:string;
  password:string;

  constructor(private  router:Router, private service:AuthService) { }

  ngOnInit(): void {
  }
  login()
  {
    this.service.login(this.username, this.password).subscribe(
      data=>{
        console.log(data);
        localStorage.setItem('loggedIn','true');
        this.router.navigate(["/dashboard"])
      },
      error=>
      {
        console.log(error);
      }
    )
  }

}
