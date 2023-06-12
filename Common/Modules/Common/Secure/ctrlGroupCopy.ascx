<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroupCopy.ascx.vb"
    Inherits="Common.ctrlGroupCopy" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    OnClientBeforeClose="OnClientBeforeClose" Height="120px" Width="350px" Modal="true"
    Title="<%$ Translate: Chọn thông tin sao chép %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <table class="table-form">
                <tr>
                    <td class="lb">
                       <%-- <%# Translate("Sao chép từ")%>--%>
                        <asp:Label ID="lblName" runat="server" Text="<%$ Translate: Sao chép từ %>"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboGroup" runat="server" EmptyMessage="<%$ Translate: Chọn mục tiêu sao chép %>" CheckBoxes="true"
                            Height="200px">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Copy %>"
                            Font-Bold="true" AutoPostBack="true">
                        </tlk:RadButton>
                        <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                            Font-Bold="true" CausesValidation="false">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('ctl00_MainContent_ctl00_MainContent_ctrlGroup_ctrlGroupFunction_ctrlGroupCopy_rwMessage_C_RadAjaxPanel1Panel');
        });

        function OnClientBeforeClose(oWnd, args) {
            var btn = $find('<%= btnNO.ClientID %>');
            btn.click();
        }
    </script>
</tlk:RadScriptBlock>
