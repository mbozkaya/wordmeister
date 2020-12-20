import 'react-perfect-scrollbar/dist/css/styles.css';
import React from 'react';
import { useRoutes } from 'react-router-dom';
import { ThemeProvider } from '@material-ui/core';
import GlobalStyles from 'src/components/GlobalStyles';
import 'src/mixins/chartjs';
import theme from 'src/theme';
import routes from 'src/routes';
import { AuthProvider } from './contexts/authContext';
import Blockui from './components/Blockui';

const App = () => {
  const routing = useRoutes(routes);

  return (
    <AuthProvider>
      <ThemeProvider theme={theme}>
        <GlobalStyles />
        {routing}
      </ThemeProvider>
      <Blockui />
    </AuthProvider>
  );
};

export default App;
