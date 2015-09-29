using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FixAMz_WebApplication
{
    public partial class ManaageAssetsUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }










        // Dispose
        protected void DeleteUserFindBtn_Click(object sender, EventArgs e)
        {/*
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemUserConnectionString"].ConnectionString);
                conn.Open();

                String empID = DeleteUserEmpIDTextBox.Text;

                string check = "select count(*) from SystemUser WHERE empID='" + empID + "'";
                SqlCommand cmd = new SqlCommand(check, conn);
                int res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (res == 1)
                {
                    String query = "SELECT empID, firstName, lastName, contactNo, email FROM Employee WHERE empID='" + empID + "'";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        DeleteEmpID.InnerHtml = dr["empID"].ToString();
                        DeleteFirstName.InnerHtml = dr["firstName"].ToString();
                        DeleteLastName.InnerHtml = dr["lastName"].ToString();
                        DeleteContact.InnerHtml = dr["contactNo"].ToString();
                        DeleteEmail.InnerHtml = dr["email"].ToString();
                    }
                    deleteUserInitState.Style.Add("display", "none");
                    deleteUserSecondState.Style.Add("display", "block");
                    DeleteUserContent.Style.Add("display", "block");
                    DeleteUserEmpIDValidator.InnerHtml = "";
                    DeleteUserEmpIDTextBox.Focus();
                }
                else
                {
                    deleteUserInitState.Style.Add("display", "block");
                    deleteUserSecondState.Style.Add("display", "none");
                    DeleteUserContent.Style.Add("display", "block");
                    DeleteUserEmpIDValidator.InnerHtml = "Employee ID not found!";
                    DeleteEmpID.Focus();
                }
          * */
        }
    }
}