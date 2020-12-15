import React, { useEffect, useState } from 'react';
import {
  Box,
  Container,
  makeStyles
} from '@material-ui/core';
import Page from 'src/components/Page';
import wordMeisterService from 'src/services/wordMeisterService';
import Results from '../customer/CustomerListView/Results';
import Toolbar from '../customer/CustomerListView/Toolbar';
import DataGrid from './DataGrid';

const useStyles = makeStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingBottom: theme.spacing(3),
    paddingTop: theme.spacing(3)
  }
}));

const KeywordView = () => {
  const classes = useStyles();
  const [data, setData] = useState([]);

  useEffect(() => {
    wordMeisterService.getKeywords().then((response) => {
      if (response.error === false) {
        setData(response.data);
      }
    });
  }, []);

  return (
    <Page
      className={classes.root}
      title="Customers"
    >
      <Container maxWidth={false}>
        <Toolbar />
        <Box mt={3}>
          <DataGrid data={data} />
        </Box>
      </Container>
    </Page>
  );
};

export default KeywordView;
