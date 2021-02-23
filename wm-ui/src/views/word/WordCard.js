/* eslint-disable max-len */
/* eslint-disable react/no-array-index-key */
import React, { useEffect, useState } from 'react';
import {
  Grid,
  makeStyles,
  Divider,
  Button,
  Box,
  List,
  ListItem,
  ListItemText,
  ListItemAvatar,
  Typography,
  Fab,
  withStyles,
} from '@material-ui/core';
import ToggleButton from '@material-ui/lab/ToggleButton';
import AddIcon from '@material-ui/icons/Add';
import StarIcon from '@material-ui/icons/Star';
import FavoriteIcon from '@material-ui/icons/Favorite';
import Rating from '@material-ui/lab/Rating';
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
  listRoot: {
    width: '100%',
    maxWidth: '100%',
    backgroundColor: theme.palette.background.paper,
  },
  typography: {
    opacity: '0.5',
    textAlign: 'center',
  },
  mainRoot: {
    flexGrow: 1,
  },
  backgroundPaper: {
    backgroundColor: theme.palette.background.paper
  },
  starIcon: {
    '&:hover': {
      transform: 'scale(1.5)',
    },
  }
}));

const StyledRating = withStyles({
  iconFilled: {
    color: '#ff6d75',
  },
  iconHover: {
    color: '#ff3d47',
  },
})(Rating);

