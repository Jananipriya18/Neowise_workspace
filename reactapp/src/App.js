import React from 'react';
import MedicineDisplay from './components/MedicineDisplay'; 
import './assets/css/index.css'; 

function App() {
  // Medical professional data with reviews
  const professionals = [
    { name: 'Dr. Alice Johnson', hospital: 'City Hospital', specialty: 'Cardiology', review: "Highly skilled in cardiology with a compassionate approach." },
    { name: 'Dr. Bob Smith', hospital: 'General Clinic', specialty: 'Orthopedics', review: "Known for excellent diagnostic skills and effective treatment plans." },
    { name: 'Dr. Carol White', hospital: 'Health Center', specialty: 'Pediatrics', review: "Great with children and provides thorough care, though wait times can be long." },
    { name: 'Dr. David Brown', hospital: 'Medical Institute', specialty: 'Neurology', review: "Expert in neurological disorders with a focus on patient-centered care." }
  ];

  return (
    <div className="app-container">
      <h1 className="heading">Medical Professional Directory</h1>
      <MedicineDisplay professionals={professionals} />
    </div>
  );
}

export default App;
