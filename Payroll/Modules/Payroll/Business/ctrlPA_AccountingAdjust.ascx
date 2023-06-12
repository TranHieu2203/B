<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_AccountingAdjust.ascx.vb"
    Inherits="Payroll.ctrlPA_AccountingAdjust" %>
<%@ Import Namespace="Framework.UI" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="370px" Scrolling="None">
        <tlk:RadToolBar ID="tbMain" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
       <table class="table-form">
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin thêm mới/ Chỉnh sửa")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="Mã nhân viên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" SkinID="Readonly"
                        ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải chọn Nhân viên " ToolTip="Bạn phải chọn Nhân viên"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOrgName" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbYear" runat="server" Text="Năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPeriod" Text="Kỳ công"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqPeriod" ControlToValidate="cboPeriod"
                        runat="server" ErrorMessage="Bạn phải chọn kỳ công " ToolTip="Bạn phải chọn kỳ công"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbAdjustDate" Text="Ngày chỉnh công"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdAdjustingDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqAdjustingDate" ControlToValidate="rdAdjustingDate"
                        runat="server" ErrorMessage="Bạn phải chọn ngày nhận công " ToolTip="Bạn phải ngày nhận công"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Phòng ban nhận"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgSetName" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqOrgSetName" ControlToValidate="txtOrgSetName"
                        runat="server" ErrorMessage="Bạn phải chọn Phòng ban nhận " ToolTip="Bạn phải Phòng ban nhận"> 
                    </asp:RequiredFieldValidator>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Số công"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnAdjustingX" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="rqAdjustingX" ControlToValidate="rnAdjustingX"
                        runat="server" ErrorMessage="Bạn phải chọn Phòng ban nhận " ToolTip="Bạn phải Phòng ban nhận"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Số công"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin Tìm kiếm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYearSearch" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Kỳ công"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriodSearch" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label7" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID,EMPLOYEE_ID,ORG_NAME,TITLE_NAME,EMPLOYEE_CODE,EMPLOYEE_NAME,YEAR,PERIOD_ID,ADJUSTING_DATE,ORG_SET_ID,ORG_SET_NAME,ADJUSTING_X,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                        UniqueName="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME"
                        UniqueName="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                        UniqueName="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Năm & tháng" DataField="PERIOD_NAME" SortExpression="PERIOD_NAME"
                        UniqueName="PERIOD_NAME" />
                    <tlk:GridBoundColumn HeaderText="Ngày điều chỉnh" DataField="ADJUSTING_DATE"
                        UniqueName="ADJUSTING_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ADJUSTING_DATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="Công điều chỉnh" DataField="ADJUSTING_X" SortExpression="ADJUSTING_X"
                        UniqueName="ADJUSTING_X">
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="Phòng ban nhận" DataField="ORG_SET_NAME" SortExpression="ORG_SET_NAME"
                        UniqueName="ORG_SET_NAME" />
                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                        SortExpression="NOTE" UniqueName="NOTE" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
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
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
