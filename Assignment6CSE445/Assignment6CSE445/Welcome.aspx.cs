using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Assignment6CSE445
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Display the username on the welcome page
            if (Session["AuthenticatedUser"] != null)
            {
                lblUsername.Text = "Welcome, " + Session["AuthenticatedUser"].ToString() + "!";
            }
            else
            {
                Response.Redirect("Member.aspx");
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string username = Session["AuthenticatedUser"].ToString();
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmNewPassword.Text;

            // Validate passwords
            if (newPassword != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            // Encrypt the new password (using your existing P.S method)
            string encryptedPassword = P.S(newPassword);

            // Update the password in the XML file
            if (UpdatePasswordInXml(username, encryptedPassword))
            {
                lblMessage.Text = "Password changed successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Failed to change password. User not found.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool UpdatePasswordInXml(string username, string newPassword)
        {
            string filePath = Server.MapPath("~/App_Data/Member.xml");

            if (!System.IO.File.Exists(filePath))
            {
                return false; // No users registered yet
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            foreach (XmlNode member in doc.SelectNodes("//Member"))
            {
                string storedUsername = member["Username"].InnerText;

                if (storedUsername == username)
                {
                    member["Password"].InnerText = newPassword; // Update the password
                    doc.Save(filePath);
                    return true; // Password updated
                }
            }

            return false; // User not found
        }
    }
}
