<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_login.aspx.cs" Inherits="AnindaCevap.AdminPanel.admin_login" %>

<!DOCTYPE html>
<html lang="tr">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>AC Admin-Panel-Login</title>

    <!-- Bootstrap CSS -->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <link href="assets/css/sb-admin-2.min.css" rel="stylesheet">
</head>

<body class="bg-gradient-primary">

    <div class="container">

        <div class="row justify-content-center align-items-center" style="height: 100vh;">

            <div class="col-xl-6">
               <form class="form" runat="server">
                <div class="card o-hidden border-0 shadow-lg my-5">
                    <div class="card-body p-5">
                        <div class="text-center">
                            <h1 class="h4 text-gray-900 mb-4">Anında Cevap Admin Panel</h1>
                        </div>                        
                            <div class="form-group">
				               <label>Kullanıcı adı</label>
				               <asp:TextBox CssClass="form-control" ID="txtName" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group">
				              <label>Şifre</label>
				              <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                    
                            <div class="col-12">
				                <asp:Button CssClass="btn btn-primary btn-user btn-block" ID="btnLogin" runat="server" Text="Giriş Yap" OnClick="btnLogin_Click" />
                            </div>

                    </div>
                </div>
               </form>
            </div>

        </div>

    </div>

    <!-- Bootstrap JS -->
    <script src="assets/vendor/jquery/jquery.min.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="assets/js/sb-admin-2.min.js"></script>
</body>

</html>
