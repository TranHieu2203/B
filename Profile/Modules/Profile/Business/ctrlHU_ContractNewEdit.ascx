<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ContractNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_ContractNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgCode" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidWorkStatus" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidSign2" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<style type="text/css">
    /*div.RadToolBar .rtbUL{
        text-align: right !important;
    }*/
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContract" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" >
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin hợp đồng")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode"  runat="server" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="<%$ Translate: Họ tên nhân viên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbTITLE" runat="server" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Đơn vị %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Công ty ký HĐLĐ %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCompanyReg" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboCompanyReg"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Công ty ký HĐLĐ. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Công ty ký HĐLĐ. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbContractType" runat="server" Text="<%$ Translate: Loại hợp đồng %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContractType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqContractType" ControlToValidate="cboContractType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại hợp đồng. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại hợp đồng. %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusvalContractType" ControlToValidate="cboContractType"
                        runat="server" ErrorMessage="<%$ Translate: Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>

            </tr>
            <tr>
                <%--<td class="lb">
                    <asp:Label ID="lbSignContract" runat="server" Text="Nơi làm việc"></asp:Label><span class="lbReq">*</span>    
                </td>
                <td class="lb">
                    <tlk:RadComboBox runat="server" ID="cboWorkPlace" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSignContract" ControlToValidate="cboWorkPlace"
                        runat="server" ErrorMessage="Bạn phải chọn Nơi làm việc."
                        ToolTip="Bạn phải chọn Nơi làm việc."> </asp:RequiredFieldValidator>
                </td>--%>
                <td class="lb">
                    <asp:Label ID="lbStartDate" runat="server" Text="<%$ Translate: Ngày bắt đầu %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack="True">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="<%$ Translate: Ngày kết thúc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile" Text="<%$ Translate: Tập tin đính kèm %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTheoryPhaseFrom" runat="server" Visible="false" Text="<%$ Translate: Giai đoạn học lý thuyết từ %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTheoryPhaseFrom" runat="server" Visible="false" >
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTheoryPhaseTo" runat="server" Visible="false" Text="<%$ Translate: Giai đoạn học lý thuyết đến %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTheoryPhaseTo" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdTheoryPhaseTo"
                        Type="Date" ControlToCompare="rdTheoryPhaseFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Giai đoạn học lý thuyết đến phải lớn hơn Giai đoạn học lý thuyết từ %>"
                        ToolTip="<%$ Translate: Giai đoạn học lý thuyết đến phải lớn hơn Giai đoạn học lý thuyết từ %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbPracticePhaseFrom" runat="server" Visible="false" Text="<%$ Translate: Giai đoạn thực tập từ %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPracticePhaseFrom" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbPracticePhaseTo" runat="server" Visible="false" Text="<%$ Translate: Giai đoạn thực tập đến %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPracticePhaseTo" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="rdPracticePhaseTo"
                        Type="Date" ControlToCompare="rdPracticePhaseFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Giai đoạn thực tập đến phải lớn hơn Giai đoạn thực tập từ %>"
                        ToolTip="<%$ Translate: Giai đoạn thực tập đến phải lớn hơn Giai đoạn thực tập từ %>"></asp:CompareValidator>
                </td>
            </tr>

            <%--<tr>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Thời gian làm việc"></asp:Label>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox ID="txtWorkTime" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>                 
            </tr>--%>
            <%--<tr>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Mô tả công việc"></asp:Label>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox ID="txtWorkDes" runat="server" Width="100%" EmptyMessage="Theo bảng mô tả công việc đính kèm">
                    </tlk:RadTextBox>
                </td>                 
            </tr>--%>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbStatus" runat="server" Text="<%$ Translate: Trạng thái %>"></asp:Label>
                      <span class="lbReq">*</span>    
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Trạng thái %>" ClientValidationFunction="cusStatus">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cusvalStatus" ControlToValidate="cboStatus" runat="server"
                        ErrorMessage="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbContractNo" runat="server" Text="<%$ Translate: Số hợp đồng %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" >
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <asp:Label ID="lbSignDate" runat="server" Text="<%$ Translate: Ngày ký %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
           
            <tr style="display: none">
                <td class="lb">
                    <asp:Label ID="lbSignName2" runat="server" Text="<%$ Translate: Người ký 2 %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignName2" runat="server" Width="130px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnSiger2" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignTitle2" runat="server" Text="<%$ Translate: Chức danh ký 2 %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle2" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="<%$ Translate: Ghi chú %>"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
             <tr>
                <td class="lb">
                    <asp:Label ID="lbSigner" runat="server" Text="<%$ Translate: Người ký %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSigner" runat="server" Width="130px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnSigner" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignTitle" runat="server" Text="<%$ Translate: Chức danh ký %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server">
                    </tlk:RadTextBox>
                </td>
               
            </tr>
            <tr id="hide1" runat="server">
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin lương")%></b>
                    <hr />
                </td>
            </tr>
            <tr id="hide2" runat="server">
                <td class="lb">
                    <asp:Label ID="lbWorking_ID" runat="server" Text="<%$ Translate: Chọn Hồ Sơ Lương %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="Working_ID" runat="server" ReadOnly="true" Width="130px" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnSalary" SkinID="ButtonView" runat="server" CausesValidation="false">
                    </tlk:RadButton>
                    <%--<asp:RequiredFieldValidator ID="reqWorking_ID" ControlToValidate="Working_ID" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Hồ sơ lương. %>" ToolTip="<%$ Translate: Bạn phải chọn Hồ sơ lương. %>"> </asp:RequiredFieldValidator>--%>
                </td>
                <%--<td class="lb">
                    <asp:Label ID="lbSalTYPE" runat="server" Text="<%# UI.Wage_WageGRoup %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalTYPE" runat="server" SkinID="LoadDemand" Enabled="False">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTaxTable" runat="server" Text="<%# UI.Wage_TaxTable %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" runat="server" SkinID="LoadDemand" Enabled="False">
                    </tlk:RadComboBox>
                </td>--%>
                
                <td class="lb">
                    <asp:Label ID="lbBasicSal" runat="server" Text="<%# UI.Wage_BasicSalary %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnBasicSal" runat="server" MinValue="0" SkinID="ReadOnly"
                        ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbPercentSalary" runat="server"><p style="margin:0 auto;">% hưởng lương</p></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="PercentSalary" runat="server" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSalary_Total" runat="server" Text="Lương thỏa thuận"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Salary_Total" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr id="hide3" runat="server">
