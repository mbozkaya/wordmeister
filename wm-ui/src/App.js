import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Router, Route, Switch, BrowserRouter, Redirect } from 'react-router-dom';
import { createBrowserHistory as History } from 'history';
import Home from './views/home';
import Signup from './pages/Signup';
import Login from './pages/Login';
import Dashboard from './views/dashboard/dashboard';
import NotFoundView from './views/common/notFound';
import { AuthContext, AuthProvider, AuthRoute, UnAuthRoute } from './contexts/authContext';
import { AuthRouteX, UnAuthRouteX } from './contexts/authRouter';
import { Header } from './views/layout/header';

const App = props => {
  return (
    <BrowserRouter>
      <AuthProvider>
        <AuthContext.Consumer>
          {({authorize}) => (
            <Router history={History()}>
              <Switch>
                <Route exact path='/' component={Home} />
                <UnAuthRouteX exact path='/signup' component={Signup} />
                <UnAuthRouteX exact path='/login' component={Login} />
                <Route exact path='/404' component={NotFoundView} />
                <Route exact path='/index' component={Home} />
                <AuthRouteX path='/dashboard' component ={Dashboard} />
                {/* <AuthRouteX path='/x' component ={Dashboard} /> */}

               <Route  path='*'>
                  <Redirect exact to='/404'/>
                </Route>

                {/* <AuthRoute>
                  <Route exact path='/dashboard' component={Dashboard} />
                </AuthRoute>
                */}
              
              </Switch>
            </Router>
          )}
        </AuthContext.Consumer>
      </AuthProvider>
    </BrowserRouter>
  )
}

export default App;
