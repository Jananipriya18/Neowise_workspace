// edit-comic.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Comic } from '../model/comic.model';
import { ComicService } from '../services/comic.service';

@Component({
  selector: 'app-edit-comic',
  templateUrl: './edit-comic.component.html',
  styleUrls: ['./edit-comic.component.css']
})
export class EditComicComponent implements OnInit {
  comic: Comic | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private comicService: ComicService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.comicService.getComicById(+id).subscribe(
        (comic) => (this.comic = comic),
        (err) => console.error(err)
      );
    }
  }

  saveComic(): void {
    if (this.comic) {
      this.comicService.updateComic(this.comic.id, this.comic).subscribe(
        () => {
          this.router.navigate(['/comics']);
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/comics']);
  }
}
