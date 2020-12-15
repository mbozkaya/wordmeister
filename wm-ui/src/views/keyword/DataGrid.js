import * as React from 'react';
import { DataGrid as DG } from '@material-ui/data-grid';
import PropTypes from 'prop-types';

const DataGrid = (props) => {
  const { data } = props;
  return (
    <div style={{ height: 300, width: '100%' }}>
      <DG
        columns={[
          { field: 'title', type: 'string' },
          { field: 'createdUser', type: 'string' },
          { field: 'createdDate', type: 'date', width: 130 },
        ]}
        rows={data}
      />
    </div>
  );
};

DataGrid.propTypes = {
  data: PropTypes.array,
};

DataGrid.DefaultPropTypes = {
  data: [],
};

export default DataGrid;
