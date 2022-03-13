    let map, heatmap;

    function initMap() {
        map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: 40.7607, lng: 111.8730 },
            zoom: 14,
        });

        map.setOptions({
            styles: [
                {
                    "featureType": "poi",
                    "stylers": [
                        { "visibility": "off" }
                    ]
                }
            ]
        });


        let geocoder = new google.maps.Geocoder();

        getPoints(geocoder)
            .then(function (returnVals) {
                //console.log(returnVals);
                heatmap = new google.maps.visualization.HeatmapLayer({
                    data: returnVals,
                    map: map,
                    radius: 50,
                });
            });
            
           
        document
            .getElementById("toggle-heatmap")
            .addEventListener("click", toggleHeatmap);
        document
            .getElementById("change-gradient")
            .addEventListener("click", changeGradient);
        document
            .getElementById("change-opacity")
            .addEventListener("click", changeOpacity);
        document
            .getElementById("change-radius")
            .addEventListener("click", changeRadius);

        var pinColor = "#FFFFFF";

        var pinSVGHole = "M12,11.5A2.5,2.5 0 0,1 9.5,9A2.5,2.5 0 0,1 12,6.5A2.5,2.5 0 0,1 14.5,9A2.5,2.5 0 0,1 12,11.5M12,2A7,7 0 0,0 5,9C5,14.25 12,22 12,22C12,22 19,14.25 19,9A7,7 0 0,0 12,2Z";
        
        var markerImage = {  // https://developers.google.com/maps/documentation/javascript/reference/marker#MarkerLabel
            path: pinSVGHole,
            anchor: new google.maps.Point(12, 17),
            fillOpacity: 1,
            fillColor: pinColor,
            strokeWeight: 2,
            strokeColor: "green",
            scale: 2,
        };

        var url = "/Home/GetUserAddresses";
        $.get(url, null, function (data) {
        })
            .done(function (data) {
                address = data;
                console.log(address);
                for (let index = 0; index < address.length; ++index) {
                    if (localStorage && localStorage[address[index]]) {
                        var latlng = JSON.parse(localStorage[address[index]]);
                        map.setCenter(new google.maps.LatLng(latlng.lat, latlng.lng));
                        addMarkerCached(latlng, markerImage);
                        console.log("Added marker " + index + " from cache.");
                    } else {
                        geocoder.geocode({ 'address': address[index] }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                map.setCenter(results[0].geometry.location);
                                addMarker(results[0].geometry.location, markerImage);
                                localStorage[address[index]] = JSON.stringify(results[0].geometry.location);
                            }
                            else {
                                alert("Geocode was not successful for the following reason: " + status);
                            }
                        });
                        console.log("Added marker " + index + " not from cache.")
                    }
                }
            })
            .fail(function (data) {
                console.log("address retrieval failed");
            })
            .always(function (data) {
            });

        const image = "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";

        /*
        geocoder.geocode({ 'address': "460 E 400 S, Salt Lake City, UT" }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                map.setCenter(results[0].geometry.location);
                var marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location,
                    icon: image,
                    animation: google.maps.Animation.DROP
                });

                var infowindow = new google.maps.InfoWindow({
                    content: '<h3 style="font-family: arial; font-size: 18pt; color: green; text-align: center;">You</h3>Address: 460 E 400 S, Salt Lake City, UT<br/>Water usage: TODO<br/>Gas usage: TODO<br/>Electricity usage: TODO<br/>'
                });
                google.maps.event.addListener(marker, 'click', function () {
                    // Calling the open method of the infoWindow 
                    infowindow.open(map, marker);
                });

            }
            else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });*/
}

function addMarkerCached(location, markerImage) {
    var myLatlng = new google.maps.LatLng(location.lat, location.lng);

    var marker = new google.maps.Marker({
        map: map,
        position: myLatlng,
        animation: google.maps.Animation.DROP,
        icon: markerImage
    });

    var infowindow = new google.maps.InfoWindow({
        content: '<h3 style="font-family: arial; font-size: 18pt; color: green; text-align: center;">Username: TODO</h3>Address: ' + location + '<br/>Water usage: TODO<br/>Gas usage: TODO<br/>Electricity usage: TODO<br/>'
    });

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    });
}


function addMarker(location, markerImage) {
    //console.log("Entered addMarker");
    //console.log(location);

    var marker = new google.maps.Marker({
        map: map,
        position: location,
        animation: google.maps.Animation.DROP,
        icon: markerImage
    });

    //console.log("After setting marker");

    var infowindow = new google.maps.InfoWindow({
        content: '<h3 style="font-family: arial; font-size: 18pt; color: green; text-align: center;">Username: TODO</h3>Address: ' + location + '<br/>Water usage: TODO<br/>Gas usage: TODO<br/>Electricity usage: TODO<br/>'
    });

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    });
}

    function toggleHeatmap() {
        heatmap.setMap(heatmap.getMap() ? null : map);
    }

    function changeGradient() {
        const gradient = [
            "rgba(0, 255, 255, 0)",
            "rgba(0, 255, 255, 1)",
            "rgba(0, 191, 255, 1)",
            "rgba(0, 127, 255, 1)",
            "rgba(0, 63, 255, 1)",
            "rgba(0, 0, 255, 1)",
            "rgba(0, 0, 223, 1)",
            "rgba(0, 0, 191, 1)",
            "rgba(0, 0, 159, 1)",
            "rgba(0, 0, 127, 1)",
            "rgba(63, 0, 91, 1)",
            "rgba(127, 0, 63, 1)",
            "rgba(191, 0, 31, 1)",
            "rgba(255, 0, 0, 1)",
        ];

        heatmap.set("gradient", heatmap.get("gradient") ? null : gradient);
    }

    function changeRadius() {
        heatmap.set("radius", heatmap.get("radius") == 50 ? 20 : 50);
    }

    function changeOpacity() {
        heatmap.set("opacity", heatmap.get("opacity") ? null : 0.2);
    }

/*async*/function get_addresses() {
    let url = "/Home/GetUserAddresses";
    return new Promise(function (resolve, reject) {
        $.get(url, null, function (data) {
        })
            .done(function (data) {
                //console.log("Printing from get_addresses: " + data);
                resolve(data);
            })
            .fail(function (data) {
                reject(data);
            })
            .always(function (data) {
            });
    })
}

async function getPoints(geocoder) {
    //console.log("entered getPoints");

    let addresses = await get_addresses();

    let heatmap_coords = []
    //console.log("After await get_addresses: " + addresses);

    for (let i = 0; i < addresses.length; i++) {
        //console.log("Address " + i + " from getPoints: " + addresses[i]);
        heatmap_coords.push(findLatLang(addresses[i], geocoder));
    }

    //console.log("Final coordinates from getPoints: " + heatmap_coords);
    return await Promise.all(heatmap_coords); // array of promises
}

function findLatLang(address, geocoder) {
    return new Promise(function (resolve, reject) {
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                resolve({ location: results[0].geometry.location, weight: 3 });
            } else {
                reject(new Error('Couldnt\'t find the location ' + address));
            }
        })
    })
}
