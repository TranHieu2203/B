<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindSalaryGroupPopup.ascx.vb"
    Inherits="Common.ctrlFindSalaryGroupPopup" %>
    <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<%@ Register Src="ctrlSalaryGroup.ascx" TagName="ctrlSalaryGroup" TagPrefix="Common" %>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="None" VisibleStatusbar="false"
    Height="420" Width="400" Modal="true" Title="<%$ Translate: Tìm kiếm đơn vị %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <p>
                <Common:ctrlSalaryGroup ID="ctrlSal1" runat="server" Height="300px" />
            </p>
            <div style="margin: 0px 10px 10px 10px; text-align: center;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
