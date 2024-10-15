import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CartoonEpisodeFormComponent } from './cartoonepisode-form.component';
import { CartoonEpisodeService } from '../services/cartoonepisode.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { CartoonEpisode } from '../models/cartoonepisode.model';

describe('CartoonEpisodeFormComponent', () => {
  let component: CartoonEpisodeFormComponent;
  let fixture: ComponentFixture<CartoonEpisodeFormComponent>;
  let cartoonEpisodeService: CartoonEpisodeService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CartoonEpisodeFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [CartoonEpisodeService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CartoonEpisodeFormComponent);
    component = fixture.componentInstance;
    cartoonEpisodeService = TestBed.inject(CartoonEpisodeService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  fit('should_create_CartoonEpisodeFormComponent', () => {
    expect(component).toBeTruthy();
  });

  // fit('CartoonEpisodeFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', () => {
  //   // Set all fields to empty strings
  //   component.newEpisode = {
  //     episodeId: 0,
  //     cartoonSeriesName: '',
  //     episodeTitle: '',
  //     releaseDate: '',
  //     directorName: '',
  //     duration: '',
  //     description: ''
  //   } as CartoonEpisode;

  //   // Manually trigger form submission
  //   component.formSubmitted = true;
  //   fixture.detectChanges();

  //   // Find the form element
  //   const form = fixture.debugElement.query(By.css('form')).nativeElement;

  //   // Trigger the form submission
  //   form.dispatchEvent(new Event('submit'));

  //   fixture.detectChanges();

  //   // Check if error messages are rendered for each field
  //   expect(fixture.debugElement.query(By.css('#cartoonSeriesName + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#episodeTitle + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#releaseDate + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#directorName + .error'))).toBeTruthy();
  //   expect(fixture.debugElement.query(By.css('#duration + .error'))).toBeTruthy();
  // });

  fit('CartoonEpisodeFormComponent_should_call_addCartoonEpisode_method_while_adding_the_episode', () => {
    // Create a mock CartoonEpisode object with all required properties
    const episode: CartoonEpisode = {
      episodeId: 1,
      cartoonSeriesName: 'Test Cartoon Series Name',
      episodeTitle: 'Test Episode Title',
      releaseDate: 'Test Release Date',
      directorName: 'Test Director Name',
      duration: 'Test Duration',
      description: 'Test Description'
    };

    const addCartoonEpisodeSpy = spyOn(component, 'addCartoonEpisode').and.callThrough();
    component.addCartoonEpisode();
    expect(addCartoonEpisodeSpy).toHaveBeenCalled();
  });
});
