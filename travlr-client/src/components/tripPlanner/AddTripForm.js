import { useState } from "react";
import { CreateTrip } from "../APIManager";

export const AddTripForm = ({ onTripAdded }) => {
  const [tripData, setTripData] = useState({
    tripName: "",
    pastTrip: false,
    description: "",
    budget: 0,
    dineOptions: [],
    stayOptions: [],
    activityOptions: []
  });

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setTripData((prevData) => ({
      ...prevData,
      [name]: value
    }));
  };

  const handleFormSubmit = async (event) => {
    event.preventDefault();

    try {
      const newTrip = await CreateTrip(tripData);
      console.log("New trip created:", newTrip);

      // Reset the form
      setTripData({
        tripName: "",
        pastTrip: false,
        description: "",
        budget: 0,
        dineOptions: [],
        stayOptions: [],
        activityOptions: []
      });

      // Notify the parent component that a new trip was added
      onTripAdded();
    } catch (error) {
      console.error("Error creating trip:", error);
    }
  };

  
  return (
    <form onSubmit={handleFormSubmit}>
      <label>
        Trip Name:
        <input
          type="text"
          name="tripName"
          value={tripData.tripName}
          onChange={handleInputChange}
        />
      </label>
      <br />

      <label>
        Past Trip:
        <input
          type="checkbox"
          name="pastTrip"
          checked={tripData.pastTrip}
          onChange={() => setTripData((prevData) => ({
            ...prevData,
            pastTrip: !prevData.pastTrip
          }))}
        />
      </label>
      <br />

      <label>
        Description:
        <textarea
          name="description"
          value={tripData.description}
          onChange={handleInputChange}
        ></textarea>
      </label>
      <br />

      <label>
        Budget:
        <input
          type="number"
          name="budget"
          value={tripData.budget}
          onChange={handleInputChange}
        />
      </label>
      <br />

      {/* Additional fields for dine options, stay options, and activity options can be added here */}

      <button type="submit">Add Trip</button>
    </form>
  );
};