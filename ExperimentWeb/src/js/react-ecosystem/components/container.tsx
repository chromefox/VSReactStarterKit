declare var global: Global;

import mapClass = require("./map-section");
import leftClass = require("./left-section");
var ReactDOM = require('react-dom');
var React = require('react');

var MapComponent = React.createClass({
    render() {
        return (
            <div id="map" className="col-lg-6">
            </div>
        );
    }
});

var LeftComponent = (new leftClass.LeftSection()).getComponent();
global.leftClass = LeftComponent;

var Container = React.createClass({
    render() {
        return (
            <div className="container">
                <LeftComponent />
                <MapComponent />
            </div>
        );
    }
});

export function render() {
    ReactDOM.render(
        <Container />,
        document.getElementById("placeholder")
    );
}