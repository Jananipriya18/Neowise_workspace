import { Component, OnInit } from '@angular/core';
import { Comic } from '../model/comic.model';
import { ComicService } from '../services/comic.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-comic-list',
  templateUrl: './comic-list.component.html',
  styleUrls: ['./comic-list.component.css'],
})
export class ComicListComponent implements OnInit {
  comics: Comic[] = [];

  constructor(private comicService: ComicService,private router: Router) {}

  ngOnInit(): void {
    this.getComics();
  }

  getComics(): void {
    try {
      this.comicService.getComics().subscribe(
        (res) => {
          console.log(res);
          this.comics = res;
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  editComic(id: number): void {
    this.router.navigate(['/edit', id]);
  }


  deleteComic(id: any): void {
    this.comicService.deleteComic(id).subscribe(() => {
      this.comics = this.comics.filter((comic) => comic.id !== id);
    });
  }
}
