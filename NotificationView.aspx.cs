﻿using System;
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
    public partial class NotificationView : System.Web.UI.Page
    {

        private String Asset;
        private String Type;
        private String Category;
        private String Subcategory;
        private String Owner;
        private String Location;
        private String Content;
        private String senduser;
        private String notid;
        private String receiveuser;
        private String Action;

        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticate_User();
            setNavBar();
            setUserName();
            viewApprover();
            Load_Variables();
            Update_Not_DB();
            Load_Notifications();
            
            //Load_Content_for_cancel();

            if (!Page.IsPostBack)
            {
                costCenter();
                personToRecommend();
                Load_Location();
                Load_Employee_Data();
                Load_Category();
                //Load_SubCategory_for_register();
                Load_Variables();
                Load_UI_Content();
                //Load_Content_for_cancel();
                //Page.MaintainScrollPositionOnPostBack = true;
            }
            
        }
        
        //Checking if the user has access to the page
        protected void Authenticate_User()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');


            if ((data[1] != "manageAssetUser") && (data[1] != "manageReport") && (data[1] != "generateReportUser"))
            {
                FormsAuthentication.SignOut();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You do not have access to this page. Please sign in to continue.'); window.location='" +
Request.ApplicationPath + "Login.aspx';", true);
            }
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

        //Setting user name on header
        protected void setUserName()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            string[] data = userData.Split(';');

            userName.InnerHtml = data[0];

        }

        //set personToRecommend according to costID
        protected void personToRecommend()
        {
            String query = "SELECT e.empID, e.firstName, e.lastName FROM Employee e INNER JOIN CostCenter c ON e.empID = c.recommendPerson WHERE c.costID='" + Session["COST_ID_MNG_ASST"] + "'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            String recoPrsn = "";
            while (dr.Read())
            {
                recoPrsn = dr["firstName"].ToString().Trim() + " " + dr["lastName"].ToString().Trim();
            }
            dr.Close();
            conn.Close();

            String query2 = "SELECT recommendPerson, approvePerson FROM CostCenter WHERE CostID='" + Session["COST_ID_MNG_ASST"] + "'";
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn2.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn2);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                Session["PRSN_TO_APP"] = dr2["approvePerson"].ToString().Trim();
            }
            dr2.Close();
            conn2.Close();
        }

        //set costID by user login
        protected void costCenter()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;

            string userData = ticket.UserData;
            //userData = "Vihanga Liyanage;admin;CO00001"
            string[] data = userData.Split(';');
            //costID = data[2];
            Session["COST_ID_MNG_ASST"] = data[2];
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

        //Update notification table when loading
        protected void Update_Not_DB()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "UPDATE Notification SET status='seen' WHERE notID='" + notid + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException exx)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write("Update_Not_DB:" + exx.ToString());
            }
            
        }

        //Load data for cancel
        protected void Load_Content_for_cancel()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT * FROM Asset WHERE assetID='" + Asset + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AddNewAssetId.InnerHtml = dr["assetID"].ToString();
                    AssetNameTextBox.Text = dr["name"].ToString();
                    AddValueTextBox.Text = dr["value"].ToString();
                    AddSalvageValueTextBox.Text = dr["salvageValue"].ToString();
                    AddAssetCategoryDropDown.SelectedValue = dr["category"].ToString();
                    Load_SubCategory_for_register();
                    AddAssetSubCategoryDropDown.SelectedValue = dr["Subcategory"].ToString();
                    AddAssetOwnerDropDown.SelectedValue = dr["owner"].ToString();
                    AddAssetLocationDropDown.SelectedValue = dr["location"].ToString();
                }
                dr.Close();
                conn.Close();

            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }

        }

        //Loading global variables
        protected void Load_Variables()
        {
            try
            {
                //Retrieving notID from URL
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    notid = Request.QueryString["id"];
                }
                else
                {
                    notid = "N00001";
                }

                Update_Not_DB();

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT * FROM Notification WHERE notID='" + notid + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Asset = dr["assetID"].ToString();
                    Type = dr["type"].ToString();
                    Action = dr["action"].ToString();
                    Content = dr["notContent"].ToString();
                    senduser = dr["sendUser"].ToString();
                    receiveuser = dr["receiveUser"].ToString();
                }
                dr.Close();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('loaded')", true);
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        //Load page data
        protected void Load_UI_Content()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT * FROM Asset WHERE assetID='" + Asset + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AssetID.InnerHtml = dr["assetID"].ToString();
                    AssetName.InnerHtml = dr["name"].ToString();
                    AssetValue.InnerHtml = dr["value"].ToString();
                    AssetSalvageValue.InnerHtml = dr["salvageValue"].ToString();
                    Category = dr["category"].ToString();
                    Subcategory = dr["Subcategory"].ToString();
                    Owner = dr["owner"].ToString();
                    Location = dr["location"].ToString();

                }
                dr.Close();

                // Get category name
                String getCatNameQuery = "SELECT name FROM Category WHERE catID='" + Category + "'";
                cmd = new SqlCommand(getCatNameQuery, conn);
                AssetCategory.InnerHtml = cmd.ExecuteScalar().ToString();
                // Get sub category name
                String getSubCatNameQuery = "SELECT name FROM SubCategory WHERE scatID='" + Subcategory + "'";
                cmd = new SqlCommand(getSubCatNameQuery, conn);
                AssetSubcategory.InnerHtml = cmd.ExecuteScalar().ToString();
                // Get owner name
                String getOwnerNameQuery = "SELECT [firstname]+ ' '+[lastname] AS [name] FROM Employee WHERE empID='" + Owner + "'";
                cmd = new SqlCommand(getOwnerNameQuery, conn);
                AssetOwner.InnerHtml = cmd.ExecuteScalar().ToString();
                // Get location name
                String getLocationNameQuery = "SELECT name FROM Location WHERE locID='" + Location + "'";
                cmd = new SqlCommand(getLocationNameQuery, conn);
                AssetLocation.InnerHtml = cmd.ExecuteScalar().ToString();

