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
    }

    $.fn.LeafletProto = function () {
        return {
            MainInit: mainInit
        }
    }
}(jQuery));