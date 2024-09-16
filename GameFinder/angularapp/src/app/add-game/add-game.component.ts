import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GameService } from '../services/game.service'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-game',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.css']
})
export class AddGameComponent implements OnInit {
  gameForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder, 
    private gameService: GameService, 
    private router: Router
  ) {
    this.gameForm = this.formBuilder.group({
      title: ['', Validators.required], // Game title is required
      releaseYear: ['', [Validators.required, Validators.min(1980), Validators.max(new Date().getFullYear())]], // Year range check
      genre: ['', Validators.required], // Genre is required
      developer: ['', Validators.required], // Developer is required
      supportContact: ['', [Validators.required, Validators.pattern(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)]] // Email validation
    });
  }

  ngOnInit(): void {}

  addGame(): void {
    if (this.gameForm.valid) {
      console.log(this.gameForm.value);
      this.gameService.addGame(this.gameForm.value)
        .subscribe(
          (res) => {
            console.log('Game added successfully:', res);
            this.router.navigateByUrl('/games');
            // Optionally reset the form or show a success message
            this.gameForm.reset();
          },
          (err) => {
            console.error('Error adding game:', err);
            // Handle error, show error message to the user
          }
        );
    } else {
      console.log('Form is invalid');
    }
  }
}
