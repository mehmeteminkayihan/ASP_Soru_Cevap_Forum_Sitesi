<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="AnindaCevap.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="sign-up-area ptb-100">
			<div class="container">
				<form class="user-form" runat="server">
					<div class="row">
						
						<div class="col-12">
							<div class="form-group">
								<label>Kullanıcı adı</label>
								<asp:TextBox CssClass="form-control" ID="txtUserName" runat="server"></asp:TextBox>
							</div>
						</div>

						<div class="col-12">
							<div class="form-group">
								<label>E-posta</label>
								<asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
							</div>
						</div>
							<div class="form-group">
								<label>Şifre</label>
								<asp:TextBox CssClass="form-control" ID="txtUserPassword" runat="server" TextMode="Password"></asp:TextBox>
							</div>
						</div>

						<div class="col-12">
							<div class="login-action">
								<div class="form-check">
									<asp:CheckBox CssClass="form-check" ID="checkContact" runat="server" />
									<label class="form-check-label" for="flexCheckDefault">
										Okudum Kabul Ediyorum <a href="privacy_policy.aspx">Gizlilik Politikası</a>
									</label>
								</div>
							</div>
						</div>

						<div class="col-12">
							<asp:Button CssClass="default-btn" ID="btnRegister" runat="server" Text="Kayıt Ol" OnClick="btnRegister_Click" />
						</div>

						<div class="col-12">
							<p class="create">Zaten Üye misin ? <a href="login.aspx">Giriş yap</a></p>  
						</div>
					</div>
				</form>
			</div>
		</div>
	
</asp:Content>
