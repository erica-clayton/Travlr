import { useEffect, useState } from "react"
import { FetchTrips } from "../APIManager"
import { Button, Card, CardActions, CardContent, Grid, Typography } from "@mui/material"
import { TripCard } from "./TripCard"



export const AllTrips = () => {

   

    const [trips, setTrips] = useState([])

    

    const fetchTrips = async () => {
        const tripsFromApi = await FetchTrips()
        setTrips(tripsFromApi)
    }

    useEffect(()=> {
        fetchTrips()
    },[])

    return (

        <main>
            <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
            <div className="bg-white">    
            <h1 className="text-4xl font-bold tracking-tight text-gray-900 p-4">Trips</h1>
            
                
                <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
                    <h2 className="sr-only">Trips</h2>

                    <div className="grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8">
                        
                        {trips.map((trip) => {
                            return (
                                <TripCard
                                    key={`trip--${trip.id}`}
                                    tripId={trip.id}
                                    budget={trip.budget}
                                    tripName={trip.tripName}
                                    description={trip.description}
                                    
                                /> 
                            )
                        })}
                    </div>
                </div>
            </div>
            </div>
        </main>

    )
}