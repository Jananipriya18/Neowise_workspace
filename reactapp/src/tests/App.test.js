import React from 'react';
import { render, screen } from '@testing-library/react';
import PerfumeDisplay from '../components/ChefDisplay';
import App from '../App';


const perfumes = [
  { name: 'Chanel No. 5', brand: 'Chanel', fragrance: 'Floral', price: 120 },
  { name: 'Dior Sauvage', brand: 'Dior', fragrance: 'Woody', price: 110 },
  { name: 'Acqua di Gio', brand: 'Giorgio Armani', fragrance: 'Citrus', price: 95 },
];


test('renders perfume items correctly', () => {
  render(<PerfumeDisplay perfumes={perfumes} />);
  
  expect(screen.getByText('Chanel No. 5')).toBeInTheDocument();
  expect(screen.getByText('Brand: Chanel')).toBeInTheDocument();
  expect(screen.getByText('Fragrance: Floral')).toBeInTheDocument();
  
  expect(screen.getByText('Dior Sauvage')).toBeInTheDocument();
  expect(screen.getByText('Brand: Dior')).toBeInTheDocument();
  expect(screen.getByText('Fragrance: Woody')).toBeInTheDocument();
  
  expect(screen.getByText('Acqua di Gio')).toBeInTheDocument();
  expect(screen.getByText('Brand: Giorgio Armani')).toBeInTheDocument();
  expect(screen.getByText('Fragrance: Citrus')).toBeInTheDocument();
});

test('renders the heading correctly', () => {
  render(<App />);
  
  // Check if the heading "Perfume Collection" is in the document
  expect(screen.getByText('Perfume Collection')).toBeInTheDocument();
});

test('renders perfume prices correctly', () => {
  render(<App />);
  
  expect(screen.getByText('Price: $120')).toBeInTheDocument();
  expect(screen.getByText('Price: $110')).toBeInTheDocument();
  expect(screen.getByText('Price: $95')).toBeInTheDocument();
});
