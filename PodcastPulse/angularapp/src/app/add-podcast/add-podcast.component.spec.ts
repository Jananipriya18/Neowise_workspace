import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import {
  FormBuilder,
  ReactiveFormsModule, FormsModule,
} from '@angular/forms';
import { AddPodcastComponent } from './add-podcast.component';
import { PodcastService } from '../services/podcast.service'; // Adjust the path as necessary
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddPodcastComponent', () => {
  let component: AddPodcastComponent;
  let fixture: ComponentFixture<AddPodcastComponent>;
  let service: PodcastService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddPodcastComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [PodcastService],
    });
    fixture = TestBed.createComponent(AddPodcastComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(PodcastService);
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
  });

  fit('should_create_AddPodcastComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_add_a_new_podcast_when_form_is_valid', fakeAsync(() => {
    const validPodcastData = {
      title: 'Tech Talks',
      description: 'A podcast about the latest in tech.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date(),
      episodeCount: 10,
      contactEmail: 'demo@gmail.com'
    };
    spyOn(service, 'addPodcast').and.returnValue(of(validPodcastData));
    component.podcastForm.setValue(validPodcastData);
    let value: boolean = component.podcastForm.valid;
    component.addPodcast();
    tick();
    expect(value).toBeTruthy();
    expect(service.addPodcast).toHaveBeenCalledWith(validPodcastData);
  }));

  fit('should_validate_required_fields', () => {
    const form = component.podcastForm;
    form.setValue({
      title: '',
      description: '',
      hostName: '',
      category: '',
      releaseDate: '',
      episodeCount: '',
      contactEmail: ''
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('title')?.hasError('required')).toBeTruthy();
    expect(form.get('description')?.hasError('required')).toBeTruthy();
    expect(form.get('hostName')?.hasError('required')).toBeTruthy();
    expect(form.get('category')?.hasError('required')).toBeTruthy();
    expect(form.get('releaseDate')?.hasError('required')).toBeTruthy();
    expect(form.get('episodeCount')?.hasError('required')).toBeTruthy();
    expect(form.get('contactEmail')?.hasError('required')).toBeTruthy();
  });

  fit('should_validate_episode_count', () => {
    const podcastForm = component.podcastForm;
    podcastForm.setValue({
      title: 'Tech Talks',
      description: 'A podcast about the latest in tech.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date(),
      episodeCount: 0,
      contactEmail: 'demo@gmail.com'
    });
    expect(podcastForm.valid).toBeFalsy();
    expect(podcastForm.get('episodeCount')?.hasError('min')).toBeTruthy();
    
    podcastForm.setValue({
      title: 'Tech Talks',
      description: 'A podcast about the latest in tech.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date(),
      episodeCount: 10,
      contactEmail: 'demo@gmail.com'
    });
    expect(podcastForm.valid).toBeTruthy();
    expect(podcastForm.get('episodeCount')?.hasError('min')).toBeFalsy();
  });

});
