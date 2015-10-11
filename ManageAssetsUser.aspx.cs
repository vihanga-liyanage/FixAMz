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
        private string disposeAssetCategoryID;
        private string disposeAssetSubCategoryID;
        private string disposeAssetLocationID;
        private string disposeAssetOwnerID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Load_Category();
                Load_SubCategory();
                Load_Location();
                Load_Employee_Data();
                setUserName();
                setAssetID();
                responseArea.InnerHtml = "";
                Page.MaintainScrollPositionOnPostBack = true;
                AddAssetCategoryDropDown.Items.Insert(0, new ListItem("--Select Category--", "0"));
                AddAssetSubCategoryDropDown.Items.Insert(0, new ListItem("--Select Sub Category--", "0"));
                AddAssetLocationDropDown.Items.Insert(0, new ListItem("--Select Location--", "0"));
                AddAssetOwnerDropDown.Items.Insert(0, new ListItem("--Select Owner--", "0"));
                AddAssetPersonToRecommendDropDown.Items.Insert(0, new ListItem("--Select Person--", "0"));
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
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(exx.ToString());
            }
        }
        
        // Signing out =================================================================
        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        // Loading data to drop downs ==================================================
        //Loading sub category dropdown
        protected void Load_SubCategory()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT name, scatID FROM SubCategory", conn);
                SqlDataReader data = cmd.ExecuteReader();

                AddAssetSubCategoryDropDown.DataSource = data;
                AddAssetSubCategoryDropDown.DataTextField = "name";
                AddAssetSubCategoryDropDown.DataValueField = "scatID";
                AddAssetSubCategoryDropDown.DataBind();
                AddAssetSubCategoryDropDown.Items.Insert(0, new ListItem("-- Select a sub category--", ""));
                data.Close();

                data = cmd.ExecuteReader();
                AssetSearchSubCategoryDropDown.DataSource = data;
                AssetSearchSubCategoryDropDown.DataTextField = "name";
                AssetSearchSubCategoryDropDown.DataValueField = "scatID";
                AssetSearchSubCategoryDropDown.DataBind();
                AssetSearchSubCategoryDropDown.Items.Insert(0, new ListItem("-- Select a sub category--", ""));
                data.Close();

                conn.Close();
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

                //Dispose asset recommend person drop down
                data = cmd.ExecuteReader(); 
                DisposeAssetPersonToRecommendDropDown.DataSource = data;
                DisposeAssetPersonToRecommendDropDown.DataTextField = "name";
                DisposeAssetPersonToRecommendDropDown.DataValueField = "empID";
                DisposeAssetPersonToRecommendDropDown.DataBind();
                DisposeAssetPersonToRecommendDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
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
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void SendForRecommendationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                string insertion_Asset = "insert into Asset (assetID, name, value, category, subcategory, owner, status, location, recommend) values (@assetid, @name, @value, @category, @subcategory,@owner, @status, @location, @recommend)";
                SqlCommand cmd = new SqlCommand(insertion_Asset, conn);
                cmd.Parameters.AddWithValue("@assetid", AddNewAssetId.InnerHtml);
                cmd.Parameters.AddWithValue("@name", RegisterAssetNameTextBox.Text);
                cmd.Parameters.AddWithValue("@value", Convert.ToInt16(AddValueTextBox.Text));
                cmd.Parameters.AddWithValue("@category", AddAssetCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@subcategory", AddAssetSubCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", AddAssetOwnerDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@status", 0);
                cmd.Parameters.AddWithValue("@location", AddAssetLocationDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@recommend", AddAssetPersonToRecommendDropDown.SelectedValue);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addNewAssetClearAll", "addNewAssetClearAll();", true);
                setAssetID();
                responseArea.Style.Add("color", "green");
                responseArea.InnerHtml = "Asset added successfully!";

            }
            catch (Exception ex)
            {
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
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
            int value = Convert.ToInt16(AssetSearchValueTextBox.Text);
            String locationID = AssetSearchLocationDropDown.SelectedValue;
            String ownerID = AssetSearchOwnerDropDown.SelectedValue;

            String query = "SELCT * FROM Asset WHERE";
            if (assetID != "")
                query += " assetID='" + assetID + "'";
            if (name != "")
                query += " AND name='" + name + "'";
            if (subCategoryID != "")
                query += " AND scatID='" + subCategoryID + "'";
            if (categoryID != "")
                query += " AND catID='" + categoryID + "'";
            if (value != 0)
                query += " AND value='" + value + "'";
            if (locationID != "")
                query += " AND locID='" + locationID + "'";
            if (ownerID != "")
                query += " AND owner='" + ownerID + "'";

            query = query.Replace("WHERE AND", "WHERE");
            Response.Write(query);
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString); //database connectivity
            conn.Open();
            conn.Close();

        }

        // Upgrade asset ===============================================================

        // Dispose asset ===============================================================

        protected void DisposeAssetFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = DisposeAssetIDTextBox.Text;

                string check = "select count(*) from Asset WHERE assetID='" + assetID + "'";
                string getassetid = "select count(*) from Notification WHERE assetID='" + assetID + "'";
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
                        DisposeAssetIDTextBox.Focus();
                    }
                    else
                    {
                        disposeAssetInitState.Style.Add("display", "block");
                        disposeAssetSecondState.Style.Add("display", "none");
                        DisposeAssetContent.Style.Add("display", "block");
                        DisposeAssetIDValidator.InnerHtml = "Asset alrady recommended to dispose!";
                        DisposeItemName.Focus();
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
                    DisposeItemName.Focus();
                }
            }
            catch (SqlException ex)
            {
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected String setNotID() //Reads the last notID from DB, calculates the next.
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
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
                return "";
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
                cmd.Parameters.AddWithValue("@type","Dispose");
                cmd.Parameters.AddWithValue("@assetid", DisposeAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@notcontent", DisposeAssetDescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@senduser", empID);
                cmd.Parameters.AddWithValue("@receiveuser", DisposeAssetPersonToRecommendDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@date",  DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");


                cmd.ExecuteNonQuery();
                
                conn.Close();

                responseArea.Style.Add("color", "green");
                responseArea.InnerHtml = "Asset '" + DisposeAssetIDTextBox.Text + "' recommended!";
                disposeAssetInitState.Style.Add("display", "block");
                disposeAssetSecondState.Style.Add("display", "none");
                DisposeAssetContent.Style.Add("display", "block");
                DisposeAssetIDTextBox.Text = "";

            }
            catch (Exception ex)
            {
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        // Transfer asset ===============================================================
        protected void TransferAssetFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = TransferAssetIDTextBox.Text;

                string check = "select count(*) from Asset WHERE assetID='" + assetID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT assetID, name, category, subcategory, location, owner, value FROM Asset WHERE assetID='" + assetID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TransferItemName.InnerHtml = dr["name"].ToString();
                        TransferCategory.InnerHtml = dr["category"].ToString();
                        TransferSubcategory.InnerHtml = dr["subcategory"].ToString();
                        TransferLocation.InnerHtml = dr["location"].ToString();
                        TransferOwner.InnerHtml = dr["owner"].ToString();
                        TransferValue.InnerHtml = dr["value"].ToString();
                    }
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
                    TransferAssetIDValidator.InnerHtml = "Asset ID not found!";
                    TransferItemName.Focus();
                }

                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('TransferAssetContent');", true);
            }
            catch (SqlException ex)
            {
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }        

    }
}