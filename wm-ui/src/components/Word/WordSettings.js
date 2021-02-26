/* eslint-disable no-restricted-globals */
import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import {
  makeStyles,
  List,
  Divider,
  ListItem,
  ListItemIcon,
  ListItemText,
  Switch,
  Typography,
  TextField,
  MenuItem,
  Grid
} from '@material-ui/core';
import FavoriteIcon from '@material-ui/icons/Favorite';
import DoneAllIcon from '@material-ui/icons/DoneAll';
import BookmarkIcon from '@material-ui/icons/Bookmark';
import FormatListNumberedIcon from '@material-ui/icons/FormatListNumbered';
import SortByAlphaIcon from '@material-ui/icons/SortByAlpha';
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
  const { onSettingsChange, drawerOpen } = props;
  const [settingsData, setSettingsData] = useState({
    isIncludeFavorite: false,
    isIncludeLearned: false,
    isIncludePoint: false,
    order: 'CreatedDate',
    orderBy: 'desc',
    point: 0,
    conditionType: 1,
  });

  const orderOptions = [
    {
      value: 'CreatedDate',
      label: 'Created Date',
    },
    {
      value: 'LearnedDate',
      label: 'Learned Date',
    },
    {
      value: 'Point',
      label: 'Point',
    },
    {
      value: 'Word.Text',
      label: 'Word',
    },
  ];
  const orderByOptions = [
    {
      value: 'asc',
      label: 'Increasing',
    },
    {
      value: 'desc',
      label: 'Decreasing',
    },
  ];
  const conditionOptions = [
    {
      value: '1',
      label: '=',
    },
    {
      value: '2',
      label: '!=',
    },
    {
      value: '3',
      label: '<',
    },
    {
      value: '4',
      label: '<=',
    },
    {
      value: '5',
      label: '>',
    },
    {
      value: '6',
      label: '>=',
    },
  ];

  const getData = () => {
    wordMeisterService.getUserWordSetting().then((response) => {
      if (response && response.error === false) {
        const { data } = response;
        setSettingsData({
          ...settingsData,
          ...data,
          point: data.point / 2,
        });
      } else {
        ToasterSnackbar.error({ message: response.errorMessage || 'An error occured' });
      }
    });
  };

  useEffect(() => { getData(); }, []);
  useEffect(() => {
    if (drawerOpen === false) {
      wordMeisterService.setUserWordSetting({ ...settingsData, point: settingsData.point * 2 })
        .then((response) => {
          if (response && response.error === false) {
            if (onSettingsChange) {
              onSettingsChange();
            }
          } else {
            ToasterSnackbar.error({ message: response.errorMessage || 'An error occured' });
          }
        });
    }
  }, [drawerOpen]);
  return (
    <>
      <div
        className={classes.list}
        role="presentation"
      >
        <List>
          <ListItem key="">
            <Typography align="center" variant="h3">Word Settings</Typography>
          </ListItem>
        </List>
        <Divider />
        <List>
          <ListItem key="Learned">
            <ListItemIcon><DoneAllIcon /></ListItemIcon>
            <ListItemText primary="Is include Learned?" />
            <Switch
              checked={settingsData.isIncludeLearned}
              color="primary"
              onChange={() => setSettingsData({
                ...settingsData,
                isIncludeLearned: !settingsData.isIncludeLearned
              })}
            />
          </ListItem>
          <Divider />
          <ListItem key="Favorite">
            <ListItemIcon><BookmarkIcon /></ListItemIcon>
            <ListItemText primary="Is include favorite?" />
            <Switch
              checked={settingsData.isIncludeFavorite}
              color="primary"
              onChange={() => setSettingsData({
                ...settingsData,
                isIncludeFavorite: !settingsData.isIncludeFavorite
              })}
            />
          </ListItem>
          <Divider />
          <ListItem key="Point">
            <ListItemIcon><FavoriteIcon /></ListItemIcon>
            <ListItemText primary="Is include point?" />
            <Switch
              checked={settingsData.isIncludePoint}
              color="primary"
              onChange={() => setSettingsData({
                ...settingsData,
                isIncludePoint: !settingsData.isIncludePoint
              })}
            />
          </ListItem>
          {
            settingsData.isIncludePoint && (
              <ListItem key="Pointnumber">
                <Grid container justify="flex-end" alignContent="space-between">
                  <Grid item xs={6}>
                    <TextField
                      id="standard-number"
                      label="Number"
                      type="number"
                      InputLabelProps={{
                        shrink: true,
                      }}
                      value={settingsData.point}
                      onChange={(e) => {
                        if (!isNaN(parseFloat(e.target.value)) && parseFloat(e.target.value) <= 5) {
                          setSettingsData({
                            ...settingsData,
                            point: parseFloat(e.target.value),
                          });
                        }
                      }}
                      inputProps={{ step: '0.5', min: 0, max: 5 }}
                    />
                  </Grid>
                  <Grid item xs={6}>
                    <TextField
                      select
                      label="Condition"
                      value={settingsData.conditionType}
                      onChange={(e) => setSettingsData({
                        ...settingsData,
                        conditionType: parseInt(e.target.value, 10),
                      })}
                    >
                      {conditionOptions.map((option) => (
                        <MenuItem key={option.value} value={option.value}>
                          {option.label}
                        </MenuItem>
                      ))}
                    </TextField>
                  </Grid>
                </Grid>
              </ListItem>
            )
          }
          <Divider />
          <ListItem key="Order">
            <ListItemIcon><FormatListNumberedIcon /></ListItemIcon>
            <TextField
              select
              label="Order Column"
              value={settingsData.order}
              onChange={(e) => setSettingsData({
                ...settingsData,
                order: e.target.value,
              })}
            >
              {orderOptions.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </TextField>
          </ListItem>
          <Divider />
          <ListItem key="OrderBy">
            <ListItemIcon><SortByAlphaIcon /></ListItemIcon>
            <TextField
              select
              label="Order By"
              value={settingsData.orderBy}
              onChange={(e) => setSettingsData({
                ...settingsData,
                orderBy: e.target.value,
              })}
            >
              {orderByOptions.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </TextField>
          </ListItem>
        </List>
      </div>
    </>
  );
};

export default WordCardSettings;

WordCardSettings.propTypes = {
  onSettingsChange: PropTypes.func,
  drawerOpen: PropTypes.bool.isRequired,
};

WordCardSettings.defaultPropTypes = {
  onSettingsChange: () => { },
};
