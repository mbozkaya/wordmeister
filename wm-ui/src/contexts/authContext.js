import React, { useLayoutEffect, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Route, Navigate
} from 'react-router-dom';
import accountService from '../services/accountService';
// import Login from '../pages/Login';
// eslint-disable-next-line linebreak-style

const AuthContext = React.createContext();

const AuthProvider = (props) => {
  const [contextState, setContextState] = useState({
    authorize: false,
    checkAuth: false,
    permission: 0,
    loginError: false,
    loginErrorMessage: '',
    signupError: false,
    signupErrorMessage: '',
    backdropOpen: false,
  });
  const { children } = props;

  const onLogin = (model) => {
    accountService.login(model).then((response) => {
      if (response && response.error === false) {
        const { data } = response;
        setContextState({
          authorize: true,
          loginError: false,
          loginErrorMessage: '',
          checkAuth: true,
        });
        localStorage.setItem('token', data.token);
        localStorage.setItem('ug', data.guid);
        localStorage.setItem('user', JSON.stringify(data));
      } else {
        setContextState({
          ...contextState,
          loginError: true,
          loginErrorMessage: response.message || 'Api not found',
        });
      }
    });
  };

  const onLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('ug');
    localStorage.removeItem('user');

    setContextState({
      authorize: false,
      permission: 0,
      checkAuth: true,
    });
  };

  const onSignup = (model) => {
    if (model === null) { return; }

    accountService.signup(model).then((response) => {
      if (response && response.error === false) {
        // signup basarili ise authorize'de olacak
        setContextState({
          // ...this.state,
          // authorize:true,
          // email:model.email
          signupError: false,
        });
        onLogin(model);
      } else {
        setContextState({
          // ...this.state,
          signupError: true,
          signupErrorMessage: response.errorMessage
        });
      }
    });
  };

  const setBackrop = () => setContextState({
    ...contextState,
    backdropOpen: !contextState.backdropOpen
  });

  useLayoutEffect(() => {
    accountService.authenticated().then((response) => {
      setContextState({
        ...contextState,
        authorize: response,
        checkAuth: true,
        permission: 0,
      });
    });
  }, []);

  return (
    <AuthContext.Provider
      value={{
        authorize: contextState.authorize,
        checkAuth: contextState.checkAuth,
        loginError: contextState.loginError,
        loginErrorMessage: contextState.loginErrorMessage,
        backdropOpen: contextState.backdropOpen,
        onLogin,
        onLogout,
        onSignup,
        setBackrop,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

const AuthRoute = ({ Component, ...rest }) => (
  <AuthContext.Consumer>
    {({ authorize, checkAuth }) => {
      let content = '';
      if (authorize) {
        content = (
          <Route
            render={(props) => (
              <Component {...props} />
            )}
            {...rest}
          />
        );
      } else if (checkAuth && !authorize) {
        content = <Navigate to="/" />;
      }
      return content;
    }}
  </AuthContext.Consumer>
);

AuthProvider.propTypes = {
  children: PropTypes.node.isRequired,
};

AuthRoute.propTypes = {
  Component: PropTypes.instanceOf(<></>).isRequired,
};

export {
  AuthContext, AuthProvider, AuthRoute,
};
