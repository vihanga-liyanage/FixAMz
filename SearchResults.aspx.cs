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
    public partial class SearchResults : System.Web.UI.Page
    {
        private string costID;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Authenticate_User();
            costCenter();
            Load_Notifications();

            if (!Page.IsPostBack)
            {
                setUserName();
                Load_Category();
                Load_Location();
                Load_Employee_Data();

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
        protected void costCenter()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');
            costID = data[2];
            Session["COST_ID_MNG_ASST"] = data[2];
        }


        //Checking if the user has access to the page
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
                output +=
                    "'>" +
                    "   <img class='col-md-3' src='img/" + dr["type"].ToString().Trim() + "Icon.png'/>" +
                    "   <div class='not-content-box col-md-10'>" +
                    "       Asset <strong>" + dr["assetName"].ToString().Trim() + "</strong> has been " + action + " to " + dr["type"].ToString().Trim() +
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
                Response.Write("Load_SubCategory_for_search:" + ex.Message.ToString());
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
                Response.Write("Load_Location_Data:" + ex.Message.ToString());
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

                //Asset search owner drop down
                AssetSearchOwnerDropDown.DataSource = data;
                AssetSearchOwnerDropDown.DataTextField = "name";
                AssetSearchOwnerDropDown.DataValueField = "empID";
                AssetSearchOwnerDropDown.DataBind();
                AssetSearchOwnerDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
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

        // Advanced asset search =======================================================
        protected void SearchAssetBtn_Click(object sender, EventArgs e)
        {
            String name = AssetSearchNameTextBox.Text.Trim();
            String subCategoryID = AssetSearchSubCategoryDropDown.SelectedValue;
            String categoryID = AssetSearchCategoryDropDown.SelectedValue;
            String value = AssetSearchValueTextBox.Text;
            // String locationID = AssetSearchLocationDropDown.SelectedValue;
            String ownerID = AssetSearchOwnerDropDown.SelectedValue;

            String resultMessage = "";

            String query = "SELECT A.assetID AS Asset_ID, A.name AS Name, A.value AS Value, C.name AS Category, SC.name AS Subcategory, (E.firstName+' '+E.lastName) AS Owner, L.name AS Location, A.approvedDate AS Approved_Date, A.recommend AS Recommended_By, A.approve AS Approved_By " +
                "FROM Asset A " +
                "INNER JOIN Category C ON A.category=C.catID " +
                "INNER JOIN SubCategory SC ON A.subcategory=SC.scatID " +
                "INNER JOIN Employee E ON A.owner=E.empID " +
                "INNER JOIN Location L ON A.location=L.locID " +
                "WHERE status=1";

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
            /* if (locationID != "")
             {
                 query += " AND location='" + locationID + "'";
                 resultMessage += locationID + ", ";
             }*/
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
                    //dt.Columns.Add("Asset ID");
                    //DataRow dtrow = dt.NewRow();
                    //dtrow["Asset ID"] = "E00001";
                    //dt.Rows.Add(dtrow);
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
    }
}