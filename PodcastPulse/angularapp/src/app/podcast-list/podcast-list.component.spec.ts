import { ComponentFixture, TestBed } from '@angular/core/testing'; 
import { PodcastListComponent } from './podcast-list.component'; // Adjust the component path if necessary
import { PodcastService } from '../services/podcast.service'; // Adjust the service path if necessary
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Podcast } from '../model/podcast.model';
import { By } from '@angular/platform-browser';

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
    },
    {
      id: 2,
      title: 'Science Weekly',
      description: 'A podcast about science.',
      hostName: 'Jane Smith',
      category: 'Science',
      releaseDate: new Date('2023-09-01'),
      contactEmail: 'contact@scienceweekly.com',
      episodeCount: 5
    }
  ];
  const mockRecommendedPodcast: Podcast = {
    id: 2,
    title: 'Science Weekly',
    description: 'A podcast about science.',
    hostName: 'Jane Smith',
    category: 'Science',
    releaseDate: new Date('2023-09-01'),
    contactEmail: 'contact@scienceweekly.com',
    episodeCount: 5
  };

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

  // Test for the recommended podcast feature
  fit('should_display_recommended_podcast_if_present', () => {
    component.recommendedPodcast = mockRecommendedPodcast;
    component.podcasts = mockPodcasts;
    
    fixture.detectChanges(); // Trigger change detection

    const recommendedRow = fixture.debugElement.query(By.css('tr[style*="background-color: #ffeb3b"]')); // Find the row by style

    expect(recommendedRow).toBeTruthy(); // Check if the row exists
    const content = recommendedRow.nativeElement.textContent.trim();
    expect(content).toContain('Recommended Episode: Science Weekly (5 episodes)'); // Check if content is correct
  });

  fit('should_not_display_recommended_podcast_if_not_present', () => {
    component.recommendedPodcast = null; // No recommended podcast
    component.podcasts = mockPodcasts;

    fixture.detectChanges(); // Trigger change detection

    const recommendedRow = fixture.debugElement.query(By.css('tr[style*="background-color: #ffeb3b"]')); // Find the row by style

    expect(recommendedRow).toBeNull(); // The row should not exist

});
