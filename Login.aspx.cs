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
                string username = UsernameTextBox.Text;
                bool isPersistent = RememberMeCheckBox.Checked;
                string userData = "";
                string type = "";

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                string query = "SELECT e.firstName, e.lastName, s.type, s.costID FROM Employee e INNER JOIN SystemUser s ON e.empID=s.empID WHERE s.username='" + username + "'";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //userData = "Vihanga Liyanage;admin;CO00001"
                    userData = dr["firstName"].ToString().Trim() + " " + dr["lastName"].ToString().Trim() +
                                ";" + dr["type"].ToString().Trim() +
                                ";" + dr["costID"].ToString().Trim();
                    type = dr["type"].ToString().Trim();
                }
                conn.Close();

                // Create a custom FormsAuthenticationTicket containing
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  username,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  isPersistent,
                  userData,
                  FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                //Redirecting to relevent page
                if (type == "admin")
                {
                    Response.Redirect("AdminUserPeopleTab.aspx");
                }
                else if (type == "manageAssetUser")
                {
                    Response.Redirect("ManageAssetsUser.aspx");
                }
                else if (type == "generateReportUser")
                {
                    Response.Redirect("ReportViewer.aspx");
                }
                else if (type == "manageReport")
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
                String check = "select count(*) from SystemUser where username='" + username + "' and password='" + encriptedPassword + "'";
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