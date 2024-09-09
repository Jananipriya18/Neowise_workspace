import React from 'react';

const ChefDisplay = ({ chefs }) => {
  return (
    <div className="chef-list-container">
      {chefs.map((chef, index) => (
        <div key={index} className="chef-item">
          <h4 className="chef-name">{chef.name}</h4>
          <p className="chef-restaurant">Restaurant: {chef.restaurant}</p>
          <p className="chef-specialty">Specialty: {chef.specialty}</p>
          <p className="chef-rating">Rating: {chef.rating} stars</p>
        </div>
      ))}
    </div>
  );
};

export default ChefDisplay;
