import React from 'react';

const Hospital = ({ doctors }) => {
  return (
    <div>
      {doctors.map((doctor, index) => (
        <div key={index}>
          <p>{doctor.name}</p> {/* Display doctor's name */}
          <p>{doctor.specialty}</p> {/* Display doctor's specialty */}
        </div>
      ))}
    </div>
  );
};

export default Hospital;
