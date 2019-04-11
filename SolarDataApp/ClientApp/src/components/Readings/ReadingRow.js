import React, { Component } from 'react';

export class ReadingRow extends Component {

    constructor( props) {
        super(props);
    }

    render() {
        if ( !this.props.reading) {
            console.log('No state exists for this row ' + this.props.reading);
            return null;
        }
        return (
            <tr key={this.props.reading.id}>
                <td>{this.props.reading.day.split("T")[0]}</td>
                <td>{this.props.reading.powerUsed} kWh</td>
                <td>{this.props.reading.solarGenerated} kWh</td>
                <td>
                    <button onClick={() => this.props.onEditClick(this.props.reading)} style={{cursor: 'pointer'}}>edit</button> &nbsp;&nbsp;
                    <button onClick={() => this.props.onDeleteClick(this.props.reading)} style={{cursor: 'pointer'}}>delete</button>
                </td>
            </tr>
        );
    }
}