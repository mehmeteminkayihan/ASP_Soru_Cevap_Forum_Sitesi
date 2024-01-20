<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="question_details.aspx.cs" Inherits="AnindaCevap.question_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="middull-content">						
                    <div class="question-details-area">
                        <div class="question-details-content">
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
                                        <asp:Label  ID="lblTitle" runat="server" Text="Label"></asp:Label>
                                    </h3>

                                    <p>
                                        <asp:Label ID="lblQuestion" runat="server" Text="Label"></asp:Label>
                                    </p>                                   
                                </div>
                            </div>
                        </div>

                        <ul class="answerss d-flex justify-content-between align-items-center">
                            <li>
                                <h3><asp:Label ID="lblTotalAnswer" runat="server" Text="Label"></asp:Label> Cevap</h3>
                            </li>
                        </ul>

                        <div runat="server" id="answerPlaceholder" class="answer-container">
                            <!-- Questions will be dynamically added here -->
                        </div>
                                
                       
                      
                            <form class="your-answer-form" runat="server">
                                <div class="col-12">
                                    <label>Cevabınız</label>
                                    <asp:TextBox CssClass="form-control" ID="txtAnswer" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="button">
                                    <div class="col-12">
                                        <div class="form-group mb-0">
                                            <asp:Button CssClass="default-btn" ID="btnAnswer" runat="server" Text="Cevabınızı gönderin" OnClick="btnAnswer_Click" />
                                        </div>
                                    </div>
                                </div>
                            </form>
                    </div>
                </div>

</asp:Content>
