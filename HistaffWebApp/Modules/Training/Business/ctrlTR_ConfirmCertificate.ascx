<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ConfirmCertificate.ascx.vb"
    Inherits="Training.ctrlTR_ConfirmCertificate" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="LeftPane" runat="server" Height="20px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td colspan="4">
                    <b>
                        <%# Translate("Thông tin bằng cấp chứng chỉ")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên bằng cấp/Chứng chỉ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCERTIFICATE_NAME" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtYEAR" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tham gia từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSTART_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEND_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCONTENT" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdIssuedDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <tlk:RadButton ID="btnConfirm" runat="server" Text="<%$ Translate: Xác nhận %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
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

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function CloseModal() {
            setTimeout(function () {
                getRadWindow().close();
            }, 0);
        }

    </script>
</tlk:RadCodeBlock>
