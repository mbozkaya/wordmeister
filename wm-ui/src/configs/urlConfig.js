import appConfig from './appConfig';

export default {
    account: {
        login: `${appConfig.api.accountService()}/login`,
        signup: `${appConfig.api.accountService()}/signup`,
        authenticated: `${appConfig.api.accountService()}/authenticated`,
    },
};