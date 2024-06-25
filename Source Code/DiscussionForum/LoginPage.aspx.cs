using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiscussionForum
{
    public partial class LoginPage : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(@"Data Source = ANSH-UNIVERSE\SQLEXPRESS08; Initial Catalog=DiscussionForum; Integrated Security = true");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected Dictionary<string, string> GetUserInfo(string email)
        {
            Dictionary<string, string> obj = new Dictionary<string, string>();
            // get the user ID
            SqlCommand cmd = new SqlCommand("select user_id from users where email = @Email", con);
            cmd.Parameters.AddWithValue("@Email", email);
            int uid = Convert.ToInt32(cmd.ExecuteScalar());
            string userIdString = uid.ToString();
            obj.Add("userId", userIdString);

            // get user role
            SqlCommand cmd2 = new SqlCommand("select role_id from users where email = @Email", con);
            cmd2.Parameters.AddWithValue("@Email", email);
            int roleId = (int)cmd2.ExecuteScalar();
            if(roleId == 1)
            {
                obj.Add("role", "admin");
            } else
            {
                obj.Add("role", "n-user");
            }

            // get user name
            SqlCommand cmd3 = new SqlCommand("select user_name from users where email = @Email", con);
            cmd3.Parameters.AddWithValue("@Email", email);
            string userName = (string)cmd3.ExecuteScalar();
            obj.Add("username", userName);

            // add the email to the object
            obj.Add("email", email);

            return obj;
        }

        protected void HandleLogin(object sender, EventArgs e)
        {
            LoginLogic();
        }

        protected void LoginLogic()
        {
            con.Open();
            // checking if the email exists in the table
            string query = "select count(*) from users where email = @Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", txtLoginEmail.Text);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                // email exists now check for the password
                string query2 = "select password from users where email = @Email";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@Email", txtLoginEmail.Text);
                string hashedPassword = (string)cmd2.ExecuteScalar();

                if (BCrypt.Net.BCrypt.Verify(txtLoginPassword.Text, hashedPassword))
                {
                    Dictionary<string, string> userInfo = GetUserInfo(txtLoginEmail.Text);
                    Session["UserID"] = userInfo["userId"];
                    Session["role"] = userInfo["role"];
                    Session["username"] = userInfo["username"];
                    Session["email"] = userInfo["email"];
                }
                Response.Redirect("~/Default.aspx");

            }
            else
            {
                // password doesn't exist
                loginError.Attributes["class"] = "text-red-500";
                loginError.InnerText = "Email or Password Invalid!";
                con.Close();
                return;
            }
            con.Close();
        }
        protected void HandleRegister(object sender, EventArgs e)
        {
            string username = txtRegisterUsername.Text;
            string email = txtRegisterEmail.Text;
            string password = txtConfirmPassword.Text;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 12);

            con.Open();
            SqlCommand cmd = new SqlCommand("RegisterUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);

            cmd.ExecuteNonQuery();
            clearRegisterForm();
            con.Close();
            LoginLogic(); // login the user just after the user registers
            Response.Redirect("~/Default.aspx");
        }

        protected void clearRegisterForm()
        {
            txtRegisterUsername.Text = string.Empty;
            txtRegisterEmail.Text = string.Empty;
            txtRegisterPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        } 
        protected void clearLoginForm()
        {
            txtLoginEmail.Text = string.Empty;
            txtLoginPassword.Text = string.Empty;
        }
    }
}