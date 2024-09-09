import React from 'react';
import PerfumeList from './components/PerfumeList';
import './index.css'; // Import the CSS file for styling

function App() {
  // Perfume data
  const perfumes = [
    { name: 'Chanel No. 5', brand: 'Chanel', price: 120 },
    { name: 'Acqua di Gio', brand: 'Giorgio Armani', price: 95 },
    { name: 'Dior Sauvage', brand: 'Christian Dior', price: 110 },
    { name: 'Black Orchid', brand: 'Tom Ford', price: 150 }
  ];

  return (
    <div className="app-container">
      <h1 className="heading">Perfume Collection</h1>
      <PerfumeList perfumes={perfumes} />
    </div>
  );
}

export default App;
