using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace FixAMz_WebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignInBtn_Click(object sender, EventArgs e)
        {
            if (AuthenticateUser(UsernameTextBox.Text, PasswordTextBox.Text))
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String check = "select type from SystemUser where username='" + UsernameTextBox.Text + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                String type = (cmd.ExecuteScalar().ToString()).Trim();
                conn.Close();

                FormsAuthentication.SetAuthCookie(UsernameTextBox.Text, RememberMeCheckBox.Checked);
                if (type == "admin")
                {
                    Response.Redirect("AdminUserPeopleTab.aspx");
                }
                else if (type == "manageAssetUser")
                {
                    Response.Redirect("ManageAssetsUser.aspx");
                }
            }
            else
            {
                responseArea.Style.Add("color", "Red");
                responseArea.InnerHtml = "Username or password is incorrect";
            }
        }
        private Boolean AuthenticateUser(String username, String password)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String encriptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                string check = "select count(*) from SystemUser where username='" + username + "' and password='" + encriptedPassword + "'";
                SqlCommand com = new SqlCommand(check, conn);
                int res = Convert.ToInt32(com.ExecuteScalar().ToString());
                conn.Close();
                return (res == 1);
            }
            catch (SqlException ex)
            {
                Response.Write(ex.ToString());
                return false;
            }
        }
    }
}