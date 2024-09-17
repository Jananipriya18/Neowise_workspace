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
import { AddComicComponent } from './add-comic.component'; // Changed to AddComicComponent
import { ComicService } from '../services/comic.service'; // Changed to ComicService
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

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
    formBuilder = TestBed.inject(FormBuilder) as any;
    fixture = TestBed.createComponent(AddComicComponent) as any;
    component = fixture.componentInstance as any;
    service = TestBed.inject(ComicService) as any;
    fixture.detectChanges();
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
  });

  fit('should_create_AddComicComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_add_a_new_comic_when_form_is_valid', fakeAsync(() => {
    const validComicData = {
      title: 'Amazing Spider-Man',
      author: 'Stan Lee',
      series: 'Spider-Man',
      publisher: 'Marvel',
      publicationDate: '1963-03-01',
      genre: 'Superhero',
      description: 'A story about Peter Parker, who gains spider-like abilities.',
    };

    spyOn(service, 'addComic').and.returnValue(of(validComicData));
    component.comicForm.setValue(validComicData); 
    let value: boolean = component.comicForm.valid;
    component.addComic();
    tick();
    expect(value).toBeTruthy();
    expect(service.addComic).toHaveBeenCalledWith(validComicData);  
  }));

  fit('should_validate_all_the_required_fields', () => {
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

  fit('should_validate_publication_date_format', () => {
    const comicForm = component.comicForm;
    comicForm.setValue({
      title: 'Amazing Spider-Man',
      author: 'Stan Lee',
      series: 'Spider-Man',
      publisher: 'Marvel',
      publicationDate: '1963-03-01',
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
