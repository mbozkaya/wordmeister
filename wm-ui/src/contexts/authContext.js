import React, { Component, useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { Route, Redirect } from 'react-router-dom';
import accountService from '../services/accountService';

const AuthContext = React.createContext();

const AuthConsumer = AuthContext.Consumer;

class AuthProvider extends Component {
    constructor(props) {
        super(props);
        this.state = {
            authorize: false,
            permission: 0,
        };

        this.onLogin = this.onLogin.bind(this);
        this.onLogout = this.onLogout.bind(this);
    }

    onLogin(model) {
        accountService.login(model).then(response => {
            if (response && response.error === false) {
                const { data } = response;
                this.setState({
                    ...this.state,
                    authorize: true,
                    email: data.email,
                });
                localStorage.setItem('token', data.token);
                localStorage.setItem('ug', data.guid);
                localStorage.setItem('user', JSON.stringify(data));
            }
        })
    };

    onLogout() {
        localStorage.removeItem('token');
        localStorage.removeItem('ug');
        localStorage.removeItem('user');

        this.setState({
            authorize: false,
            permission: 0,
        });
    }

    componentDidMount() {
        accountService.authenticated().then(response => {
            if (response) {
                this.setState({
                    authorize: true,
                    permission: 0,
                });
            }

        });
    }

    render() {
        const { children } = this.props;
        const { authorize } = this.state;
        return (
            <AuthContext.Provider
                value={{
                    authorize,
                    onLogin: this.onLogin,
                    onLogout: this.onLogout,
                }}
            >
                {children}
            </AuthContext.Provider>
        );
    }
}

const AuthRoute = ({ component: Component, ...rest }) => {
    let content = ''
    return (
        <AuthContext.Consumer>
            {
                ({ authorize }) => {
                    if (authorize) {
                        content = (<Route render={props =>
                            <Component {...props} />
                        } />)
                    } else {
                        content = <Redirect to='/login' />
                    }

                    return content;
                }
            }
        </AuthContext.Consumer>
    )

}

AuthProvider.propTypes = {
    children: PropTypes.node.isRequired,
};

export { AuthProvider, AuthConsumer, AuthContext, AuthRoute };