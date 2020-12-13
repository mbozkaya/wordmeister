import React, { Component, useEffect, useLayoutEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { Route, Navigate, withRouter, Router } from 'react-router-dom';
import accountService from '../services/accountService';
import { Layout } from '../views/layout/layout';
import { DashboardLayout } from '../views/layout/DashboardLayout/index';
// import Login from '../pages/Login';


const AuthContext = React.createContext();

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
                    loginErrorMessage: "",
                    checkAuth: true,
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


const AuthRoute = ({ Component: Component, ...rest }) => (
    <AuthContext.Consumer>
        {({ authorize, checkAuth }) => {
            let content = '';
            debugger;
            if (authorize) {
                content = (
                    <Route
                        render={props => (
                            <DashboardLayout>
                                <Component {...props} />
                            </DashboardLayout>
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

export { AuthContext, AuthProvider, AuthRoute };