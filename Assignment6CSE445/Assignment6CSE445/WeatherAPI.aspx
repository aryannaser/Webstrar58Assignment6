<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="WeatherAPI.aspx.cs" Inherits="Assignment6CSE445.WeatherAPI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>5-Day Weather Forecast</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>3-Day Weather Forecast</h2>
            <label for="ZipCodeInput">Enter ZIP Code: </label>
            <asp:TextBox ID="ZipCodeInput" runat="server" Placeholder="e.g., 10001"></asp:TextBox>
            <asp:Button ID="FetchWeatherButton" runat="server" Text="Get Weather" OnClick="FetchWeatherButton_Click" />
            <br /><br />
            <asp:Label ID="WeatherLabel" runat="server" Text="Weather data will appear here."></asp:Label>
        </div>
    </form>
</body>
</html>
