import React from 'react';

const MedicineDisplay = ({ professionals }) => {
  return (
    <div className="medicine-list-container">
      {professionals.map((professional, index) => (
        <div key={index} className="medicine-item">
          <h4 className="medicine-name">{professional.name}</h4>
          <p className="medicine-hospital">Hospital: {professional.hospital}</p>
          <p className="medicine-specialty">Specialty: {professional.specialty}</p>
          <p className="medicine-review">Review: {professional.review}</p>
        </div>
      ))}
    </div>
  );
};

export default MedicineDisplay;
