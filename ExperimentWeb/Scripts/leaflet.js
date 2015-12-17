(function ($) {
    var map, marker, circle, polygon, popup;
    function addMarker() {
        marker = L.marker([51.5, -0.09]).addTo(map);
    }

    // describe how to add shapes into the map
    function addCircle() {
        circle = L.circle([51.508, -0.11], 500, {
            color: 'red',
            fillColor: '#f03',
            fillOpacity: 0.5
        }).addTo(map);
    }

    function addPolygon() {
        polygon = L.polygon([
            [51.509, -0.08],
            [51.503, -0.06],
            [51.51, -0.047]
        ]).addTo(map);
    }

    function bindBasicPopupToMarkers() {
        marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();
        circle.bindPopup("I am a circle.");
        polygon.bindPopup("I am a polygon.");
    }

    // Automatically add a popup at the specified lat long. Will disappear upon clicking.
    function addStandalonePopup() {
        popup = L.popup()
        .setLatLng([51.49, -0.09])
        .setContent("I am a standalone popup.")
        .openOn(map);
    }

    function onMapClick(e) {
        popup
          .setLatLng(e.latlng)
          .setContent("You clicked the map at " + e.latlng.toString())
          .openOn(map);
    }

    function addOnMapClickEvent() {
        map.on('click', onMapClick);
    }

    // Researched functions
    function addMultipleMarkersWithAutoPopup() {
        var markerContent = "<div class='map-popup'> <img src='http://orig08.deviantart.net/847f/f/2012/177/e/e/hello____mine_turtle_by_harrisonb32-d54we3i.jpg' width='100' height '100' /> <p>Haha</p> <input type='button' value='Click' /> </div>";

        var marker = L.marker([51.52, -0.10]).addTo(map);
        var marker1 = L.marker([51.55, -0.09213]).addTo(map);
        var marker2 = L.marker([51.58, -0.09315]).addTo(map);

        marker.bindPopup(markerContent).openPopup(); // automatically open the popup. Only one popup can be opened at any point of time.
        marker1.bindPopup(markerContent);
        marker2.bindPopup(markerContent);
        
    }

    function handleEventsOnPopup() {
        // binding on dynamically generated HTML/content.
        $("body").on('click', '.map-popup input', function (ev) {
            alert("A");
        });
    }

    function handleSubmissionsOnPopup() {
        // try an AJAX call
    }

    // have a special icon for markers
    function addCustomMarkerIcon() {
        // add an image and border for the marker button yet.
    }

    function addHoverEvent() {
        // attempt to do something on marker hover. (open popup for example)
    }

    function dynamicallyRemapMarkers() {
        // click button

        // delete all markers and re-add new ones.
    }

    function mainInit() {
        // initializing the map with a set lat/long
        map = L.map('map').setView([51.505, -0.09], 13);

        // using mapbox's tile API to provide for the look/style of tiles
        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
            attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
            maxZoom: 18,
            id: 'ronnypm.oe842285',
            accessToken: 'pk.eyJ1Ijoicm9ubnlwbSIsImEiOiJjaWk2c2dmbTEwMXVodGZrcXo4N2NicjdsIn0.FfllDwkgKRb100f65itaRA'
        }).addTo(map);

        addMarker();
        addCircle();
        addPolygon();
        bindBasicPopupToMarkers();
        addStandalonePopup();
        addOnMapClickEvent();

        // prototype functions
        addMultipleMarkersWithAutoPopup();
        handleEventsOnPopup();
    }

    $.fn.LeafletProto = function () {
        return {
            MainInit: mainInit
        }
    }
}(jQuery));