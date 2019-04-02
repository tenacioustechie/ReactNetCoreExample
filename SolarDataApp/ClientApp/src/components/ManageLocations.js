import React, { Component } from 'react';
import { LocationEdit } from './LocationEdit';

export class ManageLocations extends Component {
  static displayName = ManageLocations.name;
  // TODO: don't hard code this
  url = "/api/Location";
  submitUrl = "/api/Location";
  showLocationEdit = false;

  constructor (props) {
    super(props);
    this.handleLocationEditSubmit = this.handleLocationEditSubmit.bind(this);
    this.handleNewLocationClick = this.handleNewLocationClick.bind(this);
    this.state = { locations: [], loading: true };
  }
  componentDidMount() {
    this.loadLocationsFromServer();
  }

  loadLocationsFromServer() {
    console.log( "get url: " + this.url);
    fetch( this.url)
      .then(response => response.json())
        .then(data => {
          console.log(data);
          this.setState({ locations: data, loading: false });
          });
  }

  handleNewLocationClick() {
    console.log("New Location Click");
  }

  handleLocationEditSubmit(location) {
    // TODO: submit to the server and refresh the list
    const data = new FormData();
    data.append('Id', 0);
    data.append('Name', location.Name);

    const xhr = new XMLHttpRequest();
    xhr.open('post', this.submitUrl, true);
    xhr.onload = () => this.loadLocationsFromServer();
    xhr.send(data);

    fetch( this.submitUrl, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify( location)
      })
      .then(response => response.json())
      .then(data => {
        console.log(data);
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
        <input type="button" onClick={this.handleNewLocationClick} value="+" />
        <LocationEdit onLocationEditSubmit={this.handleLocationEditSubmit} hidden={!this.showLocationEdit} />
      </div>
    );
  }
}
