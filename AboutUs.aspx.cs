using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.Services;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace FixAMz_WebApplication
{
    public partial class AdminAboutUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Load_Notifications();
            setNavBar();

            if (!Page.IsPostBack)
            {
                setUserName();
                Page.MaintainScrollPositionOnPostBack = true; //remember the scroll position on post back
            }

            responseBoxGreen.Style.Add("display", "none");
            responseMsgGreen.InnerHtml = "";
            responseBoxRed.Style.Add("display", "none");
            responseMsgRed.InnerHtml = "";
        }

        //Setting user name on header
        protected void setUserName()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            string[] data = userData.Split(';');

            userName.InnerHtml = data[0];

        }

        //Loading notifications
        protected void Load_Notifications()
        {
            //getting empID
            String username = HttpContext.Current.User.Identity.Name;
            String query = "SELECT empID FROM SystemUser WHERE username='" + username + "'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            String empID = (cmd.ExecuteScalar().ToString()).Trim();

            //selecting relevant notifications
            query = "SELECT notID, action, type, a.name AS assetName, notContent, e.firstName, e.lastname, date, n.status " +
                    "FROM Notification n INNER JOIN Employee e " +
                    "ON n.sendUser=e.empID " +
                    "JOIN Asset a ON n.assetID=a.assetID " +
                    "WHERE receiveUser='" + empID + "' " +
                    "ORDER BY date DESC";

            cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;
            String output = "";
            while (dr.Read())
            {
                output +=
                    "<div id='" + dr["notID"].ToString().Trim() + "' class='notification";
                //Add background color if not-seen
                if (dr["status"].ToString().Trim() == "not-seen")
                {
                    output += " not-seen";
                    count += 1;
                }
                //setting action
                string action = "None";
                if (dr["action"].ToString().Trim() == "Recommend")
                {
                    action = "requested";
                }
                else if (dr["action"].ToString().Trim() == "Approve")
                {
                    action = "recommended";
                }
                else if (dr["action"].ToString().Trim() == "Cancel")
                {
                    action = "rejected";
                }

                //setting type
                string type = dr["type"].ToString().Trim();
                if (type == "AddNew")
                {
                    type = "Register";
                }

                output +=
                    "'>" +
                    "   <img class='col-md-3' src='img/" + dr["type"].ToString().Trim() + "Icon.png'/>" +
                    "   <div class='not-content-box col-md-10'>" +
                    "       Asset <strong>" + dr["assetName"].ToString().Trim() + "</strong> has been " + action + " to " + type +
                    "       by <strong>" + dr["firstName"].ToString().Trim() + " " + dr["lastName"].ToString().Trim() + "</strong>." +
                    "       <div class='not-date col-md-offset-5 col-md-7'>" + dr["date"].ToString().Trim() + "</div>" +
                    "   </div>" +
                    "</div>";
            }

            //set notifications
            notificationsBody.InnerHtml = output;

            //set count
            if (count > 0)
            {
                notification_count.InnerHtml = Convert.ToString(count);
            }
            else
            {
                notification_count.Style.Add("display", "none");
            }

            dr.Close();
            conn.Close();
        }

        //Dynamically setting nav bar
        protected void setNavBar()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');

            if (data[1] == "manageReport")
            {
                manageReportNavBar.Style.Add("display", "block");
            }
            else if (data[1] == "generateReportUser")
            {
                generateReportUserNavBar.Style.Add("display", "block");
            }
            if (data[1] == "admin")
            {
                adminAboutNavBar.Style.Add("display", "block");
            }
            else if (data[1] == "manageAssetUser")
            {
                manageAssetUserNavBar.Style.Add("display", "block");
            }
        }

        //reload after click cancel button
        protected void cancel_clicked(object sender, EventArgs e)
        {
            Response.Redirect("AboutUs.aspx");
        }

        //Signing out
        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        [WebMethod]//username validity checking in client side with ajax
        public static int checkUsername(string Username)
        {
            //To send a JSON object -> HttpContext.Current.Response.Write("{'response' : '" + res + "'}");
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT COUNT(*) FROM SystemUser WHERE username='" + Username + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                return res;
            }
            catch (SqlException)
            {
                return 2;
            }
        }
    }
}