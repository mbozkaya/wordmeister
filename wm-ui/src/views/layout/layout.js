import React from 'react';
import PropTypes from 'prop-types';
import {Header, Footer, SideBar} from './';

const Layout = props => {
  const { topbar, sidebar, footer, onLogout, children, menu } = props;
    return (
        <main>
            <div id="wrapper">
                {topbar ? <Header onLogout={onLogout} /> : null}
                {sidebar ? <SideBar onLogout={onLogout} MenuItems={menu} /> : null}
                <div className="content-page">
                    <div className="content">
                        <div className="container-fluid">
                            {children}
                        </div>
                    </div>
                    {footer ? <Footer /> : null}
                </div>
            </div>
        </main>
    );
};

Layout.propTypes = {
    topbar: PropTypes.bool,
    sidebar: PropTypes.bool,
    footer: PropTypes.bool,
    onLogout: PropTypes.func.isRequired,
    children: PropTypes.node.isRequired,
    menu: PropTypes.object,
  };
  
  Layout.defaultProps = {
    topbar: true,
    sidebar: true,
    footer: true,
    menu: null,
  };
  

export {Layout};