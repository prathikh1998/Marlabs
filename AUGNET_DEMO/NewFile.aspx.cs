using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AUGNET_DEMO
{
    public partial class NewFile : System.Web.UI.Page
    {
        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    GetDataFromDB();
            //}
        }

        // Method to fetch data from the database
        protected void GetDataFromDB()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);
            string query = "SELECT * FROM Users";
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds, "User");
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);

            GridView1.DataSource = ds;
            GridView1.DataBind();

            Label1.Text = "Data loaded from the database";
        }

        // Handle paging
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            GridView1.SelectedIndex = -1;

            GetDataFromDB();
        }

        // Handle edit action
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetDataFromDB();
        }

        // Cancel edit action
        protected void GridView1_CancelIndex(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetDataFromDB();
        }

        // Update a record in the database
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            TextBox username = GridView1.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
            string Username = username.Text;

            TextBox email = GridView1.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
            string Email = email.Text;

            TextBox createdAt = GridView1.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;
            string createdAtString = createdAt.Text;

            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("UPDATE Users SET Username = @Username, Email = @Email, CreatedAt = @CreatedAt WHERE ID = @ID", con);

            DateTime CreatedAt;
            if (DateTime.TryParse(createdAtString, out CreatedAt))
            {
                // Format date for MySQL
                string formattedCreatedAt = CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");

                cmd.Parameters.AddWithValue("@CreatedAt", formattedCreatedAt);
            }
            else
            {
                // Handle invalid date format
                Response.Write("Invalid date format for CreatedAt.");
            }

            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@ID", ID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            GridView1.EditIndex = -1;
            GetDataFromDB();
        }

        // Delete a record from the database
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Users WHERE ID = @ID", con);

            cmd.Parameters.AddWithValue("@ID", ID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            GridView1.EditIndex = -1;
            GetDataFromDB();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetDataFromDB();
        }

        // Insert a new user into the database

    }
}
