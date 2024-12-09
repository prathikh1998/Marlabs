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
    public partial class SqlAdapterDemo : System.Web.UI.Page
    {
        DataSet ds;
        static bool flag = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getData();
                if (string.IsNullOrWhiteSpace(TextBox1.Text))
                {
                    TextBox1.Text = "1";  // Set a default value (e.g., "1")
                }

                GridView1.DataSource = ds.Tables["Users"];
                GridView1.DataBind();
                
            }
            
        }

        public void getData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);

            MySqlDataAdapter da = new MySqlDataAdapter("GetUserById;", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@userID", TextBox1.Text);
            MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT *FROM Employees;", con);
           
            //da.SelectCommand = new MySqlCommand("Select * FROM Users;");
            ds = new DataSet();
            ds.Tables.Add(new DataTable("Users"));
            ds.Tables.Add(new DataTable("Employees"));
            da.Fill(ds.Tables["Users"]);
            da1.Fill(ds.Tables["Employees"]);

           
            //Response.Write(con.State);
        }

        protected void ButtonClick(object sender, EventArgs e)
        {
            getData();
            Response.Write("Button Clicked");
            if (flag)
            {
                GridView1.DataSource = ds.Tables["Employees"];
                GridView1.DataBind();
                flag = false;
            }
            else
            {
                GridView1.DataSource = ds.Tables["Users"];
                GridView1.DataBind();
                flag = true;
            }
           
        }
    }
}