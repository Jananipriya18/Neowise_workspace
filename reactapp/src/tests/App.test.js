import React from 'react';
import { render, screen } from '@testing-library/react';
import UserProfile from '../components/UserProfile';
import Address from '../components/Address';


  const user = {
    name: 'Alice Smith',
    age: 28,
    email: 'alice.smith@example.com',
    address: {
      city: 'London',
      country: 'UK'
    }
  };

  const address = {
    city: 'Paris',
    country: 'France'
  };

  test('renders_userprofile_component_correctly', () => {
    render(<UserProfile {...user} />);
    expect(screen.getByText('User Profile')).toBeInTheDocument();
  });

  test('displays_correct_user_name', () => {
    render(<UserProfile {...user} />);
    expect(screen.getByText('Alice Smith')).toBeInTheDocument();
  });

  test('displays_correct_user_age', () => {
    render(<UserProfile {...user} />);
    expect(screen.getByText('Age: 28')).toBeInTheDocument();
  });

  test('renders_address_component_correctly', () => {
    render(<Address {...address} />);
    expect(screen.getByText('City: Paris')).toBeInTheDocument();
    expect(screen.getByText('Country: France')).toBeInTheDocument();
  });

  test('displays_correct_city_name_in_address', () => {
    render(<Address {...address} />);
    expect(screen.getByText('City: Paris')).toBeInTheDocument();
  });

  test('displays_correct_country_name_in_address', () => {
    render(<Address {...address} />);
    expect(screen.getByText('Country: France')).toBeInTheDocument();
  });
  test('does_not_display_user_name_in_address', () => {
   
    render(<Address {...address} />);
    expect(screen.queryByText('Alice Smith')).toBeNull();
  });
