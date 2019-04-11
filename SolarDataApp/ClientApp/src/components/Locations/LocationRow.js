import React, { Component } from 'react';

export class LocationRow extends Component {

    constructor( props) {
        super(props);
    }

    render() {
        if ( !this.props.location) {
            console.log('No state exists for this row ', this.props.location);
            return null;
        }
        return (
            <tr key={this.props.location.id}>
                <td>{this.props.location.id}</td>
                <td>{this.props.location.name}</td>
                <td>
                    <a onClick={() => this.props.onEditClick(this.props.location)} style={{cursor: 'pointer'}}>edit</a> &nbsp;&nbsp;|&nbsp;&nbsp;
                    <a onClick={() => this.props.onDeleteClick(this.props.location)} style={{cursor: 'pointer'}}>delete</a>
                </td>
            </tr>
        );
    }
}