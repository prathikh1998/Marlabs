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
    public partial class CommandBuilderDemo : System.Web.UI.Page
    {

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoadFnc(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);
            string query = "select *from Users where ID =" + TextBox1.Text;
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds,"User");

            ViewState["SQL_QUERY"] = query;
            ViewState["DATASET"] = ds;

            if (ds.Tables["User"].Rows.Count > 0)
            {
                DataRow row = ds.Tables["User"].Rows[0];
                TextBox2.Text = row["Username"].ToString();
                TextBox3.Text = row["Email"].ToString();
                TextBox4.Text = row["CreatedAt"].ToString();
                Label1.Text = null;
               // DataBind();

            }
            else
            {
                Label1.Text = "no record found with the ID = "+TextBox1.Text;
                Label1.ForeColor = System.Drawing.Color.Red;
                TextBox2.Text = null;
                TextBox3.Text = null;
                TextBox4.Text = null;

            }


        }

        protected void UpdateFnc(object sender, EventArgs e)
        {
            if(TextBox1.Text !=null && TextBox2.Text != null && TextBox3.Text != null && TextBox4.Text != null)
            {
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(connStr);

               
                MySqlDataAdapter da = new MySqlDataAdapter((string)ViewState["SQL_QUERY"],con);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(da);
                DataSet ds = (DataSet)ViewState["DATASET"];

                if (ds.Tables["User"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["User"].Rows[0];
                    dr["Username"] = TextBox2.Text;
                    dr["Email"] = TextBox3.Text;
                    dr["CreatedAt"] = TextBox4.Text;
                }
                int rowsupdated = da.Update(ds, "User");
                Label1.Text = rowsupdated.ToString();
                Label2.Text = builder.GetUpdateCommand().CommandText;
                Label3.Text = builder.GetInsertCommand().CommandText;
                Label4.Text = builder.GetDeleteCommand().CommandText;
            }
        }

        protected void InsertFnc(object sender, EventArgs e)
        {
            if (TextBox2.Text != null && TextBox3.Text != null)
            {
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(connStr);

                // Check if the DataSet exists in ViewState; if not, initialize it
                DataSet ds = (DataSet)ViewState["DATASET"];
                if (ds == null)
                {
                    ds = new DataSet();
                    ViewState["DATASET"] = ds;
                }

                // Check if the "User" table exists in the DataSet; if not, create it
                if (ds.Tables["User"] == null)
                {
                    DataTable userTable = new DataTable("User");
                    userTable.Columns.Add("Username", typeof(string));
                    userTable.Columns.Add("Email", typeof(string));
                    ds.Tables.Add(userTable);
                }

                // Create a new row for the DataTable
                DataRow newRow = ds.Tables["User"].NewRow();

                // Populate the new row with data from the TextBoxes
                newRow["Username"] = TextBox2.Text;
                newRow["Email"] = TextBox3.Text;

                // Add the new row to the DataTable
                ds.Tables["User"].Rows.Add(newRow);

                // Set the INSERT command for the DataAdapter
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Users", con);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(da);
                da.InsertCommand = new MySqlCommand("INSERT INTO Users (Username, Email) VALUES (@Username, @Email)", con);

                // Add parameters for the INSERT command
                da.InsertCommand.Parameters.AddWithValue("@Username", TextBox2.Text);
                da.InsertCommand.Parameters.AddWithValue("@Email", TextBox3.Text);

                try
                {
                    // Call Update to insert the new row into the database
                    da.Update(ds, "User");

                    // Provide feedback to the user
                    Response.Write("Record inserted successfully.");
                }
                catch (Exception ex)
                {
                    // Handle any errors that occurred during the insert
                    Response.Write("Error: " + ex.Message);
                }
            }
            else
            {
                // If any required input is missing, prompt the user to fill out the form
                Response.Write("Please provide all required fields.");
            }
        }



        protected void DeleteFnc(object sender, EventArgs e)
        {
            if (TextBox1.Text!=null)
            {
                string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(connStr);

                // Use the SELECT command stored in ViewState to fetch data
                MySqlDataAdapter da = new MySqlDataAdapter((string)ViewState["SQL_QUERY"], con);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(da);

                // Get the current DataSet from ViewState
                DataSet ds = (DataSet)ViewState["DATASET"];

                if (ds.Tables["User"].Rows.Count > 0) 
                {
                    // Find the row to delete based on ID (which is in TextBox1)
                    DataRow[] rowsToDelete = ds.Tables["User"].Select("ID = " + TextBox1.Text);

                    if (rowsToDelete.Length > 0)
                    {
                        // Mark the row for deletion
                        rowsToDelete[0].Delete();

                        // Set the DELETE command for the DataAdapter using parameters to prevent SQL injection
                        da.DeleteCommand = new MySqlCommand("DELETE FROM Users WHERE ID = @ID", con);
                        da.DeleteCommand.Parameters.AddWithValue("@ID", TextBox1.Text);
                        


                        // Execute the update operation (this will delete the row in the database)
                        da.Update(ds, "User");

                        // Provide feedback to the user
                        Response.Write("Record deleted successfully.");
                        TextBox1.Text = null;
                        TextBox2.Text = null;
                        TextBox3.Text = null;
                        TextBox4.Text = null;

                    }
                    else
                    {
                        // No row found with the given ID
                        Response.Write("No record found to delete.");
                    }
                }
                else
                {
                    // No data available in the DataSet
                    Response.Write("No users found.");
                }
            }
            else
            {
                // ID is empty or invalid
                Response.Write("Please provide a valid User ID.");
            }
        }

    }
}