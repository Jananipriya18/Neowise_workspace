// src/App.js
import React from 'react';
import UserProfile from './components/UserProfile';

function App() {
  const user = {
    name: 'John Doe',
    age: 30,
    email: 'john.doe@example.com',
    address: {
      city: 'New York',
      country: 'USA',
    },
  };

  return (
    <div>
      <UserProfile
        name={user.name}
        age={user.age}
        email={user.email}
        address={user.address}
      />
    </div>
  );
}

export default App;
