// tutor-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Tutor } from '../models/menu.model';
import { TutorService } from '../services/tutor.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-tutor-list',
  templateUrl: './tutor-list.component.html',
  styleUrls: ['./tutor-list.component.css']
})
export class TutorListComponent implements OnInit {
  tutors: Tutor[] = [];

  constructor(private tutorService: TutorService,private router: Router) { }

  ngOnInit(): void {
    this.loadTutors();
  }

  loadTutors(): void {
    this.tutorService.getTutors().subscribe(tutors => this.tutors = tutors);
  }

  Delete(tutorId: number): void {
    // Navigate to confirm delete page with the tutor ID as a parameter
    this.router.navigate(['/confirmDelete', tutorId]);
  }
}

