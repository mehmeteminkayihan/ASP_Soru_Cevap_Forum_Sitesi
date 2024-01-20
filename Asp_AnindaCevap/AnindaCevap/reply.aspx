<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="reply.aspx.cs" Inherits="AnindaCevap.reply" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="middull-content">                                                
                <div class="tab-content" id="myTabContent">
                    <div class="tab-pane fade show active" id="recent-questions" role="tabpanel" aria-labelledby="recent-questions-tab">
                        <form class="all-question" runat="server">
                            <div class="single-qa-box ">
                                <div class="d-flex">
                                    <div class="flex-grow-1 ms-3">
                                        <ul class="graphic-design">
                                            <li>
                                                <a>
                                                    <asp:Label CssClass="graphic-design" ID="lblName" runat="server" Text="Label"></asp:Label>
                                                </a>
                                            </li>
                                            <li>
                                                <span>
                                                    <asp:Label CssClass="graphic-design" ID="lblDate" runat="server" Text="Label"></asp:Label>
                                                </span>
                                            </li>
                                        </ul>

                                        <h3>
                                            <a>
                                                <asp:Label CssClass="graphic-design" ID="lblTitle" runat="server" Text="Label"></asp:Label>
                                            </a>
                                        </h3>

                                         <p>
                                            <asp:Label ID="lblContent" runat="server" Text="Label"></asp:Label>
                                        </p>

                                        <div class="d-flex justify-content-between align-items-center">
                                            <ul class="answer-list">
                                                <li>
                                                    <a>
                                                        <asp:Label CssClass="answer-list" ID="lblAnswer" runat="server" Text="Label"> </asp:Label> Cevap
                                                    </a>
                                                </li>
                                            </ul>                                           
                                        </div>
                                    </div>
                                </div>
                               
                            </div>

                               <div class="form-group">
                                    <label>Cevabınız</label>
                                   <asp:TextBox CssClass="form-control" ID="txtAnswer" runat="server"></asp:TextBox>

                               </div>
                                    <div class="button">
                                         <div class="col-12">
                                               <div class="form-group mb-0">
                                                   <asp:Button  CssClass="default-btn" ID="btnAnswer" runat="server" Text="Cevabınızı Gönderin" OnClick="btnAnswer_Click" />
                                              </div>
                                         </div>
                                    </div>
                          </form>
                        </div>
                </div>
            </div>

</asp:Content>
