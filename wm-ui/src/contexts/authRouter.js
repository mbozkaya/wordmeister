import React from 'react';
import { AuthConsumer, AuthContext, AuthProvider } from './authContext'
import { Route, Redirect, withRouter, useHistory } from 'react-router-dom';
import Dashboard from '../views/dashboard/dashboard';
import Login from '../pages/Login';

const AuthRouteX = ({ component: Component, ...rest }) => {
    const history = useHistory();

    return (
        <AuthContext.Consumer>
            {
                ({ authorize, checkAuth }) => {
                    let content = '';

                    console.log("authorize : " + authorize)
                    console.log("checkAuth : " + checkAuth)
                    if (authorize) {
                        content = (<Route
                            render={props =>
                                <Component {...props} />
                            }
                        />)
                    } else if (checkAuth && !authorize) {
                        content = (
                            <Route to='ds' component={Login}>
                                {
                                    history.push('/login')
                                }
                            </Route>
                        )
                    }
                    return content;
                }
            }
        </AuthContext.Consumer>
    )
}


const UnAuthRouteX = ({ component: Component, ...rest }) => {
    const history = useHistory();

    return (
        <AuthContext.Consumer>
            {
                ({ authorize, checkAuth }) => {
                    let content = '';

                    console.log("Un-authorize : " + authorize)
                    console.log("Un*checkAuth : " + checkAuth)
                    if (authorize) {
                        content = (
                            <Route component={Dashboard}>
                            {
                                history.push('/dashboard')
                            }
                        </Route>)
                    } else if (checkAuth && !authorize) {
                        content = (
                            <Route
                                render={props =>
                                    <Component {...props} />
                                }
                            />)
                    }
                    return content;
                }
            }
        </AuthContext.Consumer>
    )
}
export { AuthRouteX, UnAuthRouteX };
