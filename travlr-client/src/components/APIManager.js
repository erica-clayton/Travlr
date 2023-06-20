const GetConfig = {
    method: "GET",
    headers: { "Content-Type": "application/json" },
  }

  const GetPostConfig = (body) => {
    var config = {
        body : JSON.stringify(body),
        credentials: "include",
        method: "POST",
        headers: { "Content-Type": "application/json" },
      }
      return config
  }
  

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

    export const CreateTrip = async (tripData) => {
        try {
          const response = await fetch("https://localhost:7129/Trips", {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify(tripData)
          });
      
          if (!response.ok) {
            throw new Error("Failed to create trip");
          }
      
          const newTrip = await response.json();
          return newTrip;
        } catch (error) {
          console.error("Error creating trip:", error);
          throw error;
        }
      };
      

      export const FetchTripById = async (id) => {
        const response = await fetch(`https://localhost:7129/Trips/${id}`);
        const tripIdResponse = await response.json();
        return tripIdResponse;
     }

     export const FetchDineByTripId = async (id) => {
        const response = await fetch(`https://localhost:7129/Trips/trips/${id}/dineoptions`);
        const dineOptionsResponse = await response.json();
        return dineOptionsResponse;
     }

     export const FetchStayByTripId = async (id) => {
        const response = await fetch(`https://localhost:7129/Trips/trips/${id}/stayoptions`);
        const stayOptionsResponse = await response.json();
        return stayOptionsResponse;
     }
     
     export const FetchActivityByTripId = async (id) => {
        const response = await fetch(`https://localhost:7129/Trips/trips/${id}/activityoptions`)
        const activityOptionsResponse = await response.json();
        return activityOptionsResponse;
     }
     
     export const DeleteTrip = async (id) => {
        
          const response = await fetch(`https://localhost:7129/Trips/${id}`, {
            method: 'DELETE',
            headers: {
              'Content-Type': 'application/json'
            }
          });
      
      };
      
    
     export const UpdateTrip = async (formData) => {
       console.log(formData)
          const response = await fetch(`https://localhost:7129/Trips/${formData.id}`, {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(formData),
          });
      
      };

      