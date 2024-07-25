import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { PlaylistService } from './playlist.service'; // Adjusted service import
import { Playlist } from '../models/playlist.model';

describe('PlaylistService', () => {
  let service: PlaylistService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PlaylistService], // Changed service provider to PlaylistService
    });
    service = TestBed.inject(PlaylistService); // Changed service variable assignment to PlaylistService
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('PlaylistService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('PlaylistService_should_add_an_event_and_return_it', () => {
    const mockPlaylist: Playlist = {
      playlistId: 100,
      playlistName: 'Test Playlist Name',
      songName: 'Test Playlist Description',
      yearOfRelease: 'Test Playlist Date',
      artistName: 'Test Playlist Time',
      genre: 'Test Playlist Location',
      MovieName: 'Test Playlist Organizer'
    };

    service.addPlaylist(mockPlaylist).subscribe((event) => {
      expect(event).toEqual(mockPlaylist);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Playlist`); // Adjusted API endpoint
    expect(req.request.method).toBe('POST');
    req.flush(mockPlaylist);
  });

  fit('PlaylistService_should_get_events', () => {
    const mockPlaylists: Playlist[] = [
      {
        playlistId: 100,
        playlistName: 'Test Playlist Name',
        songName: 'Test Playlist Description',
        yearOfRelease: 'Test Playlist Date',
        artistName: 'Test Playlist Time',
        genre: 'Test Playlist Location',
        MovieName: 'Test Playlist Organizer'
      }
    ];

    service.getPlaylists().subscribe((events) => {
      expect(events).toEqual(mockPlaylists);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Playlist`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockPlaylists);
  });

  fit('PlaylistService_should_delete_event', () => {
    const playlistId = 100;

    service.deletePlaylist(playlistId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Playlist/${playlistId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('PlaylistService_should_get_event_by_id', () => {
    const playlistId = 100;
    const mockPlaylist: Playlist = {
      playlistId: playlistId,
      playlistName: 'Test Playlist Name',
      songName: 'Test Playlist Description',
      yearOfRelease: 'Test Playlist Date',
      artistName: 'Test Playlist Time',
      genre: 'Test Playlist Location',
      MovieName: 'Test Playlist Organizer'
    };

    service.getPlaylist(playlistId).subscribe((event) => {
      expect(event).toEqual(mockPlaylist);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Playlist/${playlistId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockPlaylist);
  });

  fit('PlaylistService_should_search_playlistnames', () => {
    const mockPlaylists: Playlist[] = [
      {
        playlistId: 100,
        playlistName: 'Test Playlist Name',
        songName: 'Test Playlist Description',
        yearOfRelease: 'Test Playlist Date',
        artistName: 'Test Playlist Time',
        genre: 'Test Playlist Location',
        MovieName: 'Test Playlist Organizer'
      }
    ];
  
    const searchTerm = 'Apple';
  
    service.searchPlaylists(searchTerm).subscribe((playlists) => {
      expect(playlists).toEqual(mockPlaylists);
    });
  
    const req = httpTestingController.expectOne((request) => 
      request.url.includes(`${service['apiUrl']}/api/Playlist/search`) && 
      request.params.get('searchTerm') === searchTerm
    );
  
    expect(req.request.method).toBe('GET');
    req.flush(mockPlaylists);
  }); 
  
});

