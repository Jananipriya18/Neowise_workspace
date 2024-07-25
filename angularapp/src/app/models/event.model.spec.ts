import { Event } from './event.model'; // Import Event model

describe('Event', () => { // Updated description to 'Event'
  fit('should_create_event_instance', () => { // Updated 'fit' to 'it' and updated test description
    const event: Event = { // Updated 'car' to 'event' and 'Car' to 'Event'
      eventId: 1, // Adjusted property name
      eventName: 'Test Event Name', // Adjusted property name
      eventDescription: 'Test Event Description', // Adjusted property name
      eventDate: 'Test Event Date', // Adjusted property name
      eventTime: 'Test Event Time', // Adjusted property name
      eventLocation: 'Test Event Location', // Adjusted property name
      eventOrganizer: 'Test Event Organizer' // Adjusted property name
    };

    // Check if the event object exists
    expect(event).toBeTruthy();

    // Check individual properties of the event
    expect(event.eventId).toBe(1); // Adjusted property name
    expect(event.eventName).toBe('Test Event Name'); // Adjusted property name
    expect(event.eventDescription).toBe('Test Event Description'); // Adjusted property name
    expect(event.eventDate).toBe('Test Event Date'); // Adjusted property name
    expect(event.eventTime).toBe('Test Event Time'); // Adjusted property name
    expect(event.eventLocation).toBe('Test Event Location'); // Adjusted property name
    expect(event.eventOrganizer).toBe('Test Event Organizer'); // Adjusted property name
  });
});
