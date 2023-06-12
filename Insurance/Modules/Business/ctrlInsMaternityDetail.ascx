<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsMaternityDetail.ascx.vb"
    Inherits="Insurance.ctrlInsMaternityDetail" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadToolBar ID="rtbMain" runat="server" />
<div style="display: none;">
    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
    </tlk:RadTextBox>
    <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
    </tlk:RadTextBox>
    <asp:HiddenField ID="hdORG_ID" runat="server" />
    <asp:HiddenField ID="hdTITLE_ID" runat="server" />
</div>
<div style="margin-left: 10px; margin-right: 10px;">
    <fieldset style="width: auto; height: auto">
        <legend>
            <%# Translate("Thông tin chung")%>
        </legend>
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("MSNV")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_ID" AutoPostBack="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" runat="server" ID="btnSearchEmp" SkinID="ButtonView"
                        TabIndex="1" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqtxtEMPLOYEE_ID" ControlToValidate="txtEMPLOYEE_ID"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Nhân viên. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Nhân viên. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Họ & tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFULLNAME" ReadOnly="true" runat="server" Width="300px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDEP" ReadOnly="true" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPOSITION" ReadOnly="true" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Ngày dự sinh")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ReadOnly="true"
                        ID="dateNgayDuSinh" TabIndex="5">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày dự sinh")%>
                </td>
                <td>
                    <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
                    </tlk:RadAjaxPanel>
                    <tlk:RadCodeBlock ID="RadCodeBlock2" runat="server">
                        <script type="text/javascript">
                            function OnClientSelectedIndexChanged(sender, eventArgs) {
                                $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", sender.get_id());
                            }
                        </script>
                    </tlk:RadCodeBlock>
                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="dateNgaySinh"
                        TabIndex="5" ClientEvents-OnDateSelected="OnClientSelectedIndexChanged">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Số con")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSoCon" runat="server" ShowSpinButtons="true">
                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td  <tlk:RadAjaxPanel ID="RadAjaxPanel2" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
                    </tlk:RadAjaxPanel>
                    <tlk:RadCodeBlock ID="RadCodeBlock3" runat="server">
                        <script type="text/javascript">
                            function OnClientSelectedIndexChanged(sender, eventArgs) {
                                $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", sender.get_id());
                            }
                        </script>
                    </tlk:RadCodeBlock>

                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="dateFrom"
                        TabIndex="5" ClientEvents-OnDateSelected="OnClientSelectedIndexChanged">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dateFrom"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn từ ngày. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn từ ngày. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="dateTo" TabIndex="5">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="dateTo"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn đến ngày. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn đến ngày. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hưởng chế độ thai sản")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="dataFromEnjoy"
                        TabIndex="5" ClientEvents-OnDateSelected="OnClientSelectedIndexChanged">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc hưởng chế độ thai sản")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="dataToEnjoy"
                        TabIndex="5">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tiền tạm ứng")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtTamUng" runat="server" SkinID="Money" Width="120px"
                        ShowSpinButtons="true" IncrementSettings-Step="5000">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nghỉ thai sản")%>
                </td>
                <td>
                    <tlk:RadButton ID="cbNghiThaiSan" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày đi làm sớm")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="dateNgayDiLamSom"
                        TabIndex="5">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
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
