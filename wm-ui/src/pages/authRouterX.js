import React from 'react';
import { Route, Redirect,withRouter  } from 'react-router-dom';
import { AuthConsumer, AuthContext, AuthProvider } from "../contexts/authContext.js";

const AuthRouteXX = ({ component: Component, ...rest }) => {
    return (
        <AuthContext.Consumer>
            {

                ({ authorize,checkAuth}) => {
                let content = '';

            console.log("authorize : "+authorize)
            console.log("checkAuth : "+checkAuth)

                    if (authorize) {
                        content = (<Route render={props =>
                            <Component {...props} />
                        } />)
                    } else{
                        content = <Redirect to='/login' />
                    }
                    return content;
                }
            }
        </AuthContext.Consumer>
    )

}

export { AuthRouteXX};
