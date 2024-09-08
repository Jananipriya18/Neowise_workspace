import React from 'react';
import Patient from './components/Patient';

function App() {
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

  // Inline styles for Patient and Hospital components
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

  return (
    <div style={styles.patientContainer}>
      <h2 style={styles.heading}>Patient Profile</h2>
      <Patient {...patient} styles={styles} />
    </div>
  );
}

export default App;
