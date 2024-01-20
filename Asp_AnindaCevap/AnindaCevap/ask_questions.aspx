<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ask_questions.aspx.cs" Inherits="AnindaCevap.ask_questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

                 <div class="middull-content">
                    <form class="your-answer-form" runat="server" >
                        <div class="form-group">
                            <h3>Bir Soru Oluşturun</h3>
                        </div>

                        <div class="form-group">
                            <label>Başlık</label>
                            <asp:TextBox CssClass="form-control" ID="txtTitle" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Açıklama</label>
                            <asp:TextBox CssClass="form-control" ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        
                        <div class="form-group">
                            <asp:Button  CssClass="default-btn" ID="btnQuestion" runat="server" Text="Sorunuzu Gönderin" OnClick="btnQuestion_Click" />
                        </div>
                    </form>
                  </div>
    
</asp:Content>
