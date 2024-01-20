<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="user_profile.aspx.cs" Inherits="AnindaCevap.user_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="main-content-area ptb-100">
			<div class="container">
				<div class="row">
					
						<div class="user-profile-area">
							<div class="profile-content d-flex justify-content-between align-items-center">
							    <div class="profile">
                                    <h2> 
										<asp:Label CssClass="profile" ID="lblName" runat="server" Text="Label"></asp:Label> 

                                    </h2>
                                    <p>
										Üyelik Süresi:<asp:Label CssClass="profile" ID="lblDate" runat="server" Text="Label"></asp:Label>

                                    </p>
                                </div>
								<div class="edit-btn">
									<a href="edit_profile.aspx" class="default-btn">Profilini Düzenle</a>
								</div>
							</div>

							<div class="profile-achive">
								<div class="row justify-content-center">
									<div class="col-xl-4 col-sm-6">
										<div class="single-achive">
											<h2>
												<asp:Label CssClass="single-achive" ID="lblAnswer" runat="server" Text="Label"></asp:Label>

											</h2>
											<span>Cevapların</span>
										</div>
									</div>

									<div class="col-xl-4 col-sm-6">
										<div class="single-achive">
											<h2>
												<asp:Label CssClass="single-achive" ID="lblQuestion" runat="server" Text="Label"></asp:Label>
											</h2>
											<span>Soruların</span>
										</div>
									</div>
									
									
								</div>
							</div>

							<div class="country">
								<h4>Yaşağım Yer</h4>
								<p>
									<asp:Label CssClass="country" ID="lblCountry" runat="server" Text="Label"></asp:Label>
								</p>
							</div>

							<div class="about">
								<h3>Hakkımda</h3>
			                    <p>
									<asp:Label CssClass="about" ID="lblAbout" runat="server" Text="Label"></asp:Label>
			                    </p>
								
							</div>
						</div>
				</div>
			</div>
		</div>
  
</asp:Content>
