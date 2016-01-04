import mapSection = require("./components/map-section");

//var React = require('react');
//var ReactDOM = require('react-dom');

//import container = require("./components/container");

class ReactProtoMain {
    constructor() {
        var map = new mapSection.Map();
        map.mainInit();
    }
}

// For understanding sake, use pure JS to code the container for now
declare var global: Global;

var ReactDOM = require('react-dom');
var React = require('react');

var MapComponent = React.createClass({
    render() {
        return (
            <div id="map" className= "col-lg-6" >
                </div>
        );
    }
});

var LeftComponent = React.createClass({
    getInitialState() {
        return { title: "N/A" };
    },
    handleClick(titleStr) {
        this.setState({ title: titleStr });
    },
    addMapMarker() {
        global.mapClass.addNewMarker(global.map);
    },
    render() {
        return (
            <div id="left-section" className="col-lg-6">
                        <h3>Create a new marker</h3>
                        <button id="newMarkerDemo" className="button" onClick={this.addMapMarker} >Click</button>
                        <h3>Details section</h3>
                        <div id="markerDetail">{this.state.title}</div>
                </div>
        );
    },
    componentDidMount() {
        console.log("Left component mounted... Registering change state function to global variable");
        global.updateDetailFunction = this.handleClick;
    }
});

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


ReactDOM.render(
    <Container />,
    document.getElementById("placeholder")
);

new ReactProtoMain();