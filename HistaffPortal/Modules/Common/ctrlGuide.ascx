<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGuide.ascx.vb"
    Inherits="Common.ctrlGuide" %>
<div style="height: 1200px">
    <tlk:RadSplitter runat="server" ID="splitFull" Width="100%" Height="100%">
        <tlk:RadPane ID="LeftPane" runat="server" Width="350" Scrolling="None">
            <tlk:RadTreeView ID="treeHeading" runat="server" CausesValidation="False">
            </tlk:RadTreeView>
        </tlk:RadPane>
        <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
        </tlk:RadSplitBar>
        <tlk:RadPane runat="server" ID="paneRight" Scrolling="None">
            <tlk:RadSplitter runat="server" ID="splitRight" Orientation="Horizontal">
                <tlk:RadPane runat="server" ID="panePreview">
                    <tlk:RadBinaryImage runat="server" ResizeMode="Fill" AutoAdjustImageControlSize="false"
                        ID="imgPreview" Visible="false" />
                </tlk:RadPane>
                <tlk:RadPane runat="server" ID="RadPane1" Height="30" Scrolling="None" Style="text-align: center">
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
</div>
