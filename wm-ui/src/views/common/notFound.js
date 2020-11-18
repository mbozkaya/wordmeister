import React from 'react';
import { Link, Redirect, Router, Switch, Route } from 'react-router-dom';
import Home from '../home'

const NotFound = () => {
    return (
        <div>
            <h1> 404 - Not Found </h1>
            <a href="/">Home</a>

           
        </div>
    );
}

export default NotFound;