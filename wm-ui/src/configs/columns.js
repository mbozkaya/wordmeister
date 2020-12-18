const width = { width: '200px' };

// eslint-disable-next-line import/no-mutable-exports
const columns = {
  register: [
    { field: 'title', type: 'string', headerName: 'Title' },
    {
      field: 'createdUserName', type: 'string', headerName: 'User Name', description: 'Created User Name'
    },
    {
      field: 'createdDate', type: 'date', headerName: 'Created Date'
    },
  ],
  register2: [
    {
      id: 'title', numeric: false, disablePadding: true, label: 'Title', description: 'Title'
    },
    {
      id: 'createdUserName', numeric: false, disablePadding: false, label: 'User Name', description: 'Created User Name'
    },
    {
      id: 'createdDate', numeric: false, disablePadding: false, label: 'Created Date', description: 'Created Date'
    },
    // {
    //   id: 'answerCount', numeric: true, disablePadding: false, label: 'Answer Count', description: 'Answer Count',
    // },
  ],
};

Object.values(columns)
  .map((table) => table
    .map((colName) => !colName.width && !colName.flex && Object.assign(colName, width)));
export default columns;
