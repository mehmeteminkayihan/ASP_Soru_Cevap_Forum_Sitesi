using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace AnindaCevap
{
    public partial class ask_questions : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnQuestion_Click(object sender, EventArgs e)
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
                    string questionTitle = txtTitle.Text.Trim();
                    string questionDescription = txtDescription.Text.Trim();

                    if (string.IsNullOrEmpty(questionTitle) || string.IsNullOrEmpty(questionDescription))
                    {
                        Response.Write("<script>alert('Lütfen tüm alanları doldurunuz.');</script>");
                        return;
                    }

                  
                    SqlCommand cmd = new SqlCommand("INSERT INTO Questions (user_id, question_title, questions, question_date) " +
                        "VALUES(@id, @title, @description, @date)", con);

                    cmd.Parameters.AddWithValue("@id", user_id);
                    cmd.Parameters.AddWithValue("@title", questionTitle);
                    cmd.Parameters.AddWithValue("@description", questionDescription);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd.ExecuteNonQuery();

                   
                    SqlCommand updateCmd = new SqlCommand("UPDATE User_details SET number_of_questions = number_of_questions + 1 WHERE user_id = @id", con);
                    updateCmd.Parameters.AddWithValue("@id", user_id);
                    updateCmd.ExecuteNonQuery();

                    con.Close();

                   
                    txtTitle.Text = "";
                    txtDescription.Text = "";

                    Response.Write("<script>alert('Sorunuzu sordunuz');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}
