using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AnindaCevap
{
    public partial class reply : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
             
                if (Request.QueryString["questionId"] != null)
                {
                
                    int questionId = Convert.ToInt32(Request.QueryString["questionId"]);

               
                    FetchQuestionDetails(questionId);
                }
                else
                {
                 
                }
            }
        }

        private void FetchQuestionDetails(int questionId)
        {
            try
            {
                

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                 
                    string query = @"
                        SELECT 
                            q.questions_id, 
                            q.user_id, 
                            q.question_title, 
                            q.questions, 
                            q.question_date, 
                            q.total_responses_received,
                            u.display_name
                        FROM 
                            Questions q
                        INNER JOIN 
                            User_details u ON q.user_id = u.user_id
                        WHERE
                            q.questions_id = @QuestionId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@QuestionId", questionId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                            
                                lblName.Text = reader["display_name"].ToString();
                                lblDate.Text = Convert.ToDateTime(reader["question_date"]).ToString("yyyy-MM-dd HH:mm:ss");
                                lblTitle.Text = reader["question_title"].ToString();
                                lblContent.Text = reader["questions"].ToString();
                                lblAnswer.Text = reader["total_responses_received"].ToString();
                            }
                            else
                            {
                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        protected void btnAnswer_Click(object sender, EventArgs e)
        {
           
            string userAnswer = txtAnswer.Text.Trim();
            int user_id = Convert.ToInt32(Session["user_id"]);
            int questionId = Convert.ToInt32(Request.QueryString["questionId"]);

            if (!string.IsNullOrEmpty(userAnswer))
            {
                try
                {
                   

                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        con.Open();

                        string checkAnswerQuery = "SELECT COUNT(*) FROM Answers WHERE user_id = @UserId AND questions_id = @QuestionId";

                        using (SqlCommand cmdCheckAnswer = new SqlCommand(checkAnswerQuery, con))
                        {
                            cmdCheckAnswer.Parameters.AddWithValue("@UserId", user_id);
                            cmdCheckAnswer.Parameters.AddWithValue("@QuestionId", questionId);

                            int existingAnswerCount = Convert.ToInt32(cmdCheckAnswer.ExecuteScalar());

                            if (existingAnswerCount > 0)
                            {
                                Response.Write("<script>alert('Bu soruya zaten cevap verdiniz.');</script>");
                                return; 
                            }
                        }

                       
                        using (SqlTransaction transaction = con.BeginTransaction())
                        {
                            try
                            {
                               
                                string insertAnswerQuery = @"
                            INSERT INTO Answers (questions_id, user_id, answer, answer_date)
                            VALUES (@QuestionId, @UserId, @Answer, @AnswerDate)";

                                using (SqlCommand cmdInsertAnswer = new SqlCommand(insertAnswerQuery, con, transaction))
                                {
                                    cmdInsertAnswer.Parameters.AddWithValue("@QuestionId", questionId);
                                    cmdInsertAnswer.Parameters.AddWithValue("@UserId", user_id);
                                    cmdInsertAnswer.Parameters.AddWithValue("@Answer", userAnswer);
                                    cmdInsertAnswer.Parameters.AddWithValue("@AnswerDate", DateTime.Now);

                              
                                    cmdInsertAnswer.ExecuteNonQuery();

                                    string updateTotalResponsesQuery = @"
                                UPDATE Questions
                                SET total_responses_received = total_responses_received + 1
                                WHERE questions_id = @QuestionId";

                                    using (SqlCommand cmdUpdateTotalResponses = new SqlCommand(updateTotalResponsesQuery, con, transaction))
                                    {
                                        cmdUpdateTotalResponses.Parameters.AddWithValue("@QuestionId", questionId);

                                   
                                        cmdUpdateTotalResponses.ExecuteNonQuery();
                                    }

                                    transaction.Commit();

                                    string updateNumberOfAnswersQuery = @"
                                UPDATE User_details
                                SET number_of_answers = number_of_answers + 1
                                WHERE user_id = @UserId";

                                    using (SqlCommand cmdUpdateNumberOfAnswers = new SqlCommand(updateNumberOfAnswersQuery, con, transaction))
                                    {
                                        cmdUpdateNumberOfAnswers.Parameters.AddWithValue("@UserId", user_id);

                                        cmdUpdateNumberOfAnswers.ExecuteNonQuery();
                                    }

                               
                                    transaction.Commit();

                                    Response.Write("<script>alert('Cevap başarıyla gönderildi.');</script>");
                                }
                            }
                            catch (Exception ex)
                            {
                          
                                transaction.Rollback();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
            else
            {
                Response.Write("<script>alert('Cevap gönderilmedi.');</script>");
            }
        }

    }
}