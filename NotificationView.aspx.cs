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

        protected void Page_Load(object sender, EventArgs e)
        {
            setUserName();
            Load();
            Load_Notifications();
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
            query = "SELECT notID, type, a.name AS assetName, notContent, e.firstName, e.lastname, date, n.status " +
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
                output +=
                    "'>" +
                    "   <img class='col-md-3' src='img/" + dr["type"].ToString().Trim() + "Icon.png'/>" +
                    "   <div class='not-content-box col-md-10'>" +
                    "       Asset <strong>" + dr["assetName"].ToString().Trim() + "</strong> Has been " + "recommended to " + dr["type"].ToString().Trim() +
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

        //Load page data
        protected void Load()
        {
            String notid;
            //Retrieving notID from URL
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                notid = Request.QueryString["id"];
            }
            else
            {
                notid = "N00001";
            }

            String Type = "";
            String Category = "";
            String Subcategory = "";
            String Owner = "";
            String Content = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            String query = "SELECT assetID, type, notContent FROM Notification WHERE notID='" + notid + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Asset = dr["assetID"].ToString();
                Type = dr["type"].ToString();
                Content = dr["notContent"].ToString();
            }

            dr.Close();
            String query1 = "SELECT * FROM Asset WHERE assetID='" + Asset + "'";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            while (dr1.Read())
            {
                AssetID.InnerHtml = dr1["assetID"].ToString();
                AssetName.InnerHtml = dr1["name"].ToString();
                AssetValue.InnerHtml = dr1["value"].ToString();
                AssetSalvageValue.InnerHtml = dr1["salvageValue"].ToString();
                Category = dr1["category"].ToString();
                Subcategory = dr1["Subcategory"].ToString();
                Owner = dr1["owner"].ToString();

            }
            dr1.Close();

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
            conn.Close();

            if (Type == "RegRecommend")
            {
                NotificationHeader.InnerHtml = "Add new asset Notification";
                AddnewassetState.Style.Add("display", "block");
            }

            if (Type == "Transfer")
            {
                NotificationHeader.InnerHtml = "Transfer asset Notification";
                TransferassetState.Style.Add("display", "block");
            }

            if (Type == "Upgrade")
            {
                string getvalue = "SELECT value FROM UpgradeAsset WHERE assetID='" + Asset + "' AND status= 'pending'";
                SqlCommand cmd2 = new SqlCommand(getvalue, conn);
                String value = (cmd2.ExecuteScalar().ToString()).Trim();
                NotificationHeader.InnerHtml = "Upgrade asset Notification";
                UpgradeCost.InnerHtml = value;
                UpgradeDescription.InnerHtml = Content;
                UpgradeassetState.Style.Add("display", "block");
            }

            if (Type == "Dispose")
            {
                NotificationHeader.InnerHtml = "Dispose asset Notification";
                DisposeDescription.InnerHtml = Content;
                DisposeassetState.Style.Add("display", "block");
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
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write("setUserName:" + exx.ToString());
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



        /*    protected void AddNewAssetapprovecancel_Click(object sender, EventArgs e)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                    conn.Open();
                    String notID = setNotID();
                    String canceladdnewAsset = "INSERT INTO Notification (notID, type, assetID, notContent, sendUser, receiveUser, date, status) VALUES (@notid, @type, @assetid, @notContent, @senduser, @receiveuser, @date, @status)";
                    SqlCommand cmd = new SqlCommand(canceladdnewAsset, conn);
                    cmd.Parameters.AddWithValue("@notid", notID);
                    cmd.Parameters.AddWithValue("@type", "Cancel");
                    cmd.Parameters.AddWithValue("@assetid", AssetID.InnerHtml);
                    cmd.Parameters.AddWithValue("@notContent", " ");
                    cmd.Parameters.AddWithValue("@senduser", empID);
                    cmd.Parameters.AddWithValue("@receiveuser", AddAssetPersonToRecommendDropDown.SelectedValue);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@status", "not-seen");
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("AdminUserSystemTab.aspx");
                }
                catch (SqlException ex)
                {
                    responseBoxRed.Style.Add("display", "block");
                    responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                    Response.Write(ex.ToString());
                }

            }*/

        protected void UpgradeAssetapprovecancel_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "UPDATE UpgradeAsset SET status='cancel' WHERE assetID='" + AssetID.InnerHtml + "' AND status= 'pending';";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("AdminUserSystemTab.aspx");
            }
            catch (SqlException ex)
            {
                responseBoxRed.Style.Add("display", "block");
                responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }

        }


    }
}