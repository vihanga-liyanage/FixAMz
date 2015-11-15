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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Load_Category();
                Load_Location();
                Load_Employee_Data();
                setUserName();
                setAssetID();
                Page.MaintainScrollPositionOnPostBack = true;
            }
            Load_Notifications();
            responseBoxGreen.Style.Add("display", "none");
            responseMsgGreen.InnerHtml = "";
            responseBoxRed.Style.Add("display", "none");
            responseMsgRed.InnerHtml = "";
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
            query = "SELECT notID, type, assetID, notContent, sendUser, date, status FROM Notification WHERE receiveUser='" + empID + "' ORDER BY date DESC";
            cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;
            String output = "";
            while (dr.Read())
            {
                output +=
                    "<a href='" + dr["notID"].ToString() + "'>" +
                    "   <div class='notification";
                if (dr["status"].ToString() == "not-seen")
                {
                    output += " not-seen";
                    count += 1;
                }
                output +=
                    "'>" +
                    "       <img src='img/" + dr["type"].ToString() + "Icon.png' style='opacity: 0.6;'/>" +
                            dr["notID"].ToString() +
                    "       <div class='not-date'>" + dr["date"].ToString() + "</div>" +
                    "   </div>" +
                    "</a>";
            }

            //set notifications
            notificationsBody.InnerHtml = output;

            //set count
            notification_count.InnerHtml = Convert.ToString(count);

            dr.Close();
            conn.Close();
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
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        protected void Load_SubCategory_for_search()
        {
            try
            {
                String cate2ID = AssetSearchCategoryDropDown.SelectedValue;
                SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn2.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT name, scatID FROM SubCategory where catID='" + cate2ID + "'", conn2);
                SqlDataReader data2 = cmd2.ExecuteReader();

                AssetSearchSubCategoryDropDown.DataSource = data2;
                AssetSearchSubCategoryDropDown.DataTextField = "name";
                AssetSearchSubCategoryDropDown.DataValueField = "scatID";
                AssetSearchSubCategoryDropDown.DataBind();
                AssetSearchSubCategoryDropDown.Items.Insert(0, new ListItem("-- Select a sub category--", ""));
                data2.Close();
                conn2.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
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

                data = cmd.ExecuteReader();
                AssetSearchCategoryDropDown.DataSource = data;
                AssetSearchCategoryDropDown.DataTextField = "name";
                AssetSearchCategoryDropDown.DataValueField = "catID";
                AssetSearchCategoryDropDown.DataBind();
                AssetSearchCategoryDropDown.Items.Insert(0, new ListItem("-- Select a category --", ""));
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }
        //Loading location dropdown
        protected void Load_Location()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT name, locID FROM Location", conn);
                SqlDataReader data = cmd.ExecuteReader();

                AddAssetLocationDropDown.DataSource = data;
                AddAssetLocationDropDown.DataTextField = "name";
                AddAssetLocationDropDown.DataValueField = "locID";
                AddAssetLocationDropDown.DataBind();
                AddAssetLocationDropDown.Items.Insert(0, new ListItem("-- Select a location --", ""));
                data.Close();

                data = cmd.ExecuteReader();
                AssetSearchLocationDropDown.DataSource = data;
                AssetSearchLocationDropDown.DataTextField = "name";
                AssetSearchLocationDropDown.DataValueField = "locID";
                AssetSearchLocationDropDown.DataBind();
                AssetSearchLocationDropDown.Items.Insert(0, new ListItem("-- Select a sub location --", ""));
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
                SqlCommand cmd = new SqlCommand("SELECT [firstname]+' '+[lastname] AS [name], empID FROM Employee", conn);
                SqlDataReader data = cmd.ExecuteReader();
                
                //Register new asset owner drop down
                AddAssetOwnerDropDown.DataSource = data;
                AddAssetOwnerDropDown.DataTextField = "name";
                AddAssetOwnerDropDown.DataValueField = "empID";
                AddAssetOwnerDropDown.DataBind();
                AddAssetOwnerDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();
                
                //Register new asset recommend person drop down
                data = cmd.ExecuteReader();
                AddAssetPersonToRecommendDropDown.DataSource = data;
                AddAssetPersonToRecommendDropDown.DataTextField = "name";
                AddAssetPersonToRecommendDropDown.DataValueField = "empID";
                AddAssetPersonToRecommendDropDown.DataBind();
                AddAssetPersonToRecommendDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();

                //Asset search owner drop down
                data = cmd.ExecuteReader();
                AssetSearchOwnerDropDown.DataSource = data;
                AssetSearchOwnerDropDown.DataTextField = "name";
                AssetSearchOwnerDropDown.DataValueField = "empID";
                AssetSearchOwnerDropDown.DataBind();
                AssetSearchOwnerDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();

                //Transfer asset owner drop down
                data = cmd.ExecuteReader();
                TransferOwnerDropDown.DataSource = data;
                TransferOwnerDropDown.DataTextField = "name";
                TransferOwnerDropDown.DataValueField = "empID";
                TransferOwnerDropDown.DataBind();
                //TransferOwnerDropDown.Items.Insert(0, new ListItem("-- Select an owner --", ""));
                data.Close();

                //Transfer asset recommend person drop down
                data = cmd.ExecuteReader();
                TransAssetSendForRecommendDropDown.DataSource = data;
                TransAssetSendForRecommendDropDown.DataTextField = "name";
                TransAssetSendForRecommendDropDown.DataValueField = "empID";
                TransAssetSendForRecommendDropDown.DataBind();
                TransAssetSendForRecommendDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();

                //Upgrade asset recommend person drop down
                data = cmd.ExecuteReader();
                UpgradeAssetPersonToRecommendDropDown.DataSource = data;
                UpgradeAssetPersonToRecommendDropDown.DataTextField = "name";
                UpgradeAssetPersonToRecommendDropDown.DataValueField = "empID";
                UpgradeAssetPersonToRecommendDropDown.DataBind();
                UpgradeAssetPersonToRecommendDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();

                //Dispose asset recommend person drop down
                data = cmd.ExecuteReader(); 
                DisposeAssetPersonToRecommendDropDown.DataSource = data;
                DisposeAssetPersonToRecommendDropDown.DataTextField = "name";
                DisposeAssetPersonToRecommendDropDown.DataValueField = "empID";
                DisposeAssetPersonToRecommendDropDown.DataBind();
                DisposeAssetPersonToRecommendDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();

                //Upgrade asset recommend person drop down
                data = cmd.ExecuteReader();
                UpgradeAssetPersonToRecommendDropDown.DataSource = data;
                UpgradeAssetPersonToRecommendDropDown.DataTextField = "name";
                UpgradeAssetPersonToRecommendDropDown.DataValueField = "empID";
                UpgradeAssetPersonToRecommendDropDown.DataBind();
                UpgradeAssetPersonToRecommendDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        // Regester new asset ==========================================================
        protected void setAssetID() 
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 assetID FROM Asset ORDER BY assetID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newAssetID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastAssetID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastAssetID[0]);
                    String temp = "";
                    for (int i = 1; i < lastAssetID.Length; i++)
                    {
                        temp += Convert.ToString(lastAssetID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newAssetID = chr;
                    for (int i = 1; i < lastAssetID.Length - temp.Length; i++)
                    {
                        newAssetID += "0";
                    }
                    newAssetID += temp;
                }
                else
                {
                    newAssetID = "A00001";
                }

                AddNewAssetId.InnerHtml = newAssetID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
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

                string insertion_Asset = "insert into Asset (assetID, name, value, category, subcategory, owner, status, location, recommend) values (@assetid, @name, @value, @category, @subcategory,@owner, @status, @location, @recommend)";
                cmd = new SqlCommand(insertion_Asset, conn);
                cmd.Parameters.AddWithValue("@assetid", AddNewAssetId.InnerHtml);
                cmd.Parameters.AddWithValue("@name", RegisterAssetNameTextBox.Text);
                cmd.Parameters.AddWithValue("@value", AddValueTextBox.Text);
                cmd.Parameters.AddWithValue("@category", AddAssetCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@subcategory", AddAssetSubCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", AddAssetOwnerDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@status", 0);
                cmd.Parameters.AddWithValue("@location", AddAssetLocationDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@recommend", AddAssetPersonToRecommendDropDown.SelectedValue);

                cmd.ExecuteNonQuery();

                String notID = setNotID();
                String insertDisposeAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, date, status) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @date, @status)";
                cmd = new SqlCommand(insertDisposeAsset, conn);

                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "AddNew");
                cmd.Parameters.AddWithValue("@assetid", AddNewAssetId.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", empID);
                cmd.Parameters.AddWithValue("@receiveuser", AddAssetPersonToRecommendDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
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
       
        // Advanced asset search =======================================================
        protected void SearchAssetBtn_Click(object sender, EventArgs e)
        {
            String assetID = AssetSearchIDTextBox.Text.Trim();
            String name = AssetSearchNameTextBox.Text.Trim();
            String subCategoryID = AssetSearchSubCategoryDropDown.SelectedValue;
            String categoryID = AssetSearchCategoryDropDown.SelectedValue;
            String value = AssetSearchValueTextBox.Text;
            String locationID = AssetSearchLocationDropDown.SelectedValue;
            String ownerID = AssetSearchOwnerDropDown.SelectedValue;

            String resultMessage = "";

            String query = "SELECT * FROM Asset WHERE";
            if (assetID != "")
            {
                query += " assetID='" + assetID + "'";
                resultMessage += assetID + ", ";
            }
            if (name != "")
            {
                query += " AND name='" + name + "'";
                resultMessage += name + ", ";
            }
            if (subCategoryID != "")
            {
                query += " AND subcategory='" + subCategoryID + "'";
                resultMessage += subCategoryID + ", ";
            }
            if (categoryID != "")
            {
                query += " AND category='" + categoryID + "'";
                resultMessage += categoryID + ", ";
            }
            if (value != "")
            {
                query += " AND value='" + Convert.ToInt16(value) + "'";
                resultMessage += value + ", ";
            }
            if (locationID != "")
            {
                query += " AND location='" + locationID + "'";
                resultMessage += locationID + ", ";
            }
            if (ownerID != "")
            {
                query += " AND owner='" + ownerID + "'";
                resultMessage += ownerID + ", ";
            }
            

            // Clearing the grid view
            AssetSearchGridView.DataSource = null;
            AssetSearchGridView.DataBind();

            //Remove unnessary 'and'
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

                    AssetSearchGridView.DataSource = dt;  //display found data in grid view
                    AssetSearchGridView.DataBind();
                    responseBoxGreen.Style.Add("display", "block");
                    responseMsgGreen.InnerHtml = "Search Results Found for <strong>" + resultMessage + "</strong>";
                }
                else
                {
                    responseBoxRed.Style.Add("display", "block");
                    responseMsgRed.InnerHtml = "No Results Found for " + resultMessage;
                }
                conn.Close();

                //expanding block
                AdvancedAssetSearchContent.Style.Add("display", "block");
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AdvancedAssetSearchContent');", true);
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        protected void Category_Selected_for_search(object sender, EventArgs e)
        {
            Load_SubCategory_for_search();

            AdvancedAssetSearchContent.Style.Add("display", "block");
            //updating expandingItems dictionary in javascript
            ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AdvancedAssetSearchContent');", true);
        }

        // Upgrade asset ===============================================================
        protected void UpgradeAssetFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = UpgradeAssetIDTextBox.Text;

                string check = "SELECT count(*) from Asset WHERE assetID='" + assetID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT name, category, subcategory, location, owner, value FROM Asset WHERE assetID='" + assetID + "'";
                    String query2 = "SELECT depreciationRate, lifetime FROM Asset WHERE assetID='" + assetID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    String UpgradeAssetCategoryID = "", UpgradeAssetSubcategoryID = "", UpgradeLocationID = "", UpgradeOwnerID = "";
                    while (dr.Read())
                    {
                        UpgradeAssetName.InnerHtml = dr["name"].ToString();
                        UpgradeAssetCategoryID = dr["category"].ToString();
                        UpgradeAssetSubcategoryID = dr["subcategory"].ToString();
                        UpgradeLocationID = dr["location"].ToString();
                        UpgradeOwnerID = dr["owner"].ToString();
                        UpgradeValue.InnerHtml = dr["value"].ToString() + " LKR";
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
                    // Get location name
                    String getLocNameQuery = "SELECT name FROM Location WHERE locID='" + UpgradeLocationID + "'";
                    cmd = new SqlCommand(getLocNameQuery, conn);
                    UpgradeLocation.InnerHtml = cmd.ExecuteScalar().ToString();
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
                    upgradeAssetInitState.Style.Add("display", "block");
                    upgradeAssetSecondState.Style.Add("display", "none");
                    UpgradeAssetContent.Style.Add("display", "block");
                    UpgradeAssetIDValidator.InnerHtml = "Invalid asset ID";
                }
                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpgradeAssetContent');", true);
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
                String insertUpgradeAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, date, status) VALUES (@notid, @type, @assetid, @notcontent, @senduser, @receiveuser, @date, @status)";
                cmd = new SqlCommand(insertUpgradeAsset, conn);

                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "Update");
                cmd.Parameters.AddWithValue("@assetid", UpgradeAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@notcontent", UpgradeAssetDescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@senduser", empID);
                cmd.Parameters.AddWithValue("@receiveuser", UpgradeAssetPersonToRecommendDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");

                cmd.ExecuteNonQuery();

                String insertUpgradeAsset_UpgradeAsset = "INSERT INTO UpgradeAsset (upID, assetID, value, date, description, recommend, approve) VALUES (@upid, @assetid, @value, @date, @description, @recommend, @approve)";
                cmd = new SqlCommand(insertUpgradeAsset_UpgradeAsset, conn);

                cmd.Parameters.AddWithValue("@upid", setUpID());
                cmd.Parameters.AddWithValue("@assetid", UpgradeAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@value", UpgradeAssetValueTextBox.Text);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@description", UpgradeAssetDescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@recommend", empID);
                cmd.Parameters.AddWithValue("@approve", UpgradeAssetPersonToRecommendDropDown.SelectedValue);

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

        // Transfer asset ===============================================================

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

                string check = "select count(*) from Asset WHERE assetID='" + assetID + "'";
                string getassetid = "select count(*) from TransferAsset WHERE assetID='" + assetID + "'";
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
                        String transferAssetLocationID = "";
                        String transferAssetOwnerID = "";

                        String query = "SELECT assetID, name, category, subcategory, location, owner, value FROM Asset WHERE assetID='" + assetID + "'";
                        cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            //DisposeAssetID.InnerHtml = dr["assetID"].ToString();
                            TransferItemName.InnerHtml = dr["name"].ToString();
                            transferAssetCategoryID = dr["category"].ToString();
                            transferAssetSubCategoryID = dr["subcategory"].ToString();
                            transferAssetLocationID = dr["location"].ToString();
                            transferAssetOwnerID = dr["owner"].ToString();
                            TransferValue.InnerHtml = dr["value"].ToString() + " LKR";
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
                        // Get location name
                        TransferLocationDropDown.SelectedValue = transferAssetLocationID;
                        // Get owner name
                        TransferOwnerDropDown.SelectedValue = transferAssetOwnerID;

                        transferAssetInitState.Style.Add("display", "none");
                        transferAssetSecondState.Style.Add("display", "block");
                        TransferAssetContent.Style.Add("display", "block");
                        TransferAssetIDValidator.InnerHtml = "";
                        TransferAssetIDTextBox.Focus();
                    }
                    else
                    {
                        transferAssetInitState.Style.Add("display", "block");
                        transferAssetSecondState.Style.Add("display", "none");
                        TransferAssetContent.Style.Add("display", "block");
                        TransferAssetIDValidator.InnerHtml = "Asset already recommended to transfer!";
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
                string insertion_Asset_to_transferAsset = "INSERT INTO TransferAsset (transID, assetID, type, status, date, location, owner, recommend) VALUES (@transid, @assetid, @type, @status, @date, @location, @owner, @recommend)";
                string insertion_Asset_to_notification = "INSERT INTO Notification (notID, assetID, type, notContent, sendUser, receiveUser, date, status) VALUES (@notid, @nAssetid, @nType, @nNotContent, @nSendUser, @nReceiveUser, @nDate, @nStatus)";
                cmd = new SqlCommand(insertion_Asset_to_transferAsset, conn);
                SqlCommand cmd2 = new SqlCommand(insertion_Asset_to_notification, conn);

                cmd.Parameters.AddWithValue("@transid", transID);
                cmd.Parameters.AddWithValue("@assetid", TransferAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@type", "0");
                cmd.Parameters.AddWithValue("@status", "0");
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@location", TransferLocationDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", TransferOwnerDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@recommend", TransAssetSendForRecommendDropDown.SelectedValue);
                cmd.ExecuteNonQuery();

                cmd2.Parameters.AddWithValue("@notid", notID);
                cmd2.Parameters.AddWithValue("@nAssetid", TransferAssetIDTextBox.Text);
                cmd2.Parameters.AddWithValue("@nType", "Transfer");
                cmd2.Parameters.AddWithValue("@nNotContent", "0");
                cmd2.Parameters.AddWithValue("@nSendUser", empID);
                cmd2.Parameters.AddWithValue("@nReceiveUser", TransAssetSendForRecommendDropDown.SelectedValue);
                cmd2.Parameters.AddWithValue("@nDate", DateTime.Now.ToString("yyyy-MM-dd"));
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
                string getassetid = "select count(*) from Notification WHERE assetID='" + assetID + "' and type='Dispose'";
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

                        String query = "SELECT assetID, name, category, subcategory, location, owner, value FROM Asset WHERE assetID='" + assetID + "'";
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
                            DisposeValue.InnerHtml = dr["value"].ToString() + " LKR";
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
                        disposeAssetInitState.Style.Add("display", "block");
                        disposeAssetSecondState.Style.Add("display", "none");
                        DisposeAssetContent.Style.Add("display", "block");
                        DisposeAssetIDValidator.InnerHtml = "Asset already recommended to dispose!";
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
                String insertDisposeAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, date, status) VALUES (@notid, @type, @assetid, @notcontent, @senduser, @receiveuser, @date, @status)";
                cmd = new SqlCommand(insertDisposeAsset, conn);

                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "Delete");
                cmd.Parameters.AddWithValue("@assetid", DisposeAssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notcontent", DisposeAssetDescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@senduser", empID);
                cmd.Parameters.AddWithValue("@receiveuser", DisposeAssetPersonToRecommendDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
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

    }
}