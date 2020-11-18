import React, { useEffect } from 'react';
import {
  Container,
  Grid,
  makeStyles
} from '@material-ui/core';
import Page from '../../../components/admin/page';

const useStyles = makeStyles((theme) => ({
    root: {
      backgroundColor: theme.palette.background.dark,
      minHeight: '100%',
      paddingBottom: theme.spacing(3),
      paddingTop: theme.spacing(3)
    }
  }));
  

  
const Dashboard = () => {
    const classes = useStyles();

    useEffect(()=>{console.log('effect')})
  
    return (
      <Page
        className={classes.root}
        title="Dashboard"
      >
        <Container maxWidth={false}>
          <Grid
            container
            spacing={3}
          >
              <div>test</div>
          </Grid>
        </Container>
      </Page>
    );
  };
  
  export default Dashboard;