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
    public partial class AdvancedUserSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticate_User();
            Load_Notifications();
            

            if (!Page.IsPostBack)
            {
                Load_CostCenter();
                setUserName();
                Page.MaintainScrollPositionOnPostBack = true; //remember the scroll position on post back
            }

            responseBoxGreen.Style.Add("display", "none");
            responseMsgGreen.InnerHtml = "";
            responseBoxRed.Style.Add("display", "none");
            responseMsgRed.InnerHtml = "";
        }

        //Checking if the user has access to the page
        protected void Authenticate_User()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');


            if (data[1] != "admin")
            {
                FormsAuthentication.SignOut();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You do not have access to this page. Please sign in to continue.'); window.location='" +
Request.ApplicationPath + "Login.aspx';", true);
            }
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

                //setting asset name
                string assetName = dr["assetName"].ToString().Trim();
                if (assetName.Length > 15)
                {
                    assetName = assetName.Substring(0, 12);
                    assetName += "...";
                }
                output +=
                    "'>" +
                    "   <img class='col-md-3' src='img/" + dr["type"].ToString().Trim() + "Icon.png'/>" +
                    "   <div class='not-content-box col-md-10'>" +
                    "       Asset <strong>" + assetName + "</strong> has been " + action + " to " + type +
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

        //reload after click cancel button
        protected void cancel_clicked(object sender, EventArgs e)
        {
            Response.Redirect("AdminUserPeopleTab.aspx");
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

        //loading CostCenters
        protected void Load_CostCenter()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT name, costID FROM CostCenter", conn);
                SqlDataReader data = cmd.ExecuteReader();

                SearchUserCostNameDropDown.DataSource = data;
                SearchUserCostNameDropDown.DataTextField = "name";
                SearchUserCostNameDropDown.DataValueField = "costID";
                SearchUserCostNameDropDown.DataBind();
                SearchUserCostNameDropDown.Items.Insert(0, new ListItem("-- Select a Cost Center --", ""));
                data.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        //Advanced user search
        protected void SearchUserBtn_Click(object sender, EventArgs e)
        {
            String costID = SearchUserCostNameDropDown.SelectedValue;
            String firstname = SearchFirstNameTextBox.Text.Trim();
            String lastname = SearchLastNameTextBox.Text.Trim();
            String email = SearchEmailTextBox.Text.Trim();
            String contactNo = SearchContactTextBox.Text.Trim();

            String resultMessage = "";

            String query = "SELECT E.empID AS Employee_ID, C.name AS Cost_Center, (E.firstName+' '+E.lastName) AS Name, E.contactNo AS Contact, E.email AS Email " +
                "FROM Employee E " +
                "INNER JOIN CostCenter C ON E.costID=C.costID " +
                "WHERE";

            if (costID != "")
            {
                query += " AND E.costID='" + costID + "'";
                resultMessage += "Cost Center <strong>" + SearchUserCostNameDropDown.SelectedItem + "</strong>, ";
            }
            if (firstname != "")
            {
                query += " AND firstname like '" + firstname + "%'";
                resultMessage += "First Name <strong>" + firstname + "</strong>, ";
            }
            if (lastname != "")
            {
                query += " AND lastname like '" + lastname + "%'";
                resultMessage += "Last Name <strong>" + lastname + "</strong>, ";
            }
            if (email != "")
            {
                query += " AND email like '" + email + "%'";
                resultMessage += "Email <strong>" + email + "</strong>, ";
            }
            if (contactNo != "")
            {
                query += " AND contactNo like '" + contactNo + "%'";
                resultMessage += "Contact <strong>" + contactNo + "</strong>, ";
            }

            // Clearing the grid view
            UserSearchGridView.DataSource = null;
            UserSearchGridView.DataBind();

            //Remove unnessary 'and'
            query = query.Replace("WHERE AND", "WHERE ");

            //Remove result message last comma
            resultMessage = resultMessage.Substring(0, resultMessage.Length - 2);

            //Response.Write(query + "<br>");
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString); //database connectivity
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null && reader.HasRows) //if search results found
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    UserSearchGridView.DataSource = dt;  //display found data in grid view
                    UserSearchGridView.DataBind();
                    responseBoxGreen.Style.Add("display", "block");
                    responseMsgGreen.InnerHtml = "Search results found for " + resultMessage;
                }
                else
                {
                    responseBoxRed.Style.Add("display", "block");
                    responseMsgRed.InnerHtml = "No results found for " + resultMessage;
                }
                conn.Close();

                //expanding block
                AdvancedUserSearchContent.Style.Add("display", "block");
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AdvancedUserSearchContent');", true);
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }
    }
}