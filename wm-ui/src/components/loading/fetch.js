import PropTypes from 'prop-types';
import { loading } from './loading';

export const Fetch = (url, requestOptions) => {
    loading();

    return (fetch(url, requestOptions)
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            if (response.status === 401) {
                localStorage.removeItem('user');
                if (window.location.pathname !== '/') window.location = '/';
            } else {
                throw new Error();
            }
            return false;
        })
        .then(json => {
            loading();
            return json;
        })
        .catch(ex => {
            loading();
            console.error(ex);
            return false;
        })
    );
};

Fetch.propTypes = {
    url: PropTypes.string.isRequired,
    requestOptions: PropTypes.object.isRequired,
};