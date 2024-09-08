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

const doctors = [
  { name: 'Dr. Sarah Johnson', specialty: 'Cardiology' },
  { name: 'Dr. Robert Lee', specialty: 'Neurology' },
  { name: 'Dr. Emily Davis', specialty: 'Orthopedics' },
];

// Tests for the Patient component
test('renders_patient_component_correctly', () => {
  render(<Patient {...patient} />);
  expect(screen.getByText('John Smith')).toBeInTheDocument();
});

test('displays_correct_patient_name', () => {
  render(<Patient {...patient} />);
  expect(screen.getByText('John Smith')).toBeInTheDocument();
});

test('displays_correct_patient_age', () => {
  render(<Patient {...patient} />);
  expect(screen.getByText('Age: 45')).toBeInTheDocument();
});

test('displays_correct_patient_email', () => {
  render(<Patient {...patient} />);
  expect(screen.getByText('Email: john.smith@example.com')).toBeInTheDocument();
});

test('displays_correct_admitted_since', () => {
  render(<Patient {...patient} />);
  expect(screen.getByText('Admitted Since: 2022-05-15')).toBeInTheDocument();
});

test('displays_correct_conditions_treated', () => {
  render(<Patient {...patient} />);
  expect(screen.getByText('Conditions Treated: 3')).toBeInTheDocument();
});

test('displays_correct_preferred_care', () => {
  render(<Patient {...patient} />);
  expect(screen.getByText('Preferred Care: Inpatient')).toBeInTheDocument();
});

// Tests for the Hospital component
test('renders_doctors_information_correctly', () => {
  render(<Hospital doctors={doctors} />);
  expect(screen.getByText('Dr. Sarah Johnson')).toBeInTheDocument();
  expect(screen.getByText('Cardiology')).toBeInTheDocument();
  expect(screen.getByText('Dr. Robert Lee')).toBeInTheDocument();
  expect(screen.getByText('Neurology')).toBeInTheDocument();
  expect(screen.getByText('Dr. Emily Davis')).toBeInTheDocument();
  expect(screen.getByText('Orthopedics')).toBeInTheDocument();
});
