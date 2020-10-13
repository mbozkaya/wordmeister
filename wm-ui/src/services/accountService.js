import { Fetch } from "../components/loading/fetch";
import urlConfig from '../configs/urlConfig';
import utilitity from "../configs/utilitiy";
export default {
    login: model => {
        var requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(model),
        };

        return Fetch(urlConfig.account.login, requestOptions);
    },
    signup: model => {
        var requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(model)
        };

        return Fetch(urlConfig.account.signup, requestOptions);
    },
    authenticated: () => {
        var requestOptions = {
            method: 'GET',
            headers: utilitity.authorizedHeader(),
        };

        return fetch(urlConfig.account.authenticated, requestOptions)
        .then(response => {
            if (response.ok) {
                return true;
            }
            if (response.status === 401) {
                localStorage.removeItem('user');
            } else {
                throw new Error();
            }
            return false;
        })
        .then(json => {
            return json;
        })
        .catch(ex => {
            console.error(ex);
            return false;
        });
    }
};