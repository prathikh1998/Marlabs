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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the MySQL connection string from web.config
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Use MySqlConnection instead of SqlConnection
            MySqlConnection con = new MySqlConnection(connStr);

            // Correct SQL query with proper syntax
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", con);

            try
            {
                // Open the connection
                con.Open();

                // Execute the query and bind the results to the GridView
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataSet ds = new DataSet();
                    DataTable db = new DataTable("Users1");
                    db.Columns.Add("ID");
                    db.Columns.Add("Username");
                    db.Columns.Add("Email");
                    db.Columns.Add("CreatedAt1");

                    while (reader.Read())
                    {
                        DataRow row = db.NewRow();
                        row["ID"] = reader.GetInt32("ID");
                        row["Username"] = reader.GetString("Username");
                        row["Email"] = reader.GetString("Email");
                        row["CreatedAt1"] = reader.GetDateTime("CreatedAt");
                        db.Rows.Add(row);

                    }
                    GridView1.DataSource = db;
                    GridView1.DataBind();

                }

                #region
                //GridView1.DataSource = reader; // Bind the data reader to the GridView
                //for (int i = 0; i < 5; i++)
                //{
                //    Label1.Text += reader.Read().ToString() + " ";
                //}

                //GridView1.DataBind(); // Bind data to the GridView
                #endregion
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}