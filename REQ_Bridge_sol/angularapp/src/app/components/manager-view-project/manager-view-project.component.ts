import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';
 // Adjust the path as necessary

@Component({
  selector: 'app-manager-view-project',
  templateUrl: './manager-view-project.component.html',
  styleUrls: ['./manager-view-project.component.css']
})
export class ManagerViewProjectComponent implements OnInit {
  projects: Project[] = [];
  showDeletePopup = false;
  projectToDelete: number | null = null;
  searchField = '';
  errorMessage: string = '';
  showImageModal = false;
  selectedImage: string = '';
  status: string = 'loading';

  constructor(private router: Router, private projectService: ProjectService) {}

  ngOnInit(): void {
    this.fetchProjects();
  }

  fetchProjects() {
    this.projectService.getAllProjects().subscribe(
      (data: Project[]) => {
        this.projects = data;
        this.status = 'success';
      },
      error => {
        console.error('Error fetching projects:', error);
        this.status = 'error';
      }
    );
  }

  handleDeleteClick(projectId: number) {
    this.projectToDelete = projectId;
    this.showDeletePopup = true;
  }

  navigateToEditProject(id: number) {
    console.log('Navigating to edit project:', id);
    this.router.navigate(['/manager/edit/project', id]);
  }

  handleConfirmDelete() {
    if (this.projectToDelete !== null) {
      this.projectService.deleteProject(this.projectToDelete).subscribe(
        response => {
          console.log('Project deleted successfully', response);
          this.closeDeletePopup();
          this.fetchProjects();
          this.errorMessage = '';
        },
        error => {
          console.error('Error deleting project:', error);
          this.errorMessage = 'An error occurred while deleting the project';
        }
      );
    }
  }

  openImageModal(image: string) {
    this.selectedImage = image;
    this.showImageModal = true;
  }

  closeImageModal() {
    this.showImageModal = false;
  }

  closeDeletePopup() {
    this.projectToDelete = null;
    this.showDeletePopup = false;
    this.errorMessage = '';
  }

  handleSearchChange(searchValue: string): void {
    this.searchField = searchValue;
    this.filterProjects(searchValue);
  }

  filterProjects(search: string) {
    const searchLower = search.toLowerCase();
    if (searchLower === '') {
      this.fetchProjects();
    } else {
      this.projects = this.projects.filter(
        project =>
          project.ProjectTitle.toLowerCase().includes(searchLower) ||
          project.ProjectDescription.toLowerCase().includes(searchLower)
      );
    }
  }
}
