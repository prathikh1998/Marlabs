using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUGNET_DEMO
{
    public partial class TransactionExample : System.Web.UI.Page
    {
        // Connection string for MySQL
        string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Any code you need to run when the page loads
        }

        protected void btnExecuteTransaction_Click(object sender, EventArgs e)
        {
            // Call the method to execute the transaction
            ExecuteTransaction();
        }

        private void ExecuteTransaction()
        {
            // Create a new MySQL connection
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                // Open the connection
                con.Open();

                // Start a new transaction
                MySqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // First SQL command: Update user information in MySQL
                    MySqlCommand cmd1 = new MySqlCommand("UPDATE Users SET Username = 'JohnDoe' WHERE ID = 10", con, transaction);
                    cmd1.ExecuteNonQuery();

                    // Second SQL command: Insert into another table (UserBackup) in MySQL
                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO UserBackup (Username, Email,CreatedAt) VALUES (@Username, @Email,@CreatedAt)", con, transaction);
                    cmd2.Parameters.AddWithValue("@Username", "Prathik");
                    cmd2.Parameters.AddWithValue("@Email", "Prathikh5@gmail.com");
                    cmd2.Parameters.AddWithValue("@CreatedAt", "hjello");
                    cmd2.ExecuteNonQuery();

                    // If both commands succeed, commit the transaction
                    transaction.Commit();
                    lblResult.Text = "Transaction committed successfully.";
                    lblResult.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    // If there is an error, roll back the transaction
                    transaction.Rollback();
                    lblResult.Text = "Transaction failed: " + ex.Message;
                    lblResult.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
