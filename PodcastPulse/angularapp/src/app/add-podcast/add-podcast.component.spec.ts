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

  it('should create AddPodcastComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should add a new podcast when form is valid', fakeAsync(() => {
    const validPodcastData = {
      title: 'Tech Talks',
      description: 'A podcast about the latest in tech.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date(),
      episodeCount: 10,
    };
    spyOn(service, 'addPodcast').and.returnValue(of(validPodcastData));
    component.podcastForm.setValue(validPodcastData);
    let value: boolean = component.podcastForm.valid;
    component.addPodcast();
    tick();
    expect(value).toBeTruthy();
    expect(service.addPodcast).toHaveBeenCalledWith(validPodcastData);
  }));

  it('should validate required fields', () => {
    const form = component.podcastForm;
    form.setValue({
      title: '',
      description: '',
      hostName: '',
      category: '',
      releaseDate: '',
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

  it('should validate episode count', () => {
    const podcastForm = component.podcastForm;
    podcastForm.setValue({
      title: 'Tech Talks',
      description: 'A podcast about the latest in tech.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date(),
      episodeCount: 0,
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
    });
    expect(podcastForm.valid).toBeTruthy();
    expect(podcastForm.get('episodeCount')?.hasError('min')).toBeFalsy();
  });

});
