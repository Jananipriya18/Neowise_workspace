import React from 'react';
import { render, screen } from '@testing-library/react';
import MedicineDisplay from '../components/MedicineDisplay'; 
import App from '../App';

// Sample medical professional data for testing
const professionals = [
  { name: 'Dr. Alice Johnson', hospital: 'City Hospital', specialty: 'Cardiology', review: 'Excellent service' },
  { name: 'Dr. Bob Smith', hospital: 'General Clinic', specialty: 'Orthopedics', review: 'Best diagnosis' },
  { name: 'Dr. Carol White', hospital: 'Health Center', specialty: 'Pediatrics', review: 'Very compassionate' },
  { name: 'Dr. David Brown', hospital: 'Medical Institute', specialty: 'Neurology', review: 'Highly recommended' }
];

describe('MedicineDisplay Component', () => {
  // Test 1: Renders the component with medical professional names
  test('renders medical professional names correctly', () => {
    render(<MedicineDisplay professionals={professionals} />);
    
    // Check if medical professional names are displayed
    expect(screen.getByText('Dr. Alice Johnson')).toBeInTheDocument();
    expect(screen.getByText('Dr. Bob Smith')).toBeInTheDocument();
    expect(screen.getByText('Dr. Carol White')).toBeInTheDocument();
    expect(screen.getByText('Dr. David Brown')).toBeInTheDocument();
  });

  // Test 2: Renders hospital names correctly
  test('renders medical professional hospital names correctly', () => {
    render(<MedicineDisplay professionals={professionals} />);
    
    // Check if hospital names are displayed
    expect(screen.getByText('Hospital: City Hospital')).toBeInTheDocument();
    expect(screen.getByText('Hospital: General Clinic')).toBeInTheDocument();
    expect(screen.getByText('Hospital: Health Center')).toBeInTheDocument();
    expect(screen.getByText('Hospital: Medical Institute')).toBeInTheDocument();
  });

  // Test 3: Renders medical specialty correctly
  test('renders medical specialties correctly', () => {
    render(<MedicineDisplay professionals={professionals} />);
    
    // Check if specialties are displayed
    expect(screen.getByText('Specialty: Cardiology')).toBeInTheDocument();
    expect(screen.getByText('Specialty: Orthopedics')).toBeInTheDocument();
    expect(screen.getByText('Specialty: Pediatrics')).toBeInTheDocument();
    expect(screen.getByText('Specialty: Neurology')).toBeInTheDocument();
  });

  // Test 4: Renders medical professional reviews correctly
  test('renders medical professional reviews correctly', () => {
    render(<MedicineDisplay professionals={professionals} />);
    
    // Check if reviews are displayed
    professionals.forEach(professional => {
      expect(screen.getByText(`Review: ${professional.review}`)).toBeInTheDocument();
    });
  });

  // Test 5: Renders the main heading correctly
  test('renders the main heading correctly', () => {
    render(<App />);
  
    // Check if the heading "Medical Professional Directory" is displayed
    const headingElement = screen.getByRole('heading', { name: /Medical Professional Directory/i });
    expect(headingElement).toBeInTheDocument();
  });
});
