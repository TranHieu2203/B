<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTimeWorkStandardCopy.ascx.vb"
    Inherits="Common.ctrlTimeWorkStandardCopy" %>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    OnClientBeforeClose="OnClientBeforeClose" Height="120px" Width="350px" Modal="true"
    Title="<%$ Translate: Chọn thông tin sao chép %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Năm sao chép")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboYear" runat="server" EmptyMessage="<%$ Translate: Chọn mục tiêu sao chép %>">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Copy %>"
                            Font-Bold="true" CausesValidation="false">
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
        function OnClientBeforeClose(oWnd, args) {
            var btn = $find('<%= btnNO.ClientID %>');
            btn.click();
        }
    </script>
</tlk:RadScriptBlock>
