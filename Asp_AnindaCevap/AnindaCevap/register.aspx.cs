using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnindaCevap
{
    public partial class register : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string userName = txtUserName.Text.Trim();
                    string userEmail = txtEmail.Text.Trim();
                    string userPassword = txtUserPassword.Text.Trim();

                
                    bool IsValidUsername(string username)
                    {
                        return username.Length >= 3;
                    }

                    bool IsValidPassword(string password)
                    {
                        return password.Length >= 4 &&
                               password.Any(char.IsUpper) &&
                               password.Any(char.IsLower) &&
                               password.Any(char.IsDigit) &&
                               password.Any(c => !char.IsLetterOrDigit(c));
                    }

                    bool IsValidEmail(string email)
                    {
                        try
                        {
                            var addr = new System.Net.Mail.MailAddress(email);
                            return addr.Address == email;
                        }
                        catch
                        {
                            return false;
                        }
                    }

                    bool isContactChecked = checkContact.Checked;

                    if (!isContactChecked)
                    {
                        Response.Write("<script>alert('Lütfen Gizlilik Politaksını kabul ettiğiniz onaylayınız.');</script>");
                        return; 
                    }

                    if (!IsValidUsername(userName))
                    {
                    
                        Response.Write("<script>alert('Kullanıcı adı en az 3 karakter olmalıdır.');</script>");
                        return; 
                    }

                   
                    if (!IsValidEmail(userEmail))
                    {
                     
                        Response.Write("<script>alert('Geçersiz e-posta formatı. Lütfen geçerli bir e-posta adresi girin.');</script>");
                        return;
                    }

                  
                    if (!IsValidPassword(userPassword))
                    {
                      
                        Response.Write("<script>alert('Şifre en az 4 karakter olmalı ve büyük harf, küçük harf, rakam ve özel karakter içermelidir.');</script>");
                        return;
                    }

                    SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE user_nickname = @name OR user_email = @email", con);
                    checkUserCmd.Parameters.AddWithValue("@name", userName);
                    checkUserCmd.Parameters.AddWithValue("@email", userEmail);

                    int count = (int)checkUserCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        
                        Response.Write("<script>alert('Bu kullanıcı adı veya e-posta zaten kullanılıyor.');</script>");
                    }
                    else
                    {
                        
                        string HashPassword(string passwordToHash)
                        {
                            using (SHA256 sha256Hash = SHA256.Create())
                            {
                                
                                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(passwordToHash));

                               
                                StringBuilder builder = new StringBuilder();
                                for (int i = 0; i < bytes.Length; i++)
                                {
                                    builder.Append(bytes[i].ToString("x2")); 
                                }
                                return builder.ToString();
                            }
                        }

                        string hashedPassword = HashPassword(userPassword);

                       
                        SqlCommand cmd = new SqlCommand("INSERT INTO Users (user_nickname, user_email, user_password, registration_date, user_contract, authorization_id) " +
                            "VALUES (@name, @email, @password, @date, @contact, @authorization)", con);

                        //sql injeksiyonu engellemek için parametre ile verileri alıyoruz
                        cmd.Parameters.AddWithValue("@name", userName);
                        cmd.Parameters.AddWithValue("@email", userEmail);
                        cmd.Parameters.AddWithValue("@password", hashedPassword); 
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@contact", checkContact.Checked);
                        cmd.Parameters.AddWithValue("@authorization", 2);

                        //yazılan soruguyu çalıştırmak için
                        cmd.ExecuteNonQuery();

                        SqlCommand addUserDetailsCmd = new SqlCommand("INSERT INTO user_details (display_name, user_id) VALUES (@display_name, (SELECT user_id FROM Users WHERE user_nickname = @name))", con);
                        addUserDetailsCmd.Parameters.AddWithValue("@display_name", userName);
                        addUserDetailsCmd.Parameters.AddWithValue("@name", userName);

                        addUserDetailsCmd.ExecuteNonQuery();

                        con.Close();

                        Response.Write("<script>alert('Kayıt başarılı. Giriş yap sayfasına gidin.');</script>");
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