import { Fetch } from '../components/Fetch';
import urlConfig from '../configs/urlConfig';
import utilitity from '../configs/utilitiy';

export default {
  getKeywords: () => {
    const requestOptions = {
      method: 'GET',
      headers: utilitity.authorizedHeader(),
    };

    return Fetch(urlConfig.wordMeister.getKeywords, requestOptions);
  }
};
