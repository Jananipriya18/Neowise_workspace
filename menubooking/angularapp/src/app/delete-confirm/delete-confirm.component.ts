import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TutorService } from '../services/tutor.service';
import { Tutor } from '../models/tutor.model'; // Import Tutor interface

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  tutorId: number;
  tutor: Tutor; // Initialize tutor property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private tutorService: TutorService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.tutorId = +params['id'];
      this.tutorService.getTutor(this.tutorId).subscribe(
        (tutor: Tutor) => {
          this.tutor = tutor;
        },
        error => {
          console.error('Error fetching tutor:', error);
        }
      );
    });
  }

  confirmDelete(tutorId: number): void {
    this.tutorService.deleteTutor(tutorId).subscribe(
      () => {
        console.log('Tutor deleted successfully.');
        this.router.navigate(['/viewTutors']);
      },
      (error) => {
        console.error('Error deleting tutor:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewTutors']);
  }
}
