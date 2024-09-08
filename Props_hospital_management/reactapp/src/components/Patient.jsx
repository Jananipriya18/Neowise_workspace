import React from 'react';
import Hospital from './Hospital';

const Patient = ({ name, age, email, hospital, admittedSince, conditions, preferredCare, styles }) => {
  return (
    <div>
      <h3 style={styles.subHeading}>{name}</h3>
      <p style={styles.paragraph}>Age: {age}</p>
      <p style={styles.paragraph}>Email: {email}</p>
      <p style={styles.paragraph}>Admitted Since: {admittedSince}</p>
      <p style={styles.paragraph}>Conditions Treated: {conditions}</p>
      <p style={styles.paragraph}>Preferred Care: {preferredCare}</p>
      
      <h3 style={styles.subHeading}>Doctors:</h3>
      <div style={styles.hospitalContainer}>
        <Hospital doctors={hospital.doctors} styles={styles} />
      </div>
    </div>
  );
}

export default Patient;
