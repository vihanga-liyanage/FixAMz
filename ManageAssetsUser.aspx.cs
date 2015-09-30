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

        }



        // Dispose Asset
        protected void DisposeAssetFindBtn_Click(object sender, EventArgs e)
        {  
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String assetID = DisposeAssetIDTextBox.Text;

                string check = "select count(*) from Asset WHERE asssetID='" + assetID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT assetID, name, category, subcategory, location, owner, value FROM Asset WHERE asssetID='" + assetID + "'";
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
                Response.Write(e.ToString());
            }
        }        
}
}