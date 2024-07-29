import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { CartoonEpisodeService } from './cartoon-episode.service'; // Adjusted service import
import { CartoonEpisode } from '../models/cartoon-episode.model';

describe('CartoonEpisodeService', () => {
  let service: CartoonEpisodeService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CartoonEpisodeService], // Changed service provider to CartoonEpisodeService
    });
    service = TestBed.inject(CartoonEpisodeService); // Changed service variable assignment to CartoonEpisodeService
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('CartoonEpisodeService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('CartoonEpisodeService_should_add_an_episode_and_return_it', () => {
    const mockCartoonEpisode: CartoonEpisode = {
      episodeId: 100,
      cartoonSeriesName: 'Test Cartoon Series Name',
      episodeTitle: 'Test Episode Title',
      releaseDate: '2024-07-28',
      directorName: 'Test Director Name',
      duration: 30,
      description: 'Test Description',
      genre: 'Test Genre'
    };

    service.addCartoonEpisode(mockCartoonEpisode).subscribe((episode) => {
      expect(episode).toEqual(mockCartoonEpisode);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CartoonEpisode`); // Adjusted API endpoint
    expect(req.request.method).toBe('POST');
    req.flush(mockCartoonEpisode);
  });

  fit('CartoonEpisodeService_should_get_episodes', () => {
    const mockCartoonEpisodes: CartoonEpisode[] = [
      {
        episodeId: 100,
        cartoonSeriesName: 'Test Cartoon Series Name',
        episodeTitle: 'Test Episode Title',
        releaseDate: '2024-07-28',
        directorName: 'Test Director Name',
        duration: 30,
        description: 'Test Description',
        genre: 'Test Genre'
      }
    ];

    service.getCartoonEpisodes().subscribe((episodes) => {
      expect(episodes).toEqual(mockCartoonEpisodes);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CartoonEpisode`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockCartoonEpisodes);
  });

  fit('CartoonEpisodeService_should_delete_episode', () => {
    const episodeId = 100;

    service.deleteCartoonEpisode(episodeId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CartoonEpisode/${episodeId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('CartoonEpisodeService_should_get_episode_by_id', () => {
    const episodeId = 100;
    const mockCartoonEpisode: CartoonEpisode = {
      episodeId: episodeId,
      cartoonSeriesName: 'Test Cartoon Series Name',
      episodeTitle: 'Test Episode Title',
      releaseDate: '2024-07-28',
      directorName: 'Test Director Name',
      duration: 30,
      description: 'Test Description',
      genre: 'Test Genre'
    };

    service.getCartoonEpisode(episodeId).subscribe((episode) => {
      expect(episode).toEqual(mockCartoonEpisode);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/CartoonEpisode/${episodeId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockCartoonEpisode);
  });

  fit('CartoonEpisodeService_should_search_cartoonEpisodes', () => {
    const mockCartoonEpisodes: CartoonEpisode[] = [
      {
        episodeId: 100,
        cartoonSeriesName: 'Test Cartoon Series Name',
        episodeTitle: 'Test Episode Title',
        releaseDate: '2024-07-28',
        directorName: 'Test Director Name',
        duration: 30,
        description: 'Test Description',
        genre: 'Test Genre'
      }
    ];
  
    const searchTerm = 'Apple';
  
    service.searchCartoonEpisodes(searchTerm).subscribe((episodes) => {
      expect(episodes).toEqual(mockCartoonEpisodes);
    });
  
    const req = httpTestingController.expectOne((request) => 
      request.url.includes(`${service['apiUrl']}/api/CartoonEpisode/search`) && 
      request.params.get('searchTerm') === searchTerm
    );
  
    expect(req.request.method).toBe('GET');
    req.flush(mockCartoonEpisodes);
  }); 
  
});
