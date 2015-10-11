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
                View_Category();
                View_SubCategory();
                View_Location();
                View_Owner();
                View_Person_To_Recommend();
                setUserName();
                setAssetID();
                responseArea.InnerHtml = "";
                Page.MaintainScrollPositionOnPostBack = true;
                CategoryDropDownList.Items.Insert(0, new ListItem("--Select Category--", "0"));
                SubCategoryDropDownList.Items.Insert(0, new ListItem("--Select Sub Category--", "0"));
                LocationDropDownList.Items.Insert(0, new ListItem("--Select Location--", "0"));
                OwnerDropDownList.Items.Insert(0, new ListItem("--Select Owner--", "0"));
                PersonToRecommendDropDownList.Items.Insert(0, new ListItem("--Select Person--", "0"));
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

        //Signing out ==========================================================
        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        // Regester new asset ==================================================

        //Loading category dropdown
        protected void View_Category()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand com = new SqlCommand("select *from Category", conn); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                CategoryDropDownList.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown
                CategoryDropDownList.DataValueField = ds.Tables[0].Columns["catID"].ToString();             // to retrive specific  textfield name 
                CategoryDropDownList.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                CategoryDropDownList.DataBind();  //binding dropdownlist
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }
        //Loading sub category dropdown
        protected void View_SubCategory()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand com = new SqlCommand("select *from SubCategory", conn); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                SubCategoryDropDownList.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown
                SubCategoryDropDownList.DataValueField = ds.Tables[0].Columns["scatID"].ToString();             // to retrive specific  textfield name 
                SubCategoryDropDownList.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                SubCategoryDropDownList.DataBind();  //binding dropdownlist
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }
        //Loading location dropdown
        protected void View_Location()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand com = new SqlCommand("select *from Location", conn); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                LocationDropDownList.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown
                LocationDropDownList.DataValueField = ds.Tables[0].Columns["locID"].ToString();             // to retrive specific  textfield name 
                LocationDropDownList.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                LocationDropDownList.DataBind();  //binding dropdownlist
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }
        //Loading owner dropdown
        protected void View_Owner()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand com = new SqlCommand("select * from Employee", conn); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                String ownerName = ds.Tables[0].Columns["firstName"].ToString();
                OwnerDropDownList.DataTextField = ownerName; // text field name of table dispalyed in dropdown
                OwnerDropDownList.DataValueField = ds.Tables[0].Columns["empID"].ToString();             // to retrive specific  textfield name 
                OwnerDropDownList.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                OwnerDropDownList.DataBind();  //binding dropdownlist
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }
        //Loading person to recommend dropdown
        protected void View_Person_To_Recommend()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand com = new SqlCommand("select *from Employee", conn); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                PersonToRecommendDropDownList.DataTextField = ds.Tables[0].Columns["firstname"].ToString(); // text field name of table dispalyed in dropdown
                PersonToRecommendDropDownList.DataValueField = ds.Tables[0].Columns["empID"].ToString();             // to retrive specific  textfield name 
                PersonToRecommendDropDownList.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                PersonToRecommendDropDownList.DataBind();  //binding dropdownlist
                //person to recommend dropdown in dispose asset
                DisposeAssetPersonToRecommendDropDownList.DataTextField = ds.Tables[0].Columns["firstname"].ToString();
                DisposeAssetPersonToRecommendDropDownList.DataValueField = ds.Tables[0].Columns["empID"].ToString();
                DisposeAssetPersonToRecommendDropDownList.DataSource = ds.Tables[0];
                DisposeAssetPersonToRecommendDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        // Register New Asset

        protected void setAssetID() //Reads the last empID from DB, calculates the next and set it in the web page.
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
                string insertion_Asset = "insert into Asset (assetID, name, value, category, subcategory, owner,location,status) values (@assetid, @name, @value, @category, @subcategory,@owner, @location, @status)";
                SqlCommand cmd = new SqlCommand(insertion_Asset, conn);
                cmd.Parameters.AddWithValue("@assetid", AddNewAssetId.InnerHtml);
                cmd.Parameters.AddWithValue("@name", RegisterAssetNameTextBox.Text);
                cmd.Parameters.AddWithValue("@value", AddValueTextBox.Text);
                cmd.Parameters.AddWithValue("@category", CategoryDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@subcategory", SubCategoryDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", OwnerDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@location", LocationDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@status", 0);
                /* cmd.Parameters.AddWithValue("@value", CategoryDropDownList.SelectedValue);
                 cmd.Parameters.AddWithValue("@contact", AddNewContactTextBox.Text);
                 cmd.Parameters.AddWithValue("@email", AddNewEmailTextBox.Text); */

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
       

        //Advanced asset search ================================================
        protected void SearchAssetBtn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString); //database connectivity
            conn.Open();
            conn.Close();

        }

        // Dispose asset =======================================================
        protected void DisposeAssetFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = DisposeAssetIDTextBox.Text;

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
                        DisposeAssetID.InnerHtml = dr["assetID"].ToString();
                        DisposeItemName.InnerHtml = dr["name"].ToString();
                        DisposeCategory.InnerHtml = dr["category"].ToString();
                        DisposeSubcategory.InnerHtml = dr["subcategory"].ToString();
                        DisposeLocation.InnerHtml = dr["location"].ToString();
                        DisposeOwner.InnerHtml = dr["owner"].ToString();
                        DisposeValue.InnerHtml = dr["value"].ToString();
                    }
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
                    DisposeAssetIDValidator.InnerHtml = "Asset ID not found!";
                    DisposeItemName.Focus();
                }

                conn.Close();
                //updating expandingItems dictionary in javascript
                ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('DisposeAssetContent');", true);
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
                    return newNotID;
                }
                else
                {
                    newNotID = "N00001";
                    return newNotID;
                }

                conn.Close();
            }
            catch (SqlException e)
            {
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
                return "";
            }
        }//set notid

        protected void DisposeAssetRecommendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String notID = setNotID();

                string insertDisposeAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status) VALUES (@notid,@type,@assetid,@notcontent,@senduser,@receiveuser,@status)";
                SqlCommand cmd = new SqlCommand(insertDisposeAsset, conn);
                cmd.Parameters.AddWithValue("@notid", DisposeAssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@type","Dispose");
                cmd.Parameters.AddWithValue("@assetid", DisposeAssetIDTextBox.Text);
                cmd.Parameters.AddWithValue("@notcontent", DisposeAssetDescription.Text);
                cmd.Parameters.AddWithValue("@senduser", userName.InnerHtml);
                cmd.Parameters.AddWithValue("@receiveuser", DisposeAssetPersonToRecommendDropDownList.SelectedValue);
                //cmd.Parameters.AddWithValue("@date",  DateTime);
                cmd.Parameters.AddWithValue("@status", "no");
                cmd.ExecuteNonQuery();

                conn.Close();
              





            }
            catch (Exception ex)
            {
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }


        // Transfer asset =======================================================
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