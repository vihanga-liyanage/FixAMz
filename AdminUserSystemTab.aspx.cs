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
    public partial class AdminUserSystemTab : System.Web.UI.Page
    {
       // protected void Page_Load_loc(object sender, EventArgs e)
       // {
       //    setLocID();
       // }

        protected void AddLocationBtn_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                string insertion_Location = "insert into Location (locID, name, department, zonalOffice, managerOffice, branch, address, contactNo) values (@locid, @name, @department, @zonaloffice, @manageroffice, @branch, @address, @contactno)";
                SqlCommand cmd = new SqlCommand(insertion_Location, conn);
                cmd.Parameters.AddWithValue("@locid", AddNewLocID.InnerHtml);
                cmd.Parameters.AddWithValue("@name", AddLocationNameTextBox.Text);
                cmd.Parameters.AddWithValue("@department", AddLocationDepartmentTextBox.Text);
                cmd.Parameters.AddWithValue("@zonaloffice", AddLocationZonalOfficeTextBox.Text);
                cmd.Parameters.AddWithValue("@manageroffice", AddLocationManagerOfficeTextBox.Text);
                cmd.Parameters.AddWithValue("@branch", AddLocationBranchTextBox.Text);
                cmd.Parameters.AddWithValue("@address", AddLocationAddressTextBox.Text);
                cmd.Parameters.AddWithValue("@contactno", AddLocationContactTextBox.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "addLocationClearAll", "addLocationClearAll();", true);
                setCatID();
                responseArea.Style.Add("color", "green");
                responseArea.InnerHtml = "Location " + AddLocationNameTextBox.Text + " added successfully!";


            }
            catch (Exception ex)
            {
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again lateer.";
                Response.Write(ex.ToString());
            }
        }

        protected void setLocID() //Reads the last empID from DB, calculates the next and set it in the web page.
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 locID FROM Location ORDER BY locID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newLocID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastLocID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastLocID[0]);
                    String temp = "";
                    for (int i = 1; i < lastLocID.Length; i++)
                    {
                        temp += Convert.ToString(lastLocID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newLocID = chr;
                    for (int i = 1; i < lastLocID.Length - temp.Length; i++)
                    {
                        newLocID += "0";
                    }
                    newLocID += temp;
                }
                else
                {
                    newLocID = "L00001";
                }

                AddNewLocID.InnerHtml = newLocID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again lateeer.";
                Response.Write(e.ToString());
            }
        } 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            setCatID();
            setLocID();
        }

        protected void AddCategoryBtn_Click(object sender, EventArgs e)
        {
            try
            {
               
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                    conn.Open();
                    string insertion_Category = "insert into Category (catID, name) values (@catid, @name)";
                    SqlCommand cmd = new SqlCommand(insertion_Category, conn);
                    cmd.Parameters.AddWithValue("@catid", AddNewCatID.InnerHtml);
                    cmd.Parameters.AddWithValue("@name", AddCategoryNameTextBox.Text);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "addCategoryClearAll", "addCategoryClearAll();", true);
                    setCatID();
                    responseArea.Style.Add("color", "green");
                    responseArea.InnerHtml = "Category " + AddCategoryNameTextBox.Text + " added successfully!";

                
            }
            catch (Exception ex)
            {
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
            }
 
      /*  protected void CancelAddcategoryBtn_Click(object sender, EventArgs e)
        {
            var tbs = new List<TextBox>() { AddCategoryNameTextBox };
            foreach (var textBox in tbs)
            {
                textBox.Text = "";
                responseArea.InnerHtml = "";
                
            }
        }*/

        protected void setCatID() //Reads the last empID from DB, calculates the next and set it in the web page.
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String query = "SELECT TOP 1 catID FROM Category ORDER BY catID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                String newCatID;
                if (cmd.ExecuteScalar() != null)
                {
                    String lastCatID = (cmd.ExecuteScalar().ToString()).Trim();
                    String chr = Convert.ToString(lastCatID[0]);
                    String temp = "";
                    for (int i = 1; i < lastCatID.Length; i++)
                    {
                        temp += Convert.ToString(lastCatID[i]);
                    }
                    temp = Convert.ToString(Convert.ToInt16(temp) + 1);
                    newCatID = chr;
                    for (int i = 1; i < lastCatID.Length - temp.Length; i++)
                    {
                        newCatID += "0";
                    }
                    newCatID += temp;
                }
                else
                {
                    newCatID = "C00001";
                }

                AddNewCatID.InnerHtml = newCatID;
                conn.Close();
            }
            catch (SqlException e)
            {
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }
        
        protected void CancelAddcategoryBtn_Click(object sender, EventArgs e) //Clears all text boxes
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "myFunction();", true);
        }

        protected void CatFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String catID = UpdateCategoryTextBox.Text;

                string check = "select count(*) from Category WHERE catID='" + catID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT name FROM Category WHERE catID='" + catID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UpdateCatNameTextBox.Text = dr["name"].ToString();
                    }
                    UpdateCatID.InnerHtml = catID;
                    updatecategoryrInitState.Style.Add("display", "none");
                    updateCategorySecondState.Style.Add("display", "block");
                    updateCategory.Style.Add("display", "block");
                    UpdateCatIDValidator.InnerHtml = "";
                }
                else
                {
                    updatecategoryrInitState.Style.Add("display", "block");
                    updateCategorySecondState.Style.Add("display", "none");
                    updateCategory.Style.Add("display", "block");
                    UpdateCategoryNameValidator.InnerHtml = "Invalid Category ID";
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }


        protected void LocFindBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String locID = UpdateLocationTextBox.Text;

                string check = "select count(*) from Location WHERE locID='" + locID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT name FROM Location WHERE locID='" + locID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UpdateLocNameTextBox.Text = dr["name"].ToString();
                    }
                    UpdateLocID.InnerHtml = locID;
                    updatelocationInitState.Style.Add("display", "none");
                    updatelocationSecondState.Style.Add("display", "block");
                    updateLocation.Style.Add("display", "block");
                    UpdateLocIDValidator.InnerHtml = "";
                }
                else
                {
                    updatelocationInitState.Style.Add("display", "block");
                    updatelocationSecondState.Style.Add("display", "none");
                    updateLocation.Style.Add("display", "block");
                    UpdateLocationNameValidator.InnerHtml = "Invalid Location ID";
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                responseArea.Style.Add("color", "Yellow");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(e.ToString());
            }
        }

        protected void UpdateCatBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String catID = UpdateCategoryTextBox.Text;
                string insertion_Category = "UPDATE Category SET name = @name WHERE catID='" + catID + "'";
                SqlCommand cmd = new SqlCommand(insertion_Category, conn);
                cmd.Parameters.AddWithValue("@name", UpdateCatNameTextBox.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "updateCategoryClearAll", "updateCategoryClearAll()", true);

                responseArea.Style.Add("color", "green");
                responseArea.InnerHtml = "Category '" + catID + "' updated successfully!";
                updatecategoryrInitState.Style.Add("display", "block");
                updateCategorySecondState.Style.Add("display", "none");
                updateCategory.Style.Add("display", "block");
                UpdateCategoryTextBox.Text = "";
            }
            catch (Exception ex)
            {
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }

        protected void UpdateLocBtn_click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();
                String locID = UpdateLocationTextBox.Text;
                string insertion_Location = "UPDATE Location SET name = @name WHERE locID='" + locID + "'";
                SqlCommand cmd = new SqlCommand(insertion_Location, conn);
                cmd.Parameters.AddWithValue("@name", UpdateLocNameTextBox.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "updateLocationClearAll", "updateLocationClearAll()", true);

                responseArea.Style.Add("color", "green");
                responseArea.InnerHtml = "Location '" + locID + "' updated successfully!";
                updatelocationInitState.Style.Add("display", "block");
                updatelocationSecondState.Style.Add("display", "none");
                updateLocation.Style.Add("display", "block");
                UpdateLocationTextBox.Text = "";
            }
            catch (Exception ex)
            {
                responseArea.Style.Add("color", "orangered");
                responseArea.InnerHtml = "There were some issues with the database. Please try again later.";
                Response.Write(ex.ToString());
            }
        }
    }
}
