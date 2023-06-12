<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WageNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_WageNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="hidempid1" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidTitle" runat="server" />
<asp:HiddenField ID="hidStaffRank" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="HidFrameSalary" runat="server" />
<asp:HiddenField ID="hidFrameSalaryRank" runat="server" />
<asp:HiddenField ID="hidDM_ID" runat="server" />
<style type="text/css">
    /*div.RadToolBar .rtbUL{
        text-align: right !important;
    }*/
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form" >
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="Mã nhân viên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải chọn Nhân viên " ToolTip="Bạn phải chọn Nhân viên"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbOrgName" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat="server" ID="lbDecisionNo" Text="Số tờ trình/QĐ"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
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
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin lương")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" AutoPostBack="true" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực" ToolTip="Bạn phải nhập ngày hiệu lực"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label9" runat="server" Text="Hình thức trả lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td runat="server">
                    <tlk:RadComboBox ID="cboSalPayment" runat="server" CausesValidation="false"
                        OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalPayment" runat="server" ControlToValidate="cboSalPayment"
                        ErrorMessage="Bạn phải nhập Hình thức trả lương" ToolTip="Bạn phải nhập Hình thức trả lương"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" id="hide1" runat="server">
                    <asp:Label ID="lbSalTYPE" runat="server" Text="Nhóm lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td id="hide2" runat="server">
                    <tlk:RadComboBox ID="cboSalTYPE" runat="server" AutoPostBack="true" CausesValidation="false"
                        OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnSelectedIndexChanged="cboSalTYPE_SelectedIndexChanged" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboSalTYPE"
                        ErrorMessage="Bạn phải chọn nhóm lương" ToolTip="Bạn phải chọn nhóm lương"></asp:RequiredFieldValidator>
                    <%--<asp:CustomValidator ID="cusSalType" runat="server" ClientValidationFunction="cusSalType"
                        ErrorMessage="<%# GetYouMustChoseMsg(UI.Wage_WageGRoup) %>" ToolTip="<%# GetYouMustChoseMsg(UI.Wage_WageGRoup) %>">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb" style="display: none;">
                    <asp:Label ID="Label3" runat="server" Text="Hệ số lương"></asp:Label><%--<span class="lbReq">*</span>--%>
                </td>
                <td style="display: none;">
                    <tlk:RadTextBox runat="server" ID="rtSAL_RANK_ID" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindFrameSalary" SkinID="ButtonView" CausesValidation="false" />
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rtSAL_RANK_ID"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn hệ số lương%>"> </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb" >
                    <asp:Label runat="server" ID="lbExpireDate" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td >
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThan" ErrorMessage="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực"
                        ToolTip="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực"></asp:CompareValidator>
                </td>
                <td class="lb" style="display: none;">
                    <asp:Label runat="server" ID="Label4" Text="Ngày nâng lương"></asp:Label>
                </td>
                <td style="display: none;">
                    <tlk:RadDatePicker ID="rdCOEFFICIENT" runat="server">
                    </tlk:RadDatePicker>
                </td>
                
                <td class="lb"></td>
                <td></td>
                <td class="lb"></td>
                <td></td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbbasicSalary" runat="server" Text="Lương thỏa thuận"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="basicSalary" MinValue="0" runat="server" AutoPostBack="true"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="basicSalary"
                        runat="server" ErrorMessage="Bạn phải nhập Lương thỏa thuận" ToolTip="Bạn phải nhập Lương thỏa thuận"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTaxTable" runat="server" Text="Biểu thuế"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" runat="server"  AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTaxTable" runat="server" ClientValidationFunction="cusTaxTable"
                        ErrorMessage="<%#  GetYouMustChoseMsg(UI.Wage_TaxTable) %>" ToolTip="<%#  GetYouMustChoseMsg(UI.Wage_TaxTable) %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPercentSalary" Text=""><p style="margin: 0 auto;">% hưởng lương <span class="lbReq">*</span></p></asp:Label>
                    
                </td>
                <td>
                    <%--<tlk:RadNumericTextBox runat="server" ID="rnPercentSalary" AutoPostBack="true" SkinID="Money"
                        MaxValue="100" MinValue="0">
                    </tlk:RadNumericTextBox>--%>
                    <tlk:RadComboBox ID="cboPercentSalary" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboPercentSalary"
                        runat="server" ErrorMessage="Bạn phải chọn % hưởng lương " ToolTip="Bạn phải chọn % hưởng lương"> 
                    </asp:RequiredFieldValidator>
                </td>

                <td class="lb" style="display: none;">
                    <asp:Label ID="Label1" runat="server" Text="Loại hợp đồng"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td style="display: none;">
                    <tlk:RadComboBox ID="cboEmployeeType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboEmployeeType"
                        ErrorMessage="Bạn phải chọn loại hợp đồng." ToolTip="Bạn phải chọn loại hợp đồng."></asp:RequiredFieldValidator>--%>
                </td>
                
            </tr>
            <tr  style="display:none;">
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryGroup" Text="Thang lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbSalaryGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryLevel" Text="Ngạch lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbSalaryLevel" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryRank" Text="Bậc lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbSalaryRank" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbFactorSalary" Text="Hệ số/mức tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="rnFactorSalary" AutoPostBack="true" EnabledStyle-HorizontalAlign="Right">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSalaryInsurance" runat="server" Text="Mức lương đóng bảo hiểm"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="SalaryInsurance" runat="server" AutoPostBack="true" Enabled="False"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAllowance_Total" runat="server" Text="Tổng phụ cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="cboAllowance_Total" runat="server" Enabled="False" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>

                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Lương cơ bản (BHXH)"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdSalaryBHXH" MinValue="0" runat="server"  AutoPostBack="true"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdSalaryBHXH"
                        runat="server" ErrorMessage="Bạn phải nhập Lương đóng BHXH " ToolTip="Bạn phải nhập Lương đóng BHXH"> 
                    </asp:RequiredFieldValidator>
                </td>

                <td class="lb" id="tdGas" runat="server">
                    <asp:Label ID="lbGas" runat="server" Text="Xăng xe" Visible="false"></asp:Label>
                </td>
                <td id="tdGas1" runat="server">
                    <tlk:RadNumericTextBox ID="rnGas" MinValue="0" runat="server" Visible="false" AutoPostBack="false"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" id="tdAddtionalSal" runat="server">
                    <asp:Label ID="lbAddtionalSal" runat="server" Text="Khoản bổ sung" Visible="false"></asp:Label>
                </td>
                <td id="tdAddtionalSal1" runat="server">
                    <tlk:RadNumericTextBox ID="rnAddtionalSal" MinValue="0" runat="server" Visible="false" AutoPostBack="false"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>


            </tr>
            <tr>
                <td class="lb" colspan="2" style="padding-right:0">
                    <asp:Label ID="Label7" runat="server" Text="Cho phép nhập Lương cơ bản < Mức lương tối thiểu vùng"></asp:Label>
                    <asp:CheckBox ID="chkIS_ALLOW_SALARY_LESS_THAN" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb" colspan="2" style="padding-right:23px">
                    <asp:Label ID="Label8" runat="server" Text="(đối với HSL CTV, HDDV, TTS)"></asp:Label>
                </td>
            </tr>
            <tr id="hide3" runat="server">
                <td class="lb" id="tdMinSal" runat="server">
                    <asp:Label ID="lbMinSal" runat="server" Text="Mức lương tối thiểu vùng" ></asp:Label>
                </td>
                <td id="tdMinSal1" runat="server">
                    <tlk:RadNumericTextBox ID="rnMinSal" MinValue="0" runat="server"  AutoPostBack="false" ReadOnly="true"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" id="tdPhone" runat="server">
                    <asp:Label ID="lbPhone" runat="server" Text="Điện thoại" Visible="false"></asp:Label>
                </td>
                <td id="tdPhone1" runat="server">
                    <tlk:RadNumericTextBox ID="rnPhone" MinValue="0" runat="server" Visible="false" AutoPostBack="false"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" id="tdOtherSalary1" runat="server">
                    <asp:Label runat="server" ID="lbOtherSalary1" Text="Thưởng HQCLCV" Visible="false"></asp:Label>
                </td>
                <td id="tdOtherSalary11" runat="server">
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary1" AutoPostBack="true" Visible="false" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="display: none;">
                    <asp:Label ID="lbSalary_Total" runat="server" Text="Lương chính"></asp:Label>
                </td>
                <td style="display: none;">
                    <tlk:RadNumericTextBox ID="Salary_Total" MinValue="0" runat="server" Enabled="False" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>

                <td class="lb" style="display: none">
                    <asp:Label runat="server" ID="lbOtherSalary2" Text="Phụ cấp kiêm nhiệm"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary2" AutoPostBack="true" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="display: none">
                    <asp:Label runat="server" ID="lbOtherSalary3" Text="Chi phí hỗ trợ khác"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary3" AutoPostBack="true" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary4" Text="Lương khác 4"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary4" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOtherSalary5" runat="server" Text="Thưởng chuyên cần"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOtherSalary5" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSaleCommision" runat="server" Text="Đối tượng Sale Commision"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSaleCommision" runat="server" AutoPostBack="true" CausesValidation="false"
                        OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnSelectedIndexChanged="cboSalTYPE_SelectedIndexChanged" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr style="display: none">
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="lbAllowance" runat="server" Text="Phụ cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboAllowance" runat="server" AutoPostBack="true" OnClientItemsRequesting="OnClientItemsRequesting"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" SkinID="LoadDemand"
                        ValidationGroup="Allowance">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAmount" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money" ValidationGroup="Allowance">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="lbAllowEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAllowEffectDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAllowExpireDate" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAllowExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="rdEffectDate"
                        ControlToValidate="rdAllowExpireDate" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"
                        Operator="GreaterThanEqual" ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày hiệu lực %>"
                        Type="Date"></asp:CompareValidator>
                </td>
                <td>
                    <tlk:RadButton ID="chkIsInsurrance" runat="server" AutoPostBack="false" ButtonType="ToggleButton"
                        CausesValidation="false" Enabled="false" Text=" Đóng bảo hiểm " ToggleType="CheckBox">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr style="display: none">
                <td colspan="6">
                    <tlk:RadGrid ID="rgAllow" runat="server" Height="200px" PageSize="50" SkinID="GridNotPaging"
                        Width="100%">
                        <MasterTableView ClientDataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                            CommandItemDisplay="Top" DataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE">
                            <CommandItemStyle Height="28px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnInsertAllowance" runat="server" CausesValidation="false" CommandName="InsertAllow"
                                            Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png" Text="<%$ Translate: Thêm %>"
                                            Width="70px">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteAllowance" runat="server" CausesValidation="false" CommandName="DeleteAllow"
                                            Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png" OnClientClicking="btnDeleteAllowanceClick"
                                            Text="<%$ Translate: Xóa %>" Width="70px">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                                    SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                                    SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                                    SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>--%>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="Tỷ lệ độc hại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnToxic_rate" MinValue="0" runat="server" AutoPostBack="true" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>

                <td class="lb">
                    <asp:Label ID="Label6" runat="server" Text="Phụ cấp độc hại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnToxic_salary" MinValue="0" runat="server" ReadOnly="true"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>


            </tr>
             <tr >
                <td class="lb" id="tdPC1" runat="server">
                    <asp:Label ID="lbPC1" runat="server" Visible="false" Text="PC cơm"></asp:Label>
                </td>
                <td id="tdPC11" runat="server">
                    <tlk:RadNumericTextBox ID="rnPC1" Visible="false" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>

               <td class="lb" id="tdPC2" runat="server">
                    <asp:Label ID="lbPC2" runat="server" Visible="false" Text="PC hỗ trợ nhà ở"></asp:Label>
                </td>
                <td id="tdPC21" runat="server">
                    <tlk:RadNumericTextBox ID="rnPC2" Visible="false" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>

                 <td class="lb" id="tdPC3" runat="server">
                    <asp:Label ID="lbPC3" runat="server" Visible="false" Text="PC đồng phục"></asp:Label>
                </td>
                <td id="tdPC31" runat="server">
                    <tlk:RadNumericTextBox ID="rnPC3" MinValue="0" Visible="false" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>

                 

            </tr>
            <tr>
                <td class="lb" id="tdPC4" runat="server">
                    <asp:Label ID="lbPC4" runat="server" Text="PC đi lại" Visible="false"></asp:Label>
                </td>
                <td id="tdPC41" runat="server">
                    <tlk:RadNumericTextBox ID="rnPC4" MinValue="0" runat="server" Visible="false" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>

                 <td class="lb" id="tdPC5" runat="server">
                    <asp:Label ID="lbPC5" runat="server" Text="Thưởng chuyên cần" Visible="false"></asp:Label>
                </td>
                <td id="tdPC51" runat="server">
                    <tlk:RadNumericTextBox ID="rnPC5" MinValue="0" runat="server" Visible="false" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin lương cổ phiếu (nếu có)")%>
                    <hr />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="lbShareSal" runat="server" Text="Lương cổ phiếu"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnShareSal" MinValue="0" runat="server"
                        SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin phê duyệt")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trạng thái")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ClientValidationFunction="cusStatus"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái %>" ToolTip="<%$ Translate: Bạn phải chọn Trạng thái  %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày ký")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label_Signer" runat="server" Text="Người ký"></asp:Label>
                    <%--<%# Translate("Người ký")%>--%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignName" runat="server" ReadOnly="true" SkinID="Readonly"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSign" runat="server" CausesValidation="false" SkinID="ButtonView">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="Label_title" runat="server" Text="Vị trí"></asp:Label>
                    <%--<%# Translate("Chức danh")%>--%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="6">
                    <asp:Label ID="lbInfo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindFrameSalary" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

       <%-- function cusDecisionType(oSrc, args) {
            var cbo = $find("<%# cboDecisionType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }--%>

        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusSalType(oSrc, args) {
            var cbo = $find("<%# cboSalTYPE.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
      <%--  function cusSalGroup(oSrc, args) {
            var cbo = $find("<%# cbSalaryGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        } --%>
        function cusTaxTable(oSrc, args) {
            var cbo = $find("<%# cboTaxTable.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        <%--   function cusSalLevel(oSrc, args) {
            var cbo = $find("<%# cbSalaryLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        } --%>

       <%-- function cusSalRank(oSrc, args) {
            var cbo = $find("<%# cbSalaryRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }--%>

        function btnDeleteAllowanceClick(sender, args) {
            var bCheck = $find('<%# rgAllow.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                args.set_cancel(true);
            }
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
            if (args.get_item().get_commandName() == 'SAVE') {
                <%-- var objSalBasic = $find('<%= basicSalary.ClientID %>');
               --%>
                var valueSalBasic = 0;
                var valueSalTotal = 0;
                var valueCostSupport = 0;
                var valueHSDieuChinh = 0;
                var valueTGGiuBac = 0;
                var valueLuongDieuTiet = 0;
                <%-- if (objSalBasic.get_value()) {
                    valueSalBasic = objSalBasic.get_value();
                }
             if (objSalTotal.get_value()) {
                    valueSalTotal = objSalTotal.get_value();
                } 
                if (objCostSupport.get_value()) {
                    valueCostSupport = objCostSupport.get_value();
                }
                if (objHSDieuChinh.get_value()) {
                    valueHSDieuChinh = objHSDieuChinh.get_value();
                }
                if (objTGGiuBac.get_value()) {
                    valueTGGiuBac = objTGGiuBac.get_value();
                }
                if (objLuongDieuTiet.get_value()) {
                    valueLuongDieuTiet = objLuongDieuTiet.get_value();
                }--%>
                //                if (valueSalTotal - valueSalBasic - valueCostSupport - valueHSDieuChinh != 0) {
                //                    var m = 'Tổng lương phải bằng Lương cơ bản + Chi phí hỗ trợ + Hệ số điều chỉnh';
                //                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                //                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                //                    args.set_cancel(true);
                //                }
            }
            //if (args.get_item().get_commandName() == 'CANCEL') {
            //    //getRadWindow().close(null);
            //    args.set_cancel(true);
            //}
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }


        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cbSalaryGroup.ClientID %>':
                    cbo = $find('<%= cbSalaryLevel.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cbSalaryRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= basicSalary.ClientID %>');
                    clearSelectRadnumeric(cbo);

                    break;
                case '<%= cbSalaryLevel.ClientID %>':
                    cbo = $find('<%= cbSalaryRank.ClientID %>');
                clearSelectRadcombo(cbo);
                cbo = $find('<%= basicSalary.ClientID %>');
                clearSelectRadnumeric(cbo);

                break;
            case '<%= cbSalaryRank.ClientID %>':
                    cbo = $find('<%= basicSalary.ClientID %>');
                clearSelectRadnumeric(cbo);
                var item = eventArgs.get_item();
                if (item) {
                    cbo.set_value(item.get_attributes().getAttribute("SALARY_BASIC"));
                }
                break;
            default:
                break;
        }
    }


    function OnClientItemsRequesting(sender, eventArgs) {
        var id = sender.get_id();
        var cbo;
        var value;
        switch (id) {
            case '<%= cboAllowance.ClientID %>':
                break;
            case '<%= cboSalType.ClientID %>':
                cbo = $find('<%= rdEffectDate.ClientID %>');
                    var date = cbo.get_selectedDate();
                    if (date) {
                        var day = cbo.get_selectedDate().getDate();
                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                        var month = months[cbo.get_selectedDate().getMonth()];
                        var year = cbo.get_selectedDate().getFullYear();
                        value = day + "/" + month + "/" + year;
                    }
                    break;
               <%-- case '<%= cbSalaryGroup.ClientID %>':
                    cbo = $find('<%= rdEffectDate.ClientID %>');
                    var date = cbo.get_selectedDate();
                    if (date) {
                        var day = cbo.get_selectedDate().getDate();
                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                        var month = months[cbo.get_selectedDate().getMonth()];
                        var year = cbo.get_selectedDate().getFullYear();
                        value = day + "/" + month + "/" + year;
                    }
                    break; 
                case '<%= cbSalaryLevel.ClientID %>':
                    cbo = $find('<%= cbSalaryGroup.ClientID %>');
                    value = cbo.get_value();
                    break;--%>
            case '<%= cbSalaryRank.ClientID %>':
                cbo = $find('<%= cbSalaryLevel.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = null;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;

        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function OnBasicSalaryChanged(sender, args) {
            var basicSalary = $find('<%= basicSalary.ClientID %>').get_value();
            var salaryTotal = $find('<%= Salary_Total.ClientID %>');
            var salIn = $find('<%= SalaryInsurance.ClientID %>');
            var dataItems = $find('<%= rgAllow.ClientID %>').get_masterTableView().get_dataItems();
            var allowanceIns = 0;
            var allowanceTotal = 0;
            for (var i = 0; i < dataItems.length; i++) {
                var allowAmount = parseFloat(dataItems[i].getDataKeyValue("AMOUNT"));
                if (dataItems[i].getDataKeyValue("IS_INSURRANCE") === "True") {
                    allowanceIns += allowAmount;
                }
                allowanceTotal += allowAmount;
            }
            //salaryTotal.set_value(basicSalary + allowanceTotal);
            //salIn.set_value(basicSalary + allowanceIns);
        }
        function OnValueChanged(sender, args) {
            var id = sender.get_id();
            var valueSalBasic = 0;
            var valueSalTotal = 0;
            var valueHSDieuChinh = 0;
            var valueTGGiuBac = 0;
            var valueLuongDieuTiet = 0;
          <%--    var objSalBasic = $find('<%= basicSalary.ClientID %>');
            if (objSalBasic.get_value()) {
                valueSalBasic = objSalBasic.get_value();
            }
             if (objSalTotal.get_value()) {
                valueSalTotal = objSalTotal.get_value();
            } 
            if (objHSDieuChinh.get_value()) {
                valueHSDieuChinh = objHSDieuChinh.get_value();
            } --%>
            <%-- switch (id) {
                case '<%= basicSalary.ClientID %>':
                    valueSalBasic = 0;
                    if (args.get_newValue()) {
                        valueSalBasic = args.get_newValue();
                    }
                    break;
                default:
                    break;
           
            } 
        --%>
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
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_WageNewEdit_txtEmployeeCode' && e.target.id !== 'ctl00_MainContent_ctrlHU_WageNewEdit_rtSAL_RANK_ID') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
