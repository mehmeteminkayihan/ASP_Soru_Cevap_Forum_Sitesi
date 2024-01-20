using System;


namespace AnindaCevap.AdminPanel
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user_id"] != null && Session["user_nickname"] != null)
                {
                  
                    string userId = Session["user_id"].ToString();
                    string userNickname = Session["user_nickname"].ToString();


                    lblName.Text = userNickname;
                }
                else
                {
                    Response.Redirect("admin_login.aspx");
                }
            }
        }
    }
}