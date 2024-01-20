using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace AnindaCevap
{
    public partial class user_profile : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    int user_id = Convert.ToInt32(Session["user_id"]);

                    string userDetailsQuery = "SELECT display_name, user_about, number_of_questions, number_of_answers, country FROM User_details WHERE user_id = @user_id";
                    using (SqlCommand cmd = new SqlCommand(userDetailsQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            lblName.Text = dr["display_name"].ToString();
                            lblAbout.Text = dr["user_about"].ToString();
                            lblQuestion.Text = dr["number_of_questions"].ToString();
                            lblAnswer.Text = dr["number_of_answers"].ToString();
                            lblCountry.Text = dr["country"].ToString();
                        }
                        dr.Close();
                    }

               
                    string userRegistrationDateQuery = "SELECT registration_date FROM Users WHERE user_id = @user_id";
                    using (SqlCommand cmd = new SqlCommand(userRegistrationDateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        object registrationDateObj = cmd.ExecuteScalar();
                        if (registrationDateObj != null)
                        {
                            lblDate.Text = FormatTimeElapsed(Convert.ToDateTime(registrationDateObj));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
                Response.Write("Hata: " + ex.Message);
            }
        }

        private string FormatTimeElapsed(DateTime registrationDate)
        {
            TimeSpan timeElapsed = DateTime.Now - registrationDate;

            if (timeElapsed.TotalSeconds < 60)
            {
                return $"{(int)timeElapsed.TotalSeconds} saniye önce";
            }
            else if (timeElapsed.TotalMinutes < 60)
            {
                return $"{(int)timeElapsed.TotalMinutes} dakika önce";
            }
            else if (timeElapsed.TotalHours < 24)
            {
                return $"{(int)timeElapsed.TotalHours} saat önce";
            }
            else if (timeElapsed.TotalDays < 30)
            {
                return $"{(int)timeElapsed.TotalDays} gün önce";
            }
            else if (timeElapsed.TotalDays < 365)
            {
                int months = (int)(timeElapsed.TotalDays / 30);
                return $"{months} ay önce";
            }
            else
            {
                int years = (int)(timeElapsed.TotalDays / 365);
                return $"{years} yıl önce";
            }
        }
    }
}
