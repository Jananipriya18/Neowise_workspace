import { Tutor } from './tutor.model';

describe('Tutor', () => {
  fit('should_create_tutor_instance', () => {
    const tutor: Tutor = {
      tutorId: 1,
      name: 'Test Tutor',
      email: 'Test Email',
      subjectsOffered: 'Test SubjectsOffered',
      contactNumber: 'Test ContactNumber',
      availability: 'Test Availability'
    };

    // Check if the tutor object exists
    expect(tutor).toBeTruthy();

    // Check individual properties of the tutor
    expect(tutor.tutorId).toBe(1);
    expect(tutor.name).toBe('Test Tutor');
    expect(tutor.email).toBe('Test Email');
    expect(tutor.subjectsOffered).toBe('Test SubjectsOffered');
    expect(tutor.contactNumber).toBe('Test ContactNumber');
    expect(tutor.availability).toBe('Test Availability');
});

});
