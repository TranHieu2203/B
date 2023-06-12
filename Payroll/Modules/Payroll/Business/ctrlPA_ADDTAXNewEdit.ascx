<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_ADDTAXNewEdit.ascx.vb"
    Inherits="Payroll.ctrlPA_ADDTAXNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarFamily" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
       
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 140px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="<%$ Translate: Họ tên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTITLE" runat="server" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Đơn vị %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>




            <%--============================================--%>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin thu nhập bổ sung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" TabIndex="12" Width="100%">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboYear"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn năm. %>" ToolTip="<%$ Translate: Bạn phải chọn năm. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Loại thu nhập")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboINCOME_TYPE" SkinID="LoadDemand" runat="server" Width="100%">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboINCOME_TYPE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn loại thu nhập. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTAXABLE_INCOME" Text="Thu nhập chịu thuế"></asp:Label><span class="lbReq">*</span>
                </td>
                <td >
                    <tlk:RadNumericTextBox runat="server" ID="rnTAXABLE_INCOME" AutoPostBack="true" Width="100%">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnTAXABLE_INCOME"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập thu nhập chịu thuế. %>" ToolTip="<%$ Translate: Bạn phải nhập thu nhập chịu thuế. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lblTAX_MONEY" Text="Tiền thuế"></asp:Label><span class="lbReq">*</span>
                </td>
                <td >
                    <tlk:RadNumericTextBox runat="server" ID="rnTAX_MONEY" AutoPostBack="true" Width="100%">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rnTAX_MONEY"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tiền thuế. %>" ToolTip="<%$ Translate: Bạn phải nhập tiền thuế. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Thu nhập còn lại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnREST_MONEY" ReadOnly="true"  Width="100%">
                    </tlk:RadNumericTextBox>
                    </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtNOTE" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

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
        
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            //if (args.get_item().get_commandName() == 'CANCEL') {
            //    getRadWindow().close(null);
            //    args.set_cancel(true);
            //}
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>

