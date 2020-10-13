import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Router, Route, Switch, BrowserRouter } from 'react-router-dom';
import { createBrowserHistory as History } from 'history';
import Home from './views/home';
import Signup from './pages/Signup';
import Login from './pages/Login';
import Dashboard from './views/dashboard/dashboard';
import { AuthContext, AuthProvider, AuthRoute } from './contexts/authContext';


// function App() {
//   return (
//     <div className="App">
//       <header className="App-header">
//         <img src={logo} className="App-logo" alt="logo" />
//         <p>
//           Edit <code>src/App.js</code> and save to reload.
//         </p>
//         <a
//           className="App-link"
//           href="https://reactjs.org"
//           target="_blank"
//           rel="noopener noreferrer"
//         >
//           Learn React
//         </a>
//       </header>
//     </div>
//   );
// }

const App = props => {
  return (
    <BrowserRouter>
      <AuthProvider>
        <AuthContext.Consumer>
          {() => (
            <Router history={History()}>
              <Switch>
                <Route exact path='/' component={Home} />
                <Route exact path='/signup' component={Signup} />
                <Route exact path='/login' component={Login} />
                <AuthRoute>
                  <Route exact path='/dashboard' component={Dashboard} />
                </AuthRoute>
              </Switch>
            </Router>
          )}
        </AuthContext.Consumer>
      </AuthProvider>
    </BrowserRouter>
  )
}

export default App;
