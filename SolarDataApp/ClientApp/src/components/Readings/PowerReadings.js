import React, { Component } from 'react';
import { ReadingRow } from './ReadingRow';

export class PowerReadings extends Component {
  urlLocations = "/api/Location"
  url = "/api/Reading";
  submitUrl = "/api/Reading";
  showLocationEdit = false;

    constructor( props) {
        super(props);
        this.state = { 
            locations: [], 
            loadingLocations: true, 
            locationId: null,
            loadingReadings: true, 
            readings: [],
            editing: false,
            reading: null
        };
    }
    componentDidMount() {
        this.loadLocationsFromServer();
    }
    loadLocationsFromServer() {
        this.setState( prevState => ({
            ...prevState,
            loadingLocations: true
        }));
        // get the locations from the server
        fetch( this.urlLocations)
          .then(response => response.json())
            .then(data => {
            console.log(data);
            this.setState(prevState => ({ ...prevState, loadingLocations: false,  locations: data }));
            });
    }
    loadReadingsFromServer() {
        this.setState( prevState => ({
            ...prevState,
            loadingReadings: true
        }));
        console.log("loading readings from server");
        // get the locations from the server
        if ( this.state.locationId > 0) {
          fetch( this.url + '/' + this.state.locationId)
            .then(response => response.json())
              .then(data => {
                console.log("loaded readings", data);
                this.setState(prevState => ({ ...prevState, loadingReadings: false,  readings: data }));
              });
        } else {
          console.log("location empty, can't load readings");
          this.setState(prevState => ({ ...prevState, loadingReadings: false,  readings: [] }));
        }
    }
    updateLocation(newValue) {
      console.log("updating location...", newValue);
      this.setState(prevState => ({
        ...prevState,
        locationId: newValue
      }), () => {
        // NOTE: as setState is asyncronous, this callback occurs after it has updated state
        this.loadReadingsFromServer();
      });
    }

    handleEditClick( reading) {
      console.log('Editing reading click', reading);
      this.setState(prevState => ({
        editing: true,
        reading: reading
      }));
    }
    handleDeleteClick( reading) {
      console.log('Delete reading click', reading);
      // TODO: Make API call to delete reading
      this.setState(prevState => ({
        editing: false,
        reading: null
      }));
    }
    onSaveClick() {
      console.log('On Save Click');
      // TODO: Make save API call
      // TODO: update state
      this.setState(prevState => ({
        editing: false,
        reading: null
      }));
    }
    onCancelClick() {
      console.log('On Cancel Click');
      this.setState( prevState => ({
        editing: false,
        reading: null
      }));
    }
    onNewClick() {
      console.log('On New Click');
      this.setState( prevState => ({
        editing: true,
        reading: { 
          Id: 0,
          locationId: this.state.locationId,
          day: new Date(),
          powerUsed: 0,
          solarGenerated: 0
        }
      }));
    }

    renderAddRow() {
      if ( this.state.editing ) {
        return (
          <tr>
            <th><input type="date" defaultValue={new Date(this.state.reading.day).toISOString().split("T")[0]} /></th>
            <th><input type="number" defaultValue={this.state.reading.powerUsed} /> kWh</th>
            <th><input type="number" defaultValue={this.state.reading.solarGenerated} /> kWh</th>
            <th>
                <button onClick={() => this.onSaveClick()} style={{cursor: 'pointer'}}>save</button> &nbsp;&nbsp;
                <button onClick={() => this.onCancelClick()} style={{cursor: 'pointer'}}>cancel</button>
            </th>
          </tr>
        );
      } else {
        return (
          <tr>
                <th><button onClick={() => this.onNewClick()} style={{cursor: 'pointer'}}>new</button></th>
                <th> </th>
                <th> </th>
                <th> </th>
              </tr>
        );
      }
    }
    renderReadingRows() {
      if ( !this.state.readings || this.state.readings.length === 0) {
        return (<tr><td colSpan="4" className="small">No Readings</td></tr>);
      } else {
        return this.state.readings.map(reading =>
          <ReadingRow key={reading.id} reading={reading} 
              onEditClick={() => this.handleEditClick( reading)} 
              onDeleteClick={() => this.handleDeleteClick( reading)} />
          )
      }
    }

    render() {
        if ( !this.state) {
            console.log('No state exists for this row ' + this.state);
            return null;
        }
        return (
          <div>
          <div id="locationSelection">
            <label>Location</label>
            <select id="locationSelect" onChange={(event) => this.updateLocation(event.target.value)}
              defaultValue={this.state.locationId}>
              <option value=""></option>
              {this.state.locations.map(location =>
                <option key={location.id} value={location.id}>{location.name}</option>
              )}
            </select>
          </div>
          <table className='table table-striped'>
            <thead>
              <tr>
                <th>Date</th>
                <th>Power Used</th>
                <th>Solar Generated</th>
                <th>&nbsp;</th>
              </tr>
            </thead>
            <tbody>
              { this.renderReadingRows() }
              { this.renderAddRow() }
            </tbody>
          </table>
          </div>
        );
    }
}