import React, { Component, useEffect, useLayoutEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { Route, Redirect, withRouter, Router } from 'react-router-dom';
import accountService from '../services/accountService';
// import Login from '../pages/Login';


const AuthContext = React.createContext();

const AuthConsumer = AuthContext.Consumer;

const AuthProvider = props => {

    const [contextState, setContextState] = useState({
        authorize: false,
        checkAuth: false,
        permission: 0,
        loginError: false,
        loginErrorMessage: "",
        signupError: false,
        signupErrorMessage: ""
    });

    console.log("AuthProvider");
    const { children } = props;

    const onLogin = model => {
        accountService.login(model).then(response => {
            if (response && response.error === false) {
                const { data } = response;
                setContextState({
                    authorize: true,
                    loginError: false,
                    loginErrorMessage: ""
                });
                localStorage.setItem('token', data.token);
                localStorage.setItem('ug', data.guid);
                localStorage.setItem('user', JSON.stringify(data));
            }
            else {
                setContextState({
                    ...contextState,
                    loginError: true,
                    loginErrorMessage: response.errorMessage
                });
            }
        })
    };

    const onLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('ug');
        localStorage.removeItem('user');

        setContextState({
            authorize: false,
            permission: 0,
        });
    }

    const onSignup = model => {
        if (model === null)
            return;

        accountService.signup(model).then(response => {
            if (response && response.error === false) {
                // signup basarili ise authorize'de olacak
                setContextState({
                    // ...this.state,
                    // authorize:true,
                    // email:model.email
                    signupError: false,
                    signupError: ""
                });
                alert("Registration Successful!");
                onLogin(model);

            } else {
                setContextState({
                    // ...this.state,
                    signupError: true,
                    signupErrorMessage: response.errorMessage
                })
            }
        })
    }


    useLayoutEffect(() => {
        accountService.authenticated().then(response => {
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
                onLogin,
                onLogout,
                onSignup,
            }}
        >
            {children}
        </AuthContext.Provider>
    );
}


const AuthRoute = ({ component: Component, ...rest }) => (
    <AuthContext.Consumer>
        {({ authorize, checkAuth }) => {
            let content = '';

            console.log("authorize : " + authorize)
            console.log("checkAuth : " + checkAuth)
            if (!authorize) {
                console.log("getti")

                content = (
                    <Route path='/df'
                        render={props => (
                            <Component {...props} />
                        )}
                        {...rest}
                    />
                );
            } else if (checkAuth && !authorize) {
                console.log('You must be login')
                content = <Redirect to="/" />;
            }
            return content;
        }}
    </AuthContext.Consumer>
);

AuthProvider.propTypes = {
    children: PropTypes.node.isRequired,
};

export { AuthContext, AuthProvider, AuthRoute };