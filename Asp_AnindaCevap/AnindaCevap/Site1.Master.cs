using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AnindaCevap
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
         
                if (Session["user_id"] != null && Session["user_nickname"] != null)
                {
              
                    string userId = Session["user_id"].ToString();
                    string userNickname = Session["user_nickname"].ToString();

                    loginLink.Visible = false;
                    registerLink.Visible = false;

                    logoutLink.Visible = true;
                }
                else
                {
                    logoutLink.Visible = false;     

                    
                    loginLink.Visible = true;  
                    registerLink.Visible = true;

                    HideElementsForUnauthenticatedUser();
                }

             
                DisplayCompanyInformation();
            }
        }

        private void HideElementsForUnauthenticatedUser()
        {
            askQuestionsLink.Visible = false;     
            userProfileNavItem.Visible = false; 
        }




        private void DisplayCompanyInformation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT company_phonenumber, company_email, company_address FROM Company_information", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                      
                        string phoneNumber = reader["company_phonenumber"].ToString();
                        string email = reader["company_email"].ToString();
                        string address = reader["company_address"].ToString();

                       
                        lblPhone.Text = phoneNumber;
                        lblEmail.Text = email;
                        lblAdress.Text = address;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
            
            }
        }
    }
}
