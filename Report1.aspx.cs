using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace FixAMz_WebApplication
{
    public partial class Report1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["test"]))
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
            sample.InnerHtml = html;
        }

        protected void SampleLink_clicked(object sender, EventArgs e)
        {
            HtmlAnchor a = (HtmlAnchor)sender;
            Response.Write("link " + a.ID);
        }
    }
}