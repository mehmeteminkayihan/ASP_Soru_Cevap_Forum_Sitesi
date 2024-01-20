using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnindaCevap
{
    public partial class question_details : System.Web.UI.Page
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

                    string questionQuery = @"
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

                    using (SqlCommand questionCmd = new SqlCommand(questionQuery, con))
                    {
                        questionCmd.Parameters.AddWithValue("@QuestionId", questionId);

                        using (SqlDataReader questionReader = questionCmd.ExecuteReader())
                        {
                            if (questionReader.Read())
                            {
                               
                                lblName.Text = questionReader["display_name"].ToString();
                                lblDate.Text = FormatTimeElapsed(Convert.ToDateTime(questionReader["question_date"]));
                                lblTitle.Text = questionReader["question_title"].ToString();
                                lblQuestion.Text = questionReader["questions"].ToString();
                                lblTotalAnswer.Text = questionReader["total_responses_received"].ToString();
                            }
                            else
                            {
                               
                            }
                        }
                    }

                    string answerQuery = @"
                SELECT 
                    a.answer_id,
                    a.user_id,
                    a.answer_date,
                    a.answer,
                    u.display_name
                FROM 
                    Answers a
                INNER JOIN 
                    User_details u ON a.user_id = u.user_id
                WHERE 
                    a.questions_id = @QuestionId";

                    using (SqlCommand answerCmd = new SqlCommand(answerQuery, con))
                    {
                        answerCmd.Parameters.AddWithValue("@QuestionId", questionId);

                        using (SqlDataReader answerReader = answerCmd.ExecuteReader())
                        {
                            List<string> answersHtmlList = new List<string>();

                            while (answerReader.Read())
                            {
                              
                                string answerHtml = $@"
                            <div class='answer-question-details'>
                                <div class='d-flex'>
                                    <div class='flex-grow-1 ms-3'>
                                        <ul class='latest-answer-list'>
                                            <li>
                                                {answerReader["display_name"]}
                                            </li>
                                            <li>
                                                {FormatTimeElapsed(Convert.ToDateTime(answerReader["answer_date"]))}
                                            </li>
                                        </ul>
                                        <p>
                                            {answerReader["answer"]}
                                        </p>
                                    </div>
                                </div>
                            </div>";

                                answersHtmlList.Add(answerHtml);
                            }

                            
                            answerPlaceholder.InnerHtml = string.Join("", answersHtmlList);
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




        private string FormatTimeElapsed(DateTime postDate)
        {
            TimeSpan timeElapsed = DateTime.Now - postDate;

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