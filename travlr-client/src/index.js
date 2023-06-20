import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { BrowserRouter } from 'react-router-dom';
import firebase from "firebase/compat/app"; // Import Firebase!!
import { firebaseConfig } from "./FireBaseConfig"; // Import Your Config!!
import { Travlr } from './components/Travlr';

firebase.initializeApp(firebaseConfig);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
<BrowserRouter>
    <Travlr />
</BrowserRouter>

);

