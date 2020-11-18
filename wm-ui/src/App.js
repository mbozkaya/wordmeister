import React from 'react';
import './App.css';
import { Router, Route, Switch, BrowserRouter, Redirect } from 'react-router-dom';
import { createBrowserHistory as History } from 'history';
import Home from './views/home';
import Signup from './pages/Signup';
import Login from './pages/Login';
import Dashboard from './views/dashboard/dashboard';
import NotFoundView from './views/common/notFound';
import { AuthProvider, AuthRoute } from './contexts/authContext';

const App = () => {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Router history={History()}>
          <Switch>
            <Route exact path='/' component={Home} />
            <Route exact path='/signup' component={Signup} />
            <Route exact path='/login' component={Login} />
            <Route exact path='/404' component={NotFoundView} />
            <Route exact path='/index' component={Home} />
            <AuthRoute path='/dashboard' component={Dashboard} />
            <Route path='*'>
              <Redirect exact to='/404' />
            </Route>
          </Switch>
        </Router>
      </AuthProvider>
    </BrowserRouter>
  )
}

export default App;
