﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="AnindaCevap.about" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-title-area ptb-100">
			<div class="container">
				<div class="row align-items-center">
					<div class="col-lg-6 col-md-4">
						<div class="page-title-content">
							<h2>Hakkımızda</h2>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- End Page title Area -->
	
		<!-- End Privacy policy Area -->
		<section class="ptb-100">
			<div class="container">
				<div class="main-content-text">
					<asp:Label CssClass="container" ID="lblAbout" runat="server" Text="Label"></asp:Label>
				</div>
			</div>
		</section>
</asp:Content>
