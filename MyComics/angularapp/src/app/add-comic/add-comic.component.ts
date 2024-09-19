// add-comic.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ComicService } from '../services/comic.service';
import { Router } from '@angular/router';
import { Comic } from '../model/comic.model';

@Component({
  selector: 'app-add-comic',
  templateUrl: './add-comic.component.html',
  styleUrls: ['./add-comic.component.css']
})
export class AddComicComponent implements OnInit {
  comicForm: FormGroup;
  genres: string[] = ['Superhero', 'Fantasy', 'Horror', 'Science Fiction'];

  constructor(private formBuilder: FormBuilder, private comicService: ComicService, private router: Router) {
    this.comicForm = this.formBuilder.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      series: ['', Validators.required],
      publisher: ['', Validators.required],
      genre: ['', Validators.required], // Keep genre as a required field
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  addComic(): void {
    if (this.comicForm.valid) {
      this.comicService.addComic(this.comicForm.value).subscribe(
        (res) => {
          console.log('Comic added successfully:', res);
          this.router.navigateByUrl('/comics');
          this.comicForm.reset();
        },
        (err) => {
          console.error('Error adding comic:', err);
        }
      );
    }
  }
}
