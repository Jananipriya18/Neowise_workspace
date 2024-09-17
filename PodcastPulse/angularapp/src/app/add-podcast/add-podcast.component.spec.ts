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
import { DebugElement } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddPodcastComponent', () => {
  let component: AddPodcastComponent;
  let fixture: ComponentFixture<AddPodcastComponent>;
  let service: PodcastService;
  let debugElement: DebugElement;
  let formBuilder: FormBuilder;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddPodcastComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [PodcastService],
    });
    formBuilder = TestBed.inject(FormBuilder) as any;
    fixture = TestBed.createComponent(AddPodcastComponent) as any;
    component = fixture.componentInstance as any;
    service = TestBed.inject(PodcastService) as any;
    fixture.detectChanges();
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
      contactEmail: 'host@techtalks.com',
      episodeCount: 10,
    };
    spyOn((service as any), 'addPodcast').and.returnValue(of(validPodcastData));
    (component as any).podcastForm.setValue(validPodcastData);
    let value: boolean = (component as any).podcastForm.valid;
    (component as any).addPodcast();
    tick();
    expect(value).toBeTruthy();
    expect((service as any).addPodcast).toHaveBeenCalledWith(validPodcastData);
  }));

  fit('should_validate_required_fields', () => {
    const form = (component as any).podcastForm;
    form.setValue({
      title: '',
      description: '',
      hostName: '',
      category: '',
      releaseDate: '',
      contactEmail: '',
      episodeCount: '',
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('title')?.hasError('required')).toBeTruthy();
    expect(form.get('description')?.hasError('required')).toBeTruthy();
    expect(form.get('hostName')?.hasError('required')).toBeTruthy();
    expect(form.get('category')?.hasError('required')).toBeTruthy();
    expect(form.get('releaseDate')?.hasError('required')).toBeTruthy();
    expect(form.get('episodeCount')?.hasError('required')).toBeTruthy();
  });

  fit('should_validate_email_format', () => {
    const podcastForm = (component as any).podcastForm;
    podcastForm.setValue({
      title: 'Tech Talks',
      description: 'A podcast about the latest in tech.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date(),
      contactEmail: 'invalid-email',
      episodeCount: 10,
    });
    expect(podcastForm.valid).toBeFalsy();
    expect(podcastForm.get('contactEmail')?.hasError('email')).toBeTruthy();
    
    podcastForm.setValue({
      title: 'Tech Talks',
      description: 'A podcast about the latest in tech.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date(),
      contactEmail: 'host@techtalks.com',
      episodeCount: 10,
    });
    expect(podcastForm.valid).toBeTruthy();
    expect(podcastForm.get('contactEmail')?.hasError('email')).toBeFalsy();
  });

});
