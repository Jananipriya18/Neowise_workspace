import React from 'react';
import { render, screen } from '@testing-library/react';
import Patient from '../components/Patient';
import Hospital from '../components/Hospital';

// Sample data for Patient and Hospital components
const patient = {
  name: 'John Smith',
  age: 45,
  email: 'john.smith@example.com',
  hospital: {
    doctors: [
      { name: 'Dr. Sarah Johnson', specialty: 'Cardiology' },
      { name: 'Dr. Robert Lee', specialty: 'Neurology' },
      { name: 'Dr. Emily Davis', specialty: 'Orthopedics' },
    ],
  },
  admittedSince: '2022-05-15',
  conditions: 3,
  preferredCare: 'Inpatient',
};

// Sample styles object to match the one used in App.js
const styles = {
  patientContainer: {
    backgroundColor: '#f1f1f1',
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
  hospitalContainer: {
    backgroundColor: '#fff',
    padding: '15px',
    borderRadius: '8px',
    border: '1px solid #ddd',
    marginTop: '20px',
  },
  doctorContainer: {
    padding: '10px 0',
  },
  doctorName: {
    fontSize: '16px',
    fontWeight: 'bold',
    color: '#333',
  },
  doctorSpecialty: {
    fontSize: '14px',
    color: '#777',
  },
};

const doctors = [
  { name: 'Dr. Sarah Johnson', specialty: 'Cardiology' },
  { name: 'Dr. Robert Lee', specialty: 'Neurology' },
  { name: 'Dr. Emily Davis', specialty: 'Orthopedics' },
];

// Tests for the Patient component
test('renders_patient_component_correctly', () => {
  render(<Patient {...patient} styles={styles} />);
  expect(screen.getByText('Patient Profile')).toBeInTheDocument();
});

test('displays_correct_patient_name', () => {
  render(<Patient {...patient} styles={styles} />);
  expect(screen.getByText('John Smith')).toBeInTheDocument();
});

test('displays_correct_patient_age', () => {
  render(<Patient {...patient} styles={styles} />);
  expect(screen.getByText('Age: 45')).toBeInTheDocument();
});

test('displays_correct_patient_email', () => {
  render(<Patient {...patient} styles={styles} />);
  expect(screen.getByText('Email: john.smith@example.com')).toBeInTheDocument();
});

test('displays_correct_admitted_since', () => {
  render(<Patient {...patient} styles={styles} />);
  expect(screen.getByText('Admitted Since: 2022-05-15')).toBeInTheDocument();
});

test('displays_correct_conditions_treated', () => {
  render(<Patient {...patient} styles={styles} />);
  expect(screen.getByText('Conditions Treated: 3')).toBeInTheDocument();
});

test('displays_correct_preferred_care', () => {
  render(<Patient {...patient} styles={styles} />);
  expect(screen.getByText('Preferred Care: Inpatient')).toBeInTheDocument();
});

// Tests for the Hospital component
test('renders_doctors_information_correctly', () => {
  render(<Hospital doctors={doctors} styles={styles} />);
  expect(screen.getByText('Dr. Sarah Johnson')).toBeInTheDocument();
  expect(screen.getByText('Cardiology')).toBeInTheDocument();
  expect(screen.getByText('Dr. Robert Lee')).toBeInTheDocument();
  expect(screen.getByText('Neurology')).toBeInTheDocument();
  expect(screen.getByText('Dr. Emily Davis')).toBeInTheDocument();
  expect(screen.getByText('Orthopedics')).toBeInTheDocument();
});
