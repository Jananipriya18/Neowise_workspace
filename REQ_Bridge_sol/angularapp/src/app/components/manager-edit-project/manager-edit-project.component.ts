import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-manager-edit-project',
  templateUrl: './manager-edit-project.component.html',
  styleUrls: ['./manager-edit-project.component.css']
})
export class ManagerEditProjectComponent implements OnInit {
  projectId: number;
  formData: Project = {
    ProjectId: 0,
    ProjectTitle: '',
    ProjectDescription: '',
    StartDate: '',
    EndDate: '',
    FrontEndTechStack: '',
    BackendTechStack: '',
    Database: '',
    Status: ''
  };
  errorMessage: string = '';
  successPopup: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.params['id'];
    this.getProjectDetails(this.projectId);
  }
getProjectDetails(id: number) {
  this.projectService.getProjectById(id).subscribe(
    (data: Project) => {
      // Convert date fields to the format suitable for HTML date inputs
      data.StartDate = this.datePipe.transform(data.StartDate, 'yyyy-MM-dd');
      data.EndDate = this.datePipe.transform(data.EndDate, 'yyyy-MM-dd');
      this.formData = data;
    },
    error => {
      console.error('Error fetching project details:', error);
    }
  );
}

  handleChange(event: any, field: string) {
    if (field === 'StartDate' || field === 'EndDate') {
      this.formData[field] = event.target.value;
    } else {
      this.formData[field] = event.target.value;
    }
  }

  formatDate(date: Date | string): Date {
    // If the date is already a Date object, return it directly
    if (date instanceof Date) {
      return date;
    }

    // Otherwise, parse the string date and return a Date object
    return new Date(date);
  }



  onSubmit(projectForm: NgForm) {
    if (projectForm.valid) {
      this.projectService.updateProject(this.projectId, this.formData).subscribe(
        response => {
          this.successPopup = true;
          console.log('Project updated successfully', response);
        },
        error => {
          console.error('Error updating project:', error);
          this.errorMessage = 'An error occurred while updating the project';
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }

  handleSuccessMessage() {
    this.successPopup = false;
    this.router.navigate(['/manager/view/project']);
  }
}
