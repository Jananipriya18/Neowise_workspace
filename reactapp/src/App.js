import React from 'react';
import BookReader from './components/BookReader';

function App() {
  const reader = {
    name: 'Jane Doe',
    age: 28,
    email: 'jane.doe@example.com',
    library: {
      name: 'Central Library',
      location: 'Downtown',
    },
    favoriteGenres: ['Science Fiction', 'Fantasy', 'Mystery'],
    membershipSince: '2015',
    booksRead: 120,
    preferredFormat: 'E-books',
  };

  return (
    <div>
      <BookReader
        name={reader.name}
        age={reader.age}
        email={reader.email}
        library={reader.library}
        favoriteGenres={reader.favoriteGenres}
        membershipSince={reader.membershipSince}
        booksRead={reader.booksRead}
        preferredFormat={reader.preferredFormat}
      />
    </div>
  );
}

export default App;
