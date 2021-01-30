(function () {
    var tileUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    var tileAttribution = 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>';

    var lat =0.0, lng=0.0;
    var dotNetObjectRef;
    var blueIcon = new L.Icon({
        iconUrl: 'images/marker-icon-2x-blue.png',
        shadowUrl: 'images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var goldIcon = new L.Icon({
        iconUrl: 'images/marker-icon-2x-gold.png',
        shadowUrl: 'images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var redIcon = new L.Icon({
        iconUrl: 'https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var greenIcon = new L.Icon({
        iconUrl: 'https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var orangeIcon = new L.Icon({
        iconUrl: 'images/marker-icon-2x-orange.png',
        shadowUrl: 'images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var yellowIcon = new L.Icon({
        iconUrl: 'images/marker-icon-2x-yellow.png',
        shadowUrl: 'images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var violetIcon = new L.Icon({
        iconUrl: 'images/marker-icon-2x-violet.png',
        shadowUrl: 'images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var greyIcon = new L.Icon({
        iconUrl: 'images/marker-icon-2x-grey.png',
        shadowUrl: 'images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    var blackIcon = new L.Icon({
        iconUrl: 'images/marker-icon-2x-black.png',
        shadowUrl: 'images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    // Global export
    window.deliveryMap = {
        showOrUpdate: function (elementId, markers,canAddNewMarkers,dotNetObjectReference) {
            var elem = document.getElementById(elementId);
            if (!elem) {
                throw new Error('No element with ID ' + elementId);
            }

            // Initialize map if needed
            if (!elem.map) {
                elem.map = L.map(elementId);
                elem.map.addedMarkers = [];
                L.tileLayer(tileUrl, { attribution: tileAttribution, noWrap: true, bounds: [[-90, -180], [90, 180]]  }).addTo(elem.map);
            }
            dotNetObjectRef = dotNetObjectReference;
            var map = elem.map;
            function onEachFeature(feature, layer) {
                //bind click
                layer.on('click', function (e) {
                    // e = event
                    console.log(e);
                    alert("Click!");
                    // You can make your ajax call declaration here
                    //$.ajax(... 
                });

            }
            
            if (map.addedMarkers.length == 0 && markers.length == 0)
            {
                map.fitWorld();
                if (canAddNewMarkers) {

                    map.addEventListener('mousemove', function (ev) {
                        lat = ev.latlng.lat;
                        lng = ev.latlng.lng;
                    });
                    map.on('contextmenu',
                        rightClick);
                }
            }
            else if ((map.addedMarkers.length !== markers.length) && markers.length != 0) {
                // Markers have changed, so reset
                map.addedMarkers.forEach(marker => marker.removeFrom(map));
                map.addedMarkers = markers.map(m => {
                    //return L.marker([m.y, m.x], { icon: getMarkerIcon(L, "red") }).addTo(map)
                    var color = 'green';
                    var icon = greenIcon;
                    if (m.isNegative) {
                        color = 'red';
                        var icon = redIcon;
                    }
                        var opacity = m.certainty;
                    if (opacity <= 0) {
                        opacity = 1;
                    }
                    else if (opacity >= 100) {
                        opacity = 100;
                    }
                    opacity = opacity / 200;
                    L.circle([m.y, m.x], {
                        //color: 'red',

                        color: color,
                        fillColor: color,
                        fillOpacity: opacity,
                        radius: m.radiusMeters
                    }).bindPopup(m.description).addTo(map);
                    return L.marker([m.y, m.x], { icon : icon }).bindPopup(m.description).addTo(map);
                });

                // Auto-fit the view
                var markersGroup = new L.featureGroup(map.addedMarkers);
                map.fitBounds(markersGroup.getBounds().pad(0.3));

                // Show applicable popups. Can't do this until after the view was auto-fitted.
                markers.forEach((marker, index) => {
                    if (marker.showPopup) {
                        map.addedMarkers[index].openPopup();
                    }
                });
            } else {
                // Same number of markers, so update positions/text without changing view bounds
                markers.forEach((marker, index) => {
                    animateMarkerMove(
                        map.addedMarkers[index].setPopupContent(marker.description),
                        marker,
                        4000);
                });
            }
        }
    };


    function getMarkerIcon(L, color) {
        return L.Icon({
            //iconRetinaUrl: require('@/assets/images/map_markers/marker-icon-2x-' + color + '.png'),
            //iconUrl: require('@/assets/images/map_markers/marker-icon-' + color + '.png'),
            //shadowUrl: require('@/assets/images/map_markers/marker-shadow.png'),
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41]
        })
    }
    function rightClick(
        //dotNetObjectReference
    )
    {
        dotNetObjectRef.invokeMethodAsync("AddMarker", lat, lng);
        //alert("Click! Latitude: " + lat + ", Longitude: " + lng);
    }
    function animateMarkerMove(marker, coords, durationMs) {
        if (marker.existingAnimation) {
            cancelAnimationFrame(marker.existingAnimation.callbackHandle);
        }

        marker.existingAnimation = {
            startTime: new Date(),
            durationMs: durationMs,
            startCoords: { x: marker.getLatLng().lng, y: marker.getLatLng().lat },
            endCoords: coords,
            callbackHandle: window.requestAnimationFrame(() => animateMarkerMoveFrame(marker))
        };
    }

    function animateMarkerMoveFrame(marker) {
        var anim = marker.existingAnimation;
        var proportionCompleted = (new Date().valueOf() - anim.startTime.valueOf()) / anim.durationMs;
        var coordsNow = {
            x: anim.startCoords.x + (anim.endCoords.x - anim.startCoords.x) * proportionCompleted,
            y: anim.startCoords.y + (anim.endCoords.y - anim.startCoords.y) * proportionCompleted
        };

        marker.setLatLng([coordsNow.y, coordsNow.x]);

        if (proportionCompleted < 1) {
            marker.existingAnimation.callbackHandle = window.requestAnimationFrame(
                () => animateMarkerMoveFrame(marker));
        }
    }
})();