<%--                <td class="lb">
                    <asp:Label ID="lbAllowance_Total" runat="server" Text="<%# UI.Wage_Allowance_total %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="Allowance_Total" runat="server" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>--%>
            </tr>
            <tr id="hide4" runat="server">
                <%--<td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary1" Text="Thưởng hiệu quả công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary1" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>--%>

                <%--<td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Lương đóng BHXH"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtBHXH" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>--%>

                <td class="lb" style="display: none">
                    <asp:Label runat="server" ID="lbOtherSalary2" Text="Phụ cấp kiêm nhiệm"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary2" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="display: none">
                    <asp:Label runat="server" ID="lbOtherSalary3" Text="Chi phí hỗ trợ khác"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary3" SkinID="Money" Enabled="False">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <%--<tr>
                <td colspan="6">
                   <tlk:RadGrid PageSize="50" ID="rgAllow" runat="server" SkinID="GridNotPaging">
                        <MasterTableView DataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE"
                            ClientDataKeyNames="ID,ALLOWANCE_LIST_ID,AMOUNT,IS_INSURRANCE,ALLOWANCE_LIST_NAME,EFFECT_DATE,EXPIRE_DATE">                            
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                                    SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                                    SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                                    SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>--%>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ContractNewEdit_RadSplitter1');
        });

        function cusContractType(oSrc, args) {
            var cbo = $find("<%# cboContractType.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID%>");
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


        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            //                        if (args.get_item().get_commandName() == 'CANCEL') {
            //                            getRadWindow().close(null);
            //                            args.set_cancel(true);
            //                        }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        <%--function cusSalType(oSrc, args) {
            var cbo = $find("<%# cboSalTYPE.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }--%>
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_ContractNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>
