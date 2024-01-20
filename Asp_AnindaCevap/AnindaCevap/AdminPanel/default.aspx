<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AnindaCevap.AdminPanel._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body id="page-top">
    <!-- Page Wrapper -->
    <div id="wrapper">
              
        <div id="content-wrapper" class="d-flex flex-column">
            <!-- Main Content -->
 
                <div id="content">
                    <div class="container-fluid">
                        <form runat="server">
                       
                            <asp:Button CssClass="btn btn-success" ID="btnSave" runat="server" Text="Güncelle" OnClick="btnSave_Click" />

                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" >Telefon numarası :</span>
                                </div>
                               
                                <asp:TextBox CssClass="form-control" ID="txtPhone" runat="server" TextMode="Phone"></asp:TextBox>
                            </div>

                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" >E-posta adresi :</span>
                                </div>
                                
                                <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                            </div>

                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" >Şirket adresi :</span>
                                </div>
                                
                                <asp:TextBox CssClass="form-control" ID="txtadres" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>

                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Şirket hakkında :</span>
                                </div>
                                
                                 <asp:TextBox CssClass="form-control" Rows="12" ID="txtAbout" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>

                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Gizlilik Sözleşmesi :</span>
                                </div>
                                
                                <asp:TextBox CssClass="form-control" Rows="12" ID="txtPolicy" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </form>
                    </div>
                </div>
            
            <!-- End of Main Content -->
        </div>
        <!-- End of Content Wrapper -->
    </div>
    <!-- End of Page Wrapper -->
</body>
</asp:Content>
