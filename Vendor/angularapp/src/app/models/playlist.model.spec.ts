import { Playlist } from './playlist.model'; 
describe('Playlist', () => { 
  fit('should_create_playlist_instance', () => { 
    const event: Playlist = { 
      playlistId: 1, 
      playlistName: 'Test Playlist Name', 
      songName: 'Test Playlist Description',
      yearOfRelease: 'Test Playlist Date',
      artistName: 'Test Playlist Time',
      genre: 'Test Playlist Location', 
      MovieName: 'Test Playlist Organizer' 
    };

    // Check if the event object exists
    expect(event).toBeTruthy();

    // Check individual properties of the event
    expect(event.playlistId).toBe(1); 
    expect(event.playlistName).toBe('Test Playlist Name'); 
    expect(event.songName).toBe('Test Playlist Description'); 
    expect(event.yearOfRelease).toBe('Test Playlist Date'); 
    expect(event.artistName).toBe('Test Playlist Time'); 
    expect(event.genre).toBe('Test Playlist Location'); 
    expect(event.MovieName).toBe('Test Playlist Organizer'); 
  });
});
