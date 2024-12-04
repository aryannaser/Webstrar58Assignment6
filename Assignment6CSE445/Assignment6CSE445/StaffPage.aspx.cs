using System;
using System.Web;
using System.Web.UI;

namespace Assignment6CSE445
{
    public partial class StaffPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load the current state of AllowNewUsers on initial page load
            if (!IsPostBack)
            {
                bool allowNewUsers = Application["AllowNewUsers"] as bool? ?? true; // Default to true
                Yes.Checked = allowNewUsers;
                No.Checked = !allowNewUsers;
            }
        }

        protected void Yes_CheckedChanged(object sender, EventArgs e)
        {
            if (Yes.Checked)
            {
                No.Checked = false;

                // Save state to application
                Application["AllowNewUsers"] = true;
            }
        }

        protected void No_CheckedChanged(object sender, EventArgs e)
        {
            if (No.Checked)
            {
                Yes.Checked = false;

                // Save state to application
                Application["AllowNewUsers"] = false;
            }
        }
    }
}
