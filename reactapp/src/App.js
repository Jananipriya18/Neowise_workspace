import React from 'react';
import ChefDisplay from './components/ChefDisplay'; // Import the ChefDisplay component
import './assets/css/index.css'; // Import CSS

function App() {
  // Chef data
  const chefs = [
    { name: 'Gordon Ramsay', restaurant: 'Restaurant Gordon Ramsay', specialty: 'British', rating: 5.0 },
    { name: 'Massimo Bottura', restaurant: 'Osteria Francescana', specialty: 'Italian', rating: 4.8 },
    { name: 'Hélène Darroze', restaurant: 'Hélène Darroze at The Connaught', specialty: 'French', rating: 4.9 },
    { name: 'Rene Redzepi', restaurant: 'Noma', specialty: 'Nordic', rating: 4.7 }
  ];

  return (
    <div className="app-container">
      <h1 className="heading">Chef Profile Collection</h1>
      <ChefDisplay chefs={chefs} />
    </div>
  );
}

export default App;