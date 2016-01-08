import React, { Component } from 'react';
import MainSectionJS from '../components/MainSectionJS'
import MainSectionTSX from '../components/MainSectionTSX'

export default class App extends Component {
    render() {
        return ( 
          <div>
            <h3>
              Hello React Labs!
            </h3>
            <MainSectionJS /> 
            <MainSectionTSX /> 
          </div> 
        );
    }
}
  