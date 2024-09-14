import { Component, OnInit } from '@angular/core';
import { Doctor } from '../model/doctor.model';
import { DoctorService } from '../services/doctor.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css'],
})
export class DoctorListComponent implements OnInit {
  doctors: Doctor[] = [];
  originalDoctors: Doctor[] = []; 

  constructor(private doctorService: DoctorService, private router: Router) {}

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors(): void {
    try {
      this.doctorService.getDoctors().subscribe(
        (res) => {
          console.log(res);
          this.doctors = res;
          this.originalDoctors = [...res]; // Save a copy of the original list
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
    this.router.navigate(['/editDoctor', id]);
  }

  deleteDoctor(id: any): void {
    this.doctorService.deleteDoctor(id).subscribe(() => {
      this.doctors = this.doctors.filter((doctor) => doctor.id !== id);
    });
  }
  sortByDescending(): void {
    this.doctors.sort((a, b) => b.age - a.age);
  }

  resetOrder(): void {
    this.doctors = [...this.originalDoctors];
  }
}
