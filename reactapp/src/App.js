// import React from 'react';
// import BookReader from './components/BookReader';

// function App() {
//   const reader = {
//     name: 'Jane Doe',
//     age: 28,
//     email: 'jane.doe@example.com',
//     library: {
//       name: 'Central Library',
//       location: 'Downtown',
//     },
//   };

//   return (
//     <div>
//       <BookReader
//         name={reader.name}
//         age={reader.age}
//         email={reader.email}
//         library={reader.library}
//       />
//     </div>
//   );
// }

// export default App;


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
  };

  // Inline styles for BookReader and Library components
  const styles = {
    bookReaderContainer: {
      backgroundColor: '#f9f9f9',
      padding: '20px',
      borderRadius: '10px',
      maxWidth: '600px',
      margin: '0 auto',
      boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
    },
    heading: {
      fontSize: '24px',
      color: '#333',
      marginBottom: '15px',
    },
    subHeading: {
      fontSize: '20px',
      color: '#555',
      marginBottom: '10px',
    },
    paragraph: {
      fontSize: '16px',
      color: '#666',
      marginBottom: '8px',
    },
    libraryContainer: {
      backgroundColor: '#fff',
      padding: '15px',
      borderRadius: '8px',
      border: '1px solid #ddd',
      marginTop: '20px',
    },
  };

  return (
    <div style={styles.bookReaderContainer}>
      <h2 style={styles.heading}>Book Reader Profile</h2>
      <h3 style={styles.subHeading}>{reader.name}</h3>
      <p style={styles.paragraph}>Age: {reader.age}</p>
      <p style={styles.paragraph}>Email: {reader.email}</p>
      <h3 style={styles.subHeading}>Library:</h3>
      <div style={styles.libraryContainer}>
        <p style={styles.paragraph}>Name: {reader.library.name}</p>
        <p style={styles.paragraph}>Location: {reader.library.location}</p>
      </div>
    </div>
  );
}

export default App;
