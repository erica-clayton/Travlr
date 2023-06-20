import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { DeleteTrip, FetchTripById, UpdateTrip } from "../APIManager";

export const EditTrip = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  
  const [trip, setTrip] = useState(null);
  const [formData, setFormData] = useState({
    id:0,
    tripName: "",
    pastTrip: false,
    description: "",
    budget: 0,
    dineOptions: [],
    stayOptions: [],
    activityOptions: []
    // form fields
  });

  useEffect(() => {
    const fetchTrip = async () => {
      const tripData = await FetchTripById(id);
      setTrip(tripData);
      setFormData({
        tripName: tripData.tripName,
        pastTrip: tripData.pastTrip,
        description: tripData.description,
        budget: tripData.budget,
        id: tripData.id
        // set other form fields based on tripData
      });
    };

    fetchTrip();
  }, [id]);

  const handleFormSubmit = async (event) => {
    event.preventDefault();
    

    // Call the API to update the trip
    await UpdateTrip(formData);

    
    navigate("/allTrips");
   
  };
  const handleDeleteTrip = async () => {
    // Call the API to delete the trip
    await DeleteTrip(id);

   
    navigate("/allTrips");
  };

  
  return (
    <div>
      <h2>Edit Trip</h2>
      {trip ? (
        <form onSubmit={handleFormSubmit}>
          {/* Render the form fields */}
          <input
            type="text"
            value={formData.tripName}
            onChange={(e) =>
              setFormData({ ...formData, tripName: e.target.value })
            }
          />
          <input
            type="number"
            value={formData.budget}
            onChange={(e) =>
              setFormData({ ...formData, budget: e.target.value })
            }
          />
          <textarea
            value={formData.description}
            onChange={(e) =>
              setFormData({ ...formData, description: e.target.value })
            }
          ></textarea>
          {/* Render other form fields */}
          <button type="submit">Save Changes</button>
          <button onClick={handleDeleteTrip}>Delete Trip</button>
        </form>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};
export default EditTrip;