<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignment6CSE445._Default" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Central News Agency</title>
    <style>
        /* General styling */
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
        }

        /* Navbar styling */
        .navbar {
            display: flex;
            align-items: center;
            background-color: #333;
            padding: 10px 20px;
            color: white;
            justify-content: flex-start;
            position: sticky;
            top: 0;
            z-index: 1000;
        }

            .navbar .title {
                font-size: 20px;
                font-weight: bold;
                margin-right: 15px;
            }

            .navbar .search-bar {
                flex-grow: 0;
                margin-right: auto;
            }

                .navbar .search-bar input {
                    width: 300px;
                    padding: 5px;
                    border: none;
                    border-radius: 5px;
                }

            .navbar .links {
                display: flex;
                gap: 10px;
            }

                .navbar .links a {
                    color: white;
                    text-decoration: none;
                    padding: 5px 10px;
                    border-radius: 5px;
                    background-color: #444;
                }

                    .navbar .links a:hover {
                        background-color: #555;
                    }

        /* Layout styling */
        .main-content {
            display: flex;
            margin: 20px;
        }

            .main-content .news-section {
                flex: 4;
                height: 1200px;
                border: 1px solid #ccc;
                margin-right: 20px;
                padding: 10px;
            }

            .main-content .side-sections {
                flex: 1;
                position: sticky;
                top: 70px;
                display: flex;
                flex-direction: column;
                height: calc(100vh - 70px);
            }

                .main-content .side-sections .side-section {
                    flex: 1;
                    border: 1px solid #ccc;
                    padding: 10px;
                    background-color: #fff;
                    margin-bottom: 10px;
                    overflow-y: auto;
                }

        /* Weather Section Styling */
        #weatherDetails {
            text-align: center;
            margin-top: 10px;
        }

            #weatherDetails .city {
                font-size: 18px;
                font-weight: bold;
                margin-top: 1em;
                margin-bottom: 10px;
            }

            #weatherDetails .forecast-entry {
                margin-bottom: 10px;
                font-size: 16px;
            }

        /* Stock Section Styling */
        #stockDetails {
            text-align: center;
            margin-top: 10px;
        }

            #stockDetails .stock-entry {
                margin-bottom: 10px;
                font-size: 16px;
            }

        .side-section input {
            width: 65%; /* Smaller input field */
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .side-section button {
            padding: 5px 10px;
            margin-left: 10px;
            background-color: #007bff; /* Blue background */
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

            .side-section button:hover {
                background-color: #0056b3;
            }

        .side-section h4 {
            margin-top: .25em; /* Minimal top margin */
            margin-bottom: 5px; /* Keep minimal bottom margin */
            text-align: center; /* Center the title */
        }
    </style>
</head>
<body>
    <!-- Navbar -->
    <div class="navbar">
        <div class="title">Central News Agency</div>
        <div class="search-bar">
            <input type="text" placeholder="Search news, stocks, or weather..." />
        </div>
        <div class="links">
             <a href="Member.aspx">Member</a>
            <a href="StaffLogin.aspx">Management</a> 
        </div>
    </div>

    <!-- Main content -->
    <div class="main-content">
        <div class="news-section">
            <h3>News Section</h3>
                    <asp:Repeater ID="NewsRepeater" runat="server">
    <ItemTemplate>
        <div class="news-article">
            <h4>
                <a href='<%# Eval("Url") %>' target="_blank"><%# Eval("Title") %></a>
            </h4>
            <p><%# Eval("Description") %></p>
            <p><small>Published: <%# Eval("PublishedAt") %></small></p>
        </div>
        <hr />
    </ItemTemplate>
</asp:Repeater>
        </div>
        <div class="side-sections">
            <!-- Weather Section -->
            <div class="side-section">
                <h4>Weather Report</h4>
                <div style="display: flex; justify-content: center; align-items: center; margin: 10px 0;">
                    <input id="weatherZipCode" type="text" placeholder="Enter ZIP code" />
                    <button onclick="fetchWeather()">Search</button>
                </div>
                <div id="weatherDetails"></div>
            </div>

            <!-- Stock Section -->
            <div class="side-section">
                <h4>Stock Finder</h4>
                <div style="display: flex; justify-content: center; align-items: center; margin: 10px 0;">
                    <input id="stockSymbol" type="text" placeholder="Enter stock symbol" />
                    <button onclick="fetchStock()">Search</button>
                </div>
                <div id="stockDetails"></div>
            </div>
        </div>
    </div>
</body>

<!-- JavaScript -->
<script>
    let weatherHistory = [];

    // Fetch weather data
    async function fetchWeather() {
        const zipCodeInput = document.getElementById("weatherZipCode").value || "85281";
        const weatherDetailsDiv = document.getElementById("weatherDetails");

        try {
            const response = await fetch(`https://localhost:44302/api/weather/forecast?query=${zipCodeInput}&days=3`);
            if (!response.ok) throw new Error("Unable to fetch weather data.");

            const data = await response.json();
            const city = `${data.location.name}, ${data.location.region}`;
            const forecastDays = data.forecast.forecastday.slice(0, 2);

            let weatherHtml = `<div class="city">${city}</div>`;
            forecastDays.forEach(day => {
                const date = new Date(day.date).toLocaleDateString("en-US");
                weatherHtml += `<div class="forecast-entry">${date}: ${day.day.avgtemp_f}°F - ${day.day.condition.text}</div>`;
            });

            weatherHistory.unshift(weatherHtml);
            if (weatherHistory.length > 2) weatherHistory.pop();
            weatherDetailsDiv.innerHTML = weatherHistory.join("<hr>");
        } catch (error) {
            weatherDetailsDiv.innerHTML = `<p style="color: red;">Error: ${error.message}</p>`;
        }
    }

    // Fetch stock data
    let stockHistory = []; // Array to store stock data for up to four symbols

    // Fetch stock data
    async function fetchStock() {
        const stockSymbolInput = document.getElementById("stockSymbol").value.trim().toUpperCase();
        const stockDetailsDiv = document.getElementById("stockDetails");

        if (!stockSymbolInput) return; // Exit if no input is provided

        try {
            const response = await fetch(`https://localhost:44302/api/stocks/${stockSymbolInput}`);
            if (!response.ok) throw new Error("Unable to fetch stock data.");

            const data = await response.json();
            const stockHtml = `
                <div class="stock-entry">
                    <strong>${data.Symbol}</strong>: $${parseFloat(data.Price).toFixed(2)} <br />
                    <small>Last Updated: ${data.Date}</small>
                </div>
            `;

            // Update stock history
            stockHistory.unshift(stockHtml); // Add the new stock at the top
            if (stockHistory.length > 4) stockHistory.pop(); // Remove the oldest stock if more than 4

            // Render stock history
            stockDetailsDiv.innerHTML = stockHistory.join("<hr>"); // Use horizontal lines to separate entries
        } catch (error) {
            stockDetailsDiv.innerHTML = `<p style="color: red;">Error: ${error.message}</p>`;
        }
    }

    // Fetch default weather on page load
    fetchWeather();
</script>
</html>
