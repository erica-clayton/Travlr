import { Route, Routes } from "react-router-dom";
import { ApplicationViews } from "./views/ApplicationViews";
import { Login } from "./auth/Login";

import { Register } from "./API/LoginAPI";

export const Travlr = () => {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />

      <Route
        path="*"
        element={
          <ApplicationViews />
        }
      />
    </Routes>
  );
};