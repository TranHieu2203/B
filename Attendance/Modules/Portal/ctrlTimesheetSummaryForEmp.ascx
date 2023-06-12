<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTimesheetSummaryForEmp.ascx.vb"
    Inherits="Attendance.ctrlTimesheetSummaryForEmp" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" >
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
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
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="150px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                         <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%>
                        </td>
                         <td>
                            <tlk:RadComboBox ID="cboOjbEmp" SkinID="dDropdownList" Width="150px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="" Enabled="false" >
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="" Enabled="false" >
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="" Enabled="false" >
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="2" />
                    </ClientSettings>
                    <HeaderStyle HorizontalAlign="Center" />
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" HeaderStyle-Width="150" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" HeaderStyle-Width="200" Visible="false" />
                            <tlk:GridNumericColumn HeaderText="Công chuẩn" DataField="PERIOD_STANDARD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="PERIOD_STANDARD"
                                UniqueName="PERIOD_STANDARD">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="Làm việc ngày thường (X)" DataField="WORKING_X"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="WORKING_X"
                                UniqueName="WORKING_X">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Công cơm" DataField="WORKING_MEAL"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_MEAL"
                                UniqueName="WORKING_MEAL">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Ngày nghỉ tuần" DataField="WORKING_F"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_F"
                                UniqueName="WORKING_F">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ lễ tết (LT)" DataField="WORKING_L"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_L"
                                UniqueName="WORKING_L">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ đi đường (DD)" DataField="WORKING_D"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_D"
                                UniqueName="WORKING_D">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ thai sản (TS)" DataField="WORKING_TS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_TS"
                                UniqueName="WORKING_TS">
                                <HeaderStyle Width="140px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ ốm (O)" DataField="WORKING_O"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_O"
                                UniqueName="WORKING_O">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ phép (P)" DataField="WORKING_P"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_P"
                                UniqueName="WORKING_P">
                                <HeaderStyle Width="140px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ bù (B)" DataField="WORKING_B"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_B"
                                UniqueName="WORKING_B">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ công tác (CT)" DataField="WORKING_C"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_C"
                                UniqueName="WORKING_C">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ việc riêng có hưởng lương (R)"
                                DataField="WORKING_E" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}"
                                SortExpression="WORKING_E" UniqueName="WORKING_E" Visible="false">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ đi học (DH)" DataField="WORKING_A"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_A"
                                UniqueName="WORKING_A">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ việc riêng (VR)" DataField="WORKING_V"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_V"
                                UniqueName="WORKING_V">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ biến chứng thai sản (BCTS)" DataField="WORKING_H"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_H"
                                UniqueName="WORKING_H">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ khám thai (KT)" DataField="WORKING_Q"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_Q"
                                UniqueName="WORKING_Q">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ vợ sinh (VS)" DataField="WORKING_N"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_N"
                                UniqueName="WORKING_N">
                                <HeaderStyle Width="140px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ kết hôn (KH)" DataField="WORKING_R"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_R"
                                UniqueName="WORKING_R">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ tang gia (TG)" DataField="WORKING_T"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_T"
                                UniqueName="WORKING_T">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Nghỉ không hưởng lương (NKL)" DataField="WORKING_K"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_K"
                                UniqueName="WORKING_K">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="Nghỉ có hưởng lương (NCL)" DataField="WORKING_J"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="WORKING_J"
                                UniqueName="WORKING_J">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="Tổng số ngày nghỉ có hưởng lương" DataField="TOTAL_W_SALARY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_W_SALARY"
                                UniqueName="TOTAL_W_SALARY">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="Tổng số ngày nghỉ không hưởng lương" DataField="TOTAL_W_NOSALARY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_W_NOSALARY"
                                UniqueName="TOTAL_W_NOSALARY">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Đi trễ (phút)" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Về sớm (phút)" DataField="COMEBACKOUT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="COMEBACKOUT"
                                UniqueName="COMEBACKOUT">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Tỉ lệ WOS" DataField="WORKING_ADD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n3}" SortExpression="WORKING_ADD"
                                UniqueName="WORKING_ADD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="OT ngày thường" DataField="TOTAL_FACTOR1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR1"
                                UniqueName="TOTAL_FACTOR1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="OT đêm ngày thường" DataField="TOTAL_FACTOR1_5"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR1_5"
                                UniqueName="TOTAL_FACTOR1_5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="OT ngày nghỉ" DataField="TOTAL_FACTOR2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR2"
                                UniqueName="TOTAL_FACTOR2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="OT đêm ngày nghỉ" DataField="TOTAL_FACTOR2_7"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR2_7"
                                UniqueName="TOTAL_FACTOR2_7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="OT ngày lễ" DataField="TOTAL_FACTOR3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR3"
                                UniqueName="TOTAL_FACTOR3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="OT đêm ngày lễ" DataField="TOTAL_FACTOR3_9"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="TOTAL_FACTOR3_9"
                                UniqueName="TOTAL_FACTOR3_9">
                            </tlk:GridNumericColumn>
                            <tlk:GridDateTimeColumn HeaderText="Từ ngày" DataField="DECISION_START"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DECISION_START" UniqueName="DECISION_START" />
                            <tlk:GridDateTimeColumn HeaderText="Đến ngày" DataField="DECISION_END"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DECISION_END" UniqueName="DECISION_END" />
                            <tlk:GridBoundColumn HeaderText="Đối tượng chấm công" DataField="OBJECT_ATTENDANCE_NAME"
                                SortExpression="OBJECT_ATTENDANCE_NAME" UniqueName="OBJECT_ATTENDANCE_NAME" />
                            <tlk:GridNumericColumn HeaderText="Số phút ở cơ quan" DataField="MIN_AT_WORK"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_AT_WORK"
                                UniqueName="MIN_AT_WORK">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút ngoài cơ quan" DataField="MIN_DEDUCT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_DEDUCT"
                                UniqueName="MIN_DEDUCT">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút giảm trừ do đi việc công" DataField="MIN_DEDUCT_FOR_WORK"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_DEDUCT_FOR_WORK"
                                UniqueName="MIN_DEDUCT_FOR_WORK">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút trên đơn đăng ký nghỉ" DataField="MIN_LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_LATE"
                                UniqueName="MIN_LATE">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút giảm trừ" DataField="MIN_LATE_SOON"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_LATE_SOON"
                                UniqueName="MIN_LATE_SOON">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút ngoài cơ quan sau khi giảm trừ" DataField="MIN_ON_LEAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_ON_LEAVE"
                                UniqueName="MIN_ON_LEAVE">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút đi trễ" DataField="MIN_OUT_AFTER_DEDUCT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_OUT_AFTER_DEDUCT"
                                UniqueName="MIN_OUT_AFTER_DEDUCT">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút về sớm" DataField="MIN_OUT_WORK"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_OUT_WORK"
                                UniqueName="MIN_OUT_WORK">
                                </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút đi trễ/về sớm" DataField="MIN_SOON"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_SOON"
                                UniqueName="MIN_SOON">
                                </tlk:GridNumericColumn>--%>
                        </Columns>
                        <HeaderStyle Width="105px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
        </tlk:RadPane>
    </tlk:RadSplitter>
<script type="text/javascript">var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
       var winH;
        var winW;

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlTimesheetSummaryForEmp_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlTimesheetSummaryForEmp_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTimesheetSummaryForEmp_MainPane").stop().animate({ height: winH, width: winW }, 0);
           $("#ctl00_MainContent_ctrlTimesheetSummaryForEmp_rgData").stop().animate({ height: winH-80, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTimesheetSummaryForEmp_RadPane2").stop().animate({ height: winH, width: winW }, 0);
            
            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
</script>
