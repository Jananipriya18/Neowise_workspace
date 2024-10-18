import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PodcastService } from '../services/podcast.service'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-podcast',
  templateUrl: './add-podcast.component.html',
  styleUrls: ['./add-podcast.component.css']
})
export class AddPodcastComponent implements OnInit {
  podcastForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private podcastService: PodcastService, private router: Router) {
    // Initialize the form with podcast-related fields
    this.podcastForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      hostName: ['', Validators.required],
      category: ['', Validators.required],
      releaseDate: ['', Validators.required], 
      contactEmail:['',Validators.required],
      episodeCount: ['', [Validators.required, Validators.min(1)]], // Minimum 1 episode
    });
  }

  ngOnInit(): void {}

  // Add a new podcast
  addPodcast(): void {
    if (this.podcastForm.valid) {
      console.log(this.podcastForm.value);
      this.podcastService.addPodcast(this.podcastForm.value)
        .subscribe(
          (res) => {
            console.log('Podcast added successfully:', res);
            this.router.navigateByUrl('/podcastsList');
            // Optionally reset the form or show a success message
            this.podcastForm.reset();
          },
          (err) => {
            console.error('Error adding podcast:', err);
            // Handle error, show error message to the user
          }
        );
    } else {
      console.log('Form is invalid');
    }
  }
}
