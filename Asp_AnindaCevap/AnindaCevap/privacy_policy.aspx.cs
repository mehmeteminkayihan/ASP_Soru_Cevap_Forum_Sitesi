using System;

using System.Configuration;
using System.Data.SqlClient;

namespace AnindaCevap
{
    public partial class privacy_policy : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

              
                    string query = "SELECT privacy_policy FROM Company_information";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                       
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                          
                            lblPolicy.Text = reader["privacy_policy"].ToString();
                        }

                        reader.Close();
                    }
                }
            }

        }
    }
}