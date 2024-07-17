import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OrphanageService } from 'src/app/services/orphanage.service';

@Component({
  selector: 'app-createorphanage',
  templateUrl: './createorphanage.component.html',
  styleUrls: ['./createorphanage.component.css']
})
export class CreateorphanageComponent implements OnInit {

  orphanageForm: FormGroup;
  successPopup = false;
  errorMessage = "";
  today = new Date();
  isEditMode = false;
  orphanageId: string;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private orphanageService: OrphanageService,
    private route: ActivatedRoute
  ) {
    this.orphanageForm = this.fb.group({
      orphanageName: ['', Validators.required],
      description: ['', Validators.required],
      founder: ['', Validators.required],
      status: ['Active', Validators.required] // Default value for status
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.orphanageId = params.get('id');
      if (this.orphanageId) {
        this.isEditMode = true;
        this.loadOrphanage();
      }
    });
  }

  loadOrphanage(): void {
    this.orphanageService.getOrphanageById(parseInt(this.orphanageId)).subscribe(
      (response) => {
        // Set form values
        this.orphanageForm.setValue({
          orphanageName: response.OrphanageName,
          description: response.Description,
          founder: response.Founder,
          status: response.Status // Set status from the response
        });
      },
      (error) => {
        console.error('Error fetching orphanage details:', error);
        this.router.navigate(['/error']);
      }
    );
  }

  onSubmit(): void {
    if (this.orphanageForm.valid) {
      const formData = this.orphanageForm.value;

      const orphanageObject = {
        OrphanageName: formData.orphanageName,
        Description: formData.description,
        Founder: formData.founder,
        EstablishmentDate: this.today, // Include current date
        Status: formData.status // Include status from form
      };

      if (this.isEditMode) {
        this.orphanageService.updateOrphanage(parseInt(this.orphanageId), orphanageObject).subscribe(
          (response) => {
            console.log('Orphanage updated successfully', response);
            this.successPopup = true;
          },
          (error) => {
            console.error('Error updating orphanage:', error);
            this.errorMessage = error.error.message;
          }
        );
      } else {
        this.orphanageService.addOrphanage(orphanageObject).subscribe(
          (response) => {
            console.log('Orphanage added successfully', response);
            this.successPopup = true;
          },
          (error) => {
            console.error('Error adding orphanage:', error);
            this.errorMessage = error.error.message;
          }
        );
      }
    } else {
      this.errorMessage = "All fields are required";
    }
  }

  handleSuccessMessage(): void {
    this.successPopup = false;
    this.router.navigate(['/admin/view/orphanage']); // Navigate to the desired route
  }
}
