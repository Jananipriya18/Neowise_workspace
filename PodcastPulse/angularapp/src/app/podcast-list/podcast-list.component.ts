import { Component, OnInit } from '@angular/core';
import { Podcast } from '../model/podcast.model';
import { PodcastService } from '../services/podcast.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-podcast-list',
  templateUrl: './podcast-list.component.html',
  styleUrls: ['./podcast-list.component.css'],
})
export class PodcastListComponent implements OnInit {
  podcasts: Podcast[] = [];

  constructor(private podcastService: PodcastService,private router: Router) {}

  ngOnInit(): void {
    this.getPodcasts();
  }

  getPodcasts(): void {
    try {
      this.podcastService.getPodcast().subscribe(
        (res) => {
          console.log(res);
          this.podcasts = res;
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  editPodcast(id: number): void {
    this.router.navigate(['/edit', id]);
  }


  deletePodcast(id: any): void {
    this.podcastService.deletePodcast(id).subscribe(() => {
      this.podcasts = this.podcasts.filter((podcast) => podcast.id !== id);
    });
  }
}
