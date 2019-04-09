import React from 'react';

export class LocationEdit extends React.Component {
    constructor( props) {
        super(props);
        console.log('Location Edit');
        console.log( props);
        this.state = {id: props.location.id, name: props.location.name};
        this.handleLocationChange = this.handleLocationChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleLocationChange(e) {
        this.setState({ name: e.target.value });
    }
    handleSubmit(e) {
        e.preventDefault();
        const name = this.state.name.trim();
        const id = this.state.id;
        if (!name ) {
            return;
        }
        // TODO: send request to the server
        console.log('updating location id: ' + id);
        this.props.onLocationEditSubmit({ Id: id, Name: name });
        // reset state to clear form
        this.setState({ id: 0, name: '' });
    }
    render() {
        return ( 
            <div id="editpanel">
            <form className="LocationEdit" onSubmit={this.handleSubmit}>
                <h2>Location Details</h2>
                <label>Location&nbsp;Name&nbsp;&nbsp;
                    <input type="text" 
                        id="locationName" 
                        onChange={this.handleLocationChange} 
                        value={this.state.name} 
                        maxLength="100" 
                        placeholder=" Location Name" />
                </label>
                &nbsp;&nbsp;&nbsp;
                <input type="submit" value="Save"/> {this.state.name}
            </form>
            </div>
        );
    }
}