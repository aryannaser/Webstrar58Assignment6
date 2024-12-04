<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Member.aspx.cs" Inherits="Assignment6CSE445.Member" %>

<!DOCTYPE html>
<html>
<head>
    <title>Member Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Login or Register as a Member</h2>

            <!-- Registration Section -->
            <h3>Register</h3>
            <!-- Username -->
            <label for="txtRegUsername">Username:</label>
            <asp:TextBox ID="txtRegUsername" runat="server" Placeholder="Enter username"></asp:TextBox><br /><br />

            <!-- Password -->
            <label for="txtRegPassword">Password:</label>
            <asp:TextBox ID="txtRegPassword" runat="server" TextMode="Password" Placeholder="Enter password"></asp:TextBox><br /><br />

            <!-- Confirm Password -->
            <label for="txtRegConfirmPassword">Confirm Password:</label>
            <asp:TextBox ID="txtRegConfirmPassword" runat="server" TextMode="Password" Placeholder="Confirm password"></asp:TextBox><br /><br />

            <!-- Captcha -->
            <div>
                <label for="txtRegCaptcha">Enter Captcha:</label><br />
                <asp:Image ID="imgCaptcha" runat="server" ImageUrl="CaptchaHandler.ashx" /><br /><br />
                <asp:Button ID="btnReloadCaptcha" runat="server" Text="Reload Captcha" OnClick="btnReloadCaptcha_Click" /><br /><br />
                <asp:TextBox ID="txtRegCaptcha" runat="server" Placeholder="Enter captcha"></asp:TextBox><br /><br />

                <!-- Hidden Captcha Label -->
                <asp:Label ID="lblCaptcha" runat="server" Text="Captcha goes here" style="display:none;"></asp:Label>
            </div>

            <!-- Register Button -->
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />

            <hr />

            <!-- Login Section -->
            <h3>Login</h3>
            <!-- Username -->
            <label for="txtLoginUsername">Username:</label>
            <asp:TextBox ID="txtLoginUsername" runat="server" Placeholder="Enter username"></asp:TextBox><br /><br />

            <!-- Password -->
            <label for="txtLoginPassword">Password:</label>
            <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password" Placeholder="Enter password"></asp:TextBox><br /><br />

            <!-- Login Button -->
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>
