// src/components/UserProfile.jsx
import React from 'react';
import Address from './Address';

function UserProfile({ name, age, email, address }) {
  return (
    <div>
      <h2>User Profile</h2> {/* Ensure "User Profile" is rendered as per the test */}
      <h3>{name}</h3> {/* Display user name */}
      <p>Age: {age}</p> {/* Display user age */}
      <p>Email: {email}</p> {/* Display user email */}
      
      <h3>Address:</h3>
      <Address city={address.city} country={address.country} /> {/* Pass address props */}
    </div>
  );
}

export default UserProfile;
