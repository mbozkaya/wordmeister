import appConfig from './appConfig';

export default {
  account: {
    login: `${appConfig.api.accountService()}/login`,
    signup: `${appConfig.api.accountService()}/signup`,
    authenticated: `${appConfig.api.accountService()}/authenticated`,
  },
  wordMeister: {
    getKeywords: `${appConfig.api.service()}Wordbook/GetRegister`,
    getAnswer: `${appConfig.api.service()}Wordbook/GetAnswer`,
    createRegister: `${appConfig.api.service()}Wordbook/CreateRegister`,
    updateRegister: `${appConfig.api.service()}Wordbook/UpdateRegister`,
    updateAnswer: `${appConfig.api.service()}Wordbook/UpdateAnswer`,
    checkAnswer: `${appConfig.api.service()}Wordbook/CheckAnswer`,
    removeRegister: `${appConfig.api.service()}Wordbook/RemoveRegister`,
    removeAnswer: `${appConfig.api.service()}Wordbook/RemoveAnswer`,
    addWord: `${appConfig.api.service()}Word/AddWord`,
    getWord: `${appConfig.api.service()}Word/GetWord`,
    getWords: `${appConfig.api.service()}Word/GetWords`,
    deleteWord: `${appConfig.api.service()}Word/DeleteWord`,
    updateWord: `${appConfig.api.service()}Word/UpdateWord`,
  },
};
