import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DoctorService } from '../services/doctor.service'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-doctor',
  templateUrl: './add-doctor.component.html',
  styleUrls: ['./add-doctor.component.css']
})
export class AddDoctorComponent implements OnInit {
  doctorForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private doctorService: DoctorService,    private router: Router
    ) {
    this.doctorForm = this.formBuilder.group({
      name: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(26), Validators.max(69)]], // Updated age range
      specialization: ['', Validators.required],
      department: ['', Validators.required],
      contactNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]]
    });
  }

  ngOnInit(): void {}

  addDoctor(): void {
    if (this.doctorForm.valid) {
      console.log(this.doctorForm.value);
      this.doctorService.addDoctor(this.doctorForm.value)
        .subscribe(
          (res) => {
            console.log('Doctor added successfully:', res);
            this.router.navigateByUrl('/doctors');
            // Optionally reset the form or show a success message
            this.doctorForm.reset();
          },
          (err) => {
            console.error('Error adding doctor:', err);
            // Handle error, show error message to the user
          }
        );
    } else {
      console.log('Form is invalid');
    }
  }
}
