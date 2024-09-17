import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PodcastService } from './podcast.service';

describe('PodcastService', () => {
  let service: PodcastService;
  let httpTestingController: HttpTestingController;

  const mockPodcasts = [
    {
      id: 1,
      title: 'The Tech Talk',
      description: 'A podcast discussing the latest in technology.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date('2023-01-01'),
      episodeCount: 10
    },
    {
      id: 2,
      title: 'Health Matters',
      description: 'A show about maintaining a healthy lifestyle.',
      hostName: 'Jane Doe',
      category: 'Health',
      releaseDate: new Date('2023-02-01'),
      episodeCount: 8
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PodcastService],
    });
    service = TestBed.inject(PodcastService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Ensure that there are no outstanding requests after each test
    httpTestingController.verify();
  });

  fit('should_create_service_podcast', () => {
    expect(service).toBeTruthy();
  });

  fit('should_retrieve_podcasts_from_the_API_via_GET', () => {
    (service as any).getPodcast().subscribe((podcasts) => {
      expect(podcasts).toEqual(mockPodcasts);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockPodcasts);
  });

  fit('should_add_a_podcast_via_POST', () => {
    const newPodcast = {
      title: 'Health Matters',
      description: 'A show about maintaining a healthy lifestyle.',
      hostName: 'Jane Doe',
      category: 'Health',
      releaseDate: new Date('2023-02-01'),
      episodeCount: 8
    };
    (service as any).addPodcast(newPodcast).subscribe((podcast) => {
      expect(podcast).toEqual(newPodcast);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('POST');
    req.flush(newPodcast);
  });

  fit('should_update_a_podcast_via_PUT', () => {
    const updatedPodcast = {
      id: 1,
      title: 'The Tech Talk',
      description: 'Updated description about the latest in technology.',
      hostName: 'John Smith',
      category: 'Technology',
      releaseDate: new Date('2023-01-01'),
      episodeCount: 12
    };
    (service as any).updatePodcast(updatedPodcast.id, updatedPodcast).subscribe((podcast) => {
      expect(podcast).toEqual(updatedPodcast);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${updatedPodcast.id}`);
    expect(req.request.method).toEqual('PUT');
    req.flush(updatedPodcast);
  });

  fit('should_delete_a_podcast_via_DELETE', () => {
    const podcastId = 1;
    (service as any).deletePodcast(podcastId).subscribe(() => {
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${podcastId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
