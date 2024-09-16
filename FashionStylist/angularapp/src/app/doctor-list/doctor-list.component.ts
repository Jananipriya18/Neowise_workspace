import { Component, OnInit } from '@angular/core';
import { Doctor } from '../model/stylist.model';
import { DoctorService } from '../services/doctor.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css'],
})
export class DoctorListComponent implements OnInit {
  doctors: Doctor[] = [];

  constructor(private doctorService: DoctorService,private router: Router) {}

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors(): void {
    try {
      this.doctorService.getDoctors().subscribe(
        (res) => {
          console.log(res);
          this.doctors = res;
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  editDoctor(id: number): void {
    this.router.navigate(['/edit', id]);
  }


  deleteDoctor(id: any): void {
    this.doctorService.deleteDoctor(id).subscribe(() => {
      this.doctors = this.doctors.filter((doctor) => doctor.id !== id);
    });
  }
}
