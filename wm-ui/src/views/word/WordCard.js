/* eslint-disable react/no-array-index-key */
import React, { useEffect, useState } from 'react';
import {
  Container,
  Grid,
  makeStyles,
  Card,
  CardContent,
  CardHeader,
  Divider,
  Button,
  Box,
  List,
  ListItem,
  ListItemText,
  ListItemAvatar,
  Typography
} from '@material-ui/core';
import Page from 'src/components/Page';
import ArrowForwardIosIcon from '@material-ui/icons/ArrowForwardIos';
import ArrowBackIosIcon from '@material-ui/icons/ArrowBackIos';
import wordMeisterService from 'src/services/wordMeisterService';
import ToasterSnackbar from 'src/components/ToasterSnackbar';

const useStyles = makeStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingBottom: theme.spacing(3),
    paddingTop: theme.spacing(3)
  },
  root2: {
    width: '100%',
    maxWidth: '100%',
    backgroundColor: theme.palette.background.paper,
  },
  inline: {
    display: 'inline',
  },
  typography: {
    opacity: '0.5',
    textAlign: 'center',
  }
}));

const WordCard = () => {
  const classes = useStyles();
  const [cardData, setCardData] = useState({
    userWordId: 0,
    word: '',
    description: '',
    sentences: [],
  });

  const getData = () => {
    wordMeisterService.getWordCard().then((response) => {
      if (response && response.error === false) {
        const { data } = response;
        setCardData({
          ...cardData,
          userWordId: data.userWordId,
          word: data.word,
          sentences: data.sentences,
          description: data.description,
        });
      } else {
        ToasterSnackbar.error({ message: response.errorMessage });
      }
    });
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
      <Page
        className={classes.root}
        title="Account"
      >
        <Container maxWidth="lg">
          <Grid
            container
            spacing={3}
          >
            <Grid
              lg={1}
              md={1}
              xs={1}
              container
              direction="row"
              justify="center"
              alignItems="center"
            >
              <Box>
                <Button type="button" onClick={() => getData()}>
                  <ArrowBackIosIcon />
                </Button>
              </Box>
            </Grid>
            <Grid
              item
              lg={10}
              md={10}
              xs={10}
            >
              <Card>
                <CardHeader
                  subheader={cardData.description}
                  title={cardData.word}
                  disableTypography={false}
                  titleTypographyProps={{ align: 'center', variant: 'h1' }}
                  subheaderTypographyProps={{ align: 'center' }}
                />
                <Divider />
                <CardContent>
                  <Grid
                    container
                    spacing={3}
                  >
                    <List className={classes.root2}>
                      {
                                                cardData.sentences.map((sentence, index) => (
                                                  <>
                                                    <ListItem alignItems="flex-start" key={`${index}listItem`}>
                                                      <ListItemAvatar key={`${index}avatar`}>
                                                        <Typography variant="h1" className={classes.typography} key={`${index}typo`}>{index + 1}</Typography>
                                                      </ListItemAvatar>
                                                      <ListItemText
                                                        primary={sentence}
                                                        key={`${index}listItemtext`}
                                                      />
                                                    </ListItem>
                                                    <Divider key={`${index}divider`} variant="inset" component="li" />
                                                  </>
                                                ))
                                            }
                    </List>
                  </Grid>
                </CardContent>
                <Divider />
              </Card>
            </Grid>
            <Grid
              lg={1}
              md={1}
              xs={1}
              container
              direction="row"
              justify="center"
              alignItems="center"
            >
              <Box>
                <Button type="button" onClick={() => getData()}>
                  <ArrowForwardIosIcon />
                </Button>
              </Box>
            </Grid>
          </Grid>
        </Container>
      </Page>
    </>
  );
};

export default WordCard;
