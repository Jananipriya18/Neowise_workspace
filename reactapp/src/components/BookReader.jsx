import React from 'react';
import Library from './Library';

const BookReader({ name, age, email, library, membershipSince, booksRead, preferredFormat }) {
  return (
    <div>
      <h2>Book Reader Profile</h2>
      <h3>{name}</h3>
      <p>Age: {age}</p>
      <p>Email: {email}</p>
      <p>Membership Since: {membershipSince}</p> {/* Display membership date */}
      <p>Books Read: {booksRead}</p> {/* Display number of books read */}
      <p>Preferred Format: {preferredFormat}</p> {/* Display preferred reading format */}
      
      <h3>Library:</h3>
      <Library name={library.name} location={library.location} />
    </div>
  );
}

export default BookReader;
