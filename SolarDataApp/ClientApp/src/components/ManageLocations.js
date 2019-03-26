import React, { Component } from 'react';

export class ManageLocations extends Component {
  static displayName = ManageLocations.name;

  constructor (props) {
    super(props);
    this.state = { locations: [], loading: true };

    fetch('api/Location')
      .then(response => response.json())
        .then(data => {
          console.log(data);
        this.setState({ locations: data, loading: false });
      });
  }

  static renderLocationsTable (locations) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>&nbsp;</th>
          </tr>
        </thead>
        <tbody>
          {locations.map(location =>
                    <tr key={location.id}>
                        <td>{location.id}</td>
                        <td>{location.name}</td>
                        <td></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : ManageLocations.renderLocationsTable(this.state.locations);

    return (
      <div>
        <h1>Locations</h1>
        <p>These are the locations in the app.</p>
        {contents}
      </div>
    );
  }
}
