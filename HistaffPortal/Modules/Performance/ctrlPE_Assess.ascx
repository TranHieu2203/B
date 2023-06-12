<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_Assess.ascx.vb"
    Inherits="Performance.ctrlPE_Assess" %>
<%@ Import Namespace="Common" %>
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<asp:Label ID="lblPE_CRITERIA_ID" runat="server" Visible="false" />
<asp:HiddenField ID="hidPE_OBJECT_ID" runat="server" />
<asp:HiddenField ID="hidPE_EMPLOYEE_ASSESSMENT_ID" runat="server" />
<div id="isDiv1" runat="server"  style="margin-top:1%" >
    <table>
    <tr>
        <td>
        <tlk:RadTextBox runat="server" ID="txtNote" EmptyMessage='<%$ Translate: Ý kiến %>'
            Width="300px">
        </tlk:RadTextBox>
        <%--<tlk:RadButton runat="server" ID="btnDeny" Text='<%$ Translate: Không duyệt %>' ForeColor="Red"
            ValidationGroup="Approve">
        </tlk:RadButton>--%>
        </td>
    </tr>
        </table>
</div>
<div style="overflow: auto">
    <table class="table-form">
        <tr>
            <td class="lb">
                <%# Translate("Mã nhân viên")%>
            </td>
            <td>
                <asp:Label ID="lblMaNhanVien" runat="server" />
                <asp:Label ID="lblEmpID" runat="server" Visible="false" />
            </td>
            <td class="lb">
                <%# Translate("Họ và tên")%>
            </td>
            <td>
                <asp:Label ID="lblFullName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Chức danh")%>
            </td>
            <td>
                <asp:Label ID="lblChucDanh" runat="server" />
            </td>
            <td class="lb">
                <%# Translate("Đơn vị")%>
            </td>
            <td>
                <asp:Label ID="lblDonVi" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Năm đánh giá")%>
            </td>
            <td>
                <tlk:RadNumericTextBox ID="txtYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="2900" SkinID="Number" CausesValidation="false" AutoPostBack="true">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>
            </td>
            <td class="lb">
                <%# Translate("Kiểu đánh giá")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboTypeAss" CausesValidation="false" AutoPostBack="true">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Kỳ đánh giá")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboPeriod" CausesValidation="false" AutoPostBack="true">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Trạng thái")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboTrangThai" CausesValidation="false">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Từ ngày")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdStartDate" runat="server" Enabled="false">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Đến ngày")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdEndDate" runat="server" Enabled="false">
                </tlk:RadDatePicker>
            </td>
        </tr>
        <tr style="font-weight: bold;">
            <td class="lb">
                <%# Translate("Tổng điểm NV")%>
            </td>
            <td>
                <asp:Label ID="lblTotalPoint" runat="server" />
            </td>
            <td class="lb">
                <%# Translate("Xếp hạng NV")%>
            </td>
            <td>
                <asp:Label ID="lblRating" runat="server" />
            </td>
        </tr>
        <tr style="font-weight: bold;">
            <td class="lb">
                <%# Translate("Tổng điểm QL")%>
            </td>
            <td>
                <asp:Label ID="lblTotalPointQL" runat="server" />
            </td>
            <td class="lb">
                <%# Translate("Xếp hạng QL")%>
            </td>
            <td>
                <asp:Label ID="lblRatingQL" runat="server" />
            </td>
        </tr>
    </table>
    <hr />
    <table class="table-form">
        <tr>
            <td class="lb">
                <%# Translate("Mã KPI")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtMaKPI" runat="server" Enabled="false">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Trọng số")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtTrongSo" runat="server" Enabled="false">
                </tlk:RadTextBox> %
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Tiêu chí KPI")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtTieuChiKPI" runat="server" Enabled="false">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Điểm quy đổi theo trọng số")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtQuyDoi" runat="server" Enabled="false">
                </tlk:RadTextBox> %
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Chỉ tiêu")%>
            </td>
            <td colspan="3">
                <tlk:RadTextBox ID="txtChiTieu" runat="server" Enabled="false">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Từ ngày")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdTuNgay" runat="server" Enabled="false">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Đến ngày")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdDenNgay" runat="server" Enabled="false">
                </tlk:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Kết quả")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtResult" runat="server">
                </tlk:RadTextBox>
                <%--<asp:RequiredFieldValidator ID="seqResult" ControlToValidate="txtResult" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải nhập Kết quả đánh giá %>" ToolTip="<%$ Translate: Bạn phải nhập Kết quả đánh giá %>">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                    ControlToValidate="txtResult" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>--%>
            </td>
            <td class="lb">
                <%# Translate("Ngày cập nhật")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdUpdateDate" runat="server">
                </tlk:RadDatePicker>
                <%-- <asp:RequiredFieldValidator ID="reqUpdateDate" ControlToValidate="rdUpdateDate" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải nhập Ngày cập nhật %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày cập nhật %>">
                </asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Trạng thái")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboEmp_Status" CausesValidation="false">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Ghi chú")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                </tlk:RadTextBox>
            </td>
        </tr>
    </table>
    <hr />
    <table class="table-form">
        <tr>
            <td class="lb">
                <%# Translate("Quản lý đánh giá kết quả công việc")%>
            </td>
            <td>
                <tlk:RadNumericTextBox ID="txtResult_Dir" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                    MaxValue="100">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>
                <%--   <asp:RequiredFieldValidator ID="reqResult_Dir" ControlToValidate="txtResult_Dir"
                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Kết quả quản lý đánh giá %>"
                    ToolTip="<%$ Translate: Bạn phải nhập Kết quả quản lý đánh giá %>">
                </asp:RequiredFieldValidator>--%> 
            </td>
            <td class="lb">
                <%# Translate("Ngày đánh giá")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdAss_Date" runat="server">
                </tlk:RadDatePicker>
                <%-- <asp:RequiredFieldValidator ID="reqAss_Date" ControlToValidate="rdAss_Date" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải nhập Ngày đánh giá %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày đánh giá %>">
                </asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Ghi chú")%>
            </td>
            <td colspan="3">
                <tlk:RadTextBox ID="txtRemark_Dir" runat="server" SkinID="Textbox1023" Width="100%">
                </tlk:RadTextBox>
            </td>
        </tr>
    </table>
    <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true" Height="250px"
        Width="99%">
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID, PE_CRITERIA_ID,LINK_POPUP" ClientDataKeyNames="PE_CRITERIA_ID,PE_CRITERIA_CODE,PE_CRITERIA_NAME,EXPENSE,AMONG,FROM_DATE,TO_DATE,PE_EMPLOYEE_ASSESSMENT_ID,EMPLOYEE_ID,PE_PERIO_ID,PE_OBJECT_ID,PE_CRITERIA_ID,RESULT,STATUS_EMP_ID,DIRECT_ID,UPDATE_DATE,REMARK,RESULT_DIRECT,ASS_DATE,REMARK_DIRECT,RESULT_CONVERT,LINK_POPUP">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridBoundColumn DataField="ID" Visible="false" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã KPI %>" DataField="PE_CRITERIA_CODE"
                    UniqueName="PE_CRITERIA_CODE" SortExpression="PE_CRITERIA_CODE" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tiêu chí KPI %>" DataField="PE_CRITERIA_NAME"
                    UniqueName="PE_CRITERIA_NAME" SortExpression="PE_CRITERIA_NAME" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chỉ tiêu %>" DataField="EXPENSE"
                    UniqueName="EXPENSE" SortExpression="EXPENSE" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trọng số %>" DataField="AMONG" UniqueName="AMONG"
                    SortExpression="AMONG" />
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="FROM_DATE"
                    UniqueName="FROM_DATE" SortExpression="FROM_DATE" />
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="TO_DATE"
                    UniqueName="TO_DATE" SortExpression="TO_DATE" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="RESULT" UniqueName="RESULT"
                    SortExpression="RESULT" />
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="UPDATE_DATE"
                    UniqueName="UPDATE_DATE" SortExpression="UPDATE_DATE" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm quy đổi theo trọng số NV %>" DataField="RESULT_CONVERT"
                    UniqueName="RESULT_CONVERT" SortExpression="RESULT_CONVERT" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Quản lý đánh giá kết quả công việc %>"
                    DataField="RESULT_DIRECT" UniqueName="RESULT_DIRECT" SortExpression="RESULT_DIRECT" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm quy đổi theo trọng số QL %>" DataField="RESULT_CONVERT_QL"
                    UniqueName="RESULT_CONVERT_QL" SortExpression="RESULT_CONVERT_QL" />
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày đánh giá %>" DataField="ASS_DATE"
                    UniqueName="ASS_DATE" SortExpression="ASS_DATE" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_EMP_NAME"
                    UniqueName="STATUS_EMP_NAME" SortExpression="STATUS_EMP_NAME" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="True" />
            </ClientSettings>
    </tlk:RadGrid>
    <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
</div>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                args.set_cancel(true);
                OpenNew();
            }
        }
        function POPUP(value) {
            window.open(value, "_blank");
            return false;
        }

        function OpenNew() {
            var bCheck = $find('<%= rgMain.ClientID%>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return 1;
            } else if (bCheck > 1) {
                return 2;
            }
            var id = $find('<%= rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('LINK_POPUP');
            OpenInNewTab('Default.aspx?mid=Performance&fid=ctrlPE_AssessHistory&noscroll=1&ID=' + id)
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadCodeBlock>
