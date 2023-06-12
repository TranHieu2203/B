<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_DocumentPITNewEditNewEdit.ascx.vb"
    Inherits="Payroll.ctrlPA_DocumentPITNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .pit-box{
        position:relative;
        width:100%;
        height:100%;
    }
    .pit-content{
        position:absolute;
        top:50%;
        left:50%;
        transform:translate(-50%,-50%);
        width:fit-content;
    }
    .table-form{
        width:fit-content;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
                <div class="pit-box">
                    <div class="pit-content">
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Translate: Năm %>"></asp:Label><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" Width="80px">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb">
                                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" AutoPostBack="true">
                                        <ClientEvents OnKeyPress="OnKeyPress" />
                                    </tlk:RadTextBox>
                                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                                        Width="40px">
                                    </tlk:RadButton>
                                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Khoản thu nhập %>"></asp:Label><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtTypeIncome" runat="server" Text="Thù lao">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTypeIncome"
                                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Khoản thu nhập. %>" ToolTip="<%$ Translate: Bạn phải Khoản thu nhập. %>"> </asp:RequiredFieldValidator>
                                </td>
                                <td class="lb">
                                    <asp:Label ID="lbEmployeeName" runat="server" Text="<%$ Translate: Họ tên nhân viên %>"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Thời điểm trả thu nhập %>"></asp:Label><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtMonthReply" runat="server">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMonthReply"
                                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời điểm trả thu nhập. %>" ToolTip="<%$ Translate: Bạn phải nhập Thời điểm trả thu nhập. %>"> </asp:RequiredFieldValidator>
                                </td>
                                <td class="lb">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Translate: Phòng ban %>"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="lb">
                                    <tlk:RadButton runat="server" ID="btnConfirm" Text="Xác nhận" Width="70px" OnClientClicked="CreatePIT"></tlk:RadButton>
                                </td>
                                <td>
                                    <tlk:RadButton runat="server" ID="btnCancel" Text="Bỏ qua" Width="70px" OnClientClicking="CancelPIT" CausesValidation="false"></tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        function CancelPIT(sender, args) {
            args.set_cancel(true);
            getRadWindow().close(null);
        }
        function CreatePIT(sender, eventArgs) {
            var emp = $('#<%= hidEmpID.ClientID%>').val();
            var year = $find('<%= cboYear.ClientID%>').get_value();
            var type = $find('<%= txtTypeIncome.ClientID%>').get_value();
            var month = $find('<%= txtMonthReply.ClientID%>').get_value();
            var str = emp + ';' + year + ';' + type + ';' + month
            window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_DocumentPITDetail&group=Business&strDT=' + btoa(unescape(encodeURIComponent(str))), "_blank");
        }
        window.addEventListener('keydown', function (e) {
            if (e.key == 'Enter' || e.code == 13) {
                if (e.target.id !== 'ctrlPA_DocumentPITNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
