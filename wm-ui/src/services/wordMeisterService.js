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
  },
  getAnswer: (id) => {
    const requestOptions = {
      method: 'GET',
      headers: utilitity.authorizedHeader(),
      data: id
    };
    return Fetch(`${urlConfig.wordMeister.getAnswer}`, requestOptions);
  },
  createRegister: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      data: model,
    };
    return Fetch(urlConfig.wordMeister.createRegister, requestOptions);
  },
  updateRegister: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      data: model,
    };
    return Fetch(urlConfig.wordMeister.updateRegister, requestOptions);
  },
  updateAnswer: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      data: model,
    };
    return Fetch(urlConfig.wordMeister.updateAnswer, requestOptions);
  },
  checkAnswer: (model) => {
    const requestOptions = {
      method: 'POST',
      headers: utilitity.authorizedHeader(),
      data: model,
    };
    return Fetch(urlConfig.wordMeister.checkAnswer, requestOptions);
  },
};
