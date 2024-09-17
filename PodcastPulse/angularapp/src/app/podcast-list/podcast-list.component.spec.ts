import { ComponentFixture, TestBed } from '@angular/core/testing'; 
import { PodcastListComponent } from './podcast-list.component'; // Adjust the component path if necessary
import { PodcastService } from '../services/podcast.service'; // Adjust the service path if necessary
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Podcast } from '../model/podcast.model';

describe('PodcastListComponent', () => {
  let component: PodcastListComponent;
  let fixture: ComponentFixture<PodcastListComponent>;
  let service: PodcastService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockPodcasts: Podcast[] = [
    {
      id: 1,
      title: 'Tech Talks',
      description: 'A podcast about technology trends.',
      hostName: 'John Doe',
      category: 'Technology',
      releaseDate: new Date('2023-08-15'),
      contactEmail: 'contact@techtalks.com',
      episodeCount: 10
    }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [PodcastListComponent], // Adjust the component name
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [PodcastService, { provide: Router, useValue: spy }]
    });

    fixture = TestBed.createComponent(PodcastListComponent); // Adjust the component name
    component = fixture.componentInstance;
    service = TestBed.inject(PodcastService); // Adjust the service name
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_PodcastListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_call_getPodcast', () => {
    spyOn(service, 'getPodcast').and.returnValue(of(mockPodcasts)); // Change method to getPodcast
    component.ngOnInit();
    expect(service.getPodcast).toHaveBeenCalled();
    expect(component.podcasts).toEqual(mockPodcasts);
  });

  fit('should_call_deletePodcast', () => {
    spyOn(service, 'deletePodcast').and.returnValue(of()); // Change method to deletePodcast
    component.deletePodcast(1);
    expect(service.deletePodcast).toHaveBeenCalledWith(1);
  });

});
