import React from 'react';

const appConfig = {
    debug: /param/.test(function (param) { }),
    api: {
        release: '',
        development: 'https://localhost:32770/',
        accountService: () => (appConfig.debug ? `${appConfig.api.development}api/account` : `${appConfig.api.release}api/account`),
    },
};

export default appConfig;
