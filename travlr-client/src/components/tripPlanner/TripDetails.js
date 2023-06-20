import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { FetchTripById } from "../APIManager";


export const TripDetails = () => {
    const { id } = useParams();
    const [trip, setTrip] = useState(null);
  
    useEffect(() => {
      const fetchTrip = async () => {
        const tripData = await FetchTripById(id);
        setTrip(tripData);
      };
  
      fetchTrip();
    }, [id]);
  
    if (!trip) {
      return <p>Loading...</p>;
    }
  
    return (
      <div>
        <h2>Trip Details</h2>
        <p>Trip Name: {trip.tripName}</p>
        <p>Description: {trip.description}</p>
        <p>Budget: {trip.budget}</p>
        {/* Render other trip details */}
      </div>
    );
  };
  