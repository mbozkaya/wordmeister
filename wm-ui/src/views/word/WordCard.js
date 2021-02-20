import React from 'react';
import {
  Container,
  Grid,
  makeStyles,
  Card,
  CardContent,
  CardHeader,
  Divider,
  TextField,
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
                <Button>
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
                  subheader="The information can be edited"
                  title="Profile"
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
                      <ListItem alignItems="flex-start">
                        <ListItemAvatar>
                          <Typography variant="h1" className={classes.typography}>1</Typography>
                        </ListItemAvatar>
                        <ListItemText
                          primary="Brunch this weekend?"
                          secondary={(
                            <>
                              <Typography
                                component="span"
                                variant="body2"
                                className={classes.inline}
                                color="textPrimary"
                              >
                                Ali Connors
                              </Typography>
                              {" — I'll be in your neighborhood doing errands this…"}
                            </>
                                                    )}
                        />
                      </ListItem>
                      <Divider variant="inset" component="li" />
                      <ListItem alignItems="flex-start">
                        <ListItemAvatar>
                          <Typography variant="h1" className={classes.typography}>2</Typography>
                        </ListItemAvatar>
                        <ListItemText
                          primary="Summer BBQ"
                          secondary={(
                            <>
                              <Typography
                                component="span"
                                variant="body2"
                                className={classes.inline}
                                color="textPrimary"
                              >
                                to Scott, Alex, Jennifer
                              </Typography>
                              {" — Wish I could come, but I'm out of town this…"}
                            </>
                                                    )}
                        />
                      </ListItem>
                      <Divider variant="inset" component="li" />
                      <ListItem alignItems="flex-start">
                        <ListItemAvatar>
                          <Typography variant="h1" className={classes.typography}>3</Typography>
                        </ListItemAvatar>
                        <ListItemText
                          primary="Oui Oui"
                          secondary={(
                            <>
                              <Typography
                                component="span"
                                variant="body2"
                                className={classes.inline}
                                color="textPrimary"
                              >
                                Sandra Adams
                              </Typography>
                              {' — Do you have Paris recommendations? Have you ever…'}
                            </>
                                                    )}
                        />
                      </ListItem>
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
                <Button>
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
