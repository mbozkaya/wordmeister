import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import {
  makeStyles, Button, List, Divider, ListItem, ListItemIcon, ListItemText
} from '@material-ui/core';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';
import wordMeisterService from 'src/services/wordMeisterService';
import ToasterSnackbar from '../ToasterSnackbar';

const useStyles = makeStyles({
  list: {
    width: 250,
  },
  fullList: {
    width: 'auto',
  },
});

const WordCardSettings = (props) => {
  const classes = useStyles();
  const { onSettingsChange } = props;
  const [settingsData, setSettingsData] = useState({

  });

  const getData = () => {
    wordMeisterService.getUserWordSetting().then((response) => {
      if (response && response.error === false) {
        const { data } = response;
        setSettingsData({
          ...settingsData,
          ...data
        });
      } else {
        ToasterSnackbar.error({ message: response.errorMessage || 'An error occured' });
      }
    });
  };

  useEffect(() => { getData(); }, []);
  return (
    <>
      <div
        className={classes.list}
        role="presentation"
      >
        <List>
          {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
            <ListItem button key={text}>
              <ListItemIcon>{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}</ListItemIcon>
              <ListItemText primary={text} />
            </ListItem>
          ))}
        </List>
        <Divider />
        <List>
          {['All mail', 'Trash', 'Spam'].map((text, index) => (
            <ListItem button key={text}>
              <ListItemIcon>{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}</ListItemIcon>
              <ListItemText primary={text} />
            </ListItem>
          ))}
        </List>
      </div>
    </>
  );
};

export default WordCardSettings;

WordCardSettings.propType = {
  onSettingsChange: PropTypes.func,
};

WordCardSettings.defaultPropTypes = {
  onSettingsChange: () => { },
};
