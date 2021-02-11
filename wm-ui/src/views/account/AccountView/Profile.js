/* eslint-disable react/no-array-index-key */
/* eslint-disable jsx-a11y/label-has-associated-control */
import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import moment from 'moment';
import {
  Avatar,
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Divider,
  Typography,
  makeStyles,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField,
  Input,
  GridList,
  GridListTile,
  GridListTileBar,
  ListSubheader,
  IconButton,
  Container,
  Grid,
} from '@material-ui/core';
import InfoIcon from '@material-ui/icons/Info';
import { v4 as uuid } from 'uuid';
import urlConfig from 'src/configs/urlConfig';

const user = {
  avatar: '/static/images/avatars/avatar_6.png',
  city: 'Los Angeles',
  country: 'USA',
  jobTitle: 'Senior Developer',
  name: 'Katarina Smith',
  timezone: 'GTM-7'
};

const useStyles = makeStyles((theme) => ({
  root: {},
  avatar: {
    height: 100,
    width: 100
  },
  gridList: {
    width: 500,
    height: 450,
  },
  icon: {
    color: 'rgba(255, 255, 255, 0.54)',
  },
  input: {
    display: 'none',
  },
  selectedImage: {
    border: 'solid 2px #76f976',
  },
  gridTile: {
    cursor: 'pointer',
    transition: ' box-shadow 0.6s linear;',
    '&:hover': {
      transform: 'scale(1.01)',
    }
  }
}));

const Profile = ({ className, ...rest }) => {
  const classes = useStyles();
  const [openDialog, setOpenDialog] = useState(false);
  const [tileData, setTileData] = useState([
    {
      img: '/static/images/avatars/avatar_6.png',
      title: 'Test',
      author: 'dsfs',
      selected: false,
      uui: uuid(),
    },
    {
      img: '/static/images/avatars/avatar_6.png',
      title: 'Test',
      author: 'dsfs',
      selected: false,
      uui: uuid(),
    },
    {
      img: '/static/images/avatars/avatar_6.png',
      title: 'Test',
      author: 'dsfs',
      selected: true,
      uui: uuid(),
    },
  ]);

  useEffect(() => { }, [tileData]);

  const uploadPhoto = () => {
    const selectedImage = tileData.filter((f) => f.selected);
    if (typeof selectedImage === 'undefined') {
      return false;
    }

    if (selectedImage[0].isNew && selectedImage[0].isNew === true) {
      const xhr = new XMLHttpRequest();
      const formData = new FormData();
      formData.append('file', selectedImage[0].file);
      formData.append('type', 1);
      formData.append('description', '');
      xhr.open('POST', urlConfig.account.uploadFile, true);
      const userCrendentials = JSON.parse(localStorage.getItem('user'));
      xhr.setRequestHeader('Authorization', `Bearer ${userCrendentials.token}`);
      xhr.send(formData);
      debugger;
      xhr.onload = () => {
        if (xhr.status === 200) {
          const response = JSON.parse(xhr.response);
          if (response && response.error === false) {
          }
        }
      };
    } else {

    }
  };

  return (
    <>
      <Card
        className={clsx(classes.root, className)}
        {...rest}
      >
        <CardContent>
          <Box
            alignItems="center"
            display="flex"
            flexDirection="column"
          >
            <Avatar
              className={classes.avatar}
              src={user.avatar}
            />
            <Typography
              color="textPrimary"
              gutterBottom
              variant="h3"
            >
              {user.name}
            </Typography>
            <Typography
              color="textSecondary"
              variant="body1"
            >
              {`${user.city} ${user.country}`}
            </Typography>
            <Typography
              className={classes.dateText}
              color="textSecondary"
              variant="body1"
            >
              {`${moment().format('hh:mm A')} ${user.timezone}`}
            </Typography>
          </Box>
        </CardContent>
        <Divider />
        <CardActions>
          <Button
            color="primary"
            fullWidth
            variant="text"
            onClick={() => setOpenDialog(true)}
          >
            Upload picture
          </Button>
        </CardActions>
      </Card>
      <Dialog open={openDialog} onClose={() => setOpenDialog(false)} aria-labelledby="form-dialog-title">
        <DialogTitle id="form-dialog-title">All Images</DialogTitle>
        <DialogContent>
          <Grid
            container
          >
            <Grid
              item
            >
              <GridList cellHeight={180} className={classes.gridList}>
                <GridListTile key="Subheader" cols={2} style={{ height: 'auto' }}>
                  <ListSubheader component="div">Images You Uploaded Before</ListSubheader>
                </GridListTile>
                {tileData.map((tile, index) => (
                  <GridListTile
                    key={`${tile.img}${index}`}
                    className={`${classes.gridTile} ${tile.selected && classes.selectedImage}`}
                    onClick={() => {
                      tileData.forEach((f) => { f.selected = false; });
                      tileData[tileData.findIndex((f) => f.uui === tile.uui)] = {
                        ...tile,
                        selected: true,
                      };
                      setTileData([...tileData]);
                    }}
                  >
                    <img src={tile.img} alt={tile.title} />
                    <GridListTileBar
                      title={tile.title}
                      subtitle={(
                        <span>
                          by:
                          {tile.author}
                        </span>
                      )}
                      actionIcon={(
                        <IconButton aria-label={`info about ${tile.title}`} className={classes.icon} title="">
                          <InfoIcon />
                        </IconButton>
                      )}
                    />
                  </GridListTile>
                ))}
              </GridList>
            </Grid>
            <Grid item>
              <input
                accept="image/*"
                className={classes.input}
                id="contained-button-file"
                multiple
                type="file"
                onChange={(image) => {
                  const { target: { files } } = image;

                  if (files.length < 1) {
                    return false;
                  }
                  tileData[tileData.length] = {
                    isNew: true,
                    title: files[0].name,
                    selected: false,
                    uui: uuid(),
                    file: files[0],
                  };

                  setTileData([...tileData]);

                  const fr = new FileReader();
                  fr.onload = (e) => {
                    const uri = e.target.result;
                    tileData[tileData.length - 1] = {
                      ...tileData[tileData.length - 1],
                      img: uri,
                    };
                    setTileData([...tileData]);
                  };
                  fr.readAsDataURL(files[0]);

                  return true;
                }}
                onClick={(e) => { console.log(e); }}
              />
              <label htmlFor="contained-button-file">
                <Button variant="contained" color="primary" component="span">
                  Upload
                </Button>
              </label>
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenDialog(false)} color="primary">
            Cancel
          </Button>
          <Button onClick={uploadPhoto} color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

Profile.propTypes = {
  className: PropTypes.string
};

export default Profile;
