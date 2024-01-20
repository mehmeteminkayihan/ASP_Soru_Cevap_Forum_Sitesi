using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace AnindaCevap
{
    public partial class all_questions : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageNumber = Request.QueryString["page"] != null ? Convert.ToInt32(Request.QueryString["page"]) : 1;

                int pageSize = 10;


                LoadQuestions(pageNumber, pageSize);
            }
        }

        private void LoadQuestions(int pageNumber, int pageSize)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    int startIndex = (pageNumber - 1) * pageSize;

                    string query = $@"
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
                        ORDER BY 
                            q.question_date OFFSET {startIndex} ROWS
                        FETCH NEXT {pageSize} ROWS ONLY";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            List<Question> questionList = new List<Question>();

                            foreach (DataRow row in dt.Rows)
                            {
                                Question question = new Question
                                {
                                    QuestionId = Convert.ToInt32(row["questions_id"]),
                                    UserId = Convert.ToInt32(row["user_id"]),
                                    UserName = row["display_name"].ToString(),
                                    Title = row["question_title"].ToString(),
                                    Content = row["questions"].ToString(),
                                    Date = Convert.ToDateTime(row["question_date"]),
                                    ResponsesReceived = Convert.ToInt32(row["total_responses_received"])
                                };

                                questionList.Add(question);
                            }

                            foreach (var question in questionList)
                            {
                              
                                TimeSpan timeDifference = DateTime.Now - question.Date;

                               
                                string formattedTimeDifference = FormatTimeDifference(timeDifference);

                                string cardHtml = $@"
                                    <div class='tab-content' id='myTabContent'>
                                        <div class='tab-pane fade show active' id='recent-questions' role='tabpanel' aria-labelledby='recent-questions-tab'>
                                            <div class='single-qa-box like-dislike'>
                                                <div class='d-flex'>
                                                    <div class='flex-grow-1 ms-3'>
                                                        <ul class='graphic-design'>
                                                            <li>
                                                                <a>
                                                                    {question.UserName}
                                                                </a>
                                                            </li>
                                                            <li>
                                                                <span>
                                                                    {formattedTimeDifference} önce
                                                                </span>
                                                            </li>
                                                        </ul>

                                                        <h3>
                                                            <a href='question_details.aspx?questionId={question.QuestionId}'>
                                                                    {question.Title}
                                                            </a>
                                                        </h3>

                                                        <p>
                                                            {question.Content}
                                                        </p>

                                                        <div class='d-flex justify-content-between align-items-center'>
                                                            <ul class='anser-list'>
                                                                <li>
                                                                    <a>
                                                                        {question.ResponsesReceived} Cevap
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                          <div class='default-btn'  >
                                                            <a style='color:white;' href='reply.aspx?questionId={question.QuestionId}'>
                                                                Cevapla
                                                             </a>
                                                         </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>";

                                questionContainer.InnerHtml += cardHtml;
                            }
                        }
                        else
                        {
                            
                        }

                        string pageUrl = "all_questions.aspx?page=";

                       
                        int totalQuestions = GetTotalQuestionsCount();
                        int totalPages = (int)Math.Ceiling((double)totalQuestions / pageSize);

                        for (int i = 1; i <= totalPages; i++)
                        {
                            pageUrl += i;
                            questionContainer.InnerHtml += $"<a  href='{pageUrl}'>{i}</a>&nbsp;";
                            pageUrl = "all_questions.aspx?page=";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private int GetTotalQuestionsCount()
        {
            int totalQuestions = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM Questions";

                    using (SqlCommand cmd = new SqlCommand(countQuery, con))
                    {
                        totalQuestions = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return totalQuestions;
        }

        private string FormatTimeDifference(TimeSpan timeDifference)
        {
            if (timeDifference.TotalSeconds < 60)
            {
                return $"{(int)timeDifference.TotalSeconds} saniye önce";
            }
            else if (timeDifference.TotalMinutes < 60)
            {
                return $"{(int)timeDifference.TotalMinutes} dakika önce";
            }
            else if (timeDifference.TotalHours < 24)
            {
                return $"{(int)timeDifference.TotalHours} saat önce";
            }
            else if (timeDifference.TotalDays < 30)
            {
                return $"{(int)timeDifference.TotalDays} gün önce";
            }
            else if (timeDifference.TotalDays < 365)
            {
                int months = (int)(timeDifference.TotalDays / 30);
                return $"{months} {(months == 1 ? "ay" : "ay")}";
            }
            else
            {
                int years = (int)(timeDifference.TotalDays / 365);
                return $"{years} {(years == 1 ? "yıl" : "yıl")}";
            }
        }

        public class Question
        {
            public int QuestionId { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime Date { get; set; }
            public int ResponsesReceived { get; set; }
        }
    }
}
