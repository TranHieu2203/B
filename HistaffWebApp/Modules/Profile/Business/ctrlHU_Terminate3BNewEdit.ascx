<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Terminate3BNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_Terminate3BNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarTerminate" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hiSalbasic" runat="server" />
        <asp:HiddenField ID="hidWorkStatus" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
            
                        <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStaffRankName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lương cơ bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalBasic" runat="server" ReadOnly="true" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày vào làm")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoinDateState" runat="server" Enabled="False">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Thâm niên công tác")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSeniority" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí hỗ trợ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostSupport" runat="server" ReadOnly="true" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số hợp đồng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" DateInput-Enabled="False">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" DateInput-Enabled="False">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
            
                        <%# Translate("Thông tin nghỉ việc")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate3B" runat="server" DateInput-CausesValidation="false"
                        DateInput-Enabled="False">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                
                        <%# Translate("Danh mục tài sản bàn giao")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:CheckBox ID="chkIdenitifiCard" runat="server" /><%# Translate("Thẻ gửi xe, thẻ nhân viên")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày trả")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdIdentifiDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtIdentifiStatus" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtIdentifiMoney" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:CheckBox ID="chkSunCard" runat="server" Text="Thẻ thành viên, huy hiệu" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày trả")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSunDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSunStatus" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSunMoney" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:CheckBox ID="chkInsuranceCard" runat="server" Text="Thẻ BHYT" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày trả")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdInsuranceDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtInsuranceStatus" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtInsuranceMoney" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
              
                        <%# Translate("Các tài sản, Bảo hộ lao động (đồng phục) đã giao cho người lao động")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="6" style=" height:200px">
                    <tlk:RadGrid PageSize=50 ID="rgLabourProtect" runat="server" Height="200px" Width="900px" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true">
                        <MasterTableView EditMode="InPlace" Caption="Bảo hộ lao động">
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên bảo hộ %>" DataField="LABOURPROTECTION_NAME"
                                    SortExpression="LABOURPROTECTION_NAME" UniqueName="LABOURPROTECTION_NAME" ReadOnly="true" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng %>" DataField="QUANTITY"
                                    SortExpression="QUANTITY" UniqueName="QUANTITY" DataFormatString="{0:n0}" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="UNIT_PRICE"
                                    SortExpression="UNIT_PRICE" UniqueName="UNIT_PRICE" DataFormatString="{0:n0}"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp phát %>" DataField="DAYS_ALLOCATED"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="DAYS_ALLOCATED" UniqueName="DAYS_ALLOCATED"
                                    ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Thu hồi %>" DataField="RETRIEVED"
                                    SortExpression="RETRIEVED" UniqueName="RETRIEVED" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                            <HeaderStyle Width="100px" />
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="6" style=" height:200px">
                    <tlk:RadGrid PageSize=50 ID="rgAsset" runat="server" Height="200px" Width="900px" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true">
                        <MasterTableView EditMode="InPlace" Caption="Tài sản cấp phát">
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài sản %>" DataField="ASSET_NAME"
                                    SortExpression="ASSET_NAME" UniqueName="ASSET_NAME" ReadOnly="true" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã vạch %>" DataField="ASSET_BARCODE"
                                    SortExpression="ASSET_BARCODE" UniqueName="ASSET_BARCODE" ReadOnly="true" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Giá trị tài sản %>" DataField="ASSET_VALUE"
                                    SortExpression="ASSET_VALUE" UniqueName="ASSET_VALUE" DataFormatString="{0:n0}"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp phát %>" DataField="ISSUE_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="ISSUE_DATE" UniqueName="ISSUE_DATE"
                                    ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày thu hồi %>" DataField="RETURN_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="RETURN_DATE" UniqueName="RETURN_DATE"
                                    ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày sử dụng %>" DataField="DATE_USE"
                                    SortExpression="DATE_USE" UniqueName="DATE_USE" DataFormatString="{0:n0}" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng %>" DataField="STATUS_NAME"
                                    SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" ReadOnly="true" />
                            </Columns>
                            <HeaderStyle Width="100px" />
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                 
                        <%# Translate("Thông tin số ngày phép, số ngày nghỉ bù")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số phép còn lại")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRemainingLeave" runat="server" ReadOnly="true" SkinID="ReadonlyDecimal"
                        NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkIsRemainingLeave" runat="server" Text="Có chuyển phép sang công ty mới"
                        TextAlign="Right" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số ngày nghỉ bù còn lại")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCompensatoryLeave" runat="server" ReadOnly="true"
                        SkinID="ReadonlyDecimal" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkIsCompensatoryLeave" runat="server" Text="Có chuyển số ngày nghỉ bù sang công ty mới"
                        TextAlign="Right" />
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                   
                        <%# Translate("Phê duyệt")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Trạng tháỉ %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Trạng tháỉ  %>" ClientValidationFunction="cusStatus">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cusValStatus" ControlToValidate="cboStatus" runat="server" ErrorMessage="<%$ Translate: Trạng tháỉ đã chọn không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng tháỉ đã chọn không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày ký")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Người ký")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerName" runat="server" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_Terminate3BNewEdit_RadSplitter3');
        });

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

        function clientButtonClicking(sender, args) {
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
        }


    </script>
</tlk:RadCodeBlock>
