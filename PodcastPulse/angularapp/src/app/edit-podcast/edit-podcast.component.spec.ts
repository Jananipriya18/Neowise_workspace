import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditPodcastComponent } from './edit-podcast.component';
import { PodcastService } from '../services/podcast.service';
import { of, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Podcast } from '../model/podcast.model';

describe('EditPodcastComponent', () => {
  let component: EditPodcastComponent;
  let fixture: ComponentFixture<EditPodcastComponent>;
  let service: PodcastService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockPodcast: Podcast = {
    id: 1,
    title: 'Tech Talks',
    description: 'A podcast about technology trends.',
    hostName: 'John Doe',
    category: 'Technology',
    releaseDate: new Date('2023-01-01'),
    contactEmail: 'host@techtalks.com',
    episodeCount: 12
  };

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [EditPodcastComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        PodcastService,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: spy }
      ]
    });

    fixture = TestBed.createComponent(EditPodcastComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(PodcastService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_EditPodcastComponent', () => {
    expect((component as any)).toBeTruthy();
  });

  fit('should_load_podcast_details_on_init', () => {
    spyOn((service as any), 'getPodcastById').and.returnValue(of(mockPodcast));
    (component as any).ngOnInit();
    fixture.detectChanges();

    expect((service as any).getPodcastById).toHaveBeenCalledWith(1);
    expect((component as any).podcast).toEqual(mockPodcast);
  });

  fit('should_save_podcast_details', () => {
    spyOn((service as any), 'updatePodcast').and.returnValue(of(mockPodcast));
    (component as any).podcast = { ...mockPodcast };
    (component as any).savePodcast();
    
    expect((service as any).updatePodcast).toHaveBeenCalledWith(mockPodcast.id, mockPodcast);
  });
});
