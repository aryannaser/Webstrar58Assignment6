using System;
using System.Web.UI;
using System.Xml;

namespace Assignment6CSE445
{
    public partial class Member : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if new user registrations are allowed
                bool allowNewUsers = Application["AllowNewUsers"] as bool? ?? true;
                btnRegister.Enabled = allowNewUsers;

                if (!allowNewUsers)
                {
                    btnRegister.Text = "No New Users Allowed";
                    btnRegister.ToolTip = "Registration is currently disabled.";
                }
                else
                {
                    btnRegister.Text = "Register";
                    btnRegister.ToolTip = "Click to register.";
                }

                GenerateAndSetCaptcha();
            }
        }

        private void GenerateAndSetCaptcha()
        {
            string captchaText = "ABC12";
            lblCaptcha.Text = captchaText;

            imgCaptcha.ImageUrl = "CaptchaHandler.ashx?rand=" + Guid.NewGuid();
        }

        protected void btnReloadCaptcha_Click(object sender, EventArgs e)
        {
            GenerateAndSetCaptcha();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Check if registration is allowed
            bool allowNewUsers = Application["AllowNewUsers"] as bool? ?? true;
            if (!allowNewUsers)
            {
                Response.Write("No new users are allowed at the moment. Please try again later.");
                return;
            }

            // Existing registration logic
            string username = txtRegUsername.Text;
            string password = txtRegPassword.Text;
            string confirmPassword = txtRegConfirmPassword.Text;
            string captcha = txtRegCaptcha.Text;

            if (captcha != "ABC12")
            {
                Response.Write("Captcha verification failed.");
                return;
            }

            if (password != confirmPassword)
            {
                Response.Write("Passwords do not match.");
                return;
            }

            string encryptedPassword = P.S(password);
            SaveToXml(username, encryptedPassword);

            Session["AuthenticatedUser"] = username;
            Response.Redirect("Welcome.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginUsername.Text;
            string password = txtLoginPassword.Text;

            string encryptedPassword = P.S(password);

            if (ValidateUser(username, encryptedPassword))
            {
                Session["AuthenticatedUser"] = username;
                Response.Redirect("Welcome.aspx");
            }
            else
            {
                Response.Write("Invalid username or password.");
            }
        }

        private void SaveToXml(string username, string encryptedPassword)
        {
            string filePath = Server.MapPath("~/App_Data/Member.xml");

            XmlDocument doc = new XmlDocument();
            if (!System.IO.File.Exists(filePath))
            {
                XmlElement root = doc.CreateElement("Members");
                doc.AppendChild(root);
            }
            else
            {
                doc.Load(filePath);
            }

            XmlElement member = doc.CreateElement("Member");
            XmlElement user = doc.CreateElement("Username");
            user.InnerText = username;
            XmlElement pass = doc.CreateElement("Password");
            pass.InnerText = encryptedPassword;

            member.AppendChild(user);
            member.AppendChild(pass);
            doc.DocumentElement.AppendChild(member);

            doc.Save(filePath);
        }

        private bool ValidateUser(string username, string encryptedPassword)
        {
            string filePath = Server.MapPath("~/App_Data/Member.xml");

            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            foreach (XmlNode member in doc.SelectNodes("//Member"))
            {
                string storedUsername = member["Username"].InnerText;
                string storedPassword = member["Password"].InnerText;

                if (storedUsername == username && storedPassword == encryptedPassword)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
