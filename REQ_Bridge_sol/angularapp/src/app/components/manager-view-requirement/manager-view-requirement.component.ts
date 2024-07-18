import { Component, OnInit } from '@angular/core';
import { ProjectRequirementService } from 'src/app/services/project-requirement.service'; // Adjust the path as necessary
import { ProjectRequirement } from 'src/app/models/projectRequirement.model'; // Adjust the path as necessary

@Component({
  selector: 'app-manager-view-requirement',
  templateUrl: './manager-view-requirement.component.html',
  styleUrls: ['./manager-view-requirement.component.css']
})
export class ManagerViewRequirementComponent implements OnInit {

  requirements: ProjectRequirement[] = [];
  filteredRequirements: ProjectRequirement[] = [];
  searchField: string = '';
  errorMessage: string = '';

  constructor(private requirementService: ProjectRequirementService) { }

  ngOnInit(): void {
    this.fetchAllRequirements();
  }

  fetchAllRequirements(): void {
    this.requirementService.getAllProjectRequirements().subscribe(
      (requirements) => {
        this.requirements = requirements;
        this.filteredRequirements = requirements;
      },

      (error) => {
        console.error('Error fetching requirements:', error);
      }
    );


    console.log("this.filteredRequirements ",this.requirements );
  }

  handleSearchChange(searchValue: string): void {
    this.filteredRequirements = this.requirements.filter(requirement =>
      requirement.RequirementTitle.toLowerCase().includes(searchValue.toLowerCase()) ||
      requirement.RequirementDescription.toLowerCase().includes(searchValue.toLowerCase()) ||
      requirement.Status.toLowerCase().includes(searchValue.toLowerCase())
    );
  }

  handleStatusChange(requirement: ProjectRequirement, newStatus: string): void {
    const updatedRequirement = { ...requirement, Status: newStatus };
    this.requirementService.updateProjectRequirement(requirement.RequirementId, updatedRequirement).subscribe(
      () => {
        this.errorMessage = '';
        this.fetchAllRequirements();
      },
      (error) => {
        console.error('Error updating requirement status:', error);
        this.errorMessage = 'Failed to update requirement status';
      }
    );
  }
}
