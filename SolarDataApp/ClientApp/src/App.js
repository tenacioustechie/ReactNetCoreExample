import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { ManageLocations } from './components/Locations/ManageLocations'
import { PowerReadings } from './components/Readings/PowerReadings';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/readings' component={PowerReadings} />
        <Route path='/locations' component={ManageLocations} />
      </Layout>
    );
  }
}
