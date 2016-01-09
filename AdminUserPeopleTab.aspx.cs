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

    public partial class AdminUserPeopleTab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticate_User();
            setEmpID();
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

        //loading CostCenters
        protected void Load_CostCenter()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT name, costID FROM CostCenter", conn);
                SqlDataReader data = cmd.ExecuteReader();

                AddUserCostNameDropDown.DataSource = data;
                AddUserCostNameDropDown.DataTextField = "name";
                AddUserCostNameDropDown.DataValueField = "costID";
                AddUserCostNameDropDown.DataBind();
                AddUserCostNameDropDown.Items.Insert(0, new ListItem("-- Select a Cost Center --", ""));
                data.Close();

                data = cmd.ExecuteReader();
                UpdateCostCenterDropDown.DataSource = data;
                UpdateCostCenterDropDown.DataTextField = "name";
                UpdateCostCenterDropDown.DataValueField = "costID";
                UpdateCostCenterDropDown.DataBind();
                data.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
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

        protected void setEmpID() //Reads the last empID from DB, calculates the next and set it in the web page.
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 empID FROM Employee ORDER BY empID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newEmpID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastEmpID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastEmpID[0]);
                    String temp = "";
                    for (int i = 1; i < lastEmpID.Length; i++)
                    {
                        temp += Convert.ToString(lastEmpID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newEmpID = chr;
                    for (int i = 1; i < lastEmpID.Length - temp.Length; i++)
                    {
                        newEmpID += "0";
                    }
                    newEmpID += temp;
                }
                else
                {
                    newEmpID = "E00001";
                }

                AddNewEmpID.InnerHtml = newEmpID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
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

        //Add new user
        protected void AddUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                string insertion_Employee = "insert into Employee (empID, costID, firstName, lastName, contactNo, email) values (@empid, @costID, @firstname, @lastname, @contact, @email)";
                SqlCommand cmd = new SqlCommand(insertion_Employee, conn);
                cmd.Parameters.AddWithValue("@empid", AddNewEmpID.InnerHtml);
                cmd.Parameters.AddWithValue("@costID", AddUserCostNameDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@firstname", AddNewFirstNameTextBox.Text);
                cmd.Parameters.AddWithValue("@lastname", AddNewLastNameTextBox.Text);
                cmd.Parameters.AddWithValue("@contact", AddNewContactTextBox.Text);
                cmd.Parameters.AddWithValue("@email", AddNewEmailTextBox.Text);

                cmd.ExecuteNonQuery();

                if (TypeDropDown.SelectedItem.Value != "owner")
                {
                    string insertion_User = "insert into SystemUser (empID, costID, username, password, type) values (@empid, @costid, @username, @password, @type)";
                    cmd = new SqlCommand(insertion_User, conn);

                    String encriptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(AddNewPasswordTextBox.Text, "SHA1");

                    cmd.Parameters.AddWithValue("@empid", AddNewEmpID.InnerHtml);
                    cmd.Parameters.AddWithValue("@costid", AddUserCostNameDropDown.SelectedValue);
                    cmd.Parameters.AddWithValue("@username", AddNewUsernameTextBox.Text);
                    cmd.Parameters.AddWithValue("@password", encriptedPassword);
                    cmd.Parameters.AddWithValue("@type", TypeDropDown.SelectedItem.Value);

                    cmd.ExecuteNonQuery();

                    //Sending email to the user with username and password
                    Boolean Email = SendEmail(AddNewEmailTextBox.Text, "Welcome to FixAMz", 
                        "Your username and password for FixAmz is as follows.\n\n" +
                        "Username - " + AddNewUsernameTextBox.Text + "\n" + 
                        "Password - " + Convert.ToString(AddNewPasswordTextBox.Text) + "\n\n" +
                        "Please contact system administrator incase a password reset is needed.\n\n" +
                        "Regards,\n" +
                        "Administrator"
                        );
                }

                conn.Close();
                
                ScriptManager.RegisterStartupScript(this, GetType(), "addNewClearAll", "addNewClearAll();", true);
                setEmpID();
                AddNewUserContent.Style.Add("display", "none");
                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "User " + AddNewFirstNameTextBox.Text + " " + AddNewLastNameTextBox.Text + " added successfully!";
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected Boolean SendEmail(string toAddress, string subject, string body)
        {
            Boolean result = true;

            string senderID = "fixamz@gmail.com";// use sender’s email id here..
            const string senderPassword = "fixamz@123"; // sender password here…

            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,

                };

                MailMessage message = new MailMessage(senderID, toAddress, subject, body);

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Response.Write("Email:" + ex.ToString());
                result = false;
            }

            return result;
        }

        protected void AddUserTypeDropDown_Selected(object sender, EventArgs e)
        {
            if (TypeDropDown.SelectedItem.Value != "owner")
            {
                AddUserLoginDetailContainer.Style.Add("display", "block");
                AddNewUserContent.Style.Add("display", "block");
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AddNewUserContent');", true);
            }
            else
            {
                AddUserLoginDetailContainer.Style.Add("display", "none");
                AddNewUserContent.Style.Add("display", "block");
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AddNewUserContent');", true);
            }
        }

        //Update user
        protected void UpdateUserFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String empID = UpdateEmpIDTextBox.Text;

                string check = "select count(*) from Employee WHERE empID='" + empID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query1 = "SELECT e.empID, e.firstName, e.lastName, e.contactNo, e.email, s.type FROM Employee e INNER JOIN SystemUser s ON e.empID = s.empID WHERE e.empID='" + empID + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    while (dr1.Read())
                    {
                        UpdateEmpID.InnerHtml = dr1["empID"].ToString();
                        UpdateFirstNameTextBox.Text = dr1["firstName"].ToString();
                        UpdateLastNameTextBox.Text = dr1["lastName"].ToString();
                        UpdateContactTextBox.Text = dr1["contactNo"].ToString();
                        UpdateEmailTextBox.Text = dr1["email"].ToString();
                        UpdateTypeDropDown.SelectedValue = dr1["type"].ToString();
                        
                    }
                    dr1.Close();
                   
                    updateUserInitState.Style.Add("display", "none");
                    updateUserSecondState.Style.Add("display", "block");
                    UpdateUserContent.Style.Add("display", "block");
                    UpdateEmpIDValidator.InnerHtml = "";
                }
                else
                {
                    updateUserInitState.Style.Add("display", "block");
                    updateUserSecondState.Style.Add("display", "none");
                    UpdateUserContent.Style.Add("display", "block");
                    UpdateEmpIDValidator.InnerHtml = "Invalid employee ID";
                }
                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateUserContent');", true);
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }

        }

        protected void UpdateUserTypeDropDown_Selected(object sender, EventArgs e)
        {

        }

        protected void UpdateUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String empID = UpdateEmpIDTextBox.Text;
                string insertion_Employee = "UPDATE Employee SET costID = @costID, firstName = @firstname, lastName = @lastname, contactNo = @contact, email = @email WHERE empID='" + empID + "'";
                SqlCommand cmd = new SqlCommand(insertion_Employee, conn);

                cmd.Parameters.AddWithValue("@costID", UpdateCostCenterDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@firstname", UpdateFirstNameTextBox.Text);
                cmd.Parameters.AddWithValue("@lastname", UpdateLastNameTextBox.Text);
                cmd.Parameters.AddWithValue("@contact", UpdateContactTextBox.Text);
                cmd.Parameters.AddWithValue("@email", UpdateEmailTextBox.Text);
                cmd.ExecuteNonQuery();

                string insertion_SystemUser = "UPDATE SystemUser SET costID = @costid, type = @type WHERE empID='" + empID + "'";
                cmd = new SqlCommand(insertion_SystemUser, conn);
                cmd.Parameters.AddWithValue("@costid", UpdateCostCenterDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@type", UpdateTypeDropDown.SelectedValue);
                cmd.ExecuteNonQuery();

                //Include update query for password here

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "updateClearAll", "updateClearAll();", true);

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Employee '" + empID + "' updated successfully!";

                updateUserInitState.Style.Add("display", "block");
                updateUserSecondState.Style.Add("display", "none");
                UpdateUserContent.Style.Add("display", "block");
                UpdateEmpIDTextBox.Text = "";

                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateUserContent');", true);
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        //Reset Password
        protected void ResetPasswordFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String username = ResetPasswordUsernameTextBox.Text;

                string check = "select count(*) from SystemUser WHERE username='" + username + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query1 = "SELECT username FROM SystemUser WHERE username='" + username + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    
                    while (dr1.Read())
                    {
                        ResetUsername.InnerHtml = dr1["username"].ToString();

                    }
                    dr1.Close();
                    resetPasswordInitState.Style.Add("display", "none");
                    resetPasswordSecondState.Style.Add("display", "block");
                    ResetPasswordContent.Style.Add("display", "block");
                    ResetPasswordUsernameValidator.InnerHtml = "";
                }
                else
                {
                    resetPasswordInitState.Style.Add("display", "block");
                    resetPasswordSecondState.Style.Add("display", "none");
                    ResetPasswordContent.Style.Add("display", "block");
                    ResetPasswordUsernameValidator.InnerHtml = "Invalid Username";
                }
                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('ResetPasswordContent');", true);
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }

        }

        protected void ResetPasswordBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String username = ResetPasswordUsernameTextBox.Text;

                string insertion_SystemUser = "UPDATE SystemUser SET password = @password WHERE username='" + username + "'";
                SqlCommand cmd = new SqlCommand(insertion_SystemUser, conn);

                string empID = "SELECT empID FROM SystemUser WHERE username='" + username + "'";
                SqlCommand cmd1 = new SqlCommand(empID, conn);
                String empIDget = (cmd1.ExecuteScalar().ToString()).Trim();

                string email = "SELECT email FROM Employee WHERE empID='" + empIDget + "'";
                SqlCommand cmd2 = new SqlCommand(email, conn);
                String emailAdd = (cmd2.ExecuteScalar().ToString()).Trim();

                String encriptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(ResetNewPasswordTextBox.Text, "SHA1");
                cmd.Parameters.AddWithValue("@password", encriptedPassword);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "updateClearAll", "updateClearAll();", true);

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Employee password reset completed successfully!";

                resetPasswordInitState.Style.Add("display", "block");
                resetPasswordSecondState.Style.Add("display", "none");
                ResetPasswordContent.Style.Add("display", "block");
                ResetPasswordUsernameTextBox.Text = "";

                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateUserContent');", true);
                
                //Sending email to the user with username and password
                Boolean Email = SendEmail(emailAdd, "Welcome to FixAMz",
                    "Your password is reseted for FixAmz is as follows.\n\n" +
                    "Username - " + username + "\n" +
                    "Password - " + Convert.ToString(ResetNewPasswordTextBox.Text) + "\n\n" +
                    "\n" +
                    "Regards,\n" +
                    "Administrator"
                    );
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        //Delete user
        protected void DeleteUserFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String empID = DeleteUserEmpIDTextBox.Text;

                string check = "select count(*) from Employee WHERE empID='" + empID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT empID, firstName, lastName, contactNo, email FROM Employee WHERE empID='" + empID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        DeleteEmpID.InnerHtml = dr["empID"].ToString();
                        DeleteFirstName.InnerHtml = dr["firstName"].ToString();
                        DeleteLastName.InnerHtml = dr["lastName"].ToString();
                        DeleteContact.InnerHtml =  dr["contactNo"].ToString();
                        DeleteEmail.InnerHtml = dr["email"].ToString();
                    }
                    deleteUserInitState.Style.Add("display", "none");
                    deleteUserSecondState.Style.Add("display", "block");
                    DeleteUserContent.Style.Add("display", "block");
                    DeleteUserEmpIDValidator.InnerHtml = "";
                }
                else
                {
                    deleteUserInitState.Style.Add("display", "block");
                    deleteUserSecondState.Style.Add("display", "none");
                    DeleteUserContent.Style.Add("display", "block");
                    DeleteUserEmpIDValidator.InnerHtml = "Employee ID not found!";
                }
                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('DeleteUserContent');", true);
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void DeleteUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String empID = DeleteUserEmpIDTextBox.Text;

                string deleteQuerySystemUser = "DELETE FROM SystemUser WHERE empID='" + empID + "'";
                string deleteQueryEmployee = "DELETE FROM Employee WHERE empID='" + empID + "'";
                SqlCommand cmd = new SqlCommand(deleteQuerySystemUser, conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(deleteQueryEmployee, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Employee '" + empID + "' deleted successfully!";

                deleteUserInitState.Style.Add("display", "block");
                deleteUserSecondState.Style.Add("display", "none");
                DeleteUserContent.Style.Add("display", "block");
                DeleteUserEmpIDTextBox.Text = "";

                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('DeleteUserContent');", true);
            }
            catch (SqlException)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }

        }

    }
}

