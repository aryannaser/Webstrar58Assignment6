<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="Assignment6CSE445.Welcome" %>


<!DOCTYPE html>
<html>
<head>
    <title>Welcome Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Welcome</h2>
        <asp:Label ID="lblUsername" runat="server" Text="Welcome, User!"></asp:Label><br /><br />

        <!-- Change Password Section -->
        <h3>Change Your Password</h3>
        <label for="txtNewPassword">New Password:</label>
        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Placeholder="Enter new password"></asp:TextBox><br /><br />

        <label for="txtConfirmNewPassword">Confirm New Password:</label>
        <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" Placeholder="Confirm new password"></asp:TextBox><br /><br />

        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" />

        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>

