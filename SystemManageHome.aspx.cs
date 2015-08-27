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
    public partial class SystemManageHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setCatID();
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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
            conn.Open();
            //UpdateCatNameTextBox.Visible = true;
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select name from Category where catID='" + UpdateCategoryTextBox.Text.Trim() + "'", conn);
            myReader = myCommand.ExecuteReader();

            if (myReader.Read())
            {
                UpdateCatNameTextBox.Text = myReader["name"].ToString();
                myReader.Close();
                conn.Close();
            }
        }
        
    }
    }
