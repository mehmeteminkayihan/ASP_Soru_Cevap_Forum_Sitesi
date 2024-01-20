<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="all_questions.aspx.cs" Inherits="AnindaCevap.all_questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
    <div runat="server" id="questionContainer" class="question-container">
        <!-- Questions will be dynamically added here -->
    </div>

</asp:Content>
