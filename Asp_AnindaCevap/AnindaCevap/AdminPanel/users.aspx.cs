using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnindaCevap.AdminPanel
{
    public partial class users : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
                    int user_id = Convert.ToInt32(gvUsers.DataKeys[rowindex].Value);

                   
                    SqlCommand deleteAnswersCmd = new SqlCommand("DELETE FROM Answers WHERE user_id=@user_id", con);
                    deleteAnswersCmd.Parameters.AddWithValue("@user_id", user_id);

                    int affectedAnswersRows = deleteAnswersCmd.ExecuteNonQuery();

                    
                    if (affectedAnswersRows >= 0)
                    {
                       
                        SqlCommand deleteQuestionsCmd = new SqlCommand("DELETE FROM Questions WHERE user_id=@user_id", con);
                        deleteQuestionsCmd.Parameters.AddWithValue("@user_id", user_id);

                        int affectedQuestionsRows = deleteQuestionsCmd.ExecuteNonQuery();

                        
                        if (affectedQuestionsRows >= 0)
                        {
                           
                            SqlCommand deleteUserDetailsCmd = new SqlCommand("DELETE FROM User_details WHERE user_id=@user_id", con);
                            deleteUserDetailsCmd.Parameters.AddWithValue("@user_id", user_id);

                            int affectedUserDetailsRows = deleteUserDetailsCmd.ExecuteNonQuery();

                            
                            if (affectedUserDetailsRows >= 0)
                            {
                             
                                SqlCommand deleteUserCmd = new SqlCommand("DELETE FROM Users WHERE user_id=@user_id", con);
                                deleteUserCmd.Parameters.AddWithValue("@user_id", user_id);

                                int affectedUserRows = deleteUserCmd.ExecuteNonQuery();

                                if (affectedUserRows > 0)
                                {
                                   
                                    LoadData();
                                }
                                else
                                {
                                  
                                }
                            }
                            else
                            {
                               
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                       
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
              
            }
        }

        void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvUsers.DataSource = dt;
                    gvUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            LoadData();
        }
    }
}
