import { Component } from '@angular/core';
import { Tutor } from '../models/tutor.model';
import { TutorService } from '../services/tutor.service'; // Corrected import statement
import { Router } from '@angular/router';


@Component({
  selector: 'app-tutor-form',
  templateUrl: './tutor-form.component.html',
  styleUrls: ['./tutor-form.component.css']
})

export class TutorFormComponent {
  newTutor: Tutor = {
    tutorId: 0,
    name: '',
    email: '',
    subjectsOffered: '',
    contactNumber: '',
    availability: ''
  };
  
  formSubmitted = false; 

  constructor(private tutorService: TutorService, private router: Router) { }

  addTutor(): void {
    this.formSubmitted = true; // Set formSubmitted to true on form submission
    if (this.isFormValid()) {
      this.tutorService.addTutor(this.newTutor).subscribe(() => {
        console.log('Tutor added successfully!');
        this.router.navigate(['/viewTutors']);
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.newTutor[fieldName];
    return !field && (this.formSubmitted || this.newTutor[fieldName].touched);
  }

  isFormValid(): boolean {
    return !this.isFieldInvalid('name') && !this.isFieldInvalid('email') &&
      !this.isFieldInvalid('subjectsOffered') && !this.isFieldInvalid('contactNumber') &&
      !this.isFieldInvalid('availability');
  }
}

