import React, { useEffect, useState } from 'react';
import Backdrop from '@material-ui/core/Backdrop';
import CircularProgress from '@material-ui/core/CircularProgress';
import { makeStyles } from '@material-ui/core/styles';
import { AuthContext } from 'src/contexts/authContext';

const useStyles = makeStyles((theme) => ({
  backdrop: {
    zIndex: theme.zIndex.drawer + 1,
    color: '#fff',
  },
}));

const Blockui = () => {
  const classes = useStyles();
  return (
    <AuthContext.Consumer>
      {({ backdropOpen }) => (
        <Backdrop className={classes.backdrop} open={backdropOpen}>
          <CircularProgress color="inherit" />
        </Backdrop>
      )}
    </AuthContext.Consumer>
  );
};

export default Blockui;
