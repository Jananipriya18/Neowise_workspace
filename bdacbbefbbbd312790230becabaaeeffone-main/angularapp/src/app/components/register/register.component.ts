import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Register } from 'src/app/models/register.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  newRegistration:Register = new Register();
  username  : string;
  password  : string;
  email : string;

  constructor(private router:Router, private service:AuthService) { }

  ngOnInit(): void {
  }
  register()
  {
    this.service.register(this.username, this.password, this.email).subscribe(
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
