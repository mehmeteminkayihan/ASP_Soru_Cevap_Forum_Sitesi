<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="AnindaCevap.AdminPanel.users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="data" runat="server"> 
        
<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" DataKeyNames="user_id" Width="1500px"
    AllowPaging="True" PageSize="20" OnPageIndexChanging="gvUsers_PageIndexChanging">

    <Columns>
        <asp:BoundField DataField="user_id" HeaderText="Kullanıcı ID" SortExpression="user_id" />
        <asp:BoundField DataField="user_nickname" HeaderText="Kullanıcı Adı" SortExpression="user_nickname" />
        <asp:BoundField DataField="user_email" HeaderText="E-posta" SortExpression="user_email" />
        <asp:BoundField DataField="registration_date" HeaderText="Kayıt Tarihi" SortExpression="registration_date" />
        <asp:BoundField DataField="user_contract" HeaderText="Sözleşme" SortExpression="user_contract" />
        <asp:BoundField DataField="authorization_id" HeaderText="Yetki Türü" SortExpression="registauthorization_idration_date" />

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