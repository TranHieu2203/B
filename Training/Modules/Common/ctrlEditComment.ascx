<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEditComment.ascx.vb"
    Inherits="Common.ctrlEditComment" %>
<tlk:RadWindow runat="server" ID="rwMessage" Height="130px" Width="450px" Behaviors="None"
    VisibleStatusbar="false" Modal="true" EnableViewState="false" Title="<%$ Translate: Lịch sử chỉnh sửa dữ liệu %>"
    OnClientShow="OnClientShow">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <asp:Panel ID="pnEditComment" runat="server" DefaultButton="btnYES">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <table class="table-form" style="margin-top: 20px;">
                    <%--<tr>
                <td class="lb">
                    <%# Translate("Lịch sử ghi chú: ") %>
                </td>
                <td>
                    <div style="width: 350px; height: 200px; overflow: auto; border: 1px solid black">
                        <asp:Literal ID="liHistory" runat="server">
                        </asp:Literal>
                    </div>
                </td>
            </tr>--%>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú: ") %>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtComment" runat="server" Width="350px">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Comment"
                                runat="server" ControlToValidate="txtComment" ErrorMessage="<%$ Translate: Bạn phải nhập ghi chú %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                        </td>
                        <td style="text-align: right; margin-top: 2px;">
                            <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Xác nhận %>"
                                Font-Bold="true" CausesValidation="true" ValidationGroup="Comment">
                            </tlk:RadButton>
                            <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                                Font-Bold="true" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function OnClientShow(sender, eventArgs) {
            $get("<%= txtComment.ClientID %>").focus();
        }
    </script>
</tlk:RadScriptBlock>
