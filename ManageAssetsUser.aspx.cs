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

        //Signing out ==========================================================
        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
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


    }
}