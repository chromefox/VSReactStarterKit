declare var global: Global;
var $ = require('jquery');
var L = require('leaflet');
var React = require('react');
var ReactDOM = require('react-dom');

// setting default path for leaflet
L.Icon.Default.imagePath = '/LeafletImages';

global.$ = $;
global.L = L;

export class Map {
    map;
    marker;
    circle;
    polygon;
    popup;

    constructor() {}

    addNewMarker(globalMap) {
        var markerContent = "<div>New Popup</div>";
        var marker = L.marker([51.55, -0.13]).addTo(globalMap);
        marker.bindPopup(markerContent).openPopup();
    }

    addMarker() {
        this.marker = L.marker([51.5, -0.09]).addTo(this.map);
    }

    // describe how to add shapes into the map
    addCircle() {
        this.circle = L.circle([51.508, -0.11], 500, {
            color: 'red',
            fillColor: '#f03',
            fillOpacity: 0.5
        }).addTo(this.map);
    }

    addPolygon() {
        this.polygon = L.polygon([
            [51.509, -0.08],
            [51.503, -0.06],
            [51.51, -0.047]
        ]).addTo(this.map);
    }

    bindBasicPopupToMarkers() {
        this.marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();
        this.circle.bindPopup("I am a circle.");
        this.polygon.bindPopup("I am a polygon.");
    }

    // Automatically add a popup at the specified lat long. Will disappear upon clicking.
    addStandalonePopup = () => {
        this.popup = L.popup()
            .setLatLng([51.49, -0.09])
            .setContent("I am a standalone popup.")
            .openOn(this.map);
    }

    onMapClick = (e) => {
        this.popup
            .setLatLng(e.latlng)
            .setContent("You clicked the map at " + e.latlng.toString())
            .openOn(this.map);
    }

    addOnMapClickEvent() {
        this.map.on('click', this.onMapClick);
    }

    // Researched functions
    addMultipleMarkersWithAutoPopup() {
        var markerContent = "<div class='map-popup'> <img src='http://orig08.deviantart.net/847f/f/2012/177/e/e/hello____mine_turtle_by_harrisonb32-d54we3i.jpg' width='100' height '100' /> <p>Haha</p> <input type='button' value='Click' /> </div>";

        var marker = L.marker([51.52, -0.10]).addTo(this.map);
        var marker1 = L.marker([51.55, -0.09213]).addTo(this.map);
        var marker2 = L.marker([51.58, -0.09315]).addTo(this.map);

        marker.bindPopup(markerContent).openPopup(); // automatically open the popup. Only one popup can be opened at any point of time.
        marker1.bindPopup(markerContent);
        marker2.bindPopup(markerContent);

    }

    handleEventsOnPopup = () => {
        // binding on dynamically generated HTML/content.
        $("body").on('click', '.map-popup input', function (ev) {
            //var LeftContainerDetails = React.createClass({
            //    render() {
            //        return (
            //            <div>
            //        <h2>Place {new Date() }</h2>
            //        <div className="section">
            //            Random number: {Math.random() }
            //            </div>
            //                </div>
            //        );
            //    }
            //});

            //ReactDOM.render(
            //    <LeftContainerDetails />,
            //    document.getElementById("markerDetail")
            //);
            global.leftClass.handleClick(Math.random());
        });
    }

    handleSubmissionsOnPopup() {
        // try an AJAX call
    }

    // have a special icon for markers
    addCustomMarkerIcon() {
        // add an image and border for the marker button yet.
    }

    addHoverEvent() {
        // attempt to do something on marker hover. (open popup for example)
    }

    dynamicallyRemapMarkers() {
        // click button

        // delete all markers and re-add new ones.
    }

    mainInit() {
        // initializing the map with a set lat/long
        this.map = L.map('map');
        this.map.setView([51.505, -0.09], 13);

        // using mapbox's tile API to provide for the look/style of tiles
        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
            attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
            maxZoom: 18,
            id: 'ronnypm.oe842285',
            accessToken: 'pk.eyJ1Ijoicm9ubnlwbSIsImEiOiJjaWk2c2dmbTEwMXVodGZrcXo4N2NicjdsIn0.FfllDwkgKRb100f65itaRA'
        }).addTo(this.map);

        this.addMarker();
        this.addCircle();
        this.addPolygon();
        this.bindBasicPopupToMarkers();
        this.addStandalonePopup();
        this.addOnMapClickEvent();

        // prototype functions
        this.addMultipleMarkersWithAutoPopup();
        this.handleEventsOnPopup();
        global.map = this.map;
        global.mapClass = this;
    }
}