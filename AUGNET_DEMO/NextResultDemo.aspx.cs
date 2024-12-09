using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI;

namespace AUGNET_DEMO
{
    public partial class NextResultDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Use MySqlConnection instead of SqlConnection
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("SELECT *FROM Users LIMIT 5;\r\n select * from employees;", con);

            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                // First result set (Users table)
                UsersGridView1.DataSource = reader;
                UsersGridView1.DataBind();

                // Move to the next result set (employees table)
                if (reader.NextResult())
                {
                    EmployeesGridView2.DataSource = reader;
                    EmployeesGridView2.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., log the error)
                Console.WriteLine("Error: " + ex.Message);
                // Display the error message on the page
            }
            finally
            {
                con.Close();
            }
        }
    }
}
