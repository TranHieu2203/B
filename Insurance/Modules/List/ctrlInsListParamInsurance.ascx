<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsListParamInsurance.ascx.vb"
    Inherits="Insurance.ctrlInsListParamInsurance" %>
<%@ Import Namespace="Common" %>
<link type="text/css" href="/Styles/StyleCustom.css" rel="Stylesheet" />
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter4" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPaneField" runat="server" Width="100%" Height="330px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOtherLists" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <table class="table-form">
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkForeigner" runat="server" />
                    <asp:Label runat="server" ID="lbForeigner" Text="Người nước ngoài"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectiveDate" Text="Ngày hiệu lực"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectiveDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="rdEffectiveDate"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSI_DATE" Text="Ngày chốt sổ BHXH"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtSI_DATE" MinValue="1" MaxValue="31" SkinID="Textbox15"
                        runat="server">
                        <NumberFormat DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqtxtSI_DATE" ControlToValidate="txtSI_DATE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày chốt sổ BHXH. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbHI_DATE" Text="Ngày chốt sổ BHYT"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtHI_DATE" MinValue="1" MaxValue="31" SkinID="Textbox15"
                        runat="server">
                        <NumberFormat DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtHI_DATE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày chốt sổ BHYT. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="2">
                    <%# Translate("Mức trần")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2" style="width: 150px">
                    <%# Translate("Tỉ lệ công ty")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2">
                    <%# Translate("Tỉ lệ nhân viên")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSI" Text="BHXH"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    (VND)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="radnmSI"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ mức trần đóng BHXH. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSI_COM" Text="BHXH"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_COM" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="radnmSI_COM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHXH. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSI_EMP" Text="BHXH"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_EMP" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="radnmSI_EMP"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHXH. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbHI" Text="BHYT"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmHI" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    (VND)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="radnmHI"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập  tỷ lệ mức trần đóng BHYT. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbHI_COM" Text="BHYT"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmHI_COM" runat="server" MinValue="0" MaxValue="100"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="radnmHI_COM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHYT. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbHI_EMP" Text="BHYT"></asp:Label>
                    <%--<span class="lbReq">*</span>--%> 
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmHI_EMP" runat="server" MinValue="0" MaxValue="100"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="radnmHI_EMP"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHYT. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <%--<td class="lb">
                    <%# Translate("BHTN")%>
                    &nbsp;
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmUI" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>--%>
                <td></td>
                <td></td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUI_COM" Text="BHTN"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmUI_COM" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="radnmUI_COM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHTN. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUI_EMP" Text="BHTN"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmUI_EMP" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="radnmUI_EMP"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHTN. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUI" Text="BH TNLD, BNN"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                    &nbsp;
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmUI" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    (VND)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="radnmUI"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỉ lệ mức trần đóng BH TNLD, BNN. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTNLD_BNN_COM" Text="BH TNLD, BNN"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmTNLD_BNN_COM" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="radnmTNLD_BNN_COM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BH TNLD, BNN. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTNLD_BNN_EMP" Text="BH TNLD, BNN"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmTNLD_BNN_EMP" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="radnmTNLD_BNN_EMP"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BH TNLD, BNN. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("BHXH-NN")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_NN" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    (VND)
                </td>
                <td class="lb">
                    <%# Translate("BHXH-NN")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_COM_NN" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="radnmSI_COM_NN" 
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHXH. %>"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("BHXH-NN")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_EMP_NN" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="radnmSI_EMP_NN"  
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHXH. %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="2" style="display: none">
                    <%# Translate("Hệ số hưởng ốm đau thai sản")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2" style="display: none">
                    <%# Translate("Hệ số hưởng chể độ nghỉ DS, PHSK")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2">
                    <%# Translate("Tuổi về hưu")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Chế độ ốm đau")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="radnmSICK" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Nghỉ tại nhà")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="radnmOFF_IN_HOUSE" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRETIRE_MALE" Text="Nam"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmRETIRE_MALE" MinValue="0" MaxValue="1000" runat="server">
                    </tlk:RadNumericTextBox>
                    (Tháng)
                    <asp:RequiredFieldValidator ID="Required" ControlToValidate="radnmRETIRE_MALE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tuổi về hưu nam. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Chế độ thai sản")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="radnmMATERNITY" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Nghỉ tập trung")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="radnmOFF_TOGETHER" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRETIRE_FEMALE" Text="Nữ"></asp:Label>
                    <%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmRETIRE_FEMALE" MinValue="0" MaxValue="1000" runat="server">
                    </tlk:RadNumericTextBox>
                    (Tháng)
                    <asp:RequiredFieldValidator ID="RequiredFiel" ControlToValidate="radnmRETIRE_FEMALE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tuổi về hưu nữ. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneGrid" runat="server" Scrolling="None" Width="100%" Height="100%">
        <tlk:RadGrid PageSize="50" ID="rgGridDataRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EFFECTIVE_DATE,SI,SI_EMP,SI_COM,HI,HI_EMP,HI_COM,UI,UI_EMP,UI_COM,BHTNLD_BNN_EMP,BHTNLD_BNN_COM,
                SICK,MATERNITY,OFF_IN_HOUSE,OFF_TOGETHER,RETIRE_MALE,RETIRE_FEMALE,SI_DATE,HI_DATE,SI_NN,SI_EMP_NN,SI_COM_NN,FOREIGN,FOREIGN_NAME">
                <Columns>
                    <%--<tlk:gridboundcolumn datafield="ID" uniquename="ID" visible="false" />
                    <tlk:gridboundcolumn headertext="<%$ Translate: Ngày hiệu lực %>" dataformatstring="{0:dd/MM/yyyy}"
                        datafield="EFFECTIVE_DATE" uniquename="EFFECTIVE_DATE" sortexpression="EFFECTIVE_DATE" currentfilterfunction="EqualTo" />
                    <tlk:gridboundcolumn headertext="<%$ Translate: Ngày chốt sổ BHXH %>" datafield="SI_DATE"
                        uniquename="SI_DATE" sortexpression="SI_DATE" dataformatstring="{0:N0}" />
                    <tlk:gridboundcolumn headertext="<%$ Translate: Ngày chốt sổ BHYT %>" datafield="HI_DATE"
                        uniquename="HI_DATE" sortexpression="HI_DATE" dataformatstring="{0:N0}" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: Mức trần BHXH %>" dataformatstring="{0:N0}"
                        datafield="SI" uniquename="SI" sortexpression="SI" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Nhân viên BHXH %>" dataformatstring="{0:N2}"
                        datafield="SI_EMP" uniquename="SI_EMP" sortexpression="SI_EMP" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Công ty BHXH %>" dataformatstring="{0:N2}"
                        datafield="SI_COM" uniquename="SI_COM" sortexpression="SI_COM" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: Mức trần BHYT %>" dataformatstring="{0:N0}"
                        datafield="HI" uniquename="HI" sortexpression="HI" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Nhân viên BHYT %>" datafield="HI_EMP"
                        uniquename="HI_EMP" sortexpression="HI_EMP" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Công ty BHYT %>" datafield="HI_COM"
                        uniquename="HI_COM" sortexpression="HI_COM" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Nhân viên BHTN %>" datafield="UI_EMP"
                        uniquename="UI_EMP" sortexpression="UI_EMP" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Công ty BHTN %>" datafield="UI_COM"
                        uniquename="UI_COM" sortexpression="UI_COM" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: Mức trần BH TNLD,BNN %>" dataformatstring="{0:N0}"
                        datafield="UI" uniquename="UI" sortexpression="UI" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Nhân viên BH TNLD, BNN %>" datafield="BHTNLD_BNN_EMP"
                        uniquename="BHTNLD_BNN_EMP" sortexpression="BHTNLD_BNN_EMP" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Công ty BH TNLD, BNN %>" datafield="BHTNLD_BNN_COM"
                        uniquename="BHTNLD_BNN_COM" sortexpression="BHTNLD_BNN_COM" />

                    <tlk:gridnumericcolumn headertext="<%$ Translate: Mức trần BHXH-NN %>" dataformatstring="{0:N0}"
                        datafield="SI_NN" uniquename="SI_NN" sortexpression="SI_NN" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Nhân viên BHXH-NN  %>" dataformatstring="{0:N2}"
                        datafield="SI_EMP_NN" uniquename="SI_EMP_NN" sortexpression="SI_EMP_NN" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Công ty BHXH-NN  %>" dataformatstring="{0:N2}"
                        datafield="SI_COM_NN" uniquename="SI_COM_NN" sortexpression="SI_COM_NN" />


                    <tlk:gridnumericcolumn headertext="<%$ Translate: % hường chế độ ốm đau %>" dataformatstring="{0:N0}"
                        datafield="SICK" uniquename="SICK" sortexpression="SICK" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % hưởng chế độ thai sản %>" dataformatstring="{0:N0}"
                        datafield="MATERNITY" uniquename="MATERNITY" sortexpression="MATERNITY" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Hưởng chế độ nghỉ tại nhà %> "
                        dataformatstring="{0:N0}" datafield="OFF_IN_HOUSE" uniquename="OFF_IN_HOUSE"
                        sortexpression="OFF_IN_HOUSE" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: % Hưởng chế độ nghỉ tập trung %>"
                        dataformatstring="{0:N0}" datafield="OFF_TOGETHER" uniquename="OFF_TOGETHER"
                        sortexpression="OFF_TOGETHER" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: Tuổi về hưu nam (Tháng) %>" dataformatstring="{0:N0}"
                        datafield="RETIRE_MALE" uniquename="RETIRE_MALE" sortexpression="RETIRE_MALE" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: Tuổi về hưu nữ (Tháng) %>" dataformatstring="{0:N0}"
                        datafield="RETIRE_FEMALE" uniquename="RETIRE_FEMALE" sortexpression="RETIRE_FEMALE" />
                    <tlk:gridnumericcolumn headertext="<%$ Translate: Người nước ngoài %>"
                        datafield="FOREIGN_NAME" uniquename="FOREIGN_NAME" sortexpression="FOREIGN_NAME" />--%>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlInsListParamInsurance_RadSplitter4';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListParamInsurance_RadPaneField';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListParamInsurance_RadPaneGrid';
        var validateID = 'MainContent_ctrlInsListParamInsurance_ValidationSummary1';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;
        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut(splitterID);
        //        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgGridDataRate.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) { } //ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGridDataRate');
                else
                    ResizeSplitterDefault();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter4.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPaneField.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter4.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPaneField.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPaneGrid.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
