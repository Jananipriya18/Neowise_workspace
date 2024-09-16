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
import { AddGameComponent } from './add-game.component';
import { GameService } from '../services/game.service';
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddGameComponent', () => {
  let component: AddGameComponent;
  let fixture: ComponentFixture<AddGameComponent>;
  let service: GameService;
  let debugElement: DebugElement;
  let formBuilder: FormBuilder;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddGameComponent],
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        FormsModule,
      ],
      providers: [GameService],
    });
    formBuilder = TestBed.inject(FormBuilder) as any;
    fixture = TestBed.createComponent(AddGameComponent) as any;
    component = fixture.componentInstance as any;
    service = TestBed.inject(GameService) as any;
    fixture.detectChanges();
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
  });

  fit('should_create_AddGameComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_add_a_new_game_when_form_is_valid', fakeAsync(() => {
    const validGameData = {
      title: 'The Witcher 3: Wild Hunt',
      releaseYear: 2015,
      genre: 'Action RPG',
      developer: 'CD Projekt Red',
      supportContact: 'support@cdprojektred.com',
    };
    spyOn((service as any), 'addGame').and.returnValue(of(validGameData));
    (component as any).gameForm.setValue(validGameData);
    let value: boolean = (component as any).gameForm.valid;
    (component as any).addGame();
    tick();
    expect(value).toBeTruthy();
    expect((service as any).addGame).toHaveBeenCalledWith(validGameData);
  }));

  fit('should_add_all_the_required_fields', () => {
    const form = (component as any).gameForm;
    form.setValue({
      title: '',
      releaseYear: '',
      genre: '',
      developer: '',
      supportContact: '',
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('title')?.hasError('required')).toBeTruthy();
    expect(form.get('releaseYear')?.hasError('required')).toBeTruthy();
    expect(form.get('genre')?.hasError('required')).toBeTruthy();
    expect(form.get('developer')?.hasError('required')).toBeTruthy();
    expect(form.get('supportContact')?.hasError('required')).toBeTruthy();
  });

  fit('should_validate_support_contact', () => {
    const gameForm = (component as any).gameForm;
    gameForm.setValue({
      title: 'The Witcher 3: Wild Hunt',
      releaseYear: 2015,
      genre: 'Action RPG',
      developer: 'CD Projekt Red',
      supportContact: 'support@cdprojektred.com',
    });
    expect(gameForm.valid).toBeTruthy();
    expect(gameForm.get('supportContact')?.hasError('pattern')).toBeFalsy();

    gameForm.setValue({
      title: 'The Witcher 3: Wild Hunt',
      releaseYear: 2015,
      genre: 'Action RPG',
      developer: 'CD Projekt Red',
      supportContact: 'invalidEmail',
    });
    expect(gameForm.valid).toBeFalsy();
    expect(gameForm.get('supportContact')?.hasError('pattern')).toBeTruthy();
  });
});
