using System;
using System.Web.UI;

namespace Assignment6CSE445
{
    public partial class StaffLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear any previous error message
            ErrorMessage.Text = string.Empty;
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Hardcoded credentials
            string adminUsername = "Group58";
            string adminPassword = "CSE445";

            // Validate credentials
            if (Username.Text == adminUsername && Password.Text == adminPassword)
            {
                // Redirect to StaffPage if credentials are correct
                Response.Redirect("StaffPage.aspx");
            }
            else
            {
                // Show error message
                ErrorMessage.Text = "Invalid username or password.";
            }
        }
    }
}
