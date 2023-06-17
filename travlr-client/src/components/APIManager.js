export const AddUser = async (userObj) => {
    // event.preventDefault()

    const options = {
        method: "POST",
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify(userObj)
    }

    await fetch(`https://localhost:7129/Users
    `, options)
}

export const FetchUserByFirebaseId = async (uid) => {
    const response = await fetch(`https://localhost:7129/Users/uid/${uid}`)
    const user = await response.json();
    return user
}

export const FetchTripsByUser = async (tripUserId) => {
    const response = await fetch(`https://localhost:7129/Trips/trips/${tripUserId}`)
    const userTripsArray = await response.json();
    return userTripsArray
}

export const FetchTrips = async () => {
    const response = await fetch(`https://localhost:7129/Trips`);
    
    const tripsArray = await response.json();
    return tripsArray }

    export const AddTrip = async (tripData) => {
        const response = await fetch(`https://localhost:7129/Trips`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(tripData)
        });
      
        console.log(response, "AddTrip");
        if (response.ok) {
          const newTrip = await response.json();
          return newTrip;
        } else {
          return false;
        }
      };