const WordCard = () => {
  const classes = useStyles();
  const [cardData, setCardData] = useState({
    userWordId: 0,
    word: '',
    description: '',
    sentences: [],
    isOver: false,
    isFavorite: false,
    point: 0,
    wordCount: 0,
    currentIndex: 1,
  });

  const getData = (currentIndex, isRandom = false) => {
    const model = {
      currentIndex,
      isRandom
    };
    wordMeisterService.getWordCard(model).then((response) => {
      if (response && response.error === false) {
        const { data } = response;
        setCardData({
          ...cardData,
          userWordId: data.userWordId,
          word: data.word,
          sentences: data.sentences,
          description: data.description,
          isOver: data.isOver,
          isFavorite: data.isFavorite,
          point: data.point / 2,
          wordCount: data.wordCount,
          currentIndex: data.currentIndex,
        });
      } else {
        ToasterSnackbar.error({ message: response.errorMessage });
      }
    });
  };

  const navigateButton = (e) => {
    if (e.key.toLowerCase() === 'arrowleft') {
      getData(cardData.currentIndex - 1 > 0 ? cardData.currentIndex - 1 : 1);
    } else if (e.key.toLowerCase() === 'arrowright') {
      getData(cardData.currentIndex + 1);
    } else {
      console.log(e);
    }
  };
  useEffect(() => {
    getData();
    window.addEventListener('keyup', navigateButton);
    return () => window.removeEventListener('keyup', navigateButton);
  }, []);

  const goldColor = '#ffb400';

  return (
    <>
      <Page
        className={classes.root}
        title="Account"
      >
        <div className={classes.mainRoot}>
          <Grid
            container
            spacing={3}
          >
            <Grid item xs={12}>
              <Grid container justify="center" spacing={2}>
                <Grid item xs={2} />
                <Grid item xs={8}>
                  <Grid container>
                    <Grid item xs={2} className={classes.backgroundPaper}>
                      <Grid container justify="center" alignContent="center">
                        <Box component="fieldset" mb={3} borderColor="transparent">
                          <StyledRating
                            name="customized-color"
                            value={cardData.point}
                            getLabelText={(value) => `${value} Heart${value !== 1 ? 's' : ''}`}
                            precision={0.5}
                            icon={<FavoriteIcon fontSize="inherit" />}
                            onChange={(e, v) => {
                              if (v !== cardData.point) {
                                const model = {
                                  userWordId: cardData.userWordId,
                                  point: v * 2,
                                };
                                wordMeisterService.setWordPoint(model).then((response) => {
                                  if (response && response.error === false) {
                                    setCardData({
                                      ...cardData,
                                      point: v,
                                    });
                                  } else {
                                    ToasterSnackbar.error({ message: response.errorMessage || 'An error occured' });
                                  }
                                });
                              }
                            }}
                          />
                        </Box>
                      </Grid>
                    </Grid>
                    <Grid item xs={8} className={classes.backgroundPaper}>
                      <Typography variant="h2">{cardData.word}</Typography>
                      <Divider variant="middle" />
                      <Typography variant="h5">{cardData.description}</Typography>
                    </Grid>
                    <Grid item xs={2} className={classes.backgroundPaper}>
                      <Grid container justify="center" direction="column" alignContent="center">
                        <Grid item>
                          <Box>
                            <ToggleButton
                              title={cardData.isFavorite ? 'Remove star' : 'Give star'}
                              onClick={() => {
                                const model = {
                                  userWordId: cardData.userWordId,
                                  isFavorite: !cardData.isFavorite,
                                };
                                wordMeisterService.setWordFavorite(model).then((response) => {
                                  if (response && response.error === false) {
                                    setCardData({
                                      ...cardData,
                                      isFavorite: model.isFavorite
                                    });
                                  } else {
                                    ToasterSnackbar.error({ message: response.errorMessage || 'An error occured' });
                                  }
                                });
                              }}
                            >
                              <StarIcon htmlColor={cardData.isFavorite && goldColor} className={classes.starIcon} />
                            </ToggleButton>
                          </Box>
                        </Grid>
                        {/* <Grid item>
                          <Box>
                            <Typography>{cardData.point !== 0 ? `${cardData.point}/10` : '-'}</Typography>
                          </Box>
                        </Grid> */}
                      </Grid>
                    </Grid>
                  </Grid>
                </Grid>
                <Grid item xs={2} />
              </Grid>
            </Grid>
            <Grid item xs={12}>
              <Grid container justify="center" direction="row" spacing={2}>
                <Grid item xs={2}>
                  <Grid
                    container
                    direction="row"
                    justify="flex-end"
                    alignItems="center"
                  >
                    <Box>
                      <Button
                        type="button"
                        disabled={cardData.currentIndex < 1}
                        onClick={() => getData(cardData.currentIndex - 1 > 0 ? cardData.currentIndex - 1 : 1)}
                      >
                        <ArrowBackIosIcon />
                      </Button>
                    </Box>
                  </Grid>
                </Grid>
                <Grid item xs={8}>
                  <Grid container>
                    <List className={classes.listRoot}>

                      {
                        cardData.isOver ? (
                          <ListItem>
                            <ListItemText>
                              <Typography variant="h1" className={classes.typography}>No data to show.</Typography>
                            </ListItemText>
                          </ListItem>
                        ) : (
                            <>
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
                            </>
                          )
                      }
                      <ListItem>
                        <Grid container direction="row">
                          <Grid item xs={4} />
                          <Grid item xs={4}>
                            <Grid container justify="center">
                              <Fab color="primary" aria-label="add" title="Add new custom sentence">
                                <AddIcon />
                              </Fab>
                            </Grid>
                          </Grid>
                          <Grid item xs={4} />
                        </Grid>
                      </ListItem>
                    </List>
                  </Grid>
                </Grid>
                <Grid item xs={2}>
                  <Grid
                    container
                    direction="row"
                    justify="flex-start"
                    alignItems="center"
                  >
                    <Box>
                      <Button
                        type="button"
                        disabled={cardData.currentIndex >= cardData.wordCount}
                        onClick={() => getData(cardData.currentIndex + 1)}
                      >
                        <ArrowForwardIosIcon />
                      </Button>
                    </Box>
                  </Grid>
                </Grid>
              </Grid>
            </Grid>
          </Grid>
        </div>
      </Page>
    </>
  );
};

export default WordCard;