//Add new==================
                if (Type == "AddNew" && Action == "Recommend")
                {
                    NotificationHeader.InnerHtml = "Register New Asset - Recommend";
                    AddnewassetState.Style.Add("display", "block");
                    NotificationContent.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

                if (Type == "AddNew" && Action == "Approve")
                {
                    NotificationHeader.InnerHtml = "Register New Asset - Approve";
                    AddnewassetStateApprove.Style.Add("display", "block");
                    NotificationContent.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

                if (Type == "AddNew" && Action == "Cancel")
                {
                    NotificationHeader.InnerHtml = "Register New Asset - Rejected";
                    AddnewassetStateApproveCancel.Style.Add("display", "none");
                    NotificationContent.Style.Add("display", "none");
                    EditableNotificationContent.Style.Add("display", "block");
                    Load_Content_for_cancel();
                    
                }

//Transfer=================
                if (Type == "Transfer" && Action == "Recommend")
                {
                    String get = "SELECT * FROM TransferAsset WHERE assetID='" + Asset + "' ";
                    SqlCommand cmd1 = new SqlCommand(get, conn);
                    SqlDataReader dr2 = cmd1.ExecuteReader();

                    String newlocation = "";
                    String newowner = "";
                    while (dr2.Read())
                    {
                        newlocation = dr2["location"].ToString().Trim();
                        newowner = dr2["owner"].ToString().Trim();
                    }
                    dr2.Close();
                    // Get location name
                    String getnewLocationNameQuery = "SELECT name FROM Location WHERE locID='" + newlocation + "'";
                    cmd1 = new SqlCommand(getnewLocationNameQuery, conn);
                    TransferNewlocation.InnerHtml = cmd1.ExecuteScalar().ToString();
                    // Get owner name
                    String getnewOwnerNameQuery = "SELECT [firstname]+ ' '+[lastname] AS [name] FROM Employee WHERE empID='" + newowner + "'";
                    cmd1 = new SqlCommand(getnewOwnerNameQuery, conn);
                    TransferNewowner.InnerHtml = cmd1.ExecuteScalar().ToString();

                    NotificationHeader.InnerHtml = "Transfer Asset - Recommend";                  
                    TransferassetState.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

                if (Type == "Transfer" && Action == "Approve")
                {
                    String get = "SELECT * FROM TransferAsset WHERE assetID='" + Asset + "' ";
                    SqlCommand cmd1 = new SqlCommand(get, conn);
                    SqlDataReader dr2 = cmd1.ExecuteReader();

                    String newlocation = "";
                    String newowner = "";
                    while (dr2.Read())
                    {
                        newlocation = dr2["location"].ToString().Trim();
                        newowner = dr2["owner"].ToString().Trim();
                    }
                    dr2.Close();
                    // Get location name
                    String getnewLocationNameQuery = "SELECT name FROM Location WHERE locID='" + newlocation + "'";
                    cmd1 = new SqlCommand(getnewLocationNameQuery, conn);
                    TransferAssetnewlocation.InnerHtml = cmd1.ExecuteScalar().ToString();
                    // Get owner name
                    String getnewOwnerNameQuery = "SELECT [firstname]+ ' '+[lastname] AS [name] FROM Employee WHERE empID='" + newowner + "'";
                    cmd1 = new SqlCommand(getnewOwnerNameQuery, conn);
                    TransferAssetnewowner.InnerHtml = cmd1.ExecuteScalar().ToString();

                    NotificationHeader.InnerHtml = "Transfer Asset - Approve";
                    TransferassetApproveState.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }
                if (Type == "Transfer" && Action == "Cancel")
                {
                    String get = "SELECT * FROM TransferAsset WHERE assetID='" + Asset + "' ";
                    SqlCommand cmd1 = new SqlCommand(get, conn);
                    SqlDataReader dr2 = cmd1.ExecuteReader();

                    String newlocation = "";
                    String newowner = "";
                    while (dr2.Read())
                    {
                        newlocation = dr2["location"].ToString().Trim();
                        newowner = dr2["owner"].ToString().Trim();
                    }
                    dr2.Close();
                    // Get location name
                    String getnewLocationNameQuery = "SELECT name FROM Location WHERE locID='" + newlocation + "'";
                    cmd1 = new SqlCommand(getnewLocationNameQuery, conn);
                    TransfernewlocationCancel.InnerHtml = cmd1.ExecuteScalar().ToString();
                    // Get owner name
                    String getnewOwnerNameQuery = "SELECT [firstname]+ ' '+[lastname] AS [name] FROM Employee WHERE empID='" + newowner + "'";
                    cmd1 = new SqlCommand(getnewOwnerNameQuery, conn);
                    TransfernewownerCancel.InnerHtml = cmd1.ExecuteScalar().ToString();

                    NotificationHeader.InnerHtml = "Transfer Asset - Rejected";
                    TransferassetCancelState.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

//Update====================

                if (Type == "Update" && Action == "Recommend")
                {
                    String get = "SELECT * FROM UpgradeAsset WHERE assetID='" + Asset + "' AND status= 'pending'";
                    SqlCommand cmd2 = new SqlCommand(get, conn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    String updatevalue = "";
                    String updatedescription = "";
                    while (dr2.Read())
                    {
                        updatevalue = dr2["updatedValue"].ToString().Trim();
                        updatedescription = dr2["description"].ToString().Trim();
                    }

                    NotificationHeader.InnerHtml = "Upgrade Asset - Recommend";

                    UpgradeCost.InnerHtml = updatevalue;
                    UpgradeDescription.InnerHtml = updatedescription;
                    UpgradeassetState.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

                if (Type == "Update" && Action == "Approve")
                {
                    String get = "SELECT * FROM UpgradeAsset WHERE assetID='" + Asset + "' AND status= 'pending'";
                    SqlCommand cmd2 = new SqlCommand(get, conn);
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    String updatevalue = "";
                    String updatedescription = "";
                    while (dr2.Read())
                    {
                        updatevalue = dr2["updatedValue"].ToString().Trim();
                        updatedescription = dr2["description"].ToString().Trim();
                    }
                    NotificationHeader.InnerHtml = "Upgrade Asset - Approve";

                    UpgradeCostApprove.InnerHtml = updatevalue;
                    UpgradeDescriptionApprove.InnerHtml = updatedescription;
                    UpgradeassetApprove.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

                if (Type == "Update" && Action == "Cancel")
                {
                    NotificationHeader.InnerHtml = "Upgrade Asset - Rejected";
                    UpgradeassetStateApproveCancel.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

//Dispose ====================================================
                if (Type == "Delete" && Action == "Recommend")
                {
                    NotificationHeader.InnerHtml = "Dispose Asset - Recommend";
                    DisposeDescription.InnerHtml = Content;
                    DisposeassetState.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

                if (Type == "Delete" && Action == "Approve")
                {
                    NotificationHeader.InnerHtml = "Dispose Asset - Approve";
                    SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                    //conn.Open();
                    String getvalue = "SELECT notContent FROM Notification WHERE assetID='" + Asset + "'";
                    SqlCommand cmd3 = new SqlCommand(getvalue, conn);
                    string val = cmd3.ExecuteScalar().ToString();

                    cmd3.ExecuteNonQuery();
                    conn1.Close();
                    DisposeassetApproveDescription.InnerHtml = Content;
                    DisposeassetApprove.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }

                if (Type == "Delete" && Action == "Cancel")
                {
                    NotificationHeader.InnerHtml = "Dispose Asset - Rejected";
                    SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                    
                    String getvalue = "SELECT notContent FROM Notification WHERE assetID='" + Asset + "'";
                    SqlCommand cmd3 = new SqlCommand(getvalue, conn);
                    string val = cmd3.ExecuteScalar().ToString();

                    cmd3.ExecuteNonQuery();
                    conn1.Close();
                    DisposeassetCancelDescription.InnerHtml = Content;
                    DisposeassetCancel.Style.Add("display", "block");
                    EditableNotificationContent.Style.Add("display", "none");
                }
                conn.Close();
            }
            catch (SqlException e)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
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
                data.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Load_Category:" + ex.Message.ToString());
            }
        }

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
                //AddAssetLocationDropDown.Items.Insert(0, new ListItem("-- Select a location --", ""));
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

                //Register new asset owner drop down
                AddAssetOwnerDropDown.DataSource = data;
                AddAssetOwnerDropDown.DataTextField = "name";
                AddAssetOwnerDropDown.DataValueField = "empID";
                AddAssetOwnerDropDown.DataBind();
                //AddAssetOwnerDropDown.Items.Insert(0, new ListItem("-- Select an employee --", ""));
                data.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Load_Employee_Data:" + ex.Message.ToString());
            }
        }

        // Signing out ==================================================================
        protected void SignOutLink_clicked(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        //Reads the last notID from DB, calculates the next==============================
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

        //Reads the last dispID from DB, calculates the next=============================
        protected String setdispID() //Reads the last dispID from DB, calculates the next and set it in the web page.
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 dispID FROM DisposeAsset ORDER BY dispID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newdispID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastdispID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastdispID[0]);
                    String temp = "";
                    for (int i = 1; i < lastdispID.Length; i++)
                    {
                        temp += Convert.ToString(lastdispID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newdispID = chr;
                    for (int i = 1; i < lastdispID.Length - temp.Length; i++)
                    {
                        newdispID += "0";
                    }
                    newdispID += temp;
                    return newdispID;
                }
                else
                {
                    newdispID = "E00001";
                    conn.Close();
                    return newdispID;
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

//Add new asset ==========================================

        protected void AddNewAssetSendApproveCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                //Load_Content_for_cancel();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String notID = setNotID();
                String canceladdnewAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                SqlCommand cmd = new SqlCommand(canceladdnewAsset, conn);
                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "AddNew");
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser",senduser);
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.Parameters.AddWithValue("@action", "Cancel");

                    
                cmd.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        protected void AddNewAssetSendapprove_Click(object sender, EventArgs e)
            {
                try
                {
                    Authenticate_User();
                    setNavBar();
                    setUserName();
                    Load_Variables();
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                    conn.Open();
                    String notID = setNotID();
                    String approveaddnewAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                    SqlCommand cmd = new SqlCommand(approveaddnewAsset, conn);
                    cmd.Parameters.AddWithValue("@notid", notID);
                    cmd.Parameters.AddWithValue("@type", "AddNew");
                    cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                    cmd.Parameters.AddWithValue("@notContent", " ");
                    cmd.Parameters.AddWithValue("@senduser", receiveuser);
                    cmd.Parameters.AddWithValue("@receiveuser", Session["PRSN_TO_APP"]);
                    cmd.Parameters.AddWithValue("@status", "not-seen");
                    cmd.Parameters.AddWithValue("@action", "Approve");
                    cmd.ExecuteNonQuery();

                    //delete notification
                    String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                    cmd = new SqlCommand(deleteNotQuery, conn);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    Response.Redirect("ManageAssetsUser.aspx");
                }catch (SqlException ex)
                {
                    responseBoxRed.Style.Add("display", "block");
                    responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                    Response.Write(ex.ToString());
                }
            }

        protected void AddNewAssetapprove_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                DateTime curDate = DateTime.Now;

                String quary = "UPDATE Asset SET status='1', approve='" + Session["PRSN_TO_APP"] + "',approvedDate='" + curDate + "' WHERE assetID='" + Asset + "'";

                SqlCommand cmd = new SqlCommand(quary, conn);
                cmd.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void AddNewAssetBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageAssetsUser.aspx");
        }

        protected void AddNewAssetcancel_Click(object sender, EventArgs e)
        {
            Load_Variables();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            //delete notification
            String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
            SqlCommand cmd = new SqlCommand(deleteNotQuery, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("ManageAssetsUser.aspx");
        }

        //for editable part
        protected void Category_Selected_for_register(object sender, EventArgs e)
        {
            Load_Variables();
            Load_SubCategory_for_register();
            EditableNotificationContent.Style.Add("display", "block");
            //updating expandingItems dictionary in javascript
            ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('AddNewAssetContent');", true);
        }

        protected void SendForRecAgainBtn_click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                //update asset table with changes
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String assetID = AddNewAssetId.InnerHtml;
                string update_Asset = "UPDATE Asset SET name = @name, category = @category, subcategory = @subcategory, value = @value, salvageValue = @salvageValue, location = @location, owner = @owner WHERE assetID='" + assetID + "'";
                SqlCommand cmd = new SqlCommand(update_Asset, conn);

                cmd.Parameters.AddWithValue("@name", AssetNameTextBox.Text);
                cmd.Parameters.AddWithValue("@category", AddAssetCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@subcategory", AddAssetSubCategoryDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@value", AddValueTextBox.Text);
                cmd.Parameters.AddWithValue("@salvageValue", AddSalvageValueTextBox.Text);
                cmd.Parameters.AddWithValue("@location", AddAssetLocationDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@owner", AddAssetOwnerDropDown.SelectedValue);
                cmd.ExecuteNonQuery();

                //set new notification for new changes

                //Load_Content();
                //Load_Content_for_cancel();
                //calling_functions();
                String notID = setNotID();
                String insertDisposeAsset = "INSERT INTO Notification (notID, type, action, assetID, notContent, sendUser, receiveUser, status) VALUES (@notid, @type, @action, @assetid, @notContent, @senduser, @receiveuser, @status)";
                cmd = new SqlCommand(insertDisposeAsset, conn);

                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "AddNew");
                cmd.Parameters.AddWithValue("@action", "Recommend");
                cmd.Parameters.AddWithValue("@assetid", AddNewAssetId.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser", senduser);
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addNewAssetClearAll", "addNewAssetClearAll();", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('UpdateLocationContent');", true);
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (Exception ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

//Transfer asset =========================================

        protected void TransferAssetSendapprovecancel_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String notID = setNotID();
                String canceladdnewAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                SqlCommand cmd = new SqlCommand(canceladdnewAsset, conn);
                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "Transfer");
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser", senduser);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.Parameters.AddWithValue("@action", "Cancel");
                cmd.ExecuteNonQuery();

                String query = "UPDATE TransferAsset SET status='cancel' WHERE assetID='" + AssetID.InnerHtml + "' AND status= 'pendding' ";
                SqlCommand cmd1 = new SqlCommand(query, conn);
                cmd1.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        protected void TransferAssetSendapprove_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String notID = setNotID();
                String approveaddnewAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                SqlCommand cmd = new SqlCommand(approveaddnewAsset, conn);
                cmd.Parameters.AddWithValue("@notid", notID);
                cmd.Parameters.AddWithValue("@type", "Transfer");
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser", Session["PRSN_TO_APP"]);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.Parameters.AddWithValue("@action", "Approve");
                cmd.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void transferAssetBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageAssetsUser.aspx");
        }

        protected void TransferAssetapprove_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String get = "SELECT * FROM TransferAsset WHERE assetID='" + Asset + "' AND status='pendding' ";
                SqlCommand cmd2 = new SqlCommand(get, conn);
                SqlDataReader dr2 = cmd2.ExecuteReader();

                String newlocation = "";
                String newowner = "";
                while (dr2.Read())
                {
                    newlocation = dr2["location"].ToString().Trim();
                    newowner = dr2["owner"].ToString().Trim();
                }
                dr2.Close();
                String quary = "UPDATE Asset SET owner= '"+ newowner +"', location='"+ newlocation +"' WHERE assetID='" + Asset + "'";
                SqlCommand cmd = new SqlCommand(quary, conn);
                cmd.ExecuteNonQuery();
                String query = "UPDATE TransferAsset SET status='complete',approve='" + Session["PRSN_TO_APP"] + "' WHERE assetID='" + AssetID.InnerHtml + "' AND status= 'pendding' ";
                SqlCommand cmd1 = new SqlCommand(query, conn);
                cmd1.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }


//Upgrade asset ==========================================

        protected void UpgradeAssetsendapprovecancel_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@notid", setNotID());
                cmd.Parameters.AddWithValue("@type", "Update");
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser", senduser);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.Parameters.AddWithValue("@action", "Cancel");
                cmd.ExecuteNonQuery();

                String query1 = "UPDATE UpgradeAsset SET status='cancel' WHERE assetID = '"+ Asset +"' and status='pending' ";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                cmd1.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        protected void UpgradeAssetsendapprove_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@notid", setNotID());
                cmd.Parameters.AddWithValue("@type", "Update");
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", " ");
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser", Session["PRSN_TO_APP"]);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.Parameters.AddWithValue("@action", "Approve");
                cmd.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");

            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void UpgradeAssetapprove_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                //update asset table
                String getvalue = "SELECT value FROM Asset WHERE assetID='" + Asset + "'";
                String getupvalue = "SELECT updatedValue FROM Asset WHERE assetID='" + Asset + "'";
                SqlCommand cmd1 = new SqlCommand(getvalue, conn);
                SqlCommand cmd3 = new SqlCommand(getupvalue, conn);
                string val = cmd1.ExecuteScalar().ToString();
                string upval = cmd3.ExecuteScalar().ToString();
                int value = Convert.ToInt32(val);
                int upvalue = Convert.ToInt32(val);
                cmd1.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                String getupdatedvalue = "SELECT updatedValue FROM UpgradeAsset WHERE assetID='" + Asset + "' AND status='pending'";
                SqlCommand cmd4 = new SqlCommand(getupdatedvalue, conn);
                string updatedval = cmd4.ExecuteScalar().ToString();
                int updatedvalue = Convert.ToInt16(updatedval);
                cmd4.ExecuteNonQuery();
                value = updatedvalue + value;
                upvalue = upvalue + updatedvalue;
                String quary = "UPDATE Asset SET value= '"+ value +"' WHERE assetID='" + Asset + "'";
                String quaryup = "UPDATE Asset SET updatedValue= '" + upvalue + "' WHERE assetID='" + Asset + "'";
                SqlCommand cmd = new SqlCommand(quary, conn);
                SqlCommand cmd5 = new SqlCommand(quaryup, conn);
                cmd.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();
                //update updateAsset table
                String quary1 = "UPDATE UpgradeAsset SET approve= '" + receiveuser + "', status='complete' WHERE assetID='" + Asset + "'AND status='pending'";
                SqlCommand cmd2 = new SqlCommand(quary1, conn);
                cmd2.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void upgradeAssetBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageAssetsUser.aspx");
        }


//Dispose asset ==========================================
        protected void DisposeAssetsendapprove_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@notid", setNotID());
                cmd.Parameters.AddWithValue("@type", "Delete");
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", DisposeDescription.InnerHtml);
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser", Session["PRSN_TO_APP"]);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.Parameters.AddWithValue("@action", "Approve");
                cmd.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");

            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void DisposeAssetapprove_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "INSERT INTO DisposeAsset (dispID, assetID, description, recommend, approve) VALUES (@dispid, @assetid, @description, @recommend, @approve)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dispid", setdispID());
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@description", DisposeassetApproveDescription.InnerHtml);
                cmd.Parameters.AddWithValue("@recommend", senduser);
                cmd.Parameters.AddWithValue("@approve", receiveuser);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                
                cmd.ExecuteNonQuery();

                String query1 = "UPDATE Asset SET status='0' WHERE assetID = '" + Asset + "'";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                cmd1.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");

            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void DisposeAssetcancel_Click(object sender, EventArgs e)
        {
            try
            {
                Load_Variables();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, status, action) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @status, @action)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@notid", setNotID());
                cmd.Parameters.AddWithValue("@type", "Delete");
                cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                cmd.Parameters.AddWithValue("@notContent", DisposeDescription.InnerHtml);
                cmd.Parameters.AddWithValue("@senduser", receiveuser);
                cmd.Parameters.AddWithValue("@receiveuser", senduser);
                //cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "not-seen");
                cmd.Parameters.AddWithValue("@action", "Cancel");
                cmd.ExecuteNonQuery();

                String query1 = "UPDATE UpgradeAsset SET status='cancel' WHERE assetID = '" + Asset + "' and status='pending' ";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                cmd1.ExecuteNonQuery();

                //delete notification
                String deleteNotQuery = "DELETE FROM Notification WHERE notID = '" + notid + "'";
                cmd = new SqlCommand(deleteNotQuery, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("ManageAssetsUser.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }

        protected void DisposeAssetBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageAssetsUser.aspx");
        }

        protected void viewApprover()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            // Get owner name
            String getnewOwnerNameQuery = "SELECT [firstname]+ ' '+[lastname] AS [name] FROM Employee WHERE empID='" + Session["PRSN_TO_APP"].ToString() + "'";
            SqlCommand cmd12 = new SqlCommand(getnewOwnerNameQuery, conn);
            approvepersonaddnew.InnerHtml = cmd12.ExecuteScalar().ToString();
            approvepersonupgrede.InnerHtml = cmd12.ExecuteScalar().ToString();
            approvepersontransfer.InnerHtml = cmd12.ExecuteScalar().ToString();
            approvepersondispose.InnerHtml = cmd12.ExecuteScalar().ToString();
            cmd12.ExecuteNonQuery();

            conn.Close();
        }

    }
}
