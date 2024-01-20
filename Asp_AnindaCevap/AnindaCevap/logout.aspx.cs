using System;

namespace AnindaCevap
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Oturumu kapat
            Session.Abandon();

            // Kullanıcıyı başlangıç sayfasına yönlendir
            Response.Redirect("index.aspx");
        }
    }
}
