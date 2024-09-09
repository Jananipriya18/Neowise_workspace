import React from 'react';
import PerfumeDisplay from './components/PerfumeDisplay'; // Make sure to import from the correct path
import './assets/css/index.css';

function App() {
  // Perfume data with fragrance
  const perfumes = [
    { name: 'Chanel No. 5', brand: 'Chanel', fragrance: 'Floral', price: 120 },
    { name: 'Acqua di Gio', brand: 'Giorgio Armani', fragrance: 'Citrus', price: 95 },
    { name: 'Dior Sauvage', brand: 'Christian Dior', fragrance: 'Woody', price: 110 },
    { name: 'Black Orchid', brand: 'Tom Ford', fragrance: 'Oriental', price: 150 }
  ];

  return (
    <div className="app-container">
      <h1 className="heading">Perfume Collection</h1>
      <PerfumeDisplay perfumes={perfumes} />
    </div>
  );
}

export default App;
