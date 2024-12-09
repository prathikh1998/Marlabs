using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUGNET_DEMO
{
    public partial class CacheDBApp : System.Web.UI.Page
    {
        DataSet ds;
        MySqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: Uncomment if you want to load data when the page is first loaded
            // if (!Page.IsPostBack)
            // {
            //     GetDataFromDB();
            // }
        }

        protected void GetDataFromCache()
        {
            if (Cache["DATASET"] != null)
            {
                ds = (DataSet)Cache["DATASET"];
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                Label1.Text = "No data available in the cache.";
            }
        }

        protected void GetDataFromDB()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            con = new MySqlConnection(connStr);
            string query = "SELECT * FROM Users";
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds,"User");

            // Check the name of the first table in the DataSet and print it
            string tableName = ds.Tables[0].TableName; // This will tell you the name of the table in the DataSet.
            Console.WriteLine("Table name in the DataSet: " + tableName);

            // Insert the DataSet into Cache
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);

            // Bind the GridView
            GridView1.DataSource = ds;
            GridView1.DataBind();

            Label1.Text = "Data loaded from database.";
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            GridView1.SelectedIndex = -1;
            GetDataFromCache();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetDataFromCache();
        }

        protected void GridView1_CancelIndex(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetDataFromCache();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Cache["DATASET"] == null)
            {
                Label1.Text = "No data available to update.";
                return;
            }

            ds = (DataSet)Cache["DATASET"];

            // Ensure the 'User' table exists in the dataset
            if (ds.Tables.Contains("User"))
            {
                // Set the primary key for the "User" table (assuming "ID" is the primary key)
                if (ds.Tables["User"].PrimaryKey.Length == 0)
                {
                    // If there's no primary key set, set it dynamically
                    ds.Tables["User"].PrimaryKey = new DataColumn[] { ds.Tables["User"].Columns["ID"] };
                }

                // Try to find the row using the primary key
                DataRow dr = ds.Tables["User"].Rows.Find(e.Keys["ID"]);

                if (dr != null)
                {
                    // Update the row with new values
                    dr["Username"] = e.NewValues["Username"];
                    dr["Email"] = e.NewValues["Email"];

                    // Update the cache with the modified dataset
                    Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);

                    // Refresh the GridView
                    GridView1.EditIndex = -1;
                    GetDataFromCache();
                }
                else
                {
                    Label1.Text = "Error: Unable to find the record to update.";
                }
            }
            else
            {
                Label1.Text = "Error: 'User' table not found in the dataset.";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Cache["DATASET"] == null)
            {
                Label1.Text = "No data available to delete.";
                return;
            }

            ds = (DataSet)Cache["DATASET"];

            // Ensure the 'User' table exists in the dataset
            if (ds.Tables.Contains("User"))
            {
                // Set the primary key for the "User" table (assuming "ID" is the primary key)
                if (ds.Tables["User"].PrimaryKey.Length == 0)
                {
                    // If there's no primary key set, set it dynamically
                    ds.Tables["User"].PrimaryKey = new DataColumn[] { ds.Tables["User"].Columns["ID"] };
                }

                // Try to find the row using the primary key
                DataRow dr = ds.Tables["User"].Rows.Find(e.Keys["ID"]);

                if (dr != null)
                {
                    // Delete the row
                    dr.Delete();

                    // Update the cache with the modified dataset
                    Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);

                    // Refresh the GridView
                    GetDataFromCache();
                }
                else
                {
                    Label1.Text = "Error: Unable to find the record to delete.";
                }
            }
            else
            {
                Label1.Text = "Error: 'User' table not found in the dataset.";
            }
        }


        protected void Updatedb_Click(object sender, EventArgs e)
        {
            // Check if the dataset is in the cache
            if (Cache["DATASET"] == null)
            {
                Label1.Text = "No data to update or delete.";
                return;
            }

            // Get the cached dataset
            ds = (DataSet)Cache["DATASET"];

            // Set up the connection string and the connection object
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            con = new MySqlConnection(connStr);

            // Begin the database operation (using a transaction for safety, optional)
            MySqlTransaction transaction = null;
            try
            {
                con.Open();
                transaction = con.BeginTransaction();

                // Loop through each row of the dataset to handle updates and deletions
                foreach (DataRow row in ds.Tables["User"].Rows)
                {
                    // Handle rows marked for deletion
                    if (row.RowState == DataRowState.Deleted)
                    {
                        // Get the ID of the row to be deleted
                        int id = Convert.ToInt32(row["ID", DataRowVersion.Original]);

                        // Prepare the SQL DELETE command
                        string deleteQuery = "DELETE FROM Users WHERE ID = @ID";
                        MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, con, transaction);
                        deleteCmd.Parameters.AddWithValue("@ID", id);

                        // Execute the delete command
                        deleteCmd.ExecuteNonQuery();
                    }
                    // Handle rows marked for modification
                    else if (row.RowState == DataRowState.Modified)
                    {
                        // Retrieve the ID and updated values from the row
                        int id = Convert.ToInt32(row["ID"]);
                        string username = row["Username"].ToString();
                        string email = row["Email"].ToString();
                        DateTime createdAt = Convert.ToDateTime(row["CreatedAt"]);

                        // Prepare the SQL UPDATE command
                        string updateQuery = "UPDATE Users SET Username = @Username, Email = @Email, CreatedAt = @CreatedAt WHERE ID = @ID";
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, con, transaction);

                        // Add parameters to the command to avoid SQL injection
                        updateCmd.Parameters.AddWithValue("@Username", username);
                        updateCmd.Parameters.AddWithValue("@Email", email);
                        updateCmd.Parameters.AddWithValue("@CreatedAt", createdAt.ToString("yyyy-MM-dd HH:mm:ss"));
                        updateCmd.Parameters.AddWithValue("@ID", id);

                        // Execute the update command
                        updateCmd.ExecuteNonQuery();
                    }
                }

                // Commit the transaction if everything is successful
                transaction.Commit();

                // Refresh the data (reload the data from the database and update the cache)
                GetDataFromDB();

                // Optionally, display a message that the data was updated or deleted successfully
                Label1.Text = "Data updated and deleted successfully.";
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any error occurs
                if (transaction != null) transaction.Rollback();

                // Handle any errors (log or show an error message to the user)
                Label1.Text = "Error: " + ex.Message;
            }
            finally
            {
                // Close the connection
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Load fresh data from the database if needed
            GetDataFromDB();
        }
    }
}
