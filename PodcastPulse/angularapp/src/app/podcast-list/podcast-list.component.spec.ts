import { ComponentFixture, TestBed } from '@angular/core/testing'; 
import { PodcastListComponent } from './podcast-list.component';
import { PodcastService } from '../services/podcast.service';
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
      episodeCount: 10 // Higher episode count
    },
    {
      id: 2,
      title: 'Science Weekly',
      description: 'A podcast about science.',
      hostName: 'Jane Smith',
      category: 'Science',
      releaseDate: new Date('2023-09-01'),
      contactEmail: 'contact@scienceweekly.com',
      episodeCount: 5 // Lower episode count
    }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [PodcastListComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [PodcastService, { provide: Router, useValue: spy }]
    });

    fixture = TestBed.createComponent(PodcastListComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(PodcastService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_PodcastListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_call_getPodcast', () => {
    spyOn(service, 'getPodcast').and.returnValue(of(mockPodcasts)); // Mock the service call
    component.ngOnInit();
    expect(service.getPodcast).toHaveBeenCalled();
    expect(component.podcasts).toEqual(mockPodcasts);
  });

  // Test for the recommended podcast feature based on the highest episode count
  fit('should_display_podcast_with_highest_episode_count_as_recommended', () => {
    spyOn(service, 'getPodcast').and.returnValue(of(mockPodcasts)); // Mock the service call
    component.ngOnInit();
    fixture.detectChanges(); // Trigger change detection
  
    // "Tech Talks" should be the recommended podcast (since it has the most episodes)
    const recommendedRow = fixture.debugElement.query(By.css('.recommended-row'));
  
    expect(recommendedRow).toBeTruthy(); // Check if the recommended row exists
    const content = recommendedRow.nativeElement.textContent.trim();
    expect(content).toContain('Recommended Episode: Tech Talks (10 episodes)'); // Verify content for "Tech Talks"
  });

});
