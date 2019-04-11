import React, { Component } from 'react';
import { LocationEdit } from './LocationEdit';
import { LocationRow } from './LocationRow';

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
    // this.handleEditClick = this.handleEditClick.bind(this);
    this.state = { locations: [], loading: true, editing: false, locationEditing: null };
  }
  componentDidMount() {
    this.loadLocationsFromServer();
  }

  loadLocationsFromServer() {
    console.log( "get url: " + this.url);
    this.setState({ locations: this.state.locations, loading: true, editing: this.state.editing, locationEditing: this.state.locationEditing });
    fetch( this.url)
      .then(response => response.json())
        .then(data => {
          console.log(data);
          this.setState({ locations: data, loading: false, editing: this.state.editing, locationEditing: this.state.locationEditing });
          console.log( "editing " + this.state.editing);
          });
  }

  handleNewLocationClick() {
    console.log("New Location Click");
    console.log( "editing " + this.state.editing);
    this.setState({ locations: this.state.locations, loading: this.state.loading, editing: true, locationEditing: { id: 0, name: 'New Location'} });
    console.log( "editing " + this.state.editing);
  }

  handleEditClick( location) {
    console.log('Editing click ManageLocations component');
    console.log( location);
    this.setState({ locations: this.state.locations, loading: this.state.loading, editing: true, locationEditing: location });
  }

  handleDeleteClick( location) {
    console.log('Delete Location ', location);
    const requestOptions = { method: 'DELETE' };
    fetch( this.url + '/' + location.id, requestOptions)
        .then(data => {
          console.log(data);
          this.loadLocationsFromServer();
          });
  }

  handleLocationEditSubmit(location) {
    // TODO: submit to the server and refresh the list
    //const data = new FormData();
    //data.append('Id', 0);
    //data.append('Name', location.Name);

    //const xhr = new XMLHttpRequest();
    //xhr.open('post', this.submitUrl, true);
    //xhr.onload = () => this.loadLocationsFromServer();
    //xhr.send(data);
    console.log( 'saving location... ', location);

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
        console.log( 'submit edit location response: ', data);
        console.log(data);
        this.setState({ locations: this.state.locations, loading: this.state.loading, editing: false, locationEditing: null });
        this.loadLocationsFromServer();
        });
  }

  renderLocationsTable (locations) {
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
            <LocationRow key={location.id} location={location} 
                onEditClick={() => this.handleEditClick( location)} 
                onDeleteClick={() => this.handleDeleteClick( location)} />
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : this.renderLocationsTable(this.state.locations);

    let editcontents = this.state.editing
      ? <LocationEdit onLocationEditSubmit={this.handleLocationEditSubmit} location={this.state.locationEditing} />
        : <span></span>

    return (
      <div>
        <h1>Locations</h1>
        <p>These are the locations in the app.</p>
        {contents}
        <input type="button" onClick={this.handleNewLocationClick} value="+" />
        {editcontents}
      </div>
    );
  }
}
