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

namespace FixAMz_WebApplication
{
    public partial class AdminUserPeopleTab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setEmpID();
            Load_CostCenter();
            responseBoxGreen.Style.Add("display", "none");
            responseMsgGreen.InnerHtml = "";
            responseBoxRed.Style.Add("display", "none");
            responseMsgRed.InnerHtml = "";
            Page.MaintainScrollPositionOnPostBack = true; //remember the scroll position on post back
            setUserName();
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
                AddUserCostNameDropDown.Items.Insert(0, new ListItem("-- Select a CostCenter --", ""));
                data.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        //Setting user name on header
        protected void setUserName()
        {
            try
            {
                String username = HttpContext.Current.User.Identity.Name;
                String query = "SELECT Employee.firstName, Employee.lastName FROM Employee INNER JOIN SystemUser ON Employee.empID=SystemUser.empID WHERE SystemUser.username='" + username + "'";
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                String output = "";
                while (dr.Read())
                {
                    output = dr["firstName"].ToString() + " " + dr["lastName"].ToString();
                }
                userName.InnerHtml = output;
            }
            catch (SqlException exx)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(exx.ToString());
            }
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
                    string insertion_User = "insert into SystemUser (empID, costID, username, password, type) values (@empid, @costID, @username, @password, @type)";
                    cmd = new SqlCommand(insertion_User, conn);

                    String encriptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(AddNewPasswordTextBox.Text, "SHA1");

                    cmd.Parameters.AddWithValue("@empid", AddNewEmpID.InnerHtml);
                    cmd.Parameters.AddWithValue("@costID", AddUserCostNameDropDown.SelectedValue);
                    cmd.Parameters.AddWithValue("@username", AddNewUsernameTextBox.Text);
                    cmd.Parameters.AddWithValue("@password", encriptedPassword);
                    cmd.Parameters.AddWithValue("@type", TypeDropDown.SelectedItem.Value);

                    cmd.ExecuteNonQuery();
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
                SqlCommand cmd = new SqlCommand(insertion_Employee, conn);

                cmd.Parameters.AddWithValue("@firstname", UpdateFirstNameTextBox.Text);
                cmd.Parameters.AddWithValue("@lastname", UpdateLastNameTextBox.Text);
                cmd.Parameters.AddWithValue("@contact", UpdateContactTextBox.Text);
                cmd.Parameters.AddWithValue("@email", UpdateEmailTextBox.Text);

                cmd.ExecuteNonQuery();

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
                    string ini = "0";
                    while (dr.Read())
                    {
                        DeleteEmpID.InnerHtml = dr["empID"].ToString();
                        DeleteFirstName.InnerHtml = dr["firstName"].ToString();
                        DeleteLastName.InnerHtml = dr["lastName"].ToString();
                        DeleteContact.InnerHtml = ini + dr["contactNo"].ToString();
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
