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
    public partial class GridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticate_User();
            //Load_Notifications();
            //setAssetID();

            if (!Page.IsPostBack)
            {
                setUserName();
                Load_Category();
                setUserName();
                /*setAssetID();
                costCenter();
                personToRecommend();
                Load_Location();
                Load_Employee_Data();*/
                Page.MaintainScrollPositionOnPostBack = true;
            }

            responseBoxGreen.Style.Add("display", "none");
            responseMsgGreen.InnerHtml = "";
            responseBoxRed.Style.Add("display", "none");
            responseMsgRed.InnerHtml = "";
        }

        protected void Authenticate_User()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');


            if (data[1] != "manageAssetUser")
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

        //Loading category dropdown
        protected void Load_Category()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT name, catID FROM Category", conn);
                SqlDataReader data = cmd.ExecuteReader();


                CatWiseSearchCategoryDropDown.DataSource = data;
                CatWiseSearchCategoryDropDown.DataTextField = "name";
                CatWiseSearchCategoryDropDown.DataValueField = "catID";
                CatWiseSearchCategoryDropDown.DataBind();
                CatWiseSearchCategoryDropDown.Items.Insert(0, new ListItem("-- Select a category --", ""));
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Load_Category:" + ex.Message.ToString());
            }
        }


        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CatWiseSearchFindBtn_Click(object sender, EventArgs e)
        {

        }

        protected void Load_SubCategory_for_search()
        {
            try
            {
                String cate2ID = CatWiseSearchCategoryDropDown.SelectedValue;
                SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn2.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT name, scatID FROM SubCategory where catID='" + cate2ID + "'", conn2);
                SqlDataReader data2 = cmd2.ExecuteReader();

                CatWiseSearchSubCategoryDropDown.DataSource = data2;
                CatWiseSearchSubCategoryDropDown.DataTextField = "name";
                CatWiseSearchSubCategoryDropDown.DataValueField = "scatID";
                CatWiseSearchSubCategoryDropDown.DataBind();
                CatWiseSearchSubCategoryDropDown.Items.Insert(0, new ListItem("-- Select a sub category--", ""));
                data2.Close();
                conn2.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Load_SubCategory_for_search:" + ex.Message.ToString());
            }
        }

        protected void Category_Selected_for_search(object sender, EventArgs e)
        {
            Load_SubCategory_for_search();

            CatWiseSearchContent.Style.Add("display", "block");
            //updating expandingItems dictionary in javascript
            ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AdvancedAssetSearchContent');", true);
        }

    }
}