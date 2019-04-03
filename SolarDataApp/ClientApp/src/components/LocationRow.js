import React, { Component } from 'react';

export class LocationRow extends Component {

    constructor( props) {
        super(props);
        this.state = props.location;
    }

    handleEditClick( location) {
      console.log('Editing click');
      console.log( location);
      this.props.onEditClick(location);
    }

    render() {
        if ( !this.state) {
            console.log('No state exists for this row ' + this.state);
            return null;
        }
        return (
            <tr key={this.state.id}>
                <td>{this.state.id}</td>
                <td>{this.state.name}</td>
                <td><a onClick={() => this.props.onEditClick(this.state)}>edit</a></td>
            </tr>
        );
    }
}