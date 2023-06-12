<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CandidateNegotiate.ascx.vb"
    Inherits="Recruitment.ctrlRC_CandidateNegotiate" %>
<%@ Import Namespace="Framework.UI" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidIS_EMP" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
     <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
     </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="Y">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin về yêu cầu tuyển dụng")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã YCTD: ")%>
                </td>
                <td>
                    <asp:Label ID="lblCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban tuyển dụng: ")%>
                </td>
                <td>
                    <asp:Label ID="lblOrgName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên YCTD: ")%>
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí tuyển dụng")%>
                </td>
                <td>
                    <asp:Label ID="lblTitleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
             <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin đàm phán")%>
                    <hr />
                </td>
            </tr>
        </table>
        <table id="Table1" class="table-form" runat="server">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ứng viên")%>
                </td>
                <td>
                    <asp:Label ID="lblRC_EmpCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Họ và tên ứng viên")%>
                </td>
                <td>
                    <asp:Label ID="lblRC_EmpName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbContractType" runat="server" Text="<%$ Translate: Loại hợp đồng %>"></asp:Label>
                    <span class="lbReq">*</span>
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
                <td class="lb">
                    <asp:Label ID="lbStartDate" runat="server" Text="<%$ Translate: Ngày bắt đầu %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack="True" CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="<%$ Translate: Ngày kết thúc %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdStartDate" Operator="GreaterThan" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"></asp:CompareValidator>
                </td>        
            </tr>          
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSalTYPE" runat="server" Text="Nhóm lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalTYPE" runat="server" AutoPostBack="true" CausesValidation="false" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalTYPE" runat="server" ControlToValidate="cboSalTYPE"
                        ErrorMessage="Bạn phải chọn Nhóm lương." ToolTip="Bạn phải chọn Nhóm lương."></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTaxTable" runat="server" Text="Biểu thuế"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    
                    <asp:RequiredFieldValidator ID="reqTaxTable" runat="server" ControlToValidate="cboTaxTable"
                        ErrorMessage="Bạn phải chọn Biểu thuế" ToolTip="Bạn phải chọn Biểu thuế"></asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Loại nhân viên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboEmployeeType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboEmployeeType"
                        ErrorMessage="Bạn phải chọn loại nhân viên." ToolTip="Bạn phải chọn loại nhân viên."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbProbationSal" runat="server" Text="<%$ Translate: Lương cơ bản %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnProbationSal" runat="server" MinValue="0" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqProbationSal" ControlToValidate="rnProbationSal" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Lương cơ bản. %>" ToolTip="<%$ Translate: Bạn phải nhập Lương cơ bản. %>"> 
                    </asp:RequiredFieldValidator>
                </td>                
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary1" Text="Lương HQCV"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary1" AutoPostBack="true" CausesValidation="false" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPercentSalary" Text="% hưởng lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnPercentSalary" AutoPostBack="true" CausesValidation="false" SkinID="Money"
                        MaxValue="100" MinValue="0">
                    </tlk:RadNumericTextBox>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rnPercentSalary"
                        runat="server" ErrorMessage="Bạn phải nhập % hưởng lương " ToolTip="Bạn phải nhập % hưởng lương"> 
                    </asp:RequiredFieldValidator>
                </td>
                </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOfficialSal" runat="server" Text="<%$ Translate: Tổng lương %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOfficialSal" runat="server" AutoPostBack="true" MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
        <table class="table-form" id="tbcheck" runat="server">
            <tr> 
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Ngày hiệu lực %>"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"> 
                    </asp:RequiredFieldValidator>--%>
                    </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSalTYPESAL" runat="server" Text="Nhóm lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalTYPESAL" runat="server" AutoPostBack="true" CausesValidation="false" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="reqSalTYPESAL" runat="server" ControlToValidate="cboSalTYPESAL"
                        ErrorMessage="Bạn phải chọn Nhóm lương." ToolTip="Bạn phải chọn Nhóm lương.">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTaxTableSAL" runat="server" Text="Biểu thuế"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTableSAL" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    
                    <%--<asp:RequiredFieldValidator ID="reqTaxTableSAL" runat="server" ControlToValidate="cboTaxTableSAL"
                        ErrorMessage="Bạn phải chọn Biểu thuế" ToolTip="Bạn phải chọn Biểu thuế">
                    </asp:RequiredFieldValidator>--%>
                </td>

                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Loại nhân viên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboEmployeeTypeSAL" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboEmployeeTypeSAL"
                        ErrorMessage="Bạn phải chọn loại nhân viên." ToolTip="Bạn phải chọn loại nhân viên.">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbProbationSalSAL" runat="server" Text="<%$ Translate: Lương cơ bản %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnProbationSalSAL" runat="server" MinValue="0" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                    <%--<asp:RequiredFieldValidator ID="reqProbationSalSAL" ControlToValidate="rnProbationSalSAL" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Lương cơ bản. %>" ToolTip="<%$ Translate: Bạn phải nhập Lương cơ bản. %>"> 
                    </asp:RequiredFieldValidator>--%>
                </td>                
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherSalary1SAL" Text="Lương HQCV"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnOtherSalary1SAL" AutoPostBack="true" CausesValidation="false" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="% hưởng lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnPercentSalarySAL" AutoPostBack="true" CausesValidation="false" SkinID="Money"
                        MaxValue="100" MinValue="0">
                    </tlk:RadNumericTextBox>
                    
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rnPercentSalarySAL"
                        runat="server" ErrorMessage="Bạn phải nhập % hưởng lương " ToolTip="Bạn phải nhập % hưởng lương"> 
                    </asp:RequiredFieldValidator>--%>
                </td>
                </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOfficialSalSAL" runat="server" Text="<%$ Translate: Tổng lương %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOfficialSalSAL" runat="server" AutoPostBack="true" MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                     <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
          <tr>
            <td class="item-head" colspan="6">
              <b> <%# Translate("Phụ cấp")%> </b>
                <hr />
            </td>
          </tr>
          <tr>
             <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="Phụ cấp"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboAlow" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Số tiền"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="ntxtMoneny" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbAllowanceUnit" Text="Đơn vị tính"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboAllowanceUnit" runat="server">
                    </tlk:RadComboBox>
                </td>
         </tr> 
          <tr>
            <td class="lb">
                    <asp:Label ID="lbStartDateAlow" runat="server" Text="<%$ Translate: Ngày bắt đầu %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAlowStartDate" runat="server" AutoPostBack="True" CausesValidation="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbAlowExpireDate" runat="server" Text="<%$ Translate: Ngày kết thúc %>"></asp:Label>
                </td>
				<td>
                    <tlk:RadDatePicker ID="rdAlowExpireDate" runat="server">
                    </tlk:RadDatePicker>
                </td>   
        </tr>
        </table>
         <table class="table-form">
              <tr>
                <td colspan="6">
                    <tlk:RadGrid ID="rgAllow" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        PageSize="1000" AllowSorting="false" AllowMultiRowSelection="false" CellSpacing="0"
                        GridLines="Vertical" Height="120px">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle Visible="false" />
                        <MasterTableView DataKeyNames="ID,ALLOWANCE_ID,ALLOWANCE_NAME,EFFECT_FROM,EFFECT_TO,MONEY,ALLOWANCE_UNIT_ID"
                            ClientDataKeyNames="ID,ALLOWANCE_ID,ALLOWANCE_NAME,EFFECT_FROM,EFFECT_TO,MONEY,ALLOWANCE_UNIT_ID"
                            CommandItemDisplay="Top">
                            <CommandItemStyle Height="25px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnInsertAllowance" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" CommandName="InsertAllow"
                                            ValidationGroup="grpAddAllow">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteAllowance" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" CommandName="DeleteAllow">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                <tlk:GridBoundColumn DataField="ALLOWANCE_ID" Visible="false" />
                                <tlk:GridBoundColumn DataField="ALLOWANCE_UNIT_ID" Visible="false" />
                                 <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_NAME"
                                    SortExpression="ALLOWANCE_NAME" UniqueName="ALLOWANCE_NAME" />
                                 <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị tính %>" DataField="ALLOWANCE_UNIT_NAME"
                                    SortExpression="ALLOWANCE_UNIT_NAME" UniqueName="ALLOWANCE_UNIT_NAME" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY"
                                    SortExpression="MONEY" UniqueName="MONEY" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_FROM"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_FROM" UniqueName="EFFECT_FROM"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EFFECT_TO"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_TO" UniqueName="EFFECT_TO"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
         </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
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

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            //            else if (item.get_commandName() == "SAVE") {
            //                // Nếu nhấn nút SAVE thì resize
            //                ResizeSplitter();
            //            } 
            else {
                // Nếu nhấn các nút khác thì resize default
                // ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusContractType(oSrc, args) {
            var cbo = $find("<%# cboContractType.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }
    </script>
</tlk:RadCodeBlock>
