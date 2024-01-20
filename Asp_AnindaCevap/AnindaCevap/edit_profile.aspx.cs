using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace AnindaCevap
{
    public partial class edit_profile : System.Web.UI.Page
    {
        
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

      
        protected void Page_Load(object sender, EventArgs e)
        {
           
            int user_id = Convert.ToInt32(Session["user_id"]);

          
            if (!IsPostBack)
            {
             
                KullaniciDetaylariniYukle(user_id);
            }
        }

      
        protected void btnSave_Click(object sender, EventArgs e)
        {
         
            int user_id = Convert.ToInt32(Session["user_id"]);

          
            string gorunenAd = txtName.Text;
            string konum = txtCountry.Text;
            string hakkimda = txtAbout.Text;

          
            if (KullaniciDetaylariMevcutMu(user_id))
            {
                
                KullaniciDetaylariniGuncelle(user_id, gorunenAd, konum, hakkimda);
            }
            else
            {
               
                KullaniciDetaylariEkle(user_id, gorunenAd, konum, hakkimda);
            }

            
            KullaniciDetaylariniYukle(user_id);
        }

       
        protected void btnChange_Click(object sender, EventArgs e)
        {
           
            int user_id = Convert.ToInt32(Session["user_id"]);

          
            string currentPassword = txtPassword.Text;

          
            string newPassword = txtNewPassword.Text;
            string newPasswordRepeat = txtNewPassword1.Text;

            
            if (CheckCurrentPassword(user_id, currentPassword))
            {
                
                if (newPassword == newPasswordRepeat)
                {
                  
                    string hashedPassword = HashPassword(newPassword);

                   
                    UpdatePassword(user_id, hashedPassword);

                   
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Şifre başarıyla değiştirildi.');", true);
                }
                else
                {
                 
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Yeni şifreler uyuşmuyor.');", true);
                }
            }
            else
            {
           
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Mevcut şifre yanlış.');", true);
            }
        }

      
        private bool CheckCurrentPassword(int user_id, string currentPassword)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                string query = "SELECT user_password FROM Users WHERE user_id = @user_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                         
                            string hashedPasswordFromDb = dr["user_password"].ToString();
                            return VerifyPassword(currentPassword, hashedPasswordFromDb);
                        }
                    }
                }
            }
            return false;
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private void UpdatePassword(int user_id, string newPassword)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                string query = "UPDATE Users SET user_password = @newPassword WHERE user_id = @user_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool KullaniciDetaylariMevcutMu(int user_id)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM User_details WHERE user_id = @user_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

   
        private void KullaniciDetaylariniGuncelle(int user_id, string gorunenAd, string konum, string hakkimda)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                string query = "UPDATE User_details SET display_name = @display_name, country = @country, user_about = @user_about WHERE user_id = @user_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@display_name", gorunenAd);
                    cmd.Parameters.AddWithValue("@country", konum);
                    cmd.Parameters.AddWithValue("@user_about", hakkimda);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void KullaniciDetaylariEkle(int user_id, string gorunenAd, string konum, string hakkimda)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                string query = "INSERT INTO User_details (user_id, display_name, country, user_about) VALUES (@user_id, @display_name, @country, @user_about)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@display_name", gorunenAd);
                    cmd.Parameters.AddWithValue("@country", konum);
                    cmd.Parameters.AddWithValue("@user_about", hakkimda);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void KullaniciDetaylariniYukle(int user_id)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                string query = "SELECT * FROM User_details WHERE user_id = @user_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            
                            lblName.Text = dr["display_name"].ToString();
                            txtName.Text = dr["display_name"].ToString(); 
                            txtCountry.Text = dr["country"].ToString();
                            txtAbout.Text = dr["user_about"].ToString();
                        }
                    }
                }
            }
        }

       
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] enteredPasswordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                string enteredPasswordHashString = BitConverter.ToString(enteredPasswordHash).Replace("-", "").ToLower();
                return enteredPasswordHashString.Equals(storedHash);
            }
        }
    }
}
