import { useEffect, useState } from "react";
import { Button, Card, CardActions, CardContent, Grid, Typography } from "@mui/material";
import { FetchTrips} from "../APIManager";

export const MyTrips = () => {
   const [myTrips, setTrips] = useState([]);

   const localTravlrUser = localStorage.getItem("project_user");
   const travlrUserObject = JSON.parse(localTravlrUser);

   const getActivitiesFromRepo = async () => {
      const gottenTrips = await FetchTrips(travlrUserObject.userId);
      setTrips(gottenTrips);
   };
   
   useEffect(() => {
      getActivitiesFromRepo();
   }, []);

   return (
      <>
      <Grid container spacing={2}>
         {myTrips.map(
            (trip) => {
               return (
                  <Grid item xs={3}>
                     <Card sx={{ maxWidth: 275 }} key={`activity--${trip.id}`} variant="outlined">
                        <CardContent>
                           <Typography sx={{ fontSize: 28 }} color="text.primary" gutterBottom>
                              {trip.tripName}
                           </Typography>
                           
                        </CardContent>
                        <CardActions>
                           <Button size="small">Learn More</Button>
                        </CardActions>
                     </Card>
                  </Grid>
               )
            }
         )}
      </Grid>
      </>
   )
}
