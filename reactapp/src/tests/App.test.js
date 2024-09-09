import React from 'react';
import { render, screen } from '@testing-library/react';
import PerfumeDisplay from '../components/PerfumeDisplay';


const perfumes = [
  { name: 'Chanel No. 5', brand: 'Chanel', fragrance: 'Floral' },
  { name: 'Dior Sauvage', brand: 'Dior', fragrance: 'Woody' },
  { name: 'Acqua di Gio', brand: 'Giorgio Armani', fragrance: 'Citrus' },
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
