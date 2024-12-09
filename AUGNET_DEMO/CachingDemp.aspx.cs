using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUGNET_DEMO
{
    public partial class CachingDemp : System.Web.UI.Page
    {
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Cache["data"] == null)
            {
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(connStr);

                MySqlDataAdapter da = new MySqlDataAdapter("Select *FROM Users;", con);
                ds = new DataSet();
                da.Fill(ds);
                Cache["data"] = ds;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                Label1.Text = "Data Loaded from database";

            }
            else
            {
                GridView1.DataSource = (DataSet)Cache["data"];
                GridView1 .DataBind();
                Label1.Text = "Data Loaded from cache";
            }
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Cache["data"] != null)
            {
                Cache.Remove("data");
            }
        }
    }
}