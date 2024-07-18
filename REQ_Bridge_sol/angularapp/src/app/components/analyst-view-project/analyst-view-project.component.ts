import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service'; // Ensure this path is correct
import { Project } from 'src/app/models/project.model'; // Ensure this path is correct

@Component({
  selector: 'app-analyst-view-project',
  templateUrl: './analyst-view-project.component.html',
  styleUrls: ['./analyst-view-project.component.css']
})

export class AnalystViewProjectComponent implements OnInit {

  availableProjects: Project[] = [];
  searchField = '';
  status: string = '';
  allProjects: Project[] = [];

  constructor(private projectService: ProjectService) {}

  ngOnInit(): void {
    this.fetchAvailableProjects();
  }

  fetchAvailableProjects() {
    this.status = 'loading';
    this.projectService.getAllProjects().subscribe(
      (data: Project[]) => {
        this.availableProjects = data;
        this.allProjects = data;
        this.status = '';
      },
      (error) => {
        console.error('Error fetching projects:', error);
        this.status = 'error';
      }
    );
  }




}