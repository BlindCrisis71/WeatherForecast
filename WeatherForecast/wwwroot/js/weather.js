"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/weatherHub").build();

//Disable send button until connection is established
document.getElementById("btn").disabled = true;


connection.on("ReceiveCoordinates", function (l, lt) {

    console.log(l);
    console.log(lt);
});
connection.on("ReceiveResult", function (result) {

    document.getElementById("result").innerText = result;
});
connection.on("UpdateCurrentWeather", function (result) {
    // KEY: [0]Summary, [1]Icon, [2]Temperature, [3]Temp High, [4]Temp Low, [5]PrecipProbability
    document.getElementById("currentSummary").innerText = result[0];
    document.getElementById("currentTemp").innerText = "Current Temperature: " + result[2] + " F °";
    document.getElementById("currentHiLo").innerText = "Low/High: " + result[3] + "/" + result[4];
    document.getElementById("currentChanceOfRain").innerText = "Chance Of Rain: " + result[5];
});

connection.start().then(function () {
    document.getElementById("btn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btn").addEventListener("click", function (event) {
    getGeoCoords();

    setTimeout(function () {
        connection.invoke("SendCoordinates", long, lat).catch(function (err) {
            return console.error(err.toString());
        });

    }, 2000);


    event.preventDefault();

//    var city = document.getElementById("city").value;
 //   connection.invoke("SendCoordinates", city.toString()).catch(function (err) {
  //      return console.error(err.toString());
   // });
});