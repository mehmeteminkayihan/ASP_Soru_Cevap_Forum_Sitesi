using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AnindaCevap
{
    public partial class about : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                  
                    string query = "SELECT company_about FROM Company_information";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                       
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                           
                            lblAbout.Text = reader["company_about"].ToString();
                        }

                        reader.Close();
                    }
                }
            }
        }
    }
}
