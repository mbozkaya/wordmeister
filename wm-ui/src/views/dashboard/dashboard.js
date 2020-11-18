import { useReducedMotion } from 'framer-motion';
import React from 'react';
import { AuthConsumer, AuthContext, AuthProvider } from "../../contexts/authContext";
import AnimationRevealPage from "../../helpers/AnimationRevealPage.js";
import common from '../../helpers/common'
import Hero from "../../components/hero/TwoColumnWithInput.js";
import { Header } from '../layout/header';
import { Layout } from '../layout/layout';


const Dashboard = () => {
    const user = common.currentUser();
    return (
        <AuthContext.Consumer>
            {() => (
                    <Layout sidebar={true}>  
                    <div>
                        <h1> Dashboard </h1>
                        <hr />
                        {/* {console.log({ user })} */}
                        <h2> Welcome <b>{user.firstName} , naber</b></h2>
                    </div>
                    </Layout>
            )}
        </AuthContext.Consumer>
    )
}

export default Dashboard;