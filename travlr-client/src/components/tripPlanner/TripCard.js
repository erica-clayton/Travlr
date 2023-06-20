import { Link, useNavigate } from "react-router-dom";

export const TripCard = ({ info }) => {
    const navigate = useNavigate();

    const { id, tripName, budget, description } = info;

    return (
        <section onClick={() => navigate(`/allTrips/${id}`)} key={`trip--${id}`}>
            <div>
                <h3 className="mt-4 text-sm text-gray-700">{tripName}</h3>
                <p className="mt-1 text-lg font-medium text-gray-900">${budget}</p>
                <p className="mt-1 text-lg font-medium text-gray-900">{description}</p>
                <Link to={`editTrip/${id}`}>
  <button>Edit</button>
</Link>
               
                 
            </div>
           
        </section>
    );
};
