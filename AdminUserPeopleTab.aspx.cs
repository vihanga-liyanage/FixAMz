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
            setEmpID();
            responseBoxGreen.Style.Add("display", "none");
            responseMsgGreen.InnerHtml = "";
            responseBoxRed.Style.Add("display", "none");
            responseMsgRed.InnerHtml = "";
            Page.MaintainScrollPositionOnPostBack = true; //remember the scroll position on post back
            setUserName();
            Load_Notifications();
        }
        
        //Setting user name on header
        protected void setUserName()
        {
            try
            {
                String username = HttpContext.Current.User.Identity.Name;
                String query = "SELECT e.firstName, e.lastName, s.type FROM Employee e INNER JOIN SystemUser s ON e.empID=s.empID WHERE s.username='" + username + "'";
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                String output = "";
                while (dr.Read())
                {
                    if (dr["type"].ToString().Trim() == "admin")
                    {
                        output = dr["firstName"].ToString().Trim() + " " + dr["lastName"].ToString().Trim();
                    }
                    else
                    {
                        FormsAuthentication.SignOut();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You do not have access to this page. Please sign in to continue.'); window.location='" +
Request.ApplicationPath + "Login.aspx';", true);
                    }
                }
                userName.InnerHtml = output;
            }
            catch (SqlException exx)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write("setUserName:" + exx.ToString());
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
            query = "SELECT notID, type, a.name AS assetName, notContent, e.firstName, e.lastname, date, n.status " +
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
                output +=
                    "'>" +
                    "   <img class='col-md-3' src='img/" + dr["type"].ToString().Trim() + "Icon.png'/>" +
                    "   <div class='not-content-box col-md-10'>" +
                    "       Asset <strong>" + dr["assetName"].ToString().Trim() + "</strong> has been " + "recommended to " + dr["type"].ToString().Trim() +
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
                string insertion_Employee = "insert into Employee (empID, firstName, lastName, contactNo, email) values (@empid, @firstname, @lastname, @contact, @email)";
                SqlCommand cmd = new SqlCommand(insertion_Employee, conn);
                cmd.Parameters.AddWithValue("@empid", AddNewEmpID.InnerHtml);
                cmd.Parameters.AddWithValue("@firstname", AddNewFirstNameTextBox.Text);
                cmd.Parameters.AddWithValue("@lastname", AddNewLastNameTextBox.Text);
                cmd.Parameters.AddWithValue("@contact", AddNewContactTextBox.Text);
                cmd.Parameters.AddWithValue("@email", AddNewEmailTextBox.Text);

                cmd.ExecuteNonQuery();

                if (TypeDropDown.SelectedItem.Value != "owner")
                {
                    string insertion_User = "insert into SystemUser (empID, username, password, type) values (@empid, @username, @password, @type)";
                    cmd = new SqlCommand(insertion_User, conn);

                    String encriptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(AddNewPasswordTextBox.Text, "SHA1");

                    cmd.Parameters.AddWithValue("@empid", AddNewEmpID.InnerHtml);
                    cmd.Parameters.AddWithValue("@username", AddNewUsernameTextBox.Text);
                    cmd.Parameters.AddWithValue("@password", encriptedPassword);
                    cmd.Parameters.AddWithValue("@type", TypeDropDown.SelectedItem.Value);

                    cmd.ExecuteNonQuery();

                    //Sending email to the user with username and password
                    Boolean Email = SendEmail(AddNewEmailTextBox.Text, "Welcome to FixAMz", 
                        "Your username and password for FixAmz is as follows.\n\n" +
                        "Username - " + AddNewUsernameTextBox.Text + "\n" + 
                        "Password - " + Convert.ToString(AddNewPasswordTextBox.Text) + "\n\n" +
                        "Please login to your account and change your password as you prefer.\n\n" +
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

            string senderID = "sandyperera1993@gmail.com";// use sender’s email id here..
            const string senderPassword = "ucsc@123"; // sender password here…

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

        //Advanced user search
        protected void SearchUserBtn_Click(object sender, EventArgs e)
        {
            String empID = SearchEmployeeIDTextBox.Text.Trim();
            String firstname = SearchFirstNameTextBox.Text.Trim();
            String lastname = SearchLastNameTextBox.Text.Trim();
            String email = SearchEmailTextBox.Text.Trim();
            String contactNo = SearchContactTextBox.Text.Trim();
            //String username = SearchUsernameTextBox.Text.Trim();

            String resultMessage = "";

            String query = "SELECT * FROM Employee WHERE";
            if (empID != "")
            {
                query += " empID='" + empID + "'";
                resultMessage += empID + ", ";
            }
            if (firstname != "")
            {
                query += " AND firstname like '" + firstname + "%'";
                resultMessage += firstname + ", ";
            }
            if (lastname != "")
            {
                query += " AND lastname like '" + lastname + "%'";
                resultMessage += lastname + ", ";
            }
            if (email != "")
            {
                query += " AND email like '" + email + "%'";
                resultMessage += email + ", ";
            }
            if (contactNo != "")
            {
                query += " AND contactNo like '" + contactNo + "%'";
                resultMessage += contactNo + ", ";
            }

            // Clearing the grid view
            UserSearchGridView.DataSource = null;
            UserSearchGridView.DataBind();

            query = query.Replace("WHERE AND", "WHERE ");
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
                    responseMsgGreen.InnerHtml = "Search Results Found for <strong>" + resultMessage + "</strong>";
                }
                else
                {
                    responseBoxRed.Style.Add("display", "block");
                    responseMsgRed.InnerHtml = "No Results Found for <strong>" + resultMessage + "</strong>";
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

        protected void CancelSearchBtn_Click(object sender, EventArgs e)
        {
            var tbs = new List<TextBox>() { SearchEmployeeIDTextBox, SearchFirstNameTextBox, SearchLastNameTextBox, SearchEmailTextBox, SearchContactTextBox, SearchUsernameTextBox };
            foreach (var textBox in tbs)
            {
                textBox.Text = "";
                responseBoxGreen.Style.Add("display", "none");
                responseMsgGreen.InnerHtml = "";
                UserSearchGridView.Visible = false;
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
                    String query1 = "SELECT empID, firstName, lastName, contactNo, email FROM Employee WHERE empID='" + empID + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    while (dr1.Read())
                    {
                        UpdateEmpID.InnerHtml = dr1["empID"].ToString();
                        UpdateFirstNameTextBox.Text = dr1["firstName"].ToString();
                        UpdateLastNameTextBox.Text = dr1["lastName"].ToString();
                        UpdateContactTextBox.Text = dr1["contactNo"].ToString();
                        UpdateEmailTextBox.Text = dr1["email"].ToString();
                        
                    }
                    dr1.Close();
                    String query2 = "SELECT empID, username FROM SystemUser WHERE empID='" + empID + "'";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        UpdateUsernameTextBox.Text = dr2["username"].ToString();
                    }
                    dr2.Close();
                    /*
                    String query2 = "SELECT type FROM SystemUser WHERE empID='" + empID + "'";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    UpdateTypeDropDown.SelectedValue = "owner";
                    while (dr2.Read())
                    {
                        UpdateTypeDropDown.SelectedValue = dr2["type"].ToString();
                    }
                    dr2.Close();
                    */
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
                string insertion_Employee = "UPDATE Employee SET firstName = @firstname, lastName = @lastname, contactNo = @contact, email = @email WHERE empID='" + empID + "'";
                string insertion_SystemUser = "UPDATE SystemUser SET username = @username WHERE empID='" + empID + "'";
                SqlCommand cmd = new SqlCommand(insertion_Employee, conn);
                SqlCommand cmd1 = new SqlCommand(insertion_SystemUser, conn);

                

                cmd.Parameters.AddWithValue("@firstname", UpdateFirstNameTextBox.Text);
                cmd.Parameters.AddWithValue("@lastname", UpdateLastNameTextBox.Text);
                cmd.Parameters.AddWithValue("@contact", UpdateContactTextBox.Text);
                cmd.Parameters.AddWithValue("@email", UpdateEmailTextBox.Text);

                cmd1.Parameters.AddWithValue("@username", UpdateUsernameTextBox.Text);
                

                cmd1.ExecuteNonQuery();
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

