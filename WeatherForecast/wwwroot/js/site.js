// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var lat = "";
var long = "";

//Gets the coordinates from the 'opencagedata' API from the city passed into the textbox
//Assigns the lat and long variables.
function getGeoCoords() {
    console.log("test");
    var city = document.getElementById("city").value;
    console.log(city);
    var converter_url = "https://api.opencagedata.com/geocode/v1/json?q=" + city + "&key=ae9fb52ab8b04fe783dc63dffb7c2e63";
    console.log(converter_url);

    var request = new XMLHttpRequest();
    request.open('GET', converter_url, true);

    request.onload = function () {

        if (request.status == 200) {
            // Success!
            var data = JSON.parse(request.responseText);
            lat = data.results[0].geometry.lat;
            long = data.results[0].geometry.lng;

        } else if (request.status <= 500) {
            // We reached our target server, but it returned an error

            console.log("unable to geocode! Response code: " + request.status);
            var data = JSON.parse(request.responseText);
            console.log(data.status.message);
        } else {
            console.log("server error");
        }
    };

    request.onerror = function () {
        // There was a connection error of some sort
        console.log("unable to connect to server");
    };

    request.send();
}
