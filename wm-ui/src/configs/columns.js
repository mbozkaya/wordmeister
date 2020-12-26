const width = { width: '200px' };

// eslint-disable-next-line import/no-mutable-exports
const columns = {
  register: [
    {
      id: 'title', numeric: false, disablePadding: true, label: 'Title', description: 'Title'
    },
    {
      id: 'createdUserName', numeric: false, disablePadding: false, label: 'User Name', description: 'Created User Name'
    },
    {
      id: 'createdDate', numeric: false, disablePadding: false, label: 'Created Date', description: 'Created Date', date: true
    },
    {
      id: 'action', numeric: false, disablePadding: false, label: 'Action', description: 'Action'
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
