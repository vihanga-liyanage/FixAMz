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
namespace FixAMz_WebApplication
{
    public partial class AdminUserSystemTab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticate_User();
            setCatID();
            setLocID();
            setSubCategoryID();
            Load_Notifications();

            if (!IsPostBack)
            {
                setUserName();
                Load_Category();
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

        //Loading category dropdown
        protected void Load_Category()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT name, catID FROM Category", conn);
                SqlDataReader data = cmd.ExecuteReader();

                AddSubCategoryCategoryDropDown.DataSource = data;
                AddSubCategoryCategoryDropDown.DataTextField = "name";
                AddSubCategoryCategoryDropDown.DataValueField = "catID";
                AddSubCategoryCategoryDropDown.DataBind();
                AddSubCategoryCategoryDropDown.Items.Insert(0, new ListItem("-- Select a category --", ""));
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        //reload after click cancel button
        protected void cancel_clicked(object sender, EventArgs e)
        {
            Response.Redirect("AdminUserSystemTab.aspx");
        }

        //Signing out
        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        //Add new location
        protected void setLocID()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 locID FROM Location ORDER BY locID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newLocID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastLocID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastLocID[0]);
                    String temp = "";
                    for (int i = 1; i < lastLocID.Length; i++)
                    {
                        temp += Convert.ToString(lastLocID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newLocID = chr;
                    for (int i = 1; i < lastLocID.Length - temp.Length; i++)
                    {
                        newLocID += "0";
                    }
                    newLocID += temp;
                }
                else
                {
                    newLocID = "L00001";
                }

                AddNewLocID.InnerHtml = newLocID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void AddLocationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                string insertion_Location = "INSERT INTO Location (locID, name, address, contactNo) VALUES (@locid, @name, @address, @contactno)";
                SqlCommand cmd = new SqlCommand(insertion_Location, conn);
                cmd.Parameters.AddWithValue("@locid", AddNewLocID.InnerHtml);
                cmd.Parameters.AddWithValue("@name", AddLocationNameTextBox.Text);
                cmd.Parameters.AddWithValue("@address", AddLocationAddressTextBox.Text);
                cmd.Parameters.AddWithValue("@contactno", AddLocationContactTextBox.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addLocationClearAll", "addLocationClearAll();", true);
                setCatID();
                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Location " + AddLocationNameTextBox.Text + " added successfully!";

            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        //Update location
        protected void LocFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String locID = UpdateLocationIDTextBox.Text;

                string check = "select count(*) from Location WHERE locID='" + locID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT locID, name, address, contactNo FROM Location WHERE locID='" + locID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UpdateLocID.InnerHtml = dr["locID"].ToString();
                        UpdateLocNameTextBox.Text = dr["name"].ToString();
                        UpdateLocAddressTextBox.Text = dr["address"].ToString();
                        UpdateLocContactTextBox.Text = dr["contactNo"].ToString();

                    }
                    updatelocationInitState.Style.Add("display", "none");
                    updatelocationSecondState.Style.Add("display", "block");
                    UpdateLocationContent.Style.Add("display", "block");
                    UpdateLocationIDValidator.InnerHtml = "";
                }
                else
                {
                    updatelocationInitState.Style.Add("display", "block");
                    updatelocationSecondState.Style.Add("display", "none");
                    UpdateLocationContent.Style.Add("display", "block");
                    UpdateLocationIDValidator.InnerHtml = "Invalid Location ID";
                }
                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateLocationContent');", true);
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void UpdateLocBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String locID = UpdateLocationIDTextBox.Text;
                string insertion_Location = "UPDATE Location SET name = @name, address = @address, contactNo = @contact WHERE locID='" + locID + "'";

                SqlCommand cmd = new SqlCommand(insertion_Location, conn);

                cmd.Parameters.AddWithValue("@name", UpdateLocNameTextBox.Text);
                cmd.Parameters.AddWithValue("@address", UpdateLocAddressTextBox.Text);
                cmd.Parameters.AddWithValue("@contact", UpdateLocContactTextBox.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "updateLocationClearAll", "updateLocationClearAll();", true);

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Location '" + locID + "' updated successfully!";
                updatelocationInitState.Style.Add("display", "block");
                updatelocationSecondState.Style.Add("display", "none");
                UpdateLocationContent.Style.Add("display", "block");
                UpdateLocationIDTextBox.Text = "";

                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateLocationContent');", true);
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        //Add new category
        protected void setCatID()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 catID FROM Category ORDER BY catID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newCatID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastCatID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastCatID[0]);
                    String temp = "";
                    for (int i = 1; i < lastCatID.Length; i++)
                    {
                        temp += Convert.ToString(lastCatID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newCatID = chr;
                    for (int i = 1; i < lastCatID.Length - temp.Length; i++)
                    {
                        newCatID += "0";
                    }
                    newCatID += temp;
                }
                else
                {
                    newCatID = "C00001";
                }

                AddNewCatID.InnerHtml = newCatID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void AddCategoryBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                string insertion_Category = "insert into Category (catID, name) values (@catid, @name)";
                SqlCommand cmd = new SqlCommand(insertion_Category, conn);
                cmd.Parameters.AddWithValue("@catid", AddNewCatID.InnerHtml);
                cmd.Parameters.AddWithValue("@name", AddCategoryNameTextBox.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addCategoryClearAll", "addCategoryClearAll();", true);
                setCatID();
                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Category " + AddCategoryNameTextBox.Text + " added successfully!";

            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        //Update category
        protected void UpdateCategoryGoBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String catID = UpdateCategoryIDTextBox.Text;

                string check = "select count(*) from Category WHERE catID='" + catID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT catID, name FROM Category WHERE catID='" + catID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UpdateCatID.InnerHtml = dr["catID"].ToString();
                        UpdateCategoryNameTextBox.Text = dr["name"].ToString();
                    }
                    updateCategoryInitState.Style.Add("display", "none");
                    updateCategorySecondState.Style.Add("display", "block");
                    UpdateCategoryContent.Style.Add("display", "block");
                    UpdateCategoryIDValidator.InnerHtml = "";
                }
                else
                {
                    updateCategoryInitState.Style.Add("display", "block");
                    updateCategorySecondState.Style.Add("display", "none");
                    UpdateCategoryContent.Style.Add("display", "block");
                    UpdateCategoryIDValidator.InnerHtml = "Invalid Category ID";
                }
                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateCategoryContent');", true);

                //ClientScript.RegisterStartupScript(this.GetType(), "getKeys", "getKeys();", true);
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void UpdateCatBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String catID = UpdateCategoryIDTextBox.Text;
                String name = UpdateCategoryNameTextBox.Text;

                String insertion_Category = "UPDATE Category SET name = '" + name + "' WHERE catID='" + catID + "'";
                SqlCommand cmd = new SqlCommand(insertion_Category, conn);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "updateCategoryClearAll", "updateCategoryClearAll()", true);

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Category '" + name + "' updated successfully!";
                updateCategoryInitState.Style.Add("display", "block");
                updateCategorySecondState.Style.Add("display", "none");
                UpdateCategoryContent.Style.Add("display", "block");
                UpdateCategoryIDTextBox.Text = "";

                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateCategoryContent');", true);

                //ClientScript.RegisterStartupScript(this.GetType(), "getKeys", "getKeys();", true);
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        //Add new sub category
        protected void setSubCategoryID()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 scatID FROM SubCategory ORDER BY scatID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newEmpID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastEmpID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastEmpID.Substring(0, 2));
                    String temp = "";
                    for (int i = 2; i < lastEmpID.Length; i++)
                    {
                        temp += Convert.ToString(lastEmpID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newEmpID = chr;
                    for (int i = 2; i < lastEmpID.Length - temp.Length; i++)
                    {
                        newEmpID += "0";
                    }
                    newEmpID += temp;
                }
                else
                {
                    newEmpID = "SC00001";
                }

                AddSubCategoryID.InnerHtml = newEmpID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void AddSubCategoryBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                string insertion_Category = "insert into SubCategory (scatID, catID, name, depreciationRate, lifetime) values (@scatid, @catID, @name, @depre, @lifetime)";
                SqlCommand cmd = new SqlCommand(insertion_Category, conn);

                cmd.Parameters.AddWithValue("@scatid", AddSubCategoryID.InnerHtml);
                cmd.Parameters.AddWithValue("@catID", AddSubCategoryCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@name", AddSubCategoryNameTextBox.Text);
                cmd.Parameters.AddWithValue("@depre", Convert.ToInt16(AddSubCategoryDepreciationRateTextBox.Text));
                cmd.Parameters.AddWithValue("@lifetime", Convert.ToInt16(AddSubCategoryLifetimeTextBox.Text));

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addSubCategoryClearAll", "addSubCategoryClearAll();", true);
                setSubCategoryID();
                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Sub Category " + AddSubCategoryNameTextBox.Text + " added successfully!";
                Load_Category();
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        //Update sub category
        protected void UpdateSubCategoryFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String scatID = UpdateSubCategoryIDTextBox.Text;

                string check = "select count(*) from SubCategory WHERE scatID='" + scatID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {

                    String updateSubCategoryCategoryID = "";

                    String query = "SELECT scatID, catID, name, depreciationRate, lifetime FROM SubCategory WHERE scatID='" + scatID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UpdateScatID.InnerHtml = dr["scatID"].ToString();
                        UpdateScatNameTextBox.Text = dr["name"].ToString();
                        updateSubCategoryCategoryID = dr["catID"].ToString();
                        UpdateDepRateTextBox.Text = dr["depreciationRate"].ToString();
                        UpdateLifetimeTextBox.Text = dr["lifetime"].ToString();
                        
                    }
                    dr.Close();
                    // Get category name
                    String getCatNameQuery = "SELECT name FROM Category WHERE catID='" + updateSubCategoryCategoryID + "'";
                    cmd = new SqlCommand(getCatNameQuery, conn);
                    UpdateCategory.InnerHtml = cmd.ExecuteScalar().ToString();

                    updateSubCategoryInitState.Style.Add("display", "none");
                    updateSubCategorySecondState.Style.Add("display", "block");
                    UpdateSubCategoryContent.Style.Add("display", "block");
                    UpdateSubCategoryIDValidator.InnerHtml = "";
                }
                else
                {
                    updateSubCategoryInitState.Style.Add("display", "block");
                    updateSubCategorySecondState.Style.Add("display", "none");
                    UpdateSubCategoryContent.Style.Add("display", "block");
                    UpdateSubCategoryIDValidator.InnerHtml = "Invalid Sub Category ID";
                }
                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateSubCategoryContent');", true);
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void UpdateScatBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String scatID = UpdateSubCategoryIDTextBox.Text;
                String name = UpdateScatNameTextBox.Text;

                string insertion_SubCategory = "UPDATE SubCategory SET name = @name, depreciationRate = @depreciationRate, lifetime = @lifetime WHERE scatID='" + scatID + "'";
                SqlCommand cmd = new SqlCommand(insertion_SubCategory, conn);

                cmd.Parameters.AddWithValue("@name", UpdateScatNameTextBox.Text);
                cmd.Parameters.AddWithValue("@depreciationRate", UpdateDepRateTextBox.Text);
                cmd.Parameters.AddWithValue("@lifetime", UpdateLifetimeTextBox.Text);
                

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "updateSubCategoryClearAll", "updateSubCategoryClearAll()", true);

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Sub Category '" + scatID + "' updated successfully!";
                updateSubCategoryInitState.Style.Add("display", "block");
                updateSubCategorySecondState.Style.Add("display", "none");
                UpdateSubCategoryContent.Style.Add("display", "block");
                UpdateSubCategoryIDTextBox.Text = "";
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void CalDepreciationBtn_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                List<string> assetID = new List<string>();
                List<float> value = new List<float>();
                List<float> salvageValue = new List<float>();
                List<float> updatedValue = new List<float>();
                List<string> subcategory = new List<string>();
                List<DateTime> approvedDateTime = new List<DateTime>();

                String selectAsset = "SELECT * FROM Asset WHERE status = '1'";
                SqlCommand cmd = new SqlCommand(selectAsset, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    String a = dr.GetString(0);
                    assetID.Add(a);

                    float b = (float)dr.GetDouble(3);
                    value.Add(b);

                    float c = (float)dr.GetDouble(4);
                    salvageValue.Add(c);

                    float d = (float)dr.GetDouble(5);
                    updatedValue.Add(d);

                    /*float b = dr.GetFloat(3);
                    value.Add(b);

                    float c = dr.GetFloat(4);
                    salvageValue.Add(c);

                    float d = dr.GetFloat(5);
                    updatedValue.Add(d); */

                    String f = dr.GetString(7);
                    subcategory.Add(f);

                    DateTime g = dr.GetDateTime(11);
                    approvedDateTime.Add(g);

                }
                dr.Close();


                List<int> depRate = new List<int>();

                for (int i = 0; i < assetID.Count; i++)
                {
                    //Response.Write(subcategory[i]);
                    string getRate = "SELECT depreciationRate FROM SubCategory WHERE scatID='" + subcategory[i] + "'";
                    SqlCommand cmd2 = new SqlCommand(getRate, conn);
                    String getRateget = cmd2.ExecuteScalar().ToString().Trim();
                    SqlCommand cmd1 = new SqlCommand(getRate, conn);


                    //Response.Write(getRateget);

                    depRate.Add(Int32.Parse(getRateget));

                    DateTime localDate = DateTime.Now;
                    DateTime appDate = approvedDateTime[i];
                    int diffDays = (int)(localDate - appDate).TotalDays;
                    //Response.Write(localDate);
                    //Response.Write(appDate);
                    float yearDiff = (float)diffDays / 365;
                    /*Response.Write("days "+ diffDays.ToString());
                    Response.Write("years " + yearDiff.ToString());
                    Response.Write(value[i].ToString()+"/n");
                    Response.Write(salvageValue[i].ToString() + "/n");*/


                    float newRate = (float)(Convert.ToInt32(getRateget) / 100.0);

                    float upValue = (value[i] - salvageValue[i]) * (newRate);
                    float upValuefinal = (float)upValue * yearDiff;
                    upValuefinal = value[i] - upValuefinal;
                    upValuefinal = (float)Math.Round(upValuefinal, 2);
                    updatedValue[i] = upValuefinal;

                    //Response.Write("updated value " + upValuefinal.ToString() + "/n");
                    //Response.Write("updated value " + newRate.ToString() + "/n");


                    string newValue = "UPDATE Asset SET upDatedValue='" + updatedValue[i] + "'WHERE assetID='" + assetID[i] + "'";
                    SqlCommand cmd3 = new SqlCommand(newValue, conn);
                    cmd3.ExecuteNonQuery();

                    /*Response.Write(assetID.Count);
                    Response.Write(assetID[i]);

                    Response.Write(depRate[i]);
                    Response.Write(approvedDateTime[i]);*/

                }
                conn.Close();

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
