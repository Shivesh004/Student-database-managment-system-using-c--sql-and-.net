using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DiscussionForum._Default;

namespace DiscussionForum
{
    public partial class _Default : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source = ANSH-UNIVERSE\SQLEXPRESS08; Initial Catalog=DiscussionForum; Integrated Security = true");
        public List<QuestionWithReplies> questionsWithReplies;
        public List<QuestionWithReplies> questionsWithRepliesOfUser;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            DataBind();
                questionsWithReplies = GetQuestionsWithRepliesFromDatabase();
                questionsWithRepliesOfUser = GetQuestionWithRepliesOfUser();

                
            }
        }

        protected string GetDynamicId(string prefix, int questionId)
        {
            return $"{prefix}{questionId}";
        }



        protected void btnReplyQ_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                Button btn = (Button)sender;

                int questionId = int.Parse(btn.CommandArgument);
                int createdBy = Convert.ToInt32(Session["UserID"]);
                string replyContent = replyContentQ.Text;
         
                SqlCommand cmd = new SqlCommand("insert into replies (replyContent, question_id, created_by) values ('@ReplyContent', @QuestionID, @CreatedBy);", con);
                cmd.Parameters.AddWithValue("@ReplyContent", replyContent);
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();          
            }
        }

        protected void btnReplyUser_Click(object sender, EventArgs e)
        {
            int questionId = Convert.ToInt32(((Button)sender).CommandArgument);
        }


        // Dangerous code below

        public class Reply
        {
            public int ReplyId { get; set; }
            public int CreatedBy { get; set; }
            public string ReplyContent { get; set; }
        }
        public class QuestionWithReplies
        {
            public int QuestionId { get; set; }
            public string QuestionContent { get; set; }
            public List<Reply> Replies { get; set; }
        }

        private List<QuestionWithReplies> GetQuestionWithRepliesOfUser()
        {
            if (Session["UserID"] != null)
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                List<QuestionWithReplies> questionsWithReplies = new List<QuestionWithReplies>();
                string query = $@"
    SELECT q.question_id, q.questionContent, r.reply_id, r.created_by, r.replyContent
    FROM questions q
    LEFT JOIN replies r ON q.question_id = r.question_id
    WHERE q.created_by = '{userId}'
    ORDER BY q.question_id DESC";
                SqlCommand command = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int questionId = Convert.ToInt32(reader["question_id"]);
                    string questionContent = reader["questionContent"].ToString();

                    QuestionWithReplies question = questionsWithReplies.FirstOrDefault(q => q.QuestionId == questionId);
                    if (question == null)
                    {

                        question = new QuestionWithReplies
                        {
                            QuestionId = questionId,
                            QuestionContent = questionContent,
                            Replies = new List<Reply>()
                        };
                        questionsWithReplies.Add(question);
                    }


                    if (reader["reply_id"] != DBNull.Value)
                    {

                        Reply reply = new Reply
                        {
                            ReplyId = Convert.ToInt32(reader["reply_id"]),
                            CreatedBy = Convert.ToInt32(reader["created_by"]),
                            ReplyContent = reader["replyContent"].ToString()
                        };
                        question.Replies.Add(reply);
                    }
                }
                con.Close();
                return questionsWithReplies;
            }
            List<QuestionWithReplies> empty = new List<QuestionWithReplies>();
            return empty;
        }

        private List<QuestionWithReplies> GetQuestionsWithRepliesFromDatabase()
        {
            List<QuestionWithReplies> questionsWithReplies = new List<QuestionWithReplies>();
            string query = @"
        SELECT q.question_id, q.questionContent, r.reply_id, r.created_by, r.replyContent
        FROM questions q
        LEFT JOIN replies r ON q.question_id = r.question_id
        ORDER BY q.question_id DESC";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int questionId = Convert.ToInt32(reader["question_id"]);
                string questionContent = reader["questionContent"].ToString();

                QuestionWithReplies question = questionsWithReplies.FirstOrDefault(q => q.QuestionId == questionId);
                if (question == null)
                {
                   
                    question = new QuestionWithReplies
                    {
                        QuestionId = questionId,
                        QuestionContent = questionContent,
                        Replies = new List<Reply>()
                    };
                    questionsWithReplies.Add(question);
                }

               
                if (reader["reply_id"] != DBNull.Value)
                {
                    
                    Reply reply = new Reply
                    {
                        ReplyId = Convert.ToInt32(reader["reply_id"]),
                        CreatedBy = Convert.ToInt32(reader["created_by"]),
                        ReplyContent = reader["replyContent"].ToString()
                    };
                    question.Replies.Add(reply);
                }
            }
            con.Close();
            return questionsWithReplies;
        }

        private List<int> GetIDsOfTextBoxes()
        {
            List<int> ids = new List<int>();
            foreach (var question in GetQuestionsWithRepliesFromDatabase())
            {
                ids.Add(question.QuestionId);
            }
            return ids;       
        }
        // Dangerous code above

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            } else
            {


            int userId = Convert.ToInt32(Session["UserID"]);
                SqlCommand cmd = new SqlCommand($"insert into questions (questionContent, created_by) values('{txtQuestion.Text}', {userId});", con);

            con.Open();
                cmd.ExecuteNonQuery();
                txtQuestion.Text = string.Empty;
            con.Close();

            }
        }
    }
}