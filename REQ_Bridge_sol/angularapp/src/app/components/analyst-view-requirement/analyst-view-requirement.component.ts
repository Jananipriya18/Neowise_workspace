import { Component, OnInit } from '@angular/core';
import { ProjectRequirement } from 'src/app/models/projectRequirement.model'; // Adjust the path as necessary
import { ProjectRequirementService } from 'src/app/services/project-requirement.service';

@Component({
  selector: 'app-analyst-view-requirement',
  templateUrl: './analyst-view-requirement.component.html',
  styleUrls: ['./analyst-view-requirement.component.css']
})
export class AnalystViewRequirementComponent implements OnInit {
  requirements: ProjectRequirement[] = [];
  showDeletePopup: boolean = false;
  requirementToDelete: number | null = null;
  errorMessage: string = '';

  constructor(private requirementService: ProjectRequirementService) { }

  ngOnInit(): void {
    this.fetchRequirements();
  }

  fetchRequirements(): void {
    this.requirementService.getAllProjectRequirements().subscribe(
      (data: ProjectRequirement[]) => {
        this.requirements = data;
      },
      (error) => {
        console.error('Error fetching requirements:', error);
      }
    );
  }

  handleDeleteClick(requirementId: number): void {
    this.requirementToDelete = requirementId;
    this.showDeletePopup = true;
  }

  handleConfirmDelete(): void {
    if (this.requirementToDelete !== null) {
      this.requirementService.deleteProjectRequirement(this.requirementToDelete).subscribe(
        () => {
          this.requirements = this.requirements.filter(r => r.RequirementId !== this.requirementToDelete);
          this.closeDeletePopup();
        },
        (error) => {
          this.errorMessage = 'Error deleting requirement';
          console.error('Error deleting requirement:', error);
        }
      );
    }
  }

  closeDeletePopup(): void {
    this.showDeletePopup = false;
    this.requirementToDelete = null;
    this.errorMessage = '';
  }
}
