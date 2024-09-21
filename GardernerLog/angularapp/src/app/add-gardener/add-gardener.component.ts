import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GardenerService } from '../services/gardener.service'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-gardener',
  templateUrl: './add-gardener.component.html',
  styleUrls: ['./add-gardener.component.css']
})
export class AddGardenerComponent implements OnInit {
  gardenerForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private gardenerService: GardenerService, 
    private router: Router
  ) {
    this.gardenerForm = this.formBuilder.group({
      name: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(19), Validators.max(69)]], 
      task: ['', Validators.required],
      description: ['', Validators.required],
      durationInMinutes: ['', [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {}

  addGardener(): void {
    if (this.gardenerForm.valid) {
      console.log(this.gardenerForm.value);
      this.gardenerService.addGardener(this.gardenerForm.value) // Update method to match the gardener service
        .subscribe(
          (res) => {
            console.log('Gardener added successfully:', res);
            this.router.navigateByUrl('/gardeners'); // Navigate to the gardeners list
            // Optionally reset the form or show a success message
            this.gardenerForm.reset();
          },
          (err) => {
            console.error('Error adding gardener:', err);
            // Handle error, show error message to the user
          }
        );
    } else {
      console.log('Form is invalid');
    }
  }
}
