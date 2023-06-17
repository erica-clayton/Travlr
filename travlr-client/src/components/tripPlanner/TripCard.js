import { useNavigate } from "react-router-dom"

export const TripCard = ({ tripId, tripName, budget, description }) => {

    const navigate = useNavigate()

    return (
        <section onClick={() => navigate(`/allTrips/${tripId}`)} key={`trip--${tripId}`}>
            <div>
               
                <h3 className="mt-4 text-sm text-gray-700">{tripName}</h3>
                <p className="mt-1 text-lg font-medium text-gray-900">${budget}</p>
                <p className="mt-1 text-lg font-medium text-gray-900">{description}</p>
                
            </div>
        </section>
    )
}