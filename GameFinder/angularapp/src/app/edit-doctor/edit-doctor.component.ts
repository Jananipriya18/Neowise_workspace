// edit-doctor.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Doctor } from '../model/game.model';
import { DoctorService } from '../services/game.service';

@Component({
  selector: 'app-edit-doctor',
  templateUrl: './edit-doctor.component.html',
  styleUrls: ['./edit-doctor.component.css']
})
export class EditDoctorComponent implements OnInit {
  doctor: Doctor | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private doctorService: DoctorService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.doctorService.getDoctorById(+id).subscribe(
        (doctor) => (this.doctor = doctor),
        (err) => console.error(err)
      );
    }
  }

  saveDoctor(): void {
    if (this.doctor) {
      this.doctorService.updateDoctor(this.doctor.id, this.doctor).subscribe(
        () => {
          this.router.navigate(['/doctors']);
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/doctors']);
  }
}
