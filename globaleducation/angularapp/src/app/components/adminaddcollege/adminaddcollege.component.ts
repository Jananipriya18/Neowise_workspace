import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { College } from 'src/app/models/college.model';
import { CollegeService } from 'src/app/services/college.service';

@Component({
  selector: 'app-adminaddcollege',
  templateUrl: './adminaddcollege.component.html', // Update the template URL
  styleUrls: ['./adminaddcollege.component.css']
})
export class AdminaddcollegeComponent implements OnInit {

  id: string;
  formData: College = {
    CollegeId: 0,
    CollegeName: '',
    Location: '',
    Description: '',
    Website: ''
  };
  errors: any = {};
  errorMessage: string;
  successPopup: boolean = false;

  constructor(private collegeService: CollegeService, private router: Router) {}

  ngOnInit(): void {}

  handleChange(event: any, field: string) {
    this.formData[field] = event.target.value;
    // Validate your form here and set errors if any
  }

  onSubmit(collegeForm: NgForm) {
    console.log('Form Validity:', collegeForm.valid, this.formData);
    if (collegeForm.valid) {
      this.collegeService.addCollege(this.formData).subscribe(
        (res) => {
          this.successPopup = true;
          console.log('College added successfully', res);
          collegeForm.resetForm();
        },
        (err) => {
          this.errors = err.error;
          this.errorMessage = err.error.message;
          console.error('Error adding college:', err);
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }

  handleSuccessMessage() {
    this.successPopup = false;
    this.errorMessage = '';
    this.formData = {
      CollegeId: 0,
      CollegeName: '',
      Location: '',
      Description: '',
      Website: ''
    };
  }
}
