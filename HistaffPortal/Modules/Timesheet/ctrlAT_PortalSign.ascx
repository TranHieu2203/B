<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_PortalSign.ascx.vb"
    Inherits="Attendance.ctrlAT_PortalSign" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" MinValue="1900" MaxValue="9999"
                        TabIndex="1" MaxLength="4" Width="80" AutoPostBack="true">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </td>
                <td>
                    <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="cboPeriod" runat="server"
                        TabIndex="2" ToolTip="<%$ Translate: Kỳ công %>" EmptyMessage="<%$ Translate: Chọn kỳ công %>">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: FIND %>" SkinID="ButtonFind"
                        OnClick="btnFIND_Click" TabIndex="8">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <table class="table-form">
            <asp:Repeater ID="reptDetal" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="width: 400px">
                            <%# IIf(Eval("SIGN_CODE") = "X", "<b>" & Eval("SIGN_NAME") & "</b>", Eval("SIGN_NAME"))%>
                        </td>
                        <td style="width: 100px">
                            <%# IIf(Eval("SIGN_VALUE") <> 0, IIf(Eval("SIGN_CODE") = "X", "<b>" & Format(Eval("SIGN_VALUE"), Eval("SIGN_FORMAT")) & "</b>", Format(Eval("SIGN_VALUE"), Eval("SIGN_FORMAT"))), "-")%>
                        </td>
                        
                    </tr>
                    <tr>
                    <td>
                            <%# IIf(Eval("SIGN_CODE1") = "X", "<b>" & Eval("SIGN_NAME1") & "</b>", Eval("SIGN_NAME1"))%>
                        </td>
                        <td>
                            <%# IIf(Eval("SIGN_VALUE1") <> 0, IIf(Eval("SIGN_CODE1") = "X", "<b>" & Format(Eval("SIGN_VALUE1"), Eval("SIGN_FORMAT1")) & "</b>", Format(Eval("SIGN_VALUE1"), Eval("SIGN_FORMAT1"))), "-")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>