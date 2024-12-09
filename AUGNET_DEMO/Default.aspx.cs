using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace AUGNET_DEMO
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the connection string from the web.config file
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Create a new MySQL connection
            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Define the SQL command to create a table
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Users (
                            ID INT AUTO_INCREMENT PRIMARY KEY,
                            Username VARCHAR(50) NOT NULL,
                            Email VARCHAR(100),
                            CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                        );
                    ";

                    // Create a MySQL command object
                    MySqlCommand cmd = new MySqlCommand(createTableQuery, conn);

                    // Execute the command
                    cmd.ExecuteNonQuery();

                    // Optionally, output success message to user
                    Response.Write("Table 'Users' created successfully.");
                }
                catch (MySqlException ex)
                {
                    // Handle MySQL exceptions (e.g., if the table already exists, connection issues, etc.)
                    Response.Write("MySQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions (e.g., connection errors, etc.)
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }
}
