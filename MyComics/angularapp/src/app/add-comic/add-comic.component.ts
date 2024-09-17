import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ComicService } from '../services/comic.service'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-comic',
  templateUrl: './add-comic.component.html',
  styleUrls: ['./add-comic.component.css']
})
export class AddComicComponent implements OnInit {
  comicForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private comicService: ComicService,
    private router: Router
  ) {
    this.comicForm = this.formBuilder.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      series: ['', Validators.required],
      publisher: ['', Validators.required],
      publicationDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  addComic(): void {
    if (this.comicForm.valid) {
      console.log(this.comicForm.value);
      this.comicService.addComic(this.comicForm.value).subscribe(
        (res) => {
          console.log('Comic added successfully:', res);
          this.router.navigateByUrl('/comics');
          // Optionally reset the form or show a success message
          this.comicForm.reset();
        },
        (err) => {
          console.error('Error adding comic:', err);
          // Handle error, show error message to the user
        }
      );
    } else {
      console.log('Form is invalid');
    }
  }
}
