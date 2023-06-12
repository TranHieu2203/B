<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalTimeTimesheetDetails.ascx.vb"
    Inherits="Attendance.ctrlPortalTimeTimesheetDetails" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<table class="table-form">
    <tr>
        <td class="lb">
            <%# Translate("Năm")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                TabIndex="12" Width="80px">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Kỳ công")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="150px" AutoPostBack="false"
                MaxLength="80" runat="server" ToolTip="">
            </tlk:RadComboBox>
        </td>
        <td>
            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Xem %>" SkinID="ButtonFind">
            </tlk:RadButton>
        </td>
    </tr>
</table>
<tlk:RadGrid PageSize=50 ID="rgiTime" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" HeaderStyle-Width="150" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" HeaderStyle-Width="200" Visible="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Công chuẩn %>" DataField="PERIOD_STANDARD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="PERIOD_STANDARD"
                                UniqueName="PERIOD_STANDARD">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Làm việc ngày thường (X) %>" DataField="WORKING_X"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="WORKING_X"
                                UniqueName="WORKING_X">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="<%$ Translate: Công cơm %>" DataField="WORKING_MEAL"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_MEAL"
                                UniqueName="WORKING_MEAL">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày nghỉ tuần %>" DataField="WORKING_F"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_F"
                                UniqueName="WORKING_F">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ lễ tết (LT) %>" DataField="WORKING_L"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_L"
                                UniqueName="WORKING_L">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ đi đường (DD) %>" DataField="WORKING_D"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_D"
                                UniqueName="WORKING_D">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ thai sản (TS) %>" DataField="WORKING_TS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_TS"
                                UniqueName="WORKING_TS">
                                <HeaderStyle Width="140px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ ốm (O) %>" DataField="WORKING_O"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_O"
                                UniqueName="WORKING_O">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ phép (P) %>" DataField="WORKING_P"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_P"
                                UniqueName="WORKING_P">
                                <HeaderStyle Width="140px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ bù (B) %>" DataField="WORKING_B"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_B"
                                UniqueName="WORKING_B">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ công tác (CT) %>" DataField="WORKING_C"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_C"
                                UniqueName="WORKING_C">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ việc riêng có hưởng lương (R)  %>"
                                DataField="WORKING_E" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}"
                                SortExpression="WORKING_E" UniqueName="WORKING_E" Visible="false">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ đi học (DH) %>" DataField="WORKING_A"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_A"
                                UniqueName="WORKING_A">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ việc riêng (VR) %>" DataField="WORKING_V"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_V"
                                UniqueName="WORKING_V">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ biến chứng thai sản (BCTS) %>" DataField="WORKING_H"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_H"
                                UniqueName="WORKING_H">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ khám thai (KT) %>" DataField="WORKING_Q"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_Q"
                                UniqueName="WORKING_Q">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ vợ sinh (VS) %>" DataField="WORKING_N"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_N"
                                UniqueName="WORKING_N">
                                <HeaderStyle Width="140px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ kết hôn (KH) %>" DataField="WORKING_R"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_R"
                                UniqueName="WORKING_R">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ tang gia (TG) %>" DataField="WORKING_T"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_T"
                                UniqueName="WORKING_T">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ không hưởng lương (NKL) %>" DataField="WORKING_K"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_K"
                                UniqueName="WORKING_K">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ có hưởng lương (NCL) %>" DataField="WORKING_J"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_J"
                                UniqueName="WORKING_J">
                            </tlk:GridNumericColumn>
                              <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số ngày nghỉ có hưởng lương %>" DataField="TOTAL_W_SALARY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_W_SALARY"
                                UniqueName="TOTAL_W_SALARY">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số ngày nghỉ không hưởng lương %>" DataField="TOTAL_W_NOSALARY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_W_NOSALARY"
                                UniqueName="TOTAL_W_NOSALARY">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đi trễ (phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Về sớm (phút) %>" DataField="COMEBACKOUT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="COMEBACKOUT"
                                UniqueName="COMEBACKOUT">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tỉ lệ WOS %>" DataField="WORKING_ADD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_ADD"
                                UniqueName="WORKING_ADD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT ngày thường %>" DataField="TOTAL_FACTOR1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR1"
                                UniqueName="TOTAL_FACTOR1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT đêm ngày thường %>" DataField="TOTAL_FACTOR1_5"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR1_5"
                                UniqueName="TOTAL_FACTOR1_5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT ngày nghỉ %>" DataField="TOTAL_FACTOR2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR2"
                                UniqueName="TOTAL_FACTOR2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT đêm ngày nghỉ%>" DataField="TOTAL_FACTOR2_7"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR2_7"
                                UniqueName="TOTAL_FACTOR2_7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT ngày lễ %>" DataField="TOTAL_FACTOR3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR3"
                                UniqueName="TOTAL_FACTOR3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT đêm ngày lễ %>" DataField="TOTAL_FACTOR3_9"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR3_9"
                                UniqueName="TOTAL_FACTOR3_9">
                            </tlk:GridNumericColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="DECISION_START"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DECISION_START" UniqueName="DECISION_START" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="DECISION_END"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DECISION_END" UniqueName="DECISION_END" />
                        </Columns>
                        <HeaderStyle Width="105px" />
                    </MasterTableView>
</tlk:RadGrid>
<script type="text/javascript">

    function popupclose(sender, args) {
        var m;
        var arg = args.get_argument();
        if (arg == '1') {
            m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
            var n = noty({ text: m, dismissQueue: true, type: 'success' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);

        }

    }

    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>
