using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUGNET_DEMO
{
    public partial class DataCopy : System.Web.UI.Page
    {

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDataFromDB();
            }

        }

        protected void GetDataFromDB()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);
            string query = "select *from Users";
            MySqlCommand sourceCommand = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = sourceCommand.ExecuteReader();
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connStr))
            {
                bulkcopy.DestinationTableName = "UserBackup";
                bulkcopy.WriteToServer(reader);
            }
            ds = new DataSet();
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);

            GridView1.DataSource = ds;
            GridView1.DataBind();

            Label1.Text = "Data loaded from database";

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            GridView1.SelectedIndex = -1;

            GetDataFromDB();
        }
    }
}