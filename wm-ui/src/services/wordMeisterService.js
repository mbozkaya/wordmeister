import { Fetch } from '../components/Fetch';
import urlConfig from '../configs/urlConfig';
import utilitity from '../configs/utilitiy';

export default {
  getKeywords: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(model),
    };
    return Fetch(urlConfig.wordMeister.getKeywords, requestOptions);
  },
  getAnswer: (id) => {
    const requestOptions = {
      method: 'GET',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(id),
    };
    return Fetch(`${urlConfig.wordMeister.getAnswer}`, requestOptions);
  },
  createRegister: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(model),
    };
    return Fetch(urlConfig.wordMeister.createRegister, requestOptions);
  },
  updateRegister: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(model),
    };
    return Fetch(urlConfig.wordMeister.updateRegister, requestOptions);
  },
  updateAnswer: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(model),
    };
    return Fetch(urlConfig.wordMeister.updateAnswer, requestOptions);
  },
  checkAnswer: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(model),
    };
    return Fetch(urlConfig.wordMeister.checkAnswer, requestOptions);
  },
  removeAnswer: (model) => {
    const requestOptions = {
      method: 'DELETE ',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(model),
    };
    return Fetch(urlConfig.wordMeister.removeAnswer, requestOptions);
  },
  removeRegister: (model) => {
    const requestOptions = {
      method: 'DELETE',
      headers: utilitity.authorizedHeader(),
      body: JSON.stringify(model),
    };
    return Fetch(urlConfig.wordMeister.removeRegister, requestOptions);
  },
};
