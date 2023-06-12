<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGuide.ascx.vb"
    Inherits="Common.ctrlGuide" %>
<tlk:RadSplitter runat="server" ID="splitFull" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" Width="250">
        <tlk:RadTreeView ID="treeHeading" runat="server" CausesValidation="False">
        </tlk:RadTreeView>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="paneRight">
        <tlk:RadSplitter runat="server" ID="splitRight" Orientation="Horizontal">
            <tlk:RadPane runat="server" ID="RadPane2" Height="30px" Scrolling="None">
                <div style="margin: 5px">
                    <%# Translate("Tài liệu hướng dẫn sử dụng")%>
                    <tlk:RadComboBox ID="cboGuideType" runat="server" Width="220px">
                        <Items>
                            <tlk:RadComboBoxItem Value="iProfile" Text="<%$ Translate: Hồ sơ %>" />
                            <tlk:RadComboBoxItem Value="iTime" Text="<%$ Translate: Chấm công %>" Selected="true" />
                            <tlk:RadComboBoxItem Value="iPayroll" Text="<%$ Translate: Tính lương %>" />
                        </Items>
                    </tlk:RadComboBox>
                    <tlk:RadButton runat="server" ID="btnView" Text='<%$ Translate: Xem nội dung %>'>
                    </tlk:RadButton>
                </div>
            </tlk:RadPane>  
            <tlk:RadPane runat="server" ID="panePreview">
                <tlk:RadBinaryImage runat="server" ResizeMode="Fill" AutoAdjustImageControlSize="false"
                    ID="imgPreview" Visible="false" />
            </tlk:RadPane>
            <tlk:RadPane runat="server" ID="RadPane1" Height="30">
                <ul class="ctrlBtns">
                    <li>
                        <tlk:RadButton ID="btnBack" runat="server" Width="19px" Height="21px" CssClass="ff"
                            AutoPostBack="true" Text="Next" CommandName="Right">
                            <Image ImageUrl="/Static/Images/back.png" HoveredImageUrl="/Static/Images/backHov.png">
                            </Image>
                        </tlk:RadButton>
                    </li>
                    <li>
                        <tlk:RadNumericTextBox ID="rntxtPageIndex" AutoPostBack="true" runat="server" Width="40">
                            <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                        </tlk:RadNumericTextBox>
                        <asp:Label ID="lbPage" runat="server">/ 0</asp:Label>
                    </li>
                    <li>
                        <tlk:RadButton ID="btnNext" runat="server" Width="19px" Height="21px" CssClass="raw"
                            AutoPostBack="true" Text="Previous" CommandName="Left">
                            <Image ImageUrl="/Static/Images/next.png" HoveredImageUrl="/Static/Images/nextHov.png">
                            </Image>
                        </tlk:RadButton>
                    </li>
                </ul>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>