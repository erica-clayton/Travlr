import React, { useState, useEffect } from "react";
import { AddTrip } from "../APIManager";

export const CreateListing = () => {
  const [userId, setUserId] = useState(null); // Initialize userId state as null
  const [image, setImage] = useState("");
  const [price, setPrice] = useState(0);
  const [description, setDescription] = useState("");
  const [categoryId, setCategoryId] = useState(0);
  const [location, setLocation] = useState("");
  const [dateTime, setDateTime] = useState("");
  

  useEffect(() => {
    const projUser = localStorage.getItem("project_user");
    const projUserObject = JSON.parse(projUser);

    // Retrieve userId from local storage
    const storedUserId = localStorage.getItem("userId");
    if (storedUserId) {
      setUserId(storedUserId);
    }

    // Set the current date and time as the initial value for dateTime
    setDateTime(new Date().toISOString());
    
    // Assign the value of projUserObject to userId state
    if (projUserObject) {
      setUserId(projUserObject.userId);
    }
  }, []);

  const handleFormSubmit = async (e) => {
    e.preventDefault();
    
    const tripData = {
      userId,
      image,
      price: parseInt(price),
      description,
      categoryId: parseInt(categoryId),
      location,
      dateTime
    };
    
    const newListing = await AddTrip(tripData);
    
    if (newListing) {
      // Listing added successfully
      console.log(newListing);
      // Reset form fields
      setImage("");
      setPrice(0);
      setDescription("");
      setCategoryId(0);
      setLocation("");

      // Redirect to a different page
      window.location.href = "/";
    } else {
      // Failed to add listing
      console.log("Failed to add trip");
    }
  };







      return (
        <form onSubmit={handleFormSubmit}>
          <div className="mb-6">
            <label htmlFor="image-input" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Image</label>
            <input
              type="text"
              id="image-input"
              value={image}
              onChange={(e) => setImage(e.target.value)}
              className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              required
            />
          </div>
          <div className="mb-6">
            <label htmlFor="price-input" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Price</label>
            <input
              type="text"
              id="price-input"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
              className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              required
            />
          </div>
          <div className="mb-6">
            <label htmlFor="description-input" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Description</label>
            <input
              type="text"
              id="description-input"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              required
            />
          </div>
          <div className="mb-6">
        <label htmlFor="category-input" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Category</label>
        <select
          id="category-input"
          value={categoryId}
          onChange={(e) => setCategoryId(e.target.value)}
          className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
          required
        >
          <option value="">Select a category</option>
          <option value="1">Bed</option>
          <option value="2">Bookshelf</option>
          <option value="3">Chair</option>
          <option value="4">Coffee Table</option>
          <option value="5">Couch</option>
          <option value="6">Dresser</option>
          <option value="7">Nightstand</option>
          <option value="8">Table</option>
        </select>
      </div>
          <div className="mb-6">
            <label htmlFor="location-input" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Location</label>
            <input
              type="text"
              id="location-input"
              value={location}
              onChange={(e) => setLocation(e.target.value)}
              className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              required
            />
          </div>
          <button
            type="submit"
            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            Create Listing
          </button>
        </form>
      );
    };
    
    export default CreateListing;