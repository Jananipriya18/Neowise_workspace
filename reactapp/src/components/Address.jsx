// src/components/Address.jsx
import React from 'react';

function Address({ city, country }) {
  return (
    <div>
      <p>City: {city}</p> {/* Display city */}
      <p>Country: {country}</p> {/* Display country */}
    </div>
  );
}

export default Address;
