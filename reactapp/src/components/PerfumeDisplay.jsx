import React from 'react';

const PerfumeList = ({ perfumes }) => {
  return (
    <div className="perfume-list-container">
      {perfumes.map((perfume, index) => (
        <div key={index} className="perfume-item">
          <h4 className="perfume-name">{perfume.name}</h4>
          <p className="perfume-brand">Brand: {perfume.brand}</p>
          <p className="perfume-price">Price: ${perfume.price}</p>
        </div>
      ))}
    </div>
  );
};

export default PerfumeList;