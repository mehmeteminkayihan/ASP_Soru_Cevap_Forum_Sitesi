<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="edit_profile.aspx.cs" Inherits="AnindaCevap.edit_profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="edit-profile-area">
        <div class="profile-content d-flex justify-content-between align-items-center">
            <div class="profile">
                <h3>
                    <asp:Label CssClass="profile" ID="lblName" runat="server" Text="Label"></asp:Label>
                </h3>
            </div>
        </div>

        <div class="profile-tabs">
            <ul class="nav nav-tabs" id="myTab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link active" id="edit-profile-tab" data-bs-toggle="tab" data-bs-target="#edit-profile" type="button" role="tab" aria-controls="edit-profile" aria-selected="true" onclick="showEditProfile()">Profili Düzenle</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="change-password-tab" data-bs-toggle="tab" data-bs-target="#change-password" type="button" role="tab" aria-controls="change-password" aria-selected="false" onclick="showChangePassword()">Şifre Değiştir</button>
                </li>
            </ul>

            <div class="tab-content" id="myTabContent">
                <form class="forum" runat="server">
                    <div class="tab-pane fade show active edit-profile" id="edit-profile" role="tabpanel" aria-labelledby="edit-profile-tab">
                        <div class="public-information">
                            <h3>Kamuoyunu bilgilendirme</h3>

                            <div class="form-group">
                                <label>Görünen ad</label>
                                <asp:TextBox CssClass="form-control" ID="txtName" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Konum</label>
                                <asp:TextBox CssClass="form-control" ID="txtCountry" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Hakkımda</label>
                                <asp:TextBox CssClass="form-control" rows="10" ID="txtAbout" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group mb-0">
                                        <asp:Button CssClass="default-btn" ID="btnSave" runat="server" Text="Değişiklikleri Kaydet" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="tab-pane fade edit-profile" id="change-password" role="tabpanel" aria-labelledby="change-password-tab" style="display: none;">
                        <div class="public-information pt-50">
                            <h3>Şifreyi değiştir</h3>

                            <div class="form-group">
                                <label>Mevcut Şifre</label>
                                <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Yeni Şifre</label>
                                <asp:TextBox CssClass="form-control" ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Yeni Şifre (tekrardan)</label>
                                <asp:TextBox CssClass="form-control" ID="txtNewPassword1" runat="server" TextMode="Password"></asp:TextBox>
                            </div>

                            <div class="form-group mb-0">
                                <asp:Button CssClass="default-btn" ID="btnChange" runat="server" Text="Şifreyi Değiştir" OnClick="btnChange_Click" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            showEditProfile(); // Sayfa yüklendiğinde profil düzenle kısmını görünür yap
        });

        function showEditProfile() {
            document.getElementById('edit-profile').style.display = 'block';
            document.getElementById('change-password').style.display = 'none';
        }

        function showChangePassword() {
            document.getElementById('edit-profile').style.display = 'none';
            document.getElementById('change-password').style.display = 'block';
        }
    </script>
</asp:Content>
