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
                AddAssetCategoryDropDown.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown
                AddAssetCategoryDropDown.DataValueField = ds.Tables[0].Columns["catID"].ToString();             // to retrive specific  textfield name 
                AddAssetCategoryDropDown.DataSource = ds.Tables[0];      //assigning datasource to the DropDown
                AddAssetCategoryDropDown.DataBind();  //binding DropDown
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
                AddAssetSubCategoryDropDown.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown
                AddAssetSubCategoryDropDown.DataValueField = ds.Tables[0].Columns["scatID"].ToString();             // to retrive specific  textfield name 
                AddAssetSubCategoryDropDown.DataSource = ds.Tables[0];      //assigning datasource to the DropDown
                AddAssetSubCategoryDropDown.DataBind();  //binding dropdownlist
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
                AddAssetLocationDropDown.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown
                AddAssetLocationDropDown.DataValueField = ds.Tables[0].Columns["locID"].ToString();             // to retrive specific  textfield name 
                AddAssetLocationDropDown.DataSource = ds.Tables[0];      //assigning datasource to the DropDown
                AddAssetLocationDropDown.DataBind();  //binding DropDown
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
                AddAssetOwnerDropDown.DataTextField = ownerName; // text field name of table dispalyed in dropdown
                AddAssetOwnerDropDown.DataValueField = ds.Tables[0].Columns["empID"].ToString();             // to retrive specific  textfield name 
                AddAssetOwnerDropDown.DataSource = ds.Tables[0];      //assigning datasource to the DropDown
                AddAssetOwnerDropDown.DataBind();  //binding DropDown
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
                AddAssetPersonToRecommendDropDown.DataTextField = ds.Tables[0].Columns["firstname"].ToString(); // text field name of table dispalyed in dropdown
                AddAssetPersonToRecommendDropDown.DataValueField = ds.Tables[0].Columns["empID"].ToString();             // to retrive specific  textfield name 
                AddAssetPersonToRecommendDropDown.DataSource = ds.Tables[0];      //assigning datasource to the DropDown
                AddAssetPersonToRecommendDropDown.DataBind();  //binding DropDown
                //person to recommend dropdown in dispose asset
                DisposeAssetPersonToRecommendDropDown.DataTextField = ds.Tables[0].Columns["firstname"].ToString();
                DisposeAssetPersonToRecommendDropDown.DataValueField = ds.Tables[0].Columns["empID"].ToString();
                DisposeAssetPersonToRecommendDropDown.DataSource = ds.Tables[0];
                DisposeAssetPersonToRecommendDropDown.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message.ToString());
            }
        }

        //Reads the last assetID from DB, calculates the next and set it in the web page.
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
                cmd.Parameters.AddWithValue("@value", AddValueTextBox.Text);
                cmd.Parameters.AddWithValue("@category", AddAssetCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@subcategory", AddAssetSubCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", AddAssetOwnerDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@status", 0);
                cmd.Parameters.AddWithValue("@location", AddAssetLocationDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@recommend", AddAssetPersonToRecommendDropDown.SelectedValue);
                /* cmd.Parameters.AddWithValue("@value", CategoryDropDown.SelectedValue);
                 cmd.Parameters.AddWithValue("@contact", AddNewContactTextBox.Text);
                 cmd.Parameters.AddWithValue("@email", AddNewEmailTextBox.Text); */

                cmd.ExecuteNonQuery();



                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addNewClearAll", "addNewClearAll();", true);
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

        protected void DisposeAssetRecommendBtn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "disposeClearAll", "disposeClearAll();", true);
            responseArea.Style.Add("color", "Green");
            responseArea.InnerHtml = "Asset is sent for recommendation.";
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