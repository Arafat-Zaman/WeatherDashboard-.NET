$(document).ready(function () {
    $("#getWeatherBtn").click(function () {
        var city = $("#cityInput").val();
        if (city === "") {
            alert("Please enter a city name");
            return;
        }

        $.ajax({
            url: '/Weather/GetWeather',
            type: 'GET',
            data: { city: city },
            success: function (response) {
                if (response.error) {
                    $("#weatherResult").html("<p>Error: " + response.error + "</p>");
                } else {
                    var html = "<h3>Weather in " + response.name + "</h3>";
                    html += "<p>Temperature: " + response.main.temp + "°C</p>";
                    html += "<p>Weather: " + response.weather[0].description + "</p>";
                    html += "<p>Wind Speed: " + response.wind.speed + " m/s</p>";
                    $("#weatherResult").html(html);
                }
            },
            error: function () {
                $("#weatherResult").html("<p>Unable to fetch weather data. Please try again.</p>");
            }
        });
    });
});
