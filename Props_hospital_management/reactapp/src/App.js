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

  return (
    <div>
      <h2>Patient Profile</h2>
      <Patient {...patient} />
    </div>
  );
}

export default App;
