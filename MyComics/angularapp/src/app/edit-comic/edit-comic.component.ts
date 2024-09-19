import { Component, OnInit } from '@angular/core';
import { ComicService } from '../services/comic.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Comic } from '../model/comic.model';

@Component({
  selector: 'app-edit-comic',
  templateUrl: './edit-comic.component.html',
  styleUrls: ['./edit-comic.component.css']
})
export class EditComicComponent implements OnInit {
  comic: Comic | undefined;
  genres: string[] = ['Superhero', 'Fantasy', 'Horror', 'Science Fiction'];

  constructor(
    private comicService: ComicService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.comicService.getComicById(+id).subscribe((comic) => (this.comic = comic));
    }
  }

  saveComic(): void {
    if (this.comic && this.comic.id) { // Ensure comic and id exist
      this.comicService.updateComic(this.comic.id, this.comic).subscribe(() => {
        this.router.navigateByUrl('/comicsList');
      });
    }
  }
  

  cancel(): void {
    this.router.navigateByUrl('/comicsList');
  }
}
