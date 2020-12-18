import React, { useEffect, useState } from 'react';
import {
  Box,
  Container,
  makeStyles
} from '@material-ui/core';
import columns from 'src/configs/columns';
import Page from 'src/components/Page';
import wordMeisterService from 'src/services/wordMeisterService';
import Toolbar from '../customer/CustomerListView/Toolbar';
import DataGrid from './DataGrid';
import DataTable from '../../components/Datatable/DataTable';

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

  const { register, register2 } = columns;
  return (
    <Page
      className={classes.root}
      title="Customers"
    >
      <Container maxWidth={false}>
        <Toolbar />
        <Box mt={3}>
          <DataGrid data={data} columns={register} />
        </Box>
        <Box mt={3}>
          <DataTable data={data} columns={register2} />
        </Box>
      </Container>
    </Page>
  );
};

export default KeywordView;
