using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace AnindaCevap.AdminPanel
{
    public partial class Questions : System.Web.UI.Page
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
                    int questions_id = Convert.ToInt32(gvUsers.DataKeys[rowindex].Value);

           
                    SqlCommand deleteAnswersCmd = new SqlCommand("DELETE FROM Answers WHERE questions_id=@questions_id", con);
                    deleteAnswersCmd.Parameters.AddWithValue("@questions_id", questions_id);

                    int affectedAnswersRows = deleteAnswersCmd.ExecuteNonQuery();

                    
                    if (affectedAnswersRows >= 0) 
                    {
                        SqlCommand deleteQuestionCmd = new SqlCommand("DELETE FROM Questions WHERE questions_id=@questions_id", con);
                        deleteQuestionCmd.Parameters.AddWithValue("@questions_id", questions_id);

                        int affectedQuestionRows = deleteQuestionCmd.ExecuteNonQuery();

                        if (affectedQuestionRows > 0)
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

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Questions", con);
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

