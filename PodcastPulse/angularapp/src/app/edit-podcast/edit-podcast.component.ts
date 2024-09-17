// edit-podcast.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Podcast } from '../model/podcast.model';
import { PodcastService } from '../services/podcast.service';

@Component({
  selector: 'app-edit-podcast',
  templateUrl: './edit-podcast.component.html',
  styleUrls: ['./edit-podcast.component.css']
})
export class EditPodcastComponent implements OnInit {
  podcast: Podcast | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private podcastService: PodcastService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.podcastService.getPodcastById(+id).subscribe(
        (podcast) => (this.podcast = podcast),
        (err) => console.error(err)
      );
    }
  }

  savePodcast(): void {
    if (this.podcast) {
      this.podcastService.updatePodcast(this.podcast.id, this.podcast).subscribe(
        () => {
          this.router.navigate(['/podcastsList']);
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/podcastsList']);
  }
}
