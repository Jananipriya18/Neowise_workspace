import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import {
  FormBuilder,
  ReactiveFormsModule,
  FormsModule,
  Validators,
} from '@angular/forms';
import { AddComicComponent } from './add-comic.component';
import { ComicService } from '../services/comic.service';
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { Comic } from '../model/comic.model';

describe('AddComicComponent', () => {
  let component: AddComicComponent;
  let fixture: ComponentFixture<AddComicComponent>;
  let service: ComicService;
  let debugElement: DebugElement;
  let formBuilder: FormBuilder;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddComicComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [ComicService],
    });

    formBuilder = TestBed.inject(FormBuilder);
    fixture = TestBed.createComponent(AddComicComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(ComicService);
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
  });

  fit('should create AddComicComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should add a new comic when form is valid', fakeAsync(() => {
    const validComicData: Comic = {
      id: 1,
      title: 'Amazing Spider-Man',
      author: 'Stan Lee',
      series: 'Spider-Man',
      publisher: 'Marvel',
      publicationDate: new Date('2017-07-07'),
      genre: 'Superhero',
      description: 'A story about Peter Parker, who gains spider-like abilities.',
    };

    spyOn(service, 'addComic').and.returnValue(of(validComicData));
    component.comicForm.setValue({
      title: 'Amazing Spider-Man',
      author: 'Stan Lee',
      series: 'Spider-Man',
      publisher: 'Marvel',
      publicationDate: '2017-07-07', 
      genre: 'Superhero',
      description: 'A story about Peter Parker, who gains spider-like abilities.',
    });

    component.addComic();
    tick();

    expect(component.comicForm.valid).toBeTruthy();
    expect(service.addComic).toHaveBeenCalledWith(validComicData);
  }));

  fit('should validate all the required fields', () => {
    const form = component.comicForm;
    form.setValue({
      title: '',
      author: '',
      series: '',
      publisher: '',
      publicationDate: '',
      genre: '',
      description: '',
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('title')?.hasError('required')).toBeTruthy();
    expect(form.get('author')?.hasError('required')).toBeTruthy();
    expect(form.get('series')?.hasError('required')).toBeTruthy();
    expect(form.get('publisher')?.hasError('required')).toBeTruthy();
    expect(form.get('publicationDate')?.hasError('required')).toBeTruthy();
    expect(form.get('genre')?.hasError('required')).toBeTruthy();
  });

  fit('should validate publication date format', () => {
    const comicForm = component.comicForm;
    comicForm.setValue({
      title: 'Amazing Spider-Man',
      author: 'Stan Lee',
      series: 'Spider-Man',
      publisher: 'Marvel',
      publicationDate: '2017-07-07', // Valid date format
      genre: 'Superhero',
      description: 'A story about Peter Parker, who gains spider-like abilities.',
    });

    expect(comicForm.valid).toBeTruthy();

    comicForm.setValue({
      title: 'Amazing Spider-Man',
      author: 'Stan Lee',
      series: 'Spider-Man',
      publisher: 'Marvel',
      publicationDate: '03-01-1963', // Invalid date format
      genre: 'Superhero',
      description: 'A story about Peter Parker, who gains spider-like abilities.',
    });

    expect(comicForm.valid).toBeFalsy();
    expect(comicForm.get('publicationDate')?.hasError('pattern')).toBeTruthy();
  });

});
