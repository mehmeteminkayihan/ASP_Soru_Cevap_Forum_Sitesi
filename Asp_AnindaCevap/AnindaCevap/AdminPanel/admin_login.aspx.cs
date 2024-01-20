using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace AnindaCevap.AdminPanel
{
    public partial class admin_login : System.Web.UI.Page
    {
     
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string userName = txtName.Text.Trim();
                    string password = txtPassword.Text.Trim();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, user_nickname, authorization_id FROM Users WHERE user_nickname = @name AND user_password = @password", con);
                    cmd.Parameters.AddWithValue("@name", userName);

                  
                    string hashedPassword;
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            builder.Append(bytes[i].ToString("x2"));
                        }
                        hashedPassword = builder.ToString();
                    }
                    cmd.Parameters.AddWithValue("@password", hashedPassword);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                       
                        while (reader.Read())
                        {
                           
                            int authorizationId = Convert.ToInt32(reader["authorization_id"]);

                            if (authorizationId == 1)
                            {
                               
                                Session["user_id"] = reader["user_id"].ToString();
                                Session["user_nickname"] = reader["user_nickname"].ToString();

                                Response.Redirect("default.aspx");
                            }
                            else
                            {
                               
                                Response.Write("<script>alert('Giriş yapmak için gerekli yetkiye sahip değilsiniz.');</script>");
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        con.Close();
                        Response.Write("<script>alert('Hatalı kullanıcı adı veya şifre');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}
