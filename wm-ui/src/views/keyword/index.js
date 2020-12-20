import React, { useEffect, useState } from 'react';
import {
  Box,
  Container,
  makeStyles
} from '@material-ui/core';
import columns from 'src/configs/columns';
import Page from 'src/components/Page';
import wordMeisterService from 'src/services/wordMeisterService';
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

  const getRegisters = () => wordMeisterService.getKeywords().then((response) => {
    if (response.error === false) {
      setData(response.data);
    }
  });

  const updateRegister = (model) => {
    wordMeisterService.updateRegister(model).then((response) => {
      if (response.error === false) {
        getRegisters();
      } else {
        console.log(response);
      }
    });
  };

  const insertRegister = (model) => wordMeisterService.createRegister(model).then((response) => {
    if (response.error === false) {
      getRegisters();
    } else {
      console.log(response);
    }
  });

  useEffect(() => {
    getRegisters();
  }, []);

  const { register } = columns;
  return (
    <Page
      className={classes.root}
      title="Keywords"
    >
      <Container maxWidth={false}>
        <Box mt={3}>
          <DataTable data={data} columns={register} rowEdit={(model) => updateRegister(model)} insertNewRow={(model) => insertRegister(model)} />
        </Box>
      </Container>
    </Page>
  );
};

export default KeywordView;
