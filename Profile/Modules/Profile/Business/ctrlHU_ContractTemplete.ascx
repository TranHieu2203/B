<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ContractTemplete.ascx.vb"
    Inherits="Profile.ctrlHU_ContractTemplete" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidEmployeeCode" runat="server" />
<asp:HiddenField ID="hidShortName" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidSign2" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidSalary" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<asp:HiddenField ID="hidLiquiDate" runat="server" />
<asp:HiddenField ID="hidContract_ID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidEmpOrg" runat="server" />
<asp:HiddenField ID="hidEmpTitle" runat="server" />
<asp:HiddenField ID="hidEmpWorkplace" runat="server" />
<asp:HiddenField ID="hidContractType_ID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<style type="text/css">
    /*div.RadToolBar .rtbUL{
        text-align: right !important;
    }*/
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContract" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="NORMAL" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <fieldset>
            <legend>
                <%# Translate("Thông tin nhân viên")%>
            </legend>
            <table  class="table-form">
                <tr>
                    <td class="lb" style="width: 10%">
                        <asp:Label ID="lbEmployeeCode" runat="server" Text="MSNV"></asp:Label>
                    </td>
                    <td style="width: 23%">
                        <tlk:RadTextBox ID="txtEmployeeCode" SkinID="Textbox15" AutoPostBack="true" runat="server" Width="130px">
                            <ClientEvents OnKeyPress="OnKeyPress" />
                        </tlk:RadTextBox>
                        <tlk:RadButton EnableEmbeddedSkins="false" ID="btnEmployee" SkinID="ButtonView" runat="server"
                            CausesValidation="false" Width="40px">
                        </tlk:RadButton>
                        <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb" style="width: 10%">
                        <asp:Label ID="lbEmployeeName" runat="server" Text="Họ và tên nhân viên"></asp:Label>
                    </td>
                    <td style="width: 23%">
                        <tlk:RadTextBox ID="txtEmployeeName" runat="server">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 10%">
                        <asp:Label ID="lbOrg" runat="server" Text="Phòng ban"></asp:Label>
                    </td>
                    <td style="width: 23%">
                        <tlk:RadTextBox ID="txtOrg" runat="server">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label ID="lbContract" runat="server" Text="Hợp đồng"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboContract" runat="server" CausesValidation="false" AutoPostBack="true">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="reqContract" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn hợp đồng %>"
                            ToolTip="<%$ Translate: Bạn phải chọn hợp đồng %>" ControlToValidate="cboContract">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="Label3" runat="server" Text="Từ ngày"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="radStart_Contract" runat="server" Enabled="false">
                        </tlk:RadDatePicker>
                    </td>
                    <td class="lb">
                        <asp:Label ID="Label4" runat="server" Text="Đến ngày"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="radEnd_Contract" runat="server" Enabled="false">
                        </tlk:RadDatePicker>
                    </td>
                </tr>
                <tr>

                    <td class="lb">
                        <asp:Label ID="lbAppend_TypeID" runat="server" Text="Loại phụ lục"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboAppend_TypeID" runat="server">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại phụ lục hợp đồng %>"
                            ToolTip="<%$ Translate: Bạn phải chọn loại phụ lục hợp đồng %>" ControlToValidate="cboAppend_TypeID">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbTitle1" runat="server" Text="Vị trí công việc"></asp:Label>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="txtTitle" runat="server"  Width="79%" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="display:none">
                        <asp:Label ID="Label2" runat="server" Text="Nơi làm việc"></asp:Label>
                    </td>
                    <td style="display:none">
                        <tlk:RadTextBox ID="txtWorkPlace" runat="server">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label ID="lbContract_NumAppen" runat="server" Text="Số phụ lục HĐLĐ"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtContract_NumAppen" runat="server">
                        </tlk:RadTextBox>
                        <asp:RequiredFieldValidator ID="reqContract_NumAppen" ControlToValidate="txtContract_NumAppen"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số phụ lục hợp đồng. %>"
                            ToolTip="<%$ Translate: Bạn phải nhập số phụ lục hợp đồng. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbStartDate" runat="server" Text="Ngày bắt đầu"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack="true">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>"> </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CompareStartDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải thuộc khoảng thời gian hiệu lực của hợp đồng. %>"
                            ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn thời gian hiệu lực của hợp đồng. %>"></asp:CustomValidator>
                        <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Không thể tạo phụ lục có hiệu lực nhỏ hơn ngày hiệu lực của hợp đồng. Vui lòng kiểm tra và thử lại. %>"
                            ToolTip="<%$ Translate: Không thể tạo phụ lục có hiệu lực nhỏ hơn ngày hiệu lực của hợp đồng. Vui lòng kiểm tra và thử lại. %>">
                        </asp:CustomValidator>--%>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbExpireDate" runat="server" Text="Ngày kết thúc"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdExpireDate"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày kết thúc. %>"
                            ToolTip="<%$ Translate: Bạn phải nhập ngày kết thúc. %>"> </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cval_EffectDate_ExpireDate" runat="server" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>"
                            ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>">
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="CompareExpireDate" runat="server" ErrorMessage="<%$ Translate: Ngày kết thúc phải thuộc khoảng thời gian hiệu lực của hợp đồng. %>"
                            ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn thời gian hiệu lực của hợp đồng. %>"></asp:CustomValidator>
                    </td>
                </tr>

                <tr style="display: none">
                    <td class="lb">
                        <asp:Label ID="lbSign2" runat="server" Text="Người ký 2"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtSign2" SkinID="Textbox15" runat="server" Width="130px">
                        </tlk:RadTextBox>
                        <tlk:RadButton EnableEmbeddedSkins="false" ID="btnSign2" SkinID="ButtonView" runat="server"
                            CausesValidation="false" Width="40px">
                        </tlk:RadButton>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbSign_Title2" runat="server" Text="Chức vụ người ký 2"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtSign_Title2" runat="server">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="lb">
                        <asp:Label ID="lbAppend_Content" runat="server" Text="Nội dung thay đổi"></asp:Label>
                    </td>
                    <td colspan="5">
                        <tlk:RadTextBox ID="txtAppend_Content" runat="server" Width="95%">
                        </tlk:RadTextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAppend_Content"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nội dung thay đổi. %>"
                            ToolTip="<%$ Translate: Bạn phải nhập nội dung thay đổi. %>"> </asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="lb">
                        <asp:Label ID="Label5" runat="server" Text="Mô tả phụ lục"></asp:Label>
                    </td>
                    <td colspan="4">
                        <tlk:RadTextBox ID="rtContent" runat="server" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                    </td>
                    <td colspan="4">
                        <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                        </tlk:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRemark"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ghi chú. %>" ToolTip="<%$ Translate: Bạn phải nhập ghi chú. %>"> </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label ID="lbStatus_ID" runat="server" Text="Trạng Thái"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboStatus_ID" runat="server">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái %>"
                            ToolTip="<%$ Translate: Bạn phải chọn Trạng thái %>" ControlToValidate="cboStatus_ID">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbSignDate" runat="server" Text="Ngày ký"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdSignDate" runat="server" DateInput-DisplayDateFormat="dd/MM/yyyy">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqSignDate" ControlToValidate="rdSignDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày ký. %>" ToolTip="<%$ Translate: Bạn phải nhập
                        ngày ký. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                        </tlk:RadTextBox>
                        <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                        </tlk:RadTextBox>
                        <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                            TabIndex="3" />
                        <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                            CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>

                    <td class="lb">
                        <asp:Label ID="lbSign" runat="server" Text="Người ký"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtSign" SkinID="Textbox15" runat="server" Width="130px" ReadOnly="true">
                        </tlk:RadTextBox>
                        <tlk:RadButton EnableEmbeddedSkins="false" ID="btnSign" SkinID="ButtonView" runat="server"
                            CausesValidation="false" Width="40px">
                        </tlk:RadButton>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtSign"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Người ký. %>" ToolTip="<%$ Translate: Bạn phải chọn Người ký %>"> 
                        </asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbSign_Title" runat="server" Text="Chức vụ người ký"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtSign_Title" runat="server" ReadOnly="true">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <%--THÔNG TIN LƯƠNG--%>
                <tr id="hide1" runat="server">
                    <td colspan="6">
                        <b>
                            <asp:Label ID="lbluong" runat="server" Text="Thông tin lương"></asp:Label>
                        </b>
                        <hr />
                    </td>
                </tr>
                <%--<tr>
                    <td class="lb">
                        <%# Translate("Chọn tờ trình/QĐ")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtDesionNo" runat="server" ReadOnly="true" Width="130px">
                        </tlk:RadTextBox>
                        <tlk:RadButton ID="btnSalary" SkinID="ButtonView" runat="server" CausesValidation="false">
                        </tlk:RadButton>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDesionNo"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải Chọn tờ trình/QĐ. %>" ToolTip="<%$ Translate: Bạn phải Chọn tờ trình/QĐ. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("Mức lương")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtBasicSal" runat="server" MinValue="0" SkinID="Money"
                            ReadOnly="true">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Thưởng hiệu quả công việc")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtMucBonus" runat="server" SkinID="Money">
                        </tlk:RadNumericTextBox>
                    </td>
                 </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("BHXH")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtSal_BHXH" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("BHYT")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtSal_BHYT" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("BHTN")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtSal_BHTN" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Tỷ lệ nghỉ hàng năm")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtPecentYear" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Lương nghỉ hàng năm")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtLeaveYear" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Tổng tiền lương")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtTotalSalary" runat="server" SkinID="Money" ReadOnly="True">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>--%>
                <tr id="hide2" runat="server">
                    <td class="lb">
                        <asp:Label ID="lbSalary" runat="server" Text="Chọn Hồ Sơ Lương"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="Working_ID" runat="server" ReadOnly="true" Width="130px">
                        </tlk:RadTextBox>
                        <tlk:RadButton ID="btnSalary" SkinID="ButtonView" runat="server" CausesValidation="false">
                        </tlk:RadButton>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="Working_ID"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải Chọn Hồ Sơ Lương. %>" ToolTip="<%$ Translate: Bạn phải Chọn Hồ Sơ Lương. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb" style="display:none">
                        <%# UI.Wage_WageGRoup%>
                    </td>
                    <td style="display:none">
                        <tlk:RadComboBox ID="cboSalTYPE" runat="server" SkinID="LoadDemand" Enabled="False">
                        </tlk:RadComboBox>
                        <%-- <asp:CustomValidator ID="cusSalType" runat="server" ErrorMessage="<%= groupWageWarning %>"
                                         ToolTip="<%= groupWageWarning %>" ClientValidationFunction="cusSalType">
                    </asp:CustomValidator>--%>
                    </td>
                    <td class="lb" style="display:none">
                        <%# UI.Wage_TaxTable %>
                    </td>
                    <td style="display:none">
                        <tlk:RadComboBox ID="cboTaxTable" runat="server" SkinID="LoadDemand" Enabled="False">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr id="hide3" runat="server">
                    <td class="lb">
                        <%# UI.Wage_BasicSalary %>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtBasicSal" runat="server" MinValue="0" ReadOnly="true">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb" style="display:none">
                        <asp:Label ID="lbPercentSalary" runat="server" Text="% hưởng lương"></asp:Label>
                    </td>
                    <td style="display:none">
                        <tlk:RadNumericTextBox ID="PercentSalary" runat="server" Enabled="False">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td style="display: none" class="lb">
                        <%# UI.Wage_Sal_Ins %>
                    </td>
                    <td style="display: none">
                        <tlk:RadNumericTextBox ID="SalaryInsurance" runat="server" SkinID="Money" Enabled="False">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <asp:Label ID="Label6" runat="server" Text="Lương thỏa thuận"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="Salary_Total" runat="server" SkinID="Money" Enabled="False">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td style="display: none" class="lb">
                        <%# UI.Wage_Allowance_total %>
                    </td>
                    <td style="display: none">
                        <tlk:RadNumericTextBox ID="Allowance_Total" runat="server" SkinID="Money" Enabled="False">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>
                <%-- <tr>
                  
                </tr>--%>
                <tr id="hide4" runat="server" style="display:none">
                    <td class="lb">
                        <asp:Label runat="server" ID="lbOtherSalary1" Text="Lương hiệu quả công việc"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary1" SkinID="Money" Enabled="False">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label1" Text="Lương đóng BHXH"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" ID="rnSalaryBHXH" SkinID="Money" ReadOnly="true">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="lb">
                        <asp:Label runat="server" ID="lbOtherSalary2" Text="Phụ cấp kiêm nhiệm"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary2" SkinID="Money" Enabled="False">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <asp:Label runat="server" ID="lbOtherSalary3" Text="Chi phí hỗ trợ khác"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary3" SkinID="Money" Enabled="False">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <tlk:RadDatePicker ID="radDate" runat="server" Visible="false">
                        </tlk:RadDatePicker>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="6">
                        <tlk:RadGrid ID="rgAllow" runat="server" Height="160px" Width="98%" SkinID="GridNotPaging">
                            <MasterTableView DataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                                ClientDataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderText="Tên phụ cấp" DataField="ALLOWANCE_LIST_NAME" SortExpression="ALLOWANCE_LIST_NAME"
                                        UniqueName="ALLOWANCE_LIST_NAME" />
                                    <tlk:GridNumericColumn HeaderText="Số tiền" DataField="AMOUNT" SortExpression="AMOUNT"
                                        UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </tlk:GridNumericColumn>
                                    <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                    <tlk:GridBoundColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                        Display="false" />
                                    <tlk:GridCheckBoxColumn HeaderText="Đóng bảo hiểm" DataField="IS_INSURRANCE" SortExpression="IS_INSURRANCE"
                                        UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px" Display="False">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </tlk:GridCheckBoxColumn>
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </td>
                </tr>--%>
                <tr style="visibility: hidden">
                    <td class="lb">
                        <tlk:RadTextBox ID="txtRemindLink" runat="server">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <%# Translate("Danh sách phụ lục hợp đồng")%>
            </legend>
            <tlk:RadGrid ID="rgContract" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                Height="230px" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,APPEND_NUMBER,START_DATE,EXPIRE_DATE">
                    <Columns>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="Số PLHĐ" DataField="APPEND_NUMBER" SortExpression="APPEND_NUMBER"
                            UniqueName="APPEND_NUMBER">
                            <HeaderStyle Width="250px" />
                            <ItemStyle Width="250px" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Ngày bắt đầu" DataField="START_DATE" ItemStyle-HorizontalAlign="Center"
                            SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                        <tlk:GridBoundColumn HeaderText="Ngày kết thúc" DataField="EXPIRE_DATE" ItemStyle-HorizontalAlign="Center"
                            SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </fieldset>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
//             if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
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
      
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function OnKeyPress(sender, eventArgs) 
        { 
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";   
           }      
        } 
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_ContractTemplete_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner2" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>
