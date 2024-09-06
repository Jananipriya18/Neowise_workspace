import React from 'react';
import { render, screen } from '@testing-library/react';
import BookReader from '../components/BookReader';
import Library from '../components/Library';

// Sample data for BookReader and Library components
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

const library = {
  name: 'Local Library',
  location: 'Suburb',
};

// Tests for the BookReader component
test('renders_bookreader_component_correctly', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Book Reader Profile')).toBeInTheDocument();
});

test('displays_correct_reader_name', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Jane Doe')).toBeInTheDocument();
});

test('displays_correct_reader_age', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Age: 28')).toBeInTheDocument();
});

test('displays_correct_reader_email', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Email: jane.doe@example.com')).toBeInTheDocument();
});

test('displays_correct_favorite_genres', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Favorite Genres: Science Fiction, Fantasy, Mystery')).toBeInTheDocument();
});

test('displays_correct_membership_since', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Membership Since: 2015')).toBeInTheDocument();
});

test('displays_correct_books_read', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Books Read: 120')).toBeInTheDocument();
});

test('displays_correct_preferred_format', () => {
  render(<BookReader {...reader} />);
  expect(screen.getByText('Preferred Format: E-books')).toBeInTheDocument();
});

// Tests for the Library component
test('renders_library_component_correctly', () => {
  render(<Library {...library} />);
  expect(screen.getByText('Name: Local Library')).toBeInTheDocument();
  expect(screen.getByText('Location: Suburb')).toBeInTheDocument();
});

test('displays_correct_library_name', () => {
  render(<Library {...library} />);
  expect(screen.getByText('Name: Local Library')).toBeInTheDocument();
});

test('displays_correct_library_location', () => {
  render(<Library {...library} />);
  expect(screen.getByText('Location: Suburb')).toBeInTheDocument();
});

// Ensuring BookReader does not incorrectly show Library details
test('does_not_display_library_details_in_bookreader', () => {
  render(<BookReader {...reader} />);
  expect(screen.queryByText('Name: Local Library')).toBeNull();
  expect(screen.queryByText('Location: Suburb')).toBeNull();
});
