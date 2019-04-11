import React, { Component } from 'react';

export class ReadingRow extends Component {

    constructor( props) {
        super(props);
        this.state = { reading: props.reading};
    }

    handleEditClick( reading) {
      console.log('Editing reading click');
      console.log( reading);
      this.props.onEditClick(reading);
    }

    render() {
        if ( !this.state.reading) {
            console.log('No state exists for this row ' + this.state.reading);
            return null;
        }
        return (
            <tr key={this.state.reading.id}>
                <td>{(new Date(this.state.reading.day)).toLocaleDateString()}</td>
                <td>{this.state.reading.powerUsed} kWh</td>
                <td>{this.state.reading.solarGenerated} kWh</td>
                <td>
                    <button onClick={() => this.props.onEditClick(this.state.reading)} style={{cursor: 'pointer'}}>edit</button> &nbsp;&nbsp;
                    <button onClick={() => this.props.onDeleteClick(this.state.reading)} style={{cursor: 'pointer'}}>delete</button>
                </td>
            </tr>
        );
    }
}