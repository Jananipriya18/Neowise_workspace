import React from 'react';

const Hospital = ({ doctors, styles }) => {
  return (
    <div>
      {doctors.map((doctor, index) => (
        <div key={index} style={styles.doctorContainer}>
          <p style={styles.doctorName}>{doctor.name}</p> {/* Display doctor's name */}
          <p style={styles.doctorSpecialty}>{doctor.specialty}</p> {/* Display doctor's specialty */}
        </div>
      ))}
    </div>
  );
};

export default Hospital;
