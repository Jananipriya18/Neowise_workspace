import React from 'react';

function Library({ name, location }) {
  return (
    <div>
      <p>Name: {name}</p> {/* Display library name */}
      <p>Location: {location}</p> {/* Display library location */}
    </div>
  );
}

export default Library;
