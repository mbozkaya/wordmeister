import appConfig from './appConfig';

export default {
  account: {
    login: `${appConfig.api.accountService()}/login`,
    signup: `${appConfig.api.accountService()}/signup`,
    authenticated: `${appConfig.api.accountService()}/authenticated`,
    accountInformation: `${appConfig.api.accountService()}/AccountInformation`,
    uploadFile: `${appConfig.api.accountService()}/UploadFile`,
    userImages: `${appConfig.api.accountService()}/UserImages`,
    userProfilePic: `${appConfig.api.accountService()}/UserProfilePic`,
    removeFile: `${appConfig.api.accountService()}/RemoveFile`,
  },
  wordMeister: {
    addWord: `${appConfig.api.service()}Word/AddWord`,
    getWord: `${appConfig.api.service()}Word/GetWord`,
    getWords: `${appConfig.api.service()}Word/GetWords`,
    deleteWord: `${appConfig.api.service()}Word/DeleteWord`,
    updateWord: `${appConfig.api.service()}Word/UpdateWord`,
    wordCard: `${appConfig.api.service()}Word/WordCard`,
    wordPoint: `${appConfig.api.service()}Word/WordPoint`,
    wordFavorite: `${appConfig.api.service()}Word/WordFavorite`,
    customSentence: `${appConfig.api.service()}Word/CustomSentence`,
    learned: `${appConfig.api.service()}Word/Learned`,
    userWordSetting: `${appConfig.api.service()}Word/UserWordSetting`,
  },
};
