import React from 'react';
import Hospital from './Hospital';

const Patient = ({ name, age, email, hospital, admittedSince, conditions, preferredCare }) => {
  return (
    <div>
      <h3>{name}</h3>
      <p>Age: {age}</p>
      <p>Email: {email}</p>
      <p>Admitted Since: {admittedSince}</p>
      <p>Conditions Treated: {conditions}</p>
      <p>Preferred Care: {preferredCare}</p>
      
      <h3>Doctors:</h3>
      <div>
        <Hospital doctors={hospital.doctors} />
      </div>
    </div>
  );
}

export default Patient;
