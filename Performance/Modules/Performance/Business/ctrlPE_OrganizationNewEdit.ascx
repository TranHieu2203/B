<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_OrganizationNewEdit.ascx.vb"
    Inherits="Performance.ctrlPE_OrganizationNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin đánh giá")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 100px">
                    <%# Translate("Kỳ đánh giá")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusPeriod" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn kỳ đánh giá %>"
                        ToolTip="<%$ Translate: Bạn phải chọn kỳ đánh giá %>" ClientValidationFunction="cusPeriod">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kết quả ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRESULT" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRESULT"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập kết quả đánh giá %>"
                        ToolTip="<%$ Translate: Bạn phải nhập kết quả đánh giá %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị đánh giá")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtORG" runat="server" Width="130px" ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtORG"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn bộ phận đánh giá %>"
                        ToolTip="<%$ Translate: Bạn phải chọn bộ phận đánh giá %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtREMARK" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function cusPeriod(oSrc, args) {
            var cbo = $find("<%# cboPeriod.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }

        }
    </script>
</tlk:RadCodeBlock>
