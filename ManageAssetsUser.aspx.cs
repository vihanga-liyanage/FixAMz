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
    public partial class ManaageAssetsUser : System.Web.UI.Page
    {
        private string costID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticate_User();
            costCenter();
            Load_Notifications();
            setAssetID();
            personToRecommend();
            setNavBar();
            
            if (!Page.IsPostBack)
            {
                setUserName();
                setCostCenterName();
                Load_Category();
                setAssetID();
                Load_Location();
                Load_Employee_Data();
                Load_CostCenter();

                TransferAssetIDTextBox.Text = "NWSDB/" + costID + "/";
                UpgradeAssetIDTextBox.Text = "NWSDB/" + costID + "/";
                DisposeAssetIDTextBox.Text = "NWSDB/" + costID + "/";
                Page.MaintainScrollPositionOnPostBack = true;
            }
            
            responseBoxGreen.Style.Add("display", "none");
            responseMsgGreen.InnerHtml = "";
            responseBoxRed.Style.Add("display", "none");
            responseMsgRed.InnerHtml = "";

            //Can give an alert
            //System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"" + Session["PRSN_TO_REC"] + "\")</SCRIPT>");
        }

        //set costID by user login
        protected void costCenter() {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');
            costID = data[2];
            Session["COST_ID_MNG_ASST"] = data[2];
        }

        //set personToRecommend according to costID
        protected void personToRecommend() {
            String query = "SELECT e.empID, e.firstName, e.lastName FROM Employee e INNER JOIN CostCenter c ON e.empID = c.recommendPerson WHERE c.costID='" + Session["COST_ID_MNG_ASST"] + "'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            String recoPrsn = "";
            while (dr.Read()) {
                recoPrsn = dr["firstName"].ToString().Trim() + " " + dr["lastName"].ToString().Trim();
            }
             
            AddAssetPersonToRecommend.InnerHtml = recoPrsn;
            TransAssetSendForRecommend.InnerHtml = recoPrsn;
            UpgradeAssetPersonToRecommend.InnerHtml = recoPrsn;
            DisposeAssetPersonToRecommend.InnerHtml = recoPrsn;
            dr.Close();
            conn.Close();

            String query2 = "SELECT recommendPerson, approvePerson FROM CostCenter WHERE CostID='" + Session["COST_ID_MNG_ASST"] + "'";
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn2.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn2);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                Session["PRSN_TO_REC"] = dr2["recommendPerson"].ToString().Trim();
                Session["PRSN_TO_APP"] = dr2["approvePerson"].ToString().Trim();
            }
            dr2.Close();
            conn2.Close();
        }

        //Checking if the user has access to the page
        protected void Authenticate_User()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');


            if ((data[1] != "manageAssetUser") && (data[1] != "manageReport"))
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
            else if (data[1] == "manageAssetUser")
            {
                manageAssetUserNavBar.Style.Add("display", "block");
            }
        }

        // Signing out =================================================================
        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        //Reads the last notID from DB, calculates the next=============================
        protected String setNotID()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 notID FROM Notification ORDER BY notID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newNotID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastNotID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastNotID[0]);
                    String temp = "";
                    for (int i = 1; i < lastNotID.Length; i++)
                    {
                        temp += Convert.ToString(lastNotID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newNotID = chr;
                    for (int i = 1; i < lastNotID.Length - temp.Length; i++)
                    {
                        newNotID += "0";
                    }
                    newNotID += temp;
                    conn.Close();
                    return newNotID;
                }
                else
                {
                    newNotID = "N00001";
                    conn.Close();
                    return newNotID;
                }
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
                return "";
            }
        }

        // Loading data to drop downs ==================================================
        //Loading sub category dropdown
        protected void Load_SubCategory_for_register()
        {
            try
            {
                String cateID = AddAssetCategoryDropDown.SelectedValue;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT name, scatID FROM SubCategory where catID='" + cateID + "'", conn);
                SqlDataReader data = cmd.ExecuteReader();
                
                AddAssetSubCategoryDropDown.DataSource = data;
                AddAssetSubCategoryDropDown.DataTextField = "name";
                AddAssetSubCategoryDropDown.DataValueField = "scatID";
                AddAssetSubCategoryDropDown.DataBind();
                AddAssetSubCategoryDropDown.Items.Insert(0, new ListItem("-- Select a sub category--", ""));
                data.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Load_SubCategory_for_register:" + ex.Message.ToString());
            }
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

                AddAssetCategoryDropDown.DataSource = data;
                AddAssetCategoryDropDown.DataTextField = "name";
                AddAssetCategoryDropDown.DataValueField = "catID";
                AddAssetCategoryDropDown.DataBind();
                AddAssetCategoryDropDown.Items.Insert(0, new ListItem("-- Select a category --", ""));
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Load_Category:" + ex.Message.ToString());
            }
        }

        //Loading location dropdown
        protected void Load_Location()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT cl.locID, l.name FROM CostLocation cl INNER JOIN Location l ON cl.locID = l.locID WHERE costID='" + Session["COST_ID_MNG_ASST"] + "'", conn);
                SqlDataReader data = cmd.ExecuteReader();

                AddAssetLocationDropDown.DataSource = data;
                AddAssetLocationDropDown.DataTextField = "name";
                AddAssetLocationDropDown.DataValueField = "locID";
                AddAssetLocationDropDown.DataBind();
                AddAssetLocationDropDown.Items.Insert(0, new ListItem("-- Select a location --", ""));
                data.Close();

                //Transfer asset location drop down
                data = cmd.ExecuteReader();
                TransferLocationDropDown.DataSource = data;
                TransferLocationDropDown.DataTextField = "name";
                TransferLocationDropDown.DataValueField = "locID";
                TransferLocationDropDown.DataBind();
                //TransferLocationDropDown.Items.Insert(0, new ListItem("-- Select a sub location --", ""));
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Load_Location_Data:" + ex.Message.ToString());
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

                TransferCostCeneterDropDown.DataSource = data;
                TransferCostCeneterDropDown.DataTextField = "name";
                TransferCostCeneterDropDown.DataValueField = "costID";
                TransferCostCeneterDropDown.DataBind();
                //TransferCostCeneterDropDown.Items.Insert(0, new ListItem("-- Select a Cost Center --", ""));
                data.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        //Loading employee data
        protected void Load_Employee_Data()
        {
            try
            {
                
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT [firstname]+' '+[lastname] AS [name], empID FROM Employee WHERE costID='" + Session["COST_ID_MNG_ASST"] + "'", conn);
                SqlDataReader data = cmd.ExecuteReader();
                
                //Register new asset owner drop down
                AddAssetOwnerDropDown.DataSource = data;
                AddAssetOwnerDropDown.DataTextField = "name";
                AddAssetOwnerDropDown.DataValueField = "empID";
                AddAssetOwnerDropDown.DataBind();
                AddAssetOwnerDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();

                //Transfer asset owner drop down
                data = cmd.ExecuteReader();
                TransferOwnerDropDown.DataSource = data;
                TransferOwnerDropDown.DataTextField = "name";
                TransferOwnerDropDown.DataValueField = "empID";
                TransferOwnerDropDown.DataBind();
                //TransferOwnerDropDown.Items.Insert(0, new ListItem("-- Select an owner --", ""));
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Load_Employee_Data:" + ex.Message.ToString());
            }
        }

        //reload after click cancel button
        protected void cancel_clicked(object sender, EventArgs e)
        {
            Response.Redirect("ManageAssetsUser.aspx");
        }

// Regester new asset ==========================================================
        protected void setAssetID() 
        {
            //Getting cost center
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            string[] data = userData.Split(';');
            string costID = data[2];

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 assetID FROM Asset WHERE costID='" + costID + "' ORDER BY assetID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newAssetID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastAssetID = (cmd.ExecuteScalar().ToString()).Trim();
                    String prefix = "NWSDB/" + costID + "/A";
                    //Extracting the number
                    String num = Convert.ToString(lastAssetID.Substring(13));
                    //Adding one

                    newAssetID = Convert.ToString(Convert.ToInt16(num) + 1);
                    while (newAssetID.Length < 7)
                    {
                        newAssetID = "0" + newAssetID;
                    }
                    newAssetID = prefix + newAssetID;
                }
                else
                {
                    newAssetID = "NWSDB/" + costID + "/A0000001";
                }

                AddNewAssetId.InnerHtml = newAssetID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write("setAssetID:" + e.ToString());
            }
        }

        protected void setCostCenterName()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT name FROM CostCenter WHERE costID='" + costID + "'", conn);
            String costCenterName = (cmd.ExecuteScalar().ToString()).Trim();
            AddNewCostID.InnerHtml = costCenterName;
            conn.Close();
        }

        protected void Category_Selected_for_register(object sender, EventArgs e)
        {
            Load_SubCategory_for_register();

            AddNewAssetContent.Style.Add("display", "block");
            //updating expandingItems dictionary in javascript
            ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AddNewAssetContent');", true);
        }

        protected void AddAssetRecommendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String username = HttpContext.Current.User.Identity.Name;
                String getUserIDQuery = "SELECT empID FROM SystemUser WHERE username='" + username + "'";
                SqlCommand cmd = new SqlCommand(getUserIDQuery, conn);
                String empID = (cmd.ExecuteScalar().ToString()).Trim();

                string insertion_Asset = "insert into Asset (assetID, costID, name, value, salvageValue, updatedValue, category, subcategory, owner, status, location, recommend) values (@assetid, @costid, @name, @value, @salvageValue, @updatedValue, @category, @subcategory,@owner, @status, @location, @recommend)";
                cmd = new SqlCommand(insertion_Asset, conn);
                cmd.Parameters.AddWithValue("@assetid", AddNewAssetId.InnerHtml);
                cmd.Parameters.AddWithValue("@costid", costID);
                cmd.Parameters.AddWithValue("@name", RegisterAssetNameTextBox.Text);
                cmd.Parameters.AddWithValue("@value", AddValueTextBox.Text);
                cmd.Parameters.AddWithValue("@salvageValue", AddSalvageValueTextBox.Text);
                cmd.Parameters.AddWithValue("@updatedValue", AddValueTextBox.Text);
                cmd.Parameters.AddWithValue("@category", AddAssetCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@subcategory", AddAssetSubCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", AddAssetOwnerDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@status", 0);
                cmd.Parameters.AddWithValue("@location", AddAssetLocationDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@recommend", Session["PRSN_TO_REC"]);
                cmd.ExecuteNonQuery();
                
                String notID = setNotID();
                String insertDisposeAsset = "INSERT INTO Notification (notID, type, action, assetID, notContent, sendUser, receiveUser, status) VALUES (@notid, @type, @action, @assetid, @notContent, @senduser, @receiveuser, @status)";
                cmd = new SqlCommand(insertDisposeAsset, conn);

                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "AddNew");
                cmd.Parameters.AddWithValue("@action", "Recommend");
                cmd.Parameters.AddWithValue("@assetid", AddNewAssetId.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", empID);
                cmd.Parameters.AddWithValue("@receiveuser", Session["PRSN_TO_REC"]);
                cmd.Parameters.AddWithValue("@status", "not-seen");

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addNewAssetClearAll", "addNewAssetClearAll();", true);
                setAssetID();
                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Asset sent for recommendation!";

            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        // Upgrade asset ===============================================================
        protected void UpgradeAssetFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = UpgradeAssetIDTextBox.Text;

                string check = "SELECT count(*) from Asset WHERE assetID='" + assetID + "'AND status='1'";
                string getassetid = "select count(*) from Notification WHERE assetID='" + assetID + "' AND action='Recommend' ";
                SqlCommand cmd = new SqlCommand(check, conn);
                SqlCommand cmd1 = new SqlCommand(getassetid, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                int res1 = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
                if (res == 1)
                {
                    if (res1 == 0)
                    {
                        String query = "SELECT name, category, subcategory, owner, updatedValue FROM Asset WHERE assetID='" + assetID + "'";
                        String query2 = "SELECT depreciationRate, lifetime FROM Asset WHERE assetID='" + assetID + "'";
                        cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();

                        String UpgradeAssetCategoryID = "", UpgradeAssetSubcategoryID = "", UpgradeOwnerID = "";
                        while (dr.Read())
                        {
                            UpgradeAssetName.InnerHtml = dr["name"].ToString();
                            UpgradeAssetCategoryID = dr["category"].ToString();
                            UpgradeAssetSubcategoryID = dr["subcategory"].ToString();
                            //  UpgradeLocationID = dr["location"].ToString();
                            UpgradeOwnerID = dr["owner"].ToString();
                            UpgradeValue.InnerHtml = dr["updatedValue"].ToString();
                        }
                        dr.Close();
                        // Get category name
                        String getCatNameQuery = "SELECT name FROM Category WHERE catID='" + UpgradeAssetCategoryID + "'";
                        cmd = new SqlCommand(getCatNameQuery, conn);
                        UpgradeAssetCategory.InnerHtml = cmd.ExecuteScalar().ToString();
                        // Get sub category name
                        String getSubCatNameQuery = "SELECT name FROM SubCategory WHERE scatID='" + UpgradeAssetSubcategoryID + "'";
                        cmd = new SqlCommand(getSubCatNameQuery, conn);
                        UpgradeAssetSubcategory.InnerHtml = cmd.ExecuteScalar().ToString();
                        /*      // Get location name
                              String getLocNameQuery = "SELECT name FROM Location WHERE locID='" + UpgradeLocationID + "'";
                              cmd = new SqlCommand(getLocNameQuery, conn);
                              UpgradeLocation.InnerHtml = cmd.ExecuteScalar().ToString();*/
                        // Get owner name
                        String getOwnerNameQuery = "SELECT [firstname]+' '+[lastname] FROM Employee WHERE empID='" + UpgradeOwnerID + "'";
                        cmd = new SqlCommand(getOwnerNameQuery, conn);
                        UpgradeOwner.InnerHtml = cmd.ExecuteScalar().ToString();

                        upgradeAssetInitState.Style.Add("display", "none");
                        upgradeAssetSecondState.Style.Add("display", "block");
                        UpgradeAssetContent.Style.Add("display", "block");
                        UpgradeAssetIDValidator.InnerHtml = "";
                    }
                    else
                    {
                        string gettype = "select type from Notification WHERE assetID='" + assetID + "' AND action='Recommend' ";
                        SqlCommand cmdtype = new SqlCommand(gettype, conn);
                        string type = cmdtype.ExecuteScalar().ToString();
                        cmdtype.ExecuteNonQuery();

                        upgradeAssetInitState.Style.Add("display", "block");
                        upgradeAssetSecondState.Style.Add("display", "none");
                        UpgradeAssetContent.Style.Add("display", "block");
                        UpgradeAssetIDValidator.InnerHtml = "Asset already recommended to " + type + "!";
                        TransferItemName.Focus();
                    }

                    conn.Close();
                    //updating expandingItems dictionary in javascript
                    ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('TransferAssetContent');", true);
                }
                else
                {
                    upgradeAssetInitState.Style.Add("display", "block");
                    upgradeAssetSecondState.Style.Add("display", "none");
                    UpgradeAssetContent.Style.Add("display", "block");
                    UpgradeAssetIDValidator.InnerHtml = "Invalid asset ID";
                }
                
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        protected String setUpID() //Reads the last upID from DB, calculates the next.
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 upID FROM UpgradeAsset ORDER BY upID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newUpID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastUpID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastUpID[0]);
                    String temp = "";
                    for (int i = 1; i < lastUpID.Length; i++)
                    {
                        temp += Convert.ToString(lastUpID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newUpID = chr;
                    for (int i = 1; i < lastUpID.Length - temp.Length; i++)
                    {
                        newUpID += "0";
                    }
                    newUpID += temp;
                    conn.Close();
                    return newUpID;
                }
                else
                {
                    newUpID = "U00001";
                    conn.Close();
                    return newUpID;
                }
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
                return "";
            }
        }

        protected void UpgradeAssetRecommendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();


                // Getting logged in user's ID
                String username = HttpContext.Current.User.Identity.Name;
                String getUserIDQuery = "SELECT empID FROM SystemUser WHERE username='" + username + "'";
                SqlCommand cmd = new SqlCommand(getUserIDQuery, conn);
                String empID = (cmd.ExecuteScalar().ToString()).Trim();
                String notID = setNotID();
                String insertUpgradeAsset = "INSERT INTO Notification (notID, type, action, assetID, notContent, sendUser, receiveUser, status) VALUES (@notid, @type, @action, @assetid, @notcontent, @senduser, @receiveuser, @status)";
                cmd = new SqlCommand(insertUpgradeAsset, conn);

                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "Update");
                cmd.Parameters.AddWithValue("@action", "Recommend");
                cmd.Parameters.AddWithValue("@assetid", UpgradeAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@notcontent", UpgradeAssetDescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@senduser", empID);
                cmd.Parameters.AddWithValue("@receiveuser", Session["PRSN_TO_REC"]);
                cmd.Parameters.AddWithValue("@status", "not-seen");

                cmd.ExecuteNonQuery();



                String insertUpgradeAsset_UpgradeAsset = "INSERT INTO UpgradeAsset (upID, assetID, value, updatedValue, description, recommend, status) VALUES (@upid, @assetid, @value, @updatedValue, @description, @recommend, @status)";


                cmd = new SqlCommand(insertUpgradeAsset_UpgradeAsset, conn);

                cmd.Parameters.AddWithValue("@upid", setUpID());
                cmd.Parameters.AddWithValue("@assetid", UpgradeAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@value", UpgradeValue.InnerHtml);
                cmd.Parameters.AddWithValue("@updatedValue", UpgradeAssetValueTextBox.Text);
                cmd.Parameters.AddWithValue("@description", UpgradeAssetDescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@recommend", Session["PRSN_TO_REC"]);
                cmd.Parameters.AddWithValue("@status", "pending");
                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "upgradeAssetClearAll", "upgradeAssetClearAll();", true);

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Asset '" + UpgradeAssetIDTextBox.Text + "' recommended!";

                upgradeAssetInitState.Style.Add("display", "block");
                upgradeAssetSecondState.Style.Add("display", "none");
                UpgradeAssetContent.Style.Add("display", "block");
                UpgradeAssetIDTextBox.Text = ""; 
                UpgradeAssetIDValidator.InnerHtml = "";
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpgradeAssetContent');", true);

            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        // Transfer asset ==============================================================
        protected String setTransAssetID()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 transID FROM TransferAsset ORDER BY transID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newTransAssetID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastTransAssetID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastTransAssetID[0]) + Convert.ToString(lastTransAssetID[1]);
                    String temp = "";
                    for (int i = 2; i < lastTransAssetID.Length; i++)
                    {
                        temp += Convert.ToString(lastTransAssetID[i]);
                    }

                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newTransAssetID = chr;
                    for (int i = 2; i < lastTransAssetID.Length - temp.Length; i++)
                    {
                        newTransAssetID += "0";
                    }
                    newTransAssetID += temp;
                    conn.Close();
                    return newTransAssetID;
                }
                else
                {
                    newTransAssetID = "TA00001";
                    conn.Close();
                    return newTransAssetID;
                }

                //AddNewAssetId.InnerHtml = newTransAssetID;          
                
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
                return "";
            }

        }

        protected void TransferAssetFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = TransferAssetIDTextBox.Text;

                string check = "select count(*) from Asset WHERE assetID='" + assetID + "' AND status='1'";
                string getassetid = "select count(*) from Notification WHERE assetID='" + assetID + "' AND action='Recommend' ";
                SqlCommand cmd = new SqlCommand(check, conn);
                SqlCommand cmd1 = new SqlCommand(getassetid, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                int res1 = Convert.ToInt32(cmd1.ExecuteScalar().ToString());

                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                if (res == 1)
                {
                    if (res1 == 0)
                    {
                        String transferAssetCategoryID = "";
                        String transferAssetSubCategoryID = "";
                        String transferAssetCostID = "";
                        String transferAssetLocationID = "";
                        String transferAssetOwnerID = "";

                        String query = "SELECT assetID, costID, name, category, subcategory, location, owner, updatedValue FROM Asset WHERE assetID='" + assetID + "'";
                        cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            //DisposeAssetID.InnerHtml = dr["assetID"].ToString();
                            TransferItemName.InnerHtml = dr["name"].ToString();
                            transferAssetCategoryID = dr["category"].ToString();
                            transferAssetSubCategoryID = dr["subcategory"].ToString();
                            transferAssetCostID = dr["costID"].ToString();
                            transferAssetLocationID = dr["location"].ToString();
                            transferAssetOwnerID = dr["owner"].ToString();
                            TransferValue.InnerHtml = dr["updatedValue"].ToString() + " LKR";
                        }
                        dr.Close();
                        // Get category name
                        String getCatNameQuery = "SELECT name FROM Category WHERE catID='" + transferAssetCategoryID + "'";
                        cmd = new SqlCommand(getCatNameQuery, conn);
                        TransferCategory.InnerHtml = cmd.ExecuteScalar().ToString();
                        // Get sub category name
                        String getSubCatNameQuery = "SELECT name FROM SubCategory WHERE scatID='" + transferAssetSubCategoryID + "'";
                        cmd = new SqlCommand(getSubCatNameQuery, conn);
                        TransferSubcategory.InnerHtml = cmd.ExecuteScalar().ToString();
                        //Get location name
                        TransferLocationDropDown.SelectedValue = transferAssetLocationID;
                        //Get owner name
                        TransferOwnerDropDown.SelectedValue = transferAssetOwnerID;
                        //get costID
                        TransferCostCeneterDropDown.SelectedValue = transferAssetCostID;

                        transferAssetInitState.Style.Add("display", "none");
                        transferAssetSecondState.Style.Add("display", "block");
                        TransferAssetContent.Style.Add("display", "block");
                        TransferAssetIDValidator.InnerHtml = "";
                        TransferAssetIDTextBox.Focus();
                    }
                    else
                    {
                        string gettype = "select type from Notification WHERE assetID='" + assetID + "' AND action='Recommend' ";
                        SqlCommand cmdtype = new SqlCommand(gettype, conn);
                        string type = cmdtype.ExecuteScalar().ToString();
                        cmdtype.ExecuteNonQuery();

                        transferAssetInitState.Style.Add("display", "block");
                        transferAssetSecondState.Style.Add("display", "none");
                        TransferAssetContent.Style.Add("display", "block");
                        TransferAssetIDValidator.InnerHtml = "Asset already recommended to "+ type +"!";
                        TransferItemName.Focus();
                    }

                    conn.Close();
                    //updating expandingItems dictionary in javascript
                    ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('TransferAssetContent');", true);
                }
                else
                {
                    transferAssetInitState.Style.Add("display", "block");
                    transferAssetSecondState.Style.Add("display", "none");
                    TransferAssetContent.Style.Add("display", "block");
                    TransferAssetIDValidator.InnerHtml = "Asset ID not found!";
                    TransferItemName.Focus();
                }
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void TransferAssetRecommendBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String username = HttpContext.Current.User.Identity.Name;
                String getUserIDQuery = "SELECT empID FROM SystemUser WHERE username='" + username + "'";
                SqlCommand cmd = new SqlCommand(getUserIDQuery, conn);
                String empID = (cmd.ExecuteScalar().ToString()).Trim();

                String transID = setTransAssetID();
                String notID = setNotID();
                string insertion_Asset_to_transferAsset = "INSERT INTO TransferAsset (transID, assetID, costID, type, status, location, owner, recommend) VALUES (@transid, @assetid, @costID, @type, @status, @location, @owner, @recommend)";
                string insertion_Asset_to_notification = "INSERT INTO Notification (notID, assetID, type, action, notContent, sendUser, receiveUser, status) VALUES (@notid, @nAssetid, @nType, @action, @nNotContent, @nSendUser, @nReceiveUser, @nStatus)";
                cmd = new SqlCommand(insertion_Asset_to_transferAsset, conn);
                SqlCommand cmd2 = new SqlCommand(insertion_Asset_to_notification, conn);

                cmd.Parameters.AddWithValue("@transid", transID);
                cmd.Parameters.AddWithValue("@assetid", TransferAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@costID", TransferCostCeneterDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@type", "0");
                cmd.Parameters.AddWithValue("@status", "pendding");
                cmd.Parameters.AddWithValue("@location", TransferLocationDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", TransferOwnerDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@recommend", Session["PRSN_TO_REC"]);
                cmd.ExecuteNonQuery();

                cmd2.Parameters.AddWithValue("@notid", notID);
                cmd2.Parameters.AddWithValue("@nAssetid", TransferAssetIDTextBox.Text);
                cmd2.Parameters.AddWithValue("@action", "Recommend");
                cmd2.Parameters.AddWithValue("@nType", "Transfer");
                cmd2.Parameters.AddWithValue("@nNotContent", "0");
                cmd2.Parameters.AddWithValue("@nSendUser", empID);
                cmd2.Parameters.AddWithValue("@nReceiveUser", Session["PRSN_TO_REC"]);
                //cmd2.Parameters.AddWithValue("@nDate", get);
                cmd2.Parameters.AddWithValue("@nStatus", "not-seen");
                cmd2.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "transferClearAll", "transferClearAll();", true);

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Asset '" + TransferAssetIDTextBox.Text + "' sent for recommendation!";
                TransferAssetIDTextBox.Text = "";
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        // Dispose asset ===============================================================

        protected void DisposeAssetFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = DisposeAssetIDTextBox.Text;

                string check = "select count(*) from Asset WHERE assetID='" + assetID + "'";
                string getassetid = "select count(*) from Notification WHERE assetID='" + assetID + "' and action='Recommend'";
                SqlCommand cmd = new SqlCommand(check, conn);
                SqlCommand cmd1 = new SqlCommand(getassetid, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                int res1 = Convert.ToInt32(cmd1.ExecuteScalar().ToString());

                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                if (res == 1)
                {
                    if (res1 == 0)
                    {
                        String disposeAssetCategoryID = "";
                        String disposeAssetSubCategoryID = "";
                        String disposeAssetLocationID = "";
                        String disposeAssetOwnerID = "";

                        String query = "SELECT assetID, name, category, subcategory,location, owner, updatedValue FROM Asset WHERE assetID='" + assetID + "'";
                        cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            DisposeAssetID.InnerHtml = dr["assetID"].ToString();
                            DisposeItemName.InnerHtml = dr["name"].ToString();
                            disposeAssetCategoryID = dr["category"].ToString();
                            disposeAssetSubCategoryID = dr["subcategory"].ToString();
                            disposeAssetLocationID = dr["location"].ToString();
                            disposeAssetOwnerID = dr["owner"].ToString();
                            DisposeValue.InnerHtml = dr["updatedValue"].ToString() + " LKR";
                        }
                        dr.Close();
                        // Get category name
                        String getCatNameQuery = "SELECT name FROM Category WHERE catID='" + disposeAssetCategoryID + "'";
                        cmd = new SqlCommand(getCatNameQuery, conn);
                        DisposeCategory.InnerHtml = cmd.ExecuteScalar().ToString();
                        // Get sub category name
                        String getSubCatNameQuery = "SELECT name FROM SubCategory WHERE scatID='" + disposeAssetSubCategoryID + "'";
                        cmd = new SqlCommand(getSubCatNameQuery, conn);
                        DisposeSubCategory.InnerHtml = cmd.ExecuteScalar().ToString();
                        // Get location name
                        String getLocationNameQuery = "SELECT name FROM Location WHERE locID='" + disposeAssetLocationID + "'";
                        cmd = new SqlCommand(getLocationNameQuery, conn);
                        DisposeLocation.InnerHtml = cmd.ExecuteScalar().ToString();
                        // Get owner name
                        String getOwnerNameQuery = "SELECT [firstname] + ' ' + [lastname] FROM Employee WHERE empID='" + disposeAssetOwnerID + "'";
                        cmd = new SqlCommand(getOwnerNameQuery, conn);
                        DisposeOwner.InnerHtml = cmd.ExecuteScalar().ToString();

                        disposeAssetInitState.Style.Add("display", "none");
                        disposeAssetSecondState.Style.Add("display", "block");
                        DisposeAssetContent.Style.Add("display", "block");
                        DisposeAssetIDValidator.InnerHtml = "";
                    }
                    else
                    {
                        string gettype = "select type from Notification WHERE assetID='" + assetID + "' AND action='Recommend' ";
                        SqlCommand cmdtype = new SqlCommand(gettype, conn);
                        string type = cmdtype.ExecuteScalar().ToString();
                        cmdtype.ExecuteNonQuery();

                        disposeAssetInitState.Style.Add("display", "block");
                        disposeAssetSecondState.Style.Add("display", "none");
                        DisposeAssetContent.Style.Add("display", "block");
                        DisposeAssetIDValidator.InnerHtml = "Asset already recommended to "+type+"!";
                    }

                    conn.Close();
                    //updating expandingItems dictionary in javascript
                    ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('DisposeAssetContent');", true);
                }
                else
                {
                    disposeAssetInitState.Style.Add("display", "block");
                    disposeAssetSecondState.Style.Add("display", "none");
                    DisposeAssetContent.Style.Add("display", "block");
                    DisposeAssetIDValidator.InnerHtml = "Asset ID not found!";
                }
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void DisposeAssetRecommendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                // Getting logged in user's ID
                String username = HttpContext.Current.User.Identity.Name;
                String getUserIDQuery = "SELECT empID FROM SystemUser WHERE username='" + username + "'";
                SqlCommand cmd = new SqlCommand(getUserIDQuery, conn);
                String empID = (cmd.ExecuteScalar().ToString()).Trim();
                String notID = setNotID();
                String insertDisposeAsset = "INSERT INTO Notification (notID, type, action, assetID, notContent, sendUser, receiveUser, status) VALUES (@notid, @type, @action, @assetid, @notcontent, @senduser, @receiveuser, @status)";
                cmd = new SqlCommand(insertDisposeAsset, conn);

                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "Delete");
                cmd.Parameters.AddWithValue("@action", "Recommend");
                cmd.Parameters.AddWithValue("@assetid", DisposeAssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notcontent", DisposeAssetDescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@senduser", empID);
                cmd.Parameters.AddWithValue("@receiveuser", Session["PRSN_TO_REC"]);
                cmd.Parameters.AddWithValue("@status", "not-seen");

                cmd.ExecuteNonQuery();

                conn.Close();

                responseBoxGreen.Style.Add("display", "block");
                responseMsgGreen.InnerHtml = "Asset '" + DisposeAssetIDTextBox.Text + "' sent for recommendation.";

                disposeAssetInitState.Style.Add("display", "block");
                disposeAssetSecondState.Style.Add("display", "none");
                DisposeAssetContent.Style.Add("display", "block");
                DisposeAssetIDValidator.InnerHtml = "";
                DisposeAssetIDTextBox.Text = "";

                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('DisposeAssetContent');", true);
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        public string SelectedValue { get; set; }
    }
}