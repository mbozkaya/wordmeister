import { Fetch } from "../components/loading/fetch";
import urlConfig from '../configs/urlConfig';
export default {
    login: model => {
        var requestOptions = {
            method: 'POST',
            data: model,
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
};