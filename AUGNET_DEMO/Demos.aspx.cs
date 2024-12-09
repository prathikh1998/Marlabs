using System;
using System.Configuration;
using System.Web.UI;
using MySql.Data.MySqlClient; // Add MySQL namespace

namespace AUGNET_DEMO
{
    public partial class Demos : System.Web.UI.Page
    {
        private void BindGrid()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", con);

            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                GridView1.DataSource = reader; // Bind the data reader to the GridView
                GridView1.DataBind(); // Bind data to the GridView
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., log the error)
                Console.WriteLine("Error: " + ex.Message);
                Label1.Text = "Error: " + ex.Message;  // Display the error message on the page
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }


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
                MySqlDataReader reader = cmd.ExecuteReader();
                GridView1.DataSource = reader; // Bind the data reader to the GridView
                GridView1.DataBind(); // Bind data to the GridView
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


        protected void Button_click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("AddUser", con);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Username", "Sunny1");
                cmd.Parameters.AddWithValue("@p_Email", "Sunny1@gmail.com");

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Success message
                    Label1.Text = "User added successfully!";
                    Label1.ForeColor = System.Drawing.Color.Green;  // Optionally, set the text color to green for success
                    BindGrid(); // Refresh the GridView to show the newly added user
                }
                else
                {
                    // Failure message
                    Label1.Text = "No rows were inserted.";
                    Label1.ForeColor = System.Drawing.Color.Red;  // Optionally, set the text color to red for failure
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Label1.Text = "Error: " + ex.Message;
                Label1.ForeColor = System.Drawing.Color.Red;  // Set color to red for errors
            }
            finally
            {
                con.Close();
            }
        }


    }
}