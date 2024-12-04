<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="Assignment6CSE445.StaffLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Login</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f9f9f9;
        }

        .login-container {
            border: 1px solid #ccc;
            padding: 20px;
            border-radius: 8px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 300px;
        }

        .login-container h2 {
            margin: 0 0 15px;
            text-align: center;
        }

        .login-container label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .login-container input {
            width: 100%;
            padding: 8px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .login-container button {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .login-container button:hover {
            background-color: #0056b3;
        }

        .error {
            color: red;
            text-align: center;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="login-container">
        <h2>Staff Login</h2>
        <asp:Label ID="ErrorMessage" runat="server" CssClass="error"></asp:Label>
        <label for="username">Username</label>
        <asp:TextBox ID="Username" runat="server" Placeholder="Enter username"></asp:TextBox>
        <label for="password">Password</label>
        <asp:TextBox ID="Password" runat="server" TextMode="Password" Placeholder="Enter password"></asp:TextBox>
        <asp:Button ID="LoginButton" runat="server" Text="Admin Access" OnClick="LoginButton_Click" />
    </form>
</body>
</html>
