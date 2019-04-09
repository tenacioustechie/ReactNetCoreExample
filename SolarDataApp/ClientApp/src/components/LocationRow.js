import React, { Component } from 'react';

export class LocationRow extends Component {

    constructor( props) {
        super(props);
        this.state = { location: props.location};
    }

    handleEditClick( location) {
      console.log('Editing click asdfasdf');
      console.log( location);
      this.props.onEditClick(location);
    }

    render() {
        if ( !this.state) {
            console.log('No state exists for this row ' + this.state);
            return null;
        }
        return (
            <tr key={this.state.location.id}>
                <td>{this.state.location.id}</td>
                <td>{this.state.location.name}</td>
                <td><a onClick={() => this.props.onEditClick(this.state)}>edit</a></td>
            </tr>
        );
    }
}