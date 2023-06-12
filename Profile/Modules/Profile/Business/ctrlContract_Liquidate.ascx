<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlContract_Liquidate.ascx.vb"
    Inherits="Profile.ctrlContract_Liquidate" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadToolBar ID="rtbMain" runat="server" />
<div style="margin-left: 30px; margin-right: 30px;">
    <fieldset style="width: auto; height: auto">
        <legend>
            <%# Translate("Thông tin cập nhật")%>
        </legend>
        <table class="table-form">
            <tr>
                <td class="lb">
                   <%# Translate("Ngày thanh lý HĐ")%>
                        <span class="lbReq">*</span>
                </td>
                   <td>
                        <tlk:RadDatePicker runat="Server" ID="dtpLiquiDate" CausesValidation="true">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="dtpLiquiDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày thanh lý. %>" ToolTip="<%$ Translate:  Bạn phải nhập ngày thanh lý. %>"> 
                        </asp:RequiredFieldValidator>
                    </td>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox SkinID="Textbox1023" ID="txtRemark" Width="320px" runat="server" CausesValidation="false">
                     </tlk:RadTextBox>
                      <asp:RequiredFieldValidator ID="regRemark" ControlToValidate="dtpLiquiDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày thanh lý. %>" ToolTip="<%$ Translate:  Bạn phải nhập lý do thanh lý.%>"> 
                        </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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

        function resize(width, heigth) {
            var oWindow = getRadWindow();
            oWindow.set_width(width);
            oWindow.set_height(heigth);
            oWindow.center();
        }

    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />