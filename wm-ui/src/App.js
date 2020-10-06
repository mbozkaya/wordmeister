import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Router, Route, Switch, BrowserRouter } from 'react-router-dom';
import { createBrowserHistory as History } from 'history';
import Home from './views/home';
import Signup from './pages/Signup';
import Login from './pages/Login';


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
      <Router history={History()}>
        <Switch>
          <Route exact path='/' component={Home} />
          <Route exact path='/signup' component={Signup} />
          <Route exact path='/login' component={Login} />
        </Switch>
      </Router>
    </BrowserRouter>
  )
}

export default App;
