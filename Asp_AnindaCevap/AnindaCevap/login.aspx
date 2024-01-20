<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AnindaCevap.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="login-area ptb-100">
			<div class="container">
				<form class="user-form" runat="server">
					<div class="row">
									
						<div class="col-12">
							<div class="form-group">
								<label>Kullanıcı adı</label>
								<asp:TextBox CssClass="form-control" ID="txtName" runat="server"></asp:TextBox>
							</div>
						</div>
		
						<div class="col-12">
							<div class="form-group">
								<label>Şifre</label>
								<asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
							</div>
						</div>
		
						
		
						<div class="col-12">
							<asp:Button CssClass="default-btn" ID="btnLogin" runat="server" Text="Giriş Yap" OnClick="btnLogin_Click" />
						</div>
		
						<div class="col-12">
							<p class="create">Hesabınız yok mu ? <a href="register.aspx">Kayıt ol</a></p>  
						</div>
					</div>
				</form>
			</div>
		</div>

</asp:Content>
