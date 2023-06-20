import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { FetchActivityByTripId, FetchDineByTripId, FetchStayByTripId, FetchTripById } from "../APIManager";


export const TripDetails = () => {
    const { id } = useParams();
    const [trip, setTrip] = useState(null);
    const [dine, setDine] = useState(null);
    const [stay, setStay] = useState(null);
    const [activity, setActivity] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const tripData = await FetchTripById(id);
            setTrip(tripData);

            const dineData = await FetchDineByTripId(id);
            setDine(dineData);

            const stayData = await FetchStayByTripId(id);
            setStay(stayData);

            const activityData = await FetchActivityByTripId(id);
            setActivity(activityData);
        };

        fetchData();
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
        <h2>Dine Options</h2>
            {dine && dine.map((option) => (
                <div key={option.id}>
                    <p>Dine Name: {option.dineName}</p>
                    <p>Dine Address: {option.dineAddress}</p>
                    {/* Render other dine option details */}
                </div>
            ))}
            <h2>Stay Options</h2>
            {stay && stay.map((option) => (
                <div key={option.id}>
                    <p>Stay Name: {option.stayName}</p>
                    <p>Stay Address: {option.stayAddress}</p>
                    {/* Render other stay option details */}
                </div>
            ))}
            <h2>Activity Options</h2>
            {activity && activity.map((option) => (
                <div key={option.id}>
                    <p>Activity Name: {option.activityName}</p>
                    <p>Activity Address: {option.activityAddress}</p>
                    {/* Render other activity option details */}
                </div>
            ))}
        </div>
    );
};





