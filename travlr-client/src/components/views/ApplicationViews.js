import React from "react";
import { Outlet, Route, Routes } from "react-router-dom";
import { NavBar } from "../nav/NavBar";
import { Home } from "../home/Home";
import { AllTrips } from "../tripPlanner/AllTrips";
import { TripPlanner } from "../tripPlanner/TripPlanner";


export const ApplicationViews = () => {
    const localUser = localStorage.getItem("project_user");
    const currentUser = JSON.parse(localUser);
  
    return (
      <>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="allTrips" element={<AllTrips />} />
          <Route path="tripPlanner" element={<TripPlanner/>} />

          {/* {currentUser && (
            <Route path="allTrips" element={<AllTrips />} />
          )} */}
          
        </Routes>
      </>
    );
  };
  
