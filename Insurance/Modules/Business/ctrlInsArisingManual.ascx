<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsArisingManual.ascx.vb"
    Inherits="Insurance.ctrlInsArisingManual" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<style>
    .rcTodayButton{
        display:none;
    }

     .rcCancelButton{
        display:none;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="Both">
    <tlk:RadToolBar ID="rtbMain" runat="server" />
    <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />        
        <div style="display: none;">
            <tlk:RadTextBox ID="txtID" Text="0" runat="server">
            </tlk:RadTextBox>
            <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
            </tlk:RadTextBox>
        </div>
        <div style="margin-left: 30px; margin-top: 5px; width: 1270px;">
            <fieldset style="width: auto; height: auto">
                <legend>
                    <b>
                        <asp:Label runat="server" ID="lbCommonInfo" Text="Thông tin chung">

                       </asp:Label>

                    </b>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:100px">
                            <asp:Label runat="server" ID="lbEMPLOYEE_ID" Text="MSNV"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_ID" AutoPostBack="true" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadButton EnableEmbeddedSkins="false" runat="server" ID="btnSearchEmp" SkinID="ButtonView" TabIndex="1"
                                CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqtxtEMPLOYEE_ID" ControlToValidate="txtEMPLOYEE_ID"
                                runat="server" ErrorMessage="Bạn phải nhập Nhân viên."
                                ToolTip="Bạn phải nhập Nhân viên."></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:80px">
                            <asp:Label runat="server" ID="lbFULLNAME" Text="Họ & tên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFULLNAME" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDEP" Text="Phòng ban"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtDEP" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbPOSITION" Text="Vị trí công việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtPOSITION" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="lb">
                            <asp:Label runat="server" ID="lbDoB" Text="Ngày sinh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ReadOnly="true"
                                ID="txtDoB" TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbCMND" Text="CMND"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCMND" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDateIssue" Text="Ngày cấp"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ReadOnly="true"
                                ID="txtDateIssue" TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Nơi cấp"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="BIRTH_PLACE" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <%--<tr>
                       
                        <td class="lb">
                            <asp:Label runat="server" ID="lbBirthPlace" Text="Nơi sinh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtBirthPlace" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                         <td class="lb">
                            <asp:Label runat="server" ID="Label2" Text="Số sổ bảo hiểm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="SOCIAL_NUMBER" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>--%>
                </table>
            </fieldset>
        </div>
        <div style="margin-left:30px;margin-top: 5px; width: 1270px;">
            <fieldset style="width: auto; height: auto">
                <legend>
                    <b><asp:Label runat="server" ID="lbInsInfo" Text="Thông tin bảo hiểm"></asp:Label></b>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:100px">
                            <asp:Label runat="server" ID="lbINS_ORG_ID" Text="Đơn vị bảo hiểm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlINS_ORG_ID" runat="server" TabIndex="2">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="width:120px">
                            <asp:Label runat="server" ID="lbINS_ARISING_TYPE_ID" Text="Loại biến động"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlINS_ARISING_TYPE_ID" AutoPostBack="true" CausesValidation="false" runat="server" TabIndex="3">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqddlINS_ARISING_TYPE_ID" ControlToValidate="ddlINS_ARISING_TYPE_ID"
                                runat="server"  ErrorMessage="Bạn phải nhập Loại biến động."
                                ToolTip="Bạn phải nhập Loại biến động."></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                             <asp:Label runat="server" ID="Label3" Text="Ngày báo giảm BHYT"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat ="server" ID ="rdHI_DECLARED_DATE" AutoPostBack="true"></tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label4" Text="Xử lý thẻ BHYT"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboHI_RATE_ARREARS" AutoPostBack="true"></tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbSALARY_PRE_PERIOD" Text="Lương kỳ trước"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSALARY_PRE_PERIOD" TabIndex="7"
                                runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbSALARY_NOW_PERIOD" Text="Lương kỳ này"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSALARY_NOW_PERIOD" runat="server" TabIndex="8" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbEFFECTIVE_DATE" Text="Ngày hiệu lực"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEFFECTIVE_DATE" AutoPostBack="true" TabIndex="9">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtEFFECTIVE_DATE" ControlToValidate="txtEFFECTIVE_DATE"
                                runat="server" ErrorMessage="Bạn phải nhập Ngày hiệu lực."
                                ToolTip="Bạn phải nhập Ngày hiệu lực."></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:120px">
                            <asp:Label runat="server" ID="lbEXPRIE_DATE" Text="Ngày kết thúc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEXPRIE_DATE" AutoPostBack="true" TabIndex="10">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mức đóng BHXH cũ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtSI_SAL_OLD" runat="server" SkinID="Money"
                                >
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Mức đóng BHXH mới")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtSI_SAL_NEW" runat="server" SkinID="Money"
                                >
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbARISING_FROM_MONTH" Text="Biến động Từ tháng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtARISING_FROM_MONTH"
                                TabIndex="11" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbARISING_TO_MONTH" Text="Đến tháng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtARISING_TO_MONTH"
                                TabIndex="12" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mức đóng BHYT cũ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtHI_SAL_OLD" runat="server" SkinID="Money"
                                >
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Mức đóng BHYT mới")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtHI_SAL_NEW" runat="server" SkinID="Money"
                                >
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDECLARE_DATE" Text="Đợt khai báo"></asp:Label>
                        </td>
                        <td>
                            <%--<tlk:RadDatePicker runat="server" ID="txtDECLARE_DATE" AutoPostBack="true" TabIndex="13">
                            </tlk:RadDatePicker>--%>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtDECLARE_DATE" 
                                   TabIndex="13" Culture="en-US" AutoPostBack="true">
                            </tlk:RadMonthYearPicker>
                            <asp:RequiredFieldValidator ID="reqtxtDECLARE_DATE" ControlToValidate="txtDECLARE_DATE"
                                runat="server" ErrorMessage="Bạn phải nhập Đợt khai báo."
                                ToolTip="Bạn phải nhập Đợt khai báo."></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbHEALTH_RETURN_DATE" Text="Ngày trả thẻ BHYT"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtHEALTH_RETURN_DATE" AutoPostBack="true"
                                TabIndex="15">
                            </tlk:RadDatePicker>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mức đóng BHTN cũ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtUI_SAL_OLD" runat="server" SkinID="Money"
                                >
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Mức đóng BHTN mới")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtUI_SAL_NEW" runat="server" SkinID="Money"
                                >
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNOTE" Width="100%" runat="server" TabIndex="16">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mức đóng BHTNLD_BNN cũ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rdBHTNLD_BNN_OLD" runat="server" SkinID="Money"
                                >
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Mức đóng BHTNLD_BNN mới")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rdBHTNLD_BNN_NEW" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td></td>
                        <td class="lb" colspan="3" style="text-align:left;">
                            <tlk:RadButton ID="chkSI" AutoPostBack="true" Text="BHXH" CausesValidation="false" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="4">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkHI" AutoPostBack="true" Text="BHYT" CausesValidation="false" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="5">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkUI" AutoPostBack="true" Text="BHTN" CausesValidation="false" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="6">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkTNLD_BNN" AutoPostBack="true" Text="BH TNLD, BNN" CausesValidation="false" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="6">
                            </tlk:RadButton>
                            <tlk:RadButton ID="chkIs_Foreign" AutoPostBack="true" Text="Người nước ngoài" CausesValidation="false" ToggleType="CheckBox"
                                ButtonType="ToggleButton" runat="server" TabIndex="7">
                            </tlk:RadButton>
                        </td>
                        </tr>
                    <tr style="display:none">
                        <td class="lb">
                            <%# Translate("Tiền lương mới")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rdSALARY_HD" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phụ cấp mới")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rdSALARY_PC" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin-left:30px; margin-top: 5px; width: 1270px;">
            <fieldset style="width: auto; height: auto">
                <legend>
                    <b><asp:Label runat="server" ID="lbA_SInfo" Text="Thông tin truy thu/ thoái thu"></asp:Label></b>
                </legend>
                <div style="margin-left: 44px; float:left">
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <b><asp:Label runat="server" ID="lbA_Info" Text="Thông tin truy thu"></asp:Label></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_FROM" Text="Từ tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtA_FROM"
                                        AutoPostBack="true" TabIndex="17" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_TO" Text="Đến tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtA_TO"
                                        AutoPostBack="true" TabIndex="18" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_SI" Text="BH_XH"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_SI" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_HI" Text="Bảo hiểm Y tế"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_HI" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbA_UI" Text="Bảo hiểm thất nghiệp"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtA_UI" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="lb">
                                    <%# Translate("BHTNLD_BNN")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="rdA_BHTNLD_BNN" runat="server" SkinID="Money"
                                        IncrementSettings-Step="5000" ReadOnly="true">
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <div style="margin-left: 80px; float:left">
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <b><asp:Label runat="server" ID="lbS_Info" Text="Thông tin thoái thu"></asp:Label></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_FROM" Text="Từ tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtR_FROM"
                                        AutoPostBack="true" TabIndex="19" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_TO" Text="Đến tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtR_TO"
                                        AutoPostBack="true" TabIndex="20" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_SI" Text="BH_XH"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_SI" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_HI" Text="Bảo hiểm Y tế"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_HI" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbR_UI" Text="Bảo hiểm thất nghiệp"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtR_UI" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("BHTNLD_BNN")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="rdR_BHTNLD_BNN" runat="server"
                                        IncrementSettings-Step="5000" ReadOnly="true">
                                       <%-- <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />--%>
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <div style="margin-right: 60px; float:right">
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <b><asp:Label runat="server" ID="lbBonusInfo" Text="Truy thu BHYT"></asp:Label></b>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_FROM" Text="Từ tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtO_FROM"
                                        AutoPostBack="true" TabIndex="22" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_TO" Text="Đến tháng"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtO_TO"
                                        AutoPostBack="true" TabIndex="23" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_HI" Text="Số tiền BHYT truy thu công ty"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtO_HI_COM" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label runat="server" ID="lbO_UI" Text="Số tiền BHYT truy thu nhân viên"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtO_HI_EMP" runat="server" ReadOnly="true">
                                        <NumberFormat GroupSeparator="." DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>                
            </fieldset>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<%--<tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
</tlk:RadAjaxPanel>--%>
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
        $(document).keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault();
            }
        });

    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />