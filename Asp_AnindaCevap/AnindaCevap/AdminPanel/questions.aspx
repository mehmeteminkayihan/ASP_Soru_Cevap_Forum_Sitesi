<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="questions.aspx.cs" Inherits="AnindaCevap.AdminPanel.Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.7/css/jquery.dataTables.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <form class="data" runat="server"> 
        
<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" DataKeyNames="questions_id" Width="1500px"
    AllowPaging="True" PageSize="15" OnPageIndexChanging="gvUsers_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="questions_id" HeaderText="Soru ID" SortExpression="questions_id" />
        <asp:BoundField DataField="user_id" HeaderText="Kullanıcı ID" SortExpression="user_id" />
        <asp:BoundField DataField="question_title" HeaderText="Soru Başlıgı" SortExpression="question_title" />
        <asp:BoundField DataField="questions" HeaderText="Soru" SortExpression="questions" />
        <asp:BoundField DataField="question_date" HeaderText="Yayın Tarihi" SortExpression="question_date" />
        <asp:BoundField DataField="total_responses_received" HeaderText="Toplam Cevaplanma" SortExpression="total_responses_received" />

        <asp:TemplateField>
    <HeaderTemplate>
        <div class="d-flex align-items-center justify-content-center">
            İşlemler
        </div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="d-flex align-items-center justify-content-center">
            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click">Sil</asp:LinkButton>
        </div>
    </ItemTemplate>
</asp:TemplateField>


    </Columns>
    <PagerStyle CssClass="pagination" />
</asp:GridView>

    </form>  

</asp:Content>
