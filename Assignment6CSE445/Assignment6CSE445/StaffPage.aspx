<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="Assignment6CSE445.StaffPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Central News Agency</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Central News Agency</h1>
            <h2>Staff Page</h2>
            <asp:Label ID="lblStaffName" runat="server" Text="Allow More Users:" style="font-weight: bold;"></asp:Label>
            <br />
            <asp:CheckBox ID="Yes" Text="Yes" runat="server" AutoPostBack="True" OnCheckedChanged="Yes_CheckedChanged" />
            <asp:CheckBox ID="No" Text="No" runat="server" AutoPostBack="True" OnCheckedChanged="No_CheckedChanged" />
            <br />
        </div>
    </form>
</body>
</html>
