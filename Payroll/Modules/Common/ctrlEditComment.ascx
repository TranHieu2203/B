<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEditComment.ascx.vb"
    Inherits="Common.ctrlEditComment" %>
<tlk:RadWindow runat="server" ID="rwMessage" Height="250px" Width="400px" Behaviors="None"
    VisibleStatusbar="false" Modal="true" EnableViewState="false" Title="<%$ Translate: Ghi chú %>">
    <ContentTemplate>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Lịch sử thao tác")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtHistory" ReadOnly="true" runat="server" Width="300px" TextMode="MultiLine"
                        Height="100px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtComment" runat="server" Width="300px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Comment"
                        runat="server" ControlToValidate="txtComment" ErrorMessage="<%$ Translate: Bạn phải nhập ghi chú %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                        <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Xác nhận %>"
                            Font-Bold="true" CausesValidation="true" ValidationGroup="Comment">
                        </tlk:RadButton>
                        <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                            Font-Bold="true" CausesValidation="false">
                        </tlk:RadButton>
                    </tlk:RadAjaxPanel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</tlk:RadWindow>
