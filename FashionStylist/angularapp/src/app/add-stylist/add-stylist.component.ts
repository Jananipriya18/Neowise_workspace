import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StylistService } from '../services/stylist.service'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-stylist',
  templateUrl: './add-stylist.component.html',
  styleUrls: ['./add-stylist.component.css']
})
export class AddStylistComponent implements OnInit {
  stylistForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder, 
    private stylistService: StylistService, // Adjust service name as necessary
    private router: Router
  ) {
    this.stylistForm = this.formBuilder.group({
      name: ['', Validators.required],
      expertise: ['', Validators.required],
      styleSignature: ['', Validators.required],
      availability: ['', Validators.required],
      hourlyRate: ['', [Validators.required, Validators.min(0)]], // Ensure it's a positive number
      location: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  addStylist(): void {
    if (this.stylistForm.valid) {
      console.log(this.stylistForm.value);
      this.stylistService.addStylist(this.stylistForm.value)
        .subscribe(
          (res) => {
            console.log('Stylist added successfully:', res);
            this.router.navigateByUrl('/stylistsList');
            // Optionally reset the form or show a success message
            this.stylistForm.reset();
          },
          (err) => {
            console.error('Error adding stylist:', err);
            // Handle error, show error message to the user
          }
        );
    } else {
      console.log('Form is invalid');
    }
  }
}
