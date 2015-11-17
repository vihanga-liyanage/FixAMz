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

using System.Web.UI.HtmlControls;


namespace FixAMz_WebApplication
{
    public partial class Report1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /* if (!string.IsNullOrEmpty(Request.QueryString["test"]))
            {
                TextBox.Text = Request.QueryString["test"];
            }
            else
            {
                TextBox.Text = "NO DATA PROVIDED OR COULD NOT BE READ";
            }

            int i = 0;
            string html = "";
            while (i < 5)
            {
                html += "<a id='link" + i + "' href='Report1.aspx?test=link " + i + " clicked' runat='server'>Link " + i + "</a><br><br>";
                i++;
            }
            sample.InnerHtml = html;*/
        }

        protected void CalDepreciationBtn_Click(object sender, EventArgs e)
        {

             try
             {
                 SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                 conn.Open();

                List<string> assetID = new List<string>();
                List<float> value = new List<float>();
                List<float> salvageValue = new List<float>();
                List<float> updatedValue = new List<float>();
                List<string> subcategory = new List<string>();
                List<DateTime> approvedDateTime = new List<DateTime>();
                
                String selectAsset = "SELECT * FROM Asset";
                SqlCommand cmd = new SqlCommand(selectAsset,conn);

                SqlDataReader dr = cmd.ExecuteReader();
                
                while(dr.Read())
                {
                    String a = dr.GetString(0);
                    assetID.Add(a);

                    float b = (float)dr.GetDouble(2);
                    value.Add(b);

                    float c = (float)dr.GetDouble(3);
                    salvageValue.Add(c);

                    float d = (float)dr.GetDouble(4);
                    updatedValue.Add(d); 

                    String f = dr.GetString(6);
                    subcategory.Add(f);

                    DateTime g = dr.GetDateTime(10);
                    approvedDateTime.Add(g);
 
                }
                dr.Close();
               

                List<int> depRate = new List<int>();

                for (int i = 0; i < assetID.Count; i++)
                {
                    Response.Write(subcategory[i]);
                    string getRate = "SELECT depreciationRate FROM SubCategory WHERE scatID='" + subcategory[i] + "'";
                    SqlCommand cmd2 = new SqlCommand(getRate, conn);
                    String getRateget = cmd2.ExecuteScalar().ToString().Trim();
                    SqlCommand cmd1 = new SqlCommand(getRate, conn);

                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    Response.Write(getRateget);

                    depRate.Add(Int32.Parse(getRateget));

                    DateTime localDate = DateTime.Now;
                    DateTime appDate = approvedDateTime[i];
                    int diffDays = (int)(localDate - appDate).TotalDays;
                    Response.Write(localDate);
                    Response.Write(appDate);
                    float yearDiff = (float)diffDays / 365;
                    Response.Write("days "+ diffDays.ToString());
                    Response.Write("years " + yearDiff.ToString());
                    Response.Write(value[i].ToString()+"/n");
                    Response.Write(salvageValue[i].ToString() + "/n");

                    float upValue = (value[i] - salvageValue[i]) *  (yearDiff);
                    float newRate = (float)(Convert.ToInt32(getRateget)/100.0);
                    float upValuefinal = (float)upValue *newRate;
                    //upValuefinal = Math.Round(upValuefinal, 2);
                    
                    Response.Write("updated value " + upValue.ToString() + "/n");
                    Response.Write("updated value " + newRate.ToString() + "/n");
                }
                 for (int i = 0; i < assetID.Count; i++)
                 {
                   Response.Write(assetID.Count);
                   Response.Write(assetID[i]);
                   
                   Response.Write(depRate[i]);
                   Response.Write(approvedDateTime[i]);
                  }

                    //updating expandingItems dictionary in javascript
                    ClientScript.RegisterStartupScript(this.GetType(), "setExpandingItem", "setExpandingItem('DeleteUserContent');", true);
             }
             catch (SqlException)
             {
                 /*responseBoxRed.Style.Add("display", "block");
                 responseMsgRed.InnerHtml = "There were some issues with the database. Please try again later.";
                 Response.Write(e.ToString());*/
             }
         }
            /*HtmlAnchor a = (HtmlAnchor)sender;
            Response.Write("link " + a.ID);*/

        }
    }
