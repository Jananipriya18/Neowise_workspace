import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { VendorService } from './playlist.service'; // Adjusted service import
import { Vendor } from '../models/playlist.model';

describe('VendorService', () => {
  let service: VendorService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [VendorService], // Changed service provider to VendorService
    });
    service = TestBed.inject(VendorService); // Changed service variable assignment to VendorService
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('VendorService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('VendorService_should_add_a_playlist_and_return_it', () => {
    const mockVendor: Vendor = {
      playlistId: 100,
      playlistName: 'Test Vendor Name',
      songName: 'Test Vendor Song Name',
      yearOfRelease: 'Test Vendor Year of Release',
      artistName: 'Test Vendor Artist Name',
      genre: 'Test Vendor Genre',
      MovieName: 'Test Vendor Movie Name'
    };

    service.addVendor(mockVendor).subscribe((event) => {
      expect(event).toEqual(mockVendor);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Vendor`); // Adjusted API endpoint
    expect(req.request.method).toBe('POST');
    req.flush(mockVendor);
  });

  fit('VendorService_should_get_playlists', () => {
    const mockVendors: Vendor[] = [
      {
        playlistId: 100,
        playlistName: 'Test Vendor Name',
        songName: 'Test Vendor Song Name',
        yearOfRelease: 'Test Vendor Year of Release',
        artistName: 'Test Vendor Artist Name',
        genre: 'Test Vendor Genre',
        MovieName: 'Test Vendor Movie Name'
      }
    ];

    service.getVendors().subscribe((events) => {
      expect(events).toEqual(mockVendors);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Vendor`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockVendors);
  });

  fit('VendorService_should_delete_playlist', () => {
    const playlistId = 100;

    service.deleteVendor(playlistId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Vendor/${playlistId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('VendorService_should_get_playlist_by_id', () => {
    const playlistId = 100;
    const mockVendor: Vendor = {
      playlistId: playlistId,
      playlistName: 'Test Vendor Name',
      songName: 'Test Vendor Song Name',
      yearOfRelease: 'Test Vendor Year of Release',
      artistName: 'Test Vendor Artist Name',
      genre: 'Test Vendor Genre',
      MovieName: 'Test Vendor Movie Name'
    };

    service.getVendor(playlistId).subscribe((event) => {
      expect(event).toEqual(mockVendor);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/api/Vendor/${playlistId}`); // Adjusted API endpoint
    expect(req.request.method).toBe('GET');
    req.flush(mockVendor);
  });

  fit('VendorService_should_search_playlistnames', () => {
    const mockVendors: Vendor[] = [
      {
        playlistId: 100,
        playlistName: 'Test Vendor Name',
        songName: 'Test Vendor Song Name',
        yearOfRelease: 'Test Vendor Year of Release',
        artistName: 'Test Vendor Artist Name',
        genre: 'Test Vendor Genre',
        MovieName: 'Test Vendor Movie Name'
      }
    ];
  
    const searchTerm = 'Test Vendor Name';
  
    service.searchVendors(searchTerm).subscribe((playlists) => {
      expect(playlists).toEqual(mockVendors);
    });
  
    const req = httpTestingController.expectOne((request) => 
      request.url.includes(`${service['apiUrl']}/api/Vendor/search`) && 
      request.params.get('searchTerm') === searchTerm
    );
  
    expect(req.request.method).toBe('GET');
    req.flush(mockVendors);
  }); 
  
});

