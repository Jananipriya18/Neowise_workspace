import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-manager-add-project',
  templateUrl: './manager-add-project.component.html',
  styleUrls: ['./manager-add-project.component.css']
})
export class ManagerAddProjectComponent implements OnInit {
  formData: Project = {
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

  constructor(private projectService: ProjectService, private router: Router) {}

  ngOnInit(): void {}

  handleChange(event: any, field: string) {
    this.formData[field] = event.target.value;
  }

  onSubmit(projectForm: NgForm) {
    if (projectForm.valid) {
      this.projectService.addProject(this.formData).subscribe(
        response => {
          this.successPopup = true;
          console.log('Project added successfully', response);
          projectForm.resetForm();
        },
        error => {
          this.errorMessage = error.error.message;
          console.error(error);
        }
      );
    } else {
      this.errorMessage = 'All fields are required';
    }
  }

  handleSuccessMessage() {
    this.successPopup = false;
    this.errorMessage = '';
    this.formData = {
      ProjectTitle: '',
      ProjectDescription: '',
      StartDate: '',
      EndDate: '',
      FrontEndTechStack: '',
      BackendTechStack: '',
      Database: '',
      Status: ''
    };
  }
}
