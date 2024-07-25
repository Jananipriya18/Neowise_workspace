import { Playlist } from './playlist.model'; // Import Playlist model

describe('Playlist', () => { // Updated description to 'Playlist'
  fit('should_create_event_instance', () => { // Updated 'fit' to 'it' and updated test description
    const event: Playlist = { // Updated 'car' to 'event' and 'Car' to 'Playlist'
      playlistId: 1, // Adjusted property name
      playlistName: 'Test Playlist Name', // Adjusted property name
      songName: 'Test Playlist Description', // Adjusted property name
      yearOfRelease: 'Test Playlist Date', // Adjusted property name
      artistName: 'Test Playlist Time', // Adjusted property name
      genre: 'Test Playlist Location', // Adjusted property name
      MovieName: 'Test Playlist Organizer' // Adjusted property name
    };

    // Check if the event object exists
    expect(event).toBeTruthy();

    // Check individual properties of the event
    expect(event.playlistId).toBe(1); // Adjusted property name
    expect(event.playlistName).toBe('Test Playlist Name'); // Adjusted property name
    expect(event.songName).toBe('Test Playlist Description'); // Adjusted property name
    expect(event.yearOfRelease).toBe('Test Playlist Date'); // Adjusted property name
    expect(event.artistName).toBe('Test Playlist Time'); // Adjusted property name
    expect(event.genre).toBe('Test Playlist Location'); // Adjusted property name
    expect(event.MovieName).toBe('Test Playlist Organizer'); // Adjusted property name
  });
});
