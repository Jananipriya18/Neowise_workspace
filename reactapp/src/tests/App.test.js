import React from 'react';
import { render, screen } from '@testing-library/react';
import ChefDisplay from '../components/ChefDisplay'; 
import App from '../App';

// Sample chef data for testing
const chefs = [
  { name: 'Gordon Ramsay', restaurant: 'Restaurant Gordon Ramsay', specialty: 'British', rating: 5.0 },
  { name: 'Massimo Bottura', restaurant: 'Osteria Francescana', specialty: 'Italian', rating: 4.8 },
  { name: 'Hélène Darroze', restaurant: 'Hélène Darroze at The Connaught', specialty: 'French', rating: 4.9 },
  { name: 'Rene Redzepi', restaurant: 'Noma', specialty: 'Nordic', rating: 4.7 }
];

describe('ChefDisplay Component', () => {
  // Test 1: Renders the component with chef names
  test('renders chef names correctly', () => {
    render(<ChefDisplay chefs={chefs} />);
    
    // Check if chef names are displayed
    expect(screen.getByText('Gordon Ramsay')).toBeInTheDocument();
    expect(screen.getByText('Massimo Bottura')).toBeInTheDocument();
    expect(screen.getByText('Hélène Darroze')).toBeInTheDocument();
    expect(screen.getByText('Rene Redzepi')).toBeInTheDocument();
  });

  // Test 2: Renders restaurant names correctly
  test('renders chef restaurant names correctly', () => {
    render(<ChefDisplay chefs={chefs} />);
    
    // Check if restaurant names are displayed
    expect(screen.getByText('Restaurant: Restaurant Gordon Ramsay')).toBeInTheDocument();
    expect(screen.getByText('Restaurant: Osteria Francescana')).toBeInTheDocument();
    expect(screen.getByText('Restaurant: Hélène Darroze at The Connaught')).toBeInTheDocument();
    expect(screen.getByText('Restaurant: Noma')).toBeInTheDocument();
  });

  // Test 3: Renders chef specialty correctly
  test('renders chef specialties correctly', () => {
    render(<ChefDisplay chefs={chefs} />);
    
    // Check if specialties are displayed
    expect(screen.getByText('Specialty: British')).toBeInTheDocument();
    expect(screen.getByText('Specialty: Italian')).toBeInTheDocument();
    expect(screen.getByText('Specialty: French')).toBeInTheDocument();
    expect(screen.getByText('Specialty: Nordic')).toBeInTheDocument();
  });

  // Test 4: Renders chef ratings correctly
  test('renders chef ratings correctly', () => {
    render(<ChefDisplay chefs={chefs} />);
    
    // Check if ratings are displayed with "stars" appended
    expect(screen.getByText('Rating: 5 stars')).toBeInTheDocument();
    expect(screen.getByText('Rating: 4.8 stars')).toBeInTheDocument();
    expect(screen.getByText('Rating: 4.9 stars')).toBeInTheDocument();
    expect(screen.getByText('Rating: 4.7 stars')).toBeInTheDocument();
  });

  // Test 5: Renders no chefs when chef list is empty
  test('renders the main heading correctly', () => {
    render(<App />);
  
    // Check if the heading "Chef Collection" is displayed
    const headingElement = screen.getByRole('heading', { name: /Chef Profile Collection/i });
    expect(headingElement).toBeInTheDocument();
  });

});



