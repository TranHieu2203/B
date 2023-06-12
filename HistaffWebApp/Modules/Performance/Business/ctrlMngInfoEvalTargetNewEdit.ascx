<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMngInfoEvalTargetNewEdit.ascx.vb"
    Inherits="Performance.ctrlMngInfoEvalTargetNewEdit" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    .ctl00_MainContent_ctrlMngInfoEvalTargetNewEdit_RadTextBox1 {
        height: 100px;
    }
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidGoalID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="400px">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <div style="width: 100%; float: left;">
            <div style="width: 100%; float: left;">
                <table class="table-form">
                    <tr>
                        <td colspan="4">
                            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin nhân viên")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Code" ReadOnly="true">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_id" Visible="False">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reEMPLOYEE_CODE" ControlToValidate="rtEmployee_Code"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn mã nhân viên %>"
                                ToolTip="<%$ Translate: Chưa chọn mã nhân viên %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnID" ReadOnly="true" Visible="false">
                            </tlk:RadNumericTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Name" AutoPostBack="true" Width="81%">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Chức danh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Name" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Id" ReadOnly="true" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtOrg_Name" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtOrg_id" ReadOnly="true" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboStatus"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn trạng thái %>"
                                ToolTip="<%$ Translate: Chưa chọn trạng thái %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboYear" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPeriod" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqPeriod" ControlToValidate="cboPeriod"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Kỳ đánh giá %>"
                                ToolTip="<%$ Translate: Chưa chọn Kỳ đánh giá %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdStartDate" CausesValidation="false" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEndDate" CausesValidation="false" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb"></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkPortalShow" Text="Hiển thị portal nhân viên" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày bắt đầu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffectDate" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Ngày bắt đầu %>"
                                ToolTip="<%$ Translate: Chưa chọn Ngày bắt đầu %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Số tháng")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtMonthNumber" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Lý do đánh giá")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="txtGoal" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqGoal" ControlToValidate="txtGoal"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Lý do đánh giá %>"
                                ToolTip="<%$ Translate: Chưa chọn Lý do đánh giá %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb"></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkIsConfirm" Text="KPI đã được xác nhận" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="7">
                            <tlk:RadTextBox runat="server" ID="txtNote" CausesValidation="false" txtNote="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin tiêu chí đánh giá")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chỉ số đo lường")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCriteria" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chỉ số đo lường")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="txtCriteria" txtCriteria="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                            <%--<asp:RequiredFieldValidator ID="reqCriteria" ControlToValidate="txtCriteria"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Chỉ số đo lường %>"
                                ToolTip="<%$ Translate: Chưa chọn Chỉ số đo lường %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị tính")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboUnit" CausesValidation="false">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqUnit" ControlToValidate="cboUnit"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Đơn vị tính %>"
                                ToolTip="<%$ Translate: Chưa chọn Đơn vị tính %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Tần suất đo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboFrequency" CausesValidation="false">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqFrequency" ControlToValidate="cboFrequency"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Tần suất đo %>"
                                ToolTip="<%$ Translate: Chưa chọn Tần suất đo %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("PP & Công thức")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="txtRemark" txtCriteria="txtRemark" Width="100%">
                            </tlk:RadTextBox>
                            <%--<asp:RequiredFieldValidator ID="reqRemark" ControlToValidate="txtRemark"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn PP & Công thức %>"
                                ToolTip="<%$ Translate: Chưa chọn PP & Công thức %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Nguồn đo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboSource" CausesValidation="false">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqSource" ControlToValidate="cboSource"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Nguồn đo %>"
                                ToolTip="<%$ Translate: Chưa chọn Nguồn đo %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại tiêu chí")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCriteriaType" CausesValidation="false" AutoPostBack="True">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqCriteriaType" ControlToValidate="cboCriteriaType"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Loại tiêu chí %>"
                                ToolTip="<%$ Translate: Chưa chọn Loại tiêu chí %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại đánh giá")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboEvaluateType" CausesValidation="false" AutoPostBack="True">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqEvaluateType" ControlToValidate="cboEvaluateType"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Loại đánh giá %>"
                                ToolTip="<%$ Translate: Chưa chọn Loại đánh giá %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Chỉ tiêu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" AutoPostBack="true" ID="txtTargets">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <%--<asp:RequiredFieldValidator ID="reqTargets" ControlToValidate="txtTargets"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Chỉ tiêu %>"
                                ToolTip="<%$ Translate: Chưa chọn Chỉ tiêu %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngưỡng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" AutoPostBack="true" ID="txtThreshold">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <%--<asp:RequiredFieldValidator ID="reqThreshold" ControlToValidate="txtThreshold"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Ngưỡng %>"
                                ToolTip="<%$ Translate: Chưa chọn Ngưỡng %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Trọng số")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnWeighted">
                            </tlk:RadNumericTextBox>
                            <%--<asp:RequiredFieldValidator ID="reqWeighted" ControlToValidate="rnWeighted"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Trọng số %>"
                                ToolTip="<%$ Translate: Chưa chọn Trọng số %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" AllowPaging="true" runat="server" EditMode="InPlace" Height="100%" Scrolling="None">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,KPI_ASSESSMENT_TEXT,IS_IN_DB,EMPLOYEE_POINT,RATIO" ClientDataKeyNames="ID,KPI_ASSESSMENT_TEXT" CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="72px" ID="btnAdd" runat="server" Text="Thêm" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                CausesValidation="false" CommandName="Add" TabIndex="3">
                            </tlk:RadButton>
                            <tlk:RadButton Width="102px" ID="btnCalculate" runat="server" Text="Điểm chuẩn" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/refresh.png"
                                CausesValidation="false" CommandName="Calculate" TabIndex="4">
                            </tlk:RadButton>
                            <tlk:RadButton Width="112px" ID="btnResult" runat="server" Text="Ghi nhận KQ" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/import1.png"
                                CausesValidation="false" CommandName="Result" TabIndex="5" OnClientClicked="btnResultClick" AutoPostBack="false">
                            </tlk:RadButton>
                            <tlk:RadButton Width="112px" ID="btnResultHistory" runat="server" Text="Xem lịch sử" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/document_ok.png"
                                CausesValidation="false" CommandName="ResultHistory" TabIndex="6" OnClientClicked="btnResultHistoryClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="70px" ID="btnDelete" runat="server" Text="Xóa" CausesValidation="false" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                CommandName="Delete" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="ID Chỉ số đo lường" DataField="KPI_ASSESSMENT" Visible="false"
                        ReadOnly="true" UniqueName="KPI_ASSESSMENT" SortExpression="KPI_ASSESSMENT" />
                    <tlk:GridBoundColumn HeaderText="Chỉ số đo lường" DataField="KPI_ASSESSMENT_TEXT" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="KPI_ASSESSMENT_TEXT" SortExpression="KPI_ASSESSMENT_TEXT" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Loại KPI" DataField="KPI_TYPE" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="KPI_TYPE" SortExpression="KPI_TYPE" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị tính" DataField="UNIT_NAME" UniqueName="UNIT_NAME"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="UNIT_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Tần suất đo" DataField="FREQUENCY_NAME" UniqueName="FREQUENCY_NAME"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="FREQUENCY_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Công thức tính" DataField="DESCRIPTION" UniqueName="DESCRIPTION"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="DESCRIPTION" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Nguồn đo" DataField="SOURCE_NAME" UniqueName="SOURCE_NAME"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="SOURCE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Loại tiêu chí" DataField="GOAL_TYPE_NAME" UniqueName="GOAL_TYPE_NAME"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="GOAL_TYPE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Chỉ tiêu" DataField="TARGET" UniqueName="TARGET"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="TARGET" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Ngưỡng" DataField="TARGET_MIN" UniqueName="TARGET_MIN"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="TARGET_MIN" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Trọng số" DataField="RATIO" UniqueName="RATIO"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="RATIO" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Điểm chuẩn" DataField="BENCHMARK" UniqueName="BENCHMARK"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="BENCHMARK" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="NV thực hiện" DataField="EMPLOYEE_ACTUAL" UniqueName="EMPLOYEE_ACTUAL"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="EMPLOYEE_ACTUAL" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridNumericColumn HeaderText="Điểm NV" DataField="EMPLOYEE_POINT" UniqueName="EMPLOYEE_POINT" DataFormatString="{0:N0}"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="EMPLOYEE_POINT" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" UniqueName="NOTE"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="NOTE" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="QL đánh giá" DataField="DIRECT_ACTUAL" UniqueName="DIRECT_ACTUAL"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="DIRECT_ACTUAL" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridNumericColumn HeaderText="Điểm đánh giá" DataField="DIRECT_POINT" UniqueName="DIRECT_POINT" DataFormatString="{0:N0}"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="DIRECT_POINT" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Ghi chú của QLTT" DataField="NOTE_QLTT" UniqueName="NOTE_QLTT"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="NOTE_QLTT" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Đã phát sinh" DataField="IS_IN_DB" UniqueName="IS_IN_DB"
                        HeaderStyle-Width="75px" ReadOnly="true" SortExpression="IS_IN_DB" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlMngInfoEvalTargetNewEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlMngInfoEvalTargetNewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlMngInfoEvalTargetNewEdit_RadPane2';
        var validateID = 'MainContent_ctrlMngInfoEvalTargetNewEdit_valSum';
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

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function btnResultHistoryClick(sender, args) {
            var m;
            var n;
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var goalID = document.getElementById("<%= hidGoalID.ClientID %>").value;
                if (goalID == 0) {
                    m = '<%# Translate("Bạn cần lưu trước khi sử dụng chức năng này") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                else {
                    var oWindow = radopen('Dialog.aspx?mid=Performance&fid=ctrlResultRecordHistory&group=Business&Criteria=All&GoalID=' + goalID, "rwPopup");
                    var pos = $("html").offset();
                    oWindow.moveTo(pos.left, pos.top);
                    oWindow.maximize();
                }
            }
            else if (bCheck > 0) {
                var str = ""
                for (var i = 0; i < bCheck; i++) {
                    var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[i].getDataKeyValue('KPI_ASSESSMENT_TEXT');
                    var id2 = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[i].getDataKeyValue('ID');
                    console.log(id2);
                    if (id2 == 0) {
                        m = '<%# Translate("Bạn cần lưu trước khi sử dụng chức năng này") %>';
                        n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        return;
                    }
                    if (str === "") {
                        str = id2
                    }
                    else {
                        str = str + "," + id2
                    }
                }
                var goalID = document.getElementById("<%= hidGoalID.ClientID %>").value;
                if (goalID == 0) {
                    m = '<%# Translate("Bạn cần lưu trước khi sử dụng chức năng này") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                else {
                    var oWindow = radopen('Dialog.aspx?mid=Performance&fid=ctrlResultRecordHistory&group=Business&Criteria=' + str + '&GoalID=' + goalID, "rwPopup");
                    var pos = $("html").offset();
                    oWindow.moveTo(pos.left, pos.top);
                    oWindow.maximize();
                }
            }
        }
        function btnResultClick(sender, args) {
            var m;
            var n;
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 0) {
                var str = ""
                for (var i = 0; i < bCheck; i++) {
                    var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[i].getDataKeyValue('KPI_ASSESSMENT_TEXT');
                    var id2 = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[i].getDataKeyValue('ID');
                    console.log(id2);
                    if (id2 == 0) {
                        m = '<%# Translate("Bạn cần lưu trước khi sử dụng chức năng này") %>';
                        n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        return;
                    }
                    if (str === "") {
                        str = id
                    }
                    else {
                        str = str + "," + id
                    }
                }
                var goalID = document.getElementById("<%= hidGoalID.ClientID %>").value;
                if (goalID == 0) {
                    m = '<%# Translate("Bạn cần lưu trước khi sử dụng chức năng này") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                else {
                    var oWindow = radopen('Dialog.aspx?mid=Performance&fid=ctrlResultRecord&group=Business&Criteria=' + str + '&GoalID=' + goalID, "rwPopup");
                    var pos = $("html").offset();
                    oWindow.moveTo(pos.left, pos.top);
                    oWindow.maximize();
                }
            }
        }
        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule');
                else
                    //                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                    ResizeSplitterDefault();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            debugger;
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == 'CANCEL') {
                //  getRadWindow().close(null);
                //  args.set_cancel(true);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function OnClientClose(sender, args) {
            console.log($find("<%= rgData.ClientID %>").get_masterTableView());
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
    </script>
</tlk:RadCodeBlock>
