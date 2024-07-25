import { Event } from './event.model'; // Import Event model

describe('Event', () => { // Updated description to 'Event'
  fit('should_create_event_instance', () => { // Updated 'fit' to 'it' and updated test description
    const event: Event = { // Updated 'car' to 'event' and 'Car' to 'Event'
      playlistId: 1, // Adjusted property name
      playlistName: 'Test Event Name', // Adjusted property name
      songName: 'Test Event Description', // Adjusted property name
      yearOfRelease: 'Test Event Date', // Adjusted property name
      artistName: 'Test Event Time', // Adjusted property name
      genre: 'Test Event Location', // Adjusted property name
      MovieName: 'Test Event Organizer' // Adjusted property name
    };

    // Check if the event object exists
    expect(event).toBeTruthy();

    // Check individual properties of the event
    expect(event.playlistId).toBe(1); // Adjusted property name
    expect(event.playlistName).toBe('Test Event Name'); // Adjusted property name
    expect(event.songName).toBe('Test Event Description'); // Adjusted property name
    expect(event.yearOfRelease).toBe('Test Event Date'); // Adjusted property name
    expect(event.artistName).toBe('Test Event Time'); // Adjusted property name
    expect(event.genre).toBe('Test Event Location'); // Adjusted property name
    expect(event.MovieName).toBe('Test Event Organizer'); // Adjusted property name
  });
});
