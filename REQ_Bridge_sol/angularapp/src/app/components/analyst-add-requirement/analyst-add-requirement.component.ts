import { Component, OnInit } from '@angular/core';
import { ProjectRequirementService } from 'src/app/services/project-requirement.service'; // Adjust the path as per your project structure
import { ProjectRequirement } from 'src/app/models/projectRequirement.model'; // Adjust the path as per your project structure
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-analyst-add-requirement',
  templateUrl: './analyst-add-requirement.component.html',
  styleUrls: ['./analyst-add-requirement.component.css']
})
export class AnalystAddRequirementComponent implements OnInit {

  requirementTitle: string = '';
  requirementDescription: string = '';
  successPopup: boolean = false;

  constructor(private requirementService: ProjectRequirementService) { }

  ngOnInit(): void {
  }

  submitRequirement(requirementForm: NgForm) {
    const userId = Number(localStorage.getItem('userId')) || 0; // Assuming user id is stored in localStorage
    const newRequirement: ProjectRequirement = {
      UserId: userId,
      RequirementTitle: this.requirementTitle,
      RequirementDescription: this.requirementDescription,
      Status: 'Pending' // Assuming the initial status is 'Pending'
    };

    this.requirementService.addProjectRequirement(newRequirement).subscribe(
      (response) => {
        console.log('Requirement submitted successfully', response);
        // Show success popup
        this.successPopup = true;
        // Clear form fields
        this.requirementTitle = '';
        this.requirementDescription = '';
        requirementForm.resetForm();
      },
      (error) => {
        console.error('Error submitting requirement:', error);
        // Handle error message display or other actions as required
      }
    );
  }

  closeSuccessPopup() {
    // Close success popup
    this.successPopup = false;
  }

}
