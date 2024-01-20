using System;
using System.Data.SqlClient;
using System.Configuration;

namespace AnindaCevap.AdminPanel
{
    public partial class _default : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                DisplayCompanyInformation();
            }
        }

       
        private bool RecordExists()
        {
            bool exists = false;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT COUNT(*) FROM Company_information";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    exists = count > 0;
                }
            }

            return exists;
        }

 
        private void InsertRecord(string companyAbout, string companyPhoneNumber, string companyEmail, string companyAddress, string privacyPolicy)
        {
         
            companyAbout = companyAbout.Replace(Environment.NewLine, "<br>");
            privacyPolicy = privacyPolicy.Replace(Environment.NewLine, "<br>");

            string query = "INSERT INTO Company_information (company_about, company_phonenumber, company_email, company_address, privacy_policy) " +
                           "VALUES (@companyAbout, @companyPhoneNumber, @companyEmail, @companyAddress, @privacyPolicy)";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                   
                    cmd.Parameters.AddWithValue("@companyAbout", companyAbout);
                    cmd.Parameters.AddWithValue("@companyPhoneNumber", companyPhoneNumber);
                    cmd.Parameters.AddWithValue("@companyEmail", companyEmail);
                    cmd.Parameters.AddWithValue("@companyAddress", companyAddress);
                    cmd.Parameters.AddWithValue("@privacyPolicy", privacyPolicy);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

           
        }

      
        private void UpdateRecord(string companyAbout, string companyPhoneNumber, string companyEmail, string companyAddress, string privacyPolicy)
        {
           
            companyAbout = companyAbout.Replace(Environment.NewLine, "<br>");
            privacyPolicy = privacyPolicy.Replace(Environment.NewLine, "<br>");

            string query = "UPDATE Company_information SET " +
                           "company_about = @companyAbout, " +
                           "company_phonenumber = @companyPhoneNumber, " +
                           "company_email = @companyEmail, " +
                           "company_address = @companyAddress, " +
                           "privacy_policy = @privacyPolicy";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                 
                    cmd.Parameters.AddWithValue("@companyAbout", companyAbout);
                    cmd.Parameters.AddWithValue("@companyPhoneNumber", companyPhoneNumber);
                    cmd.Parameters.AddWithValue("@companyEmail", companyEmail);
                    cmd.Parameters.AddWithValue("@companyAddress", companyAddress);
                    cmd.Parameters.AddWithValue("@privacyPolicy", privacyPolicy);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

           
        }

       
        private void DisplayCompanyInformation()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT TOP 1 * FROM Company_information";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                       
                        txtAbout.Text = reader["company_about"].ToString().Replace("<br>", Environment.NewLine);
                        txtPhone.Text = reader["company_phonenumber"].ToString();
                        txtEmail.Text = reader["company_email"].ToString();
                        txtadres.Text = reader["company_address"].ToString();
                        txtPolicy.Text = reader["privacy_policy"].ToString().Replace("<br>", Environment.NewLine);
                    }

                    reader.Close();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
         
            string companyAbout = txtAbout.Text;
            string companyPhoneNumber = txtPhone.Text;
            string companyEmail = txtEmail.Text;
            string companyAddress = txtadres.Text;
            string privacyPolicy = txtPolicy.Text;

           
            if (!RecordExists())
            {
               
                InsertRecord(companyAbout, companyPhoneNumber, companyEmail, companyAddress, privacyPolicy);
            }
            else
            {
               
                UpdateRecord(companyAbout, companyPhoneNumber, companyEmail, companyAddress, privacyPolicy);
            }
        }
    }
}
