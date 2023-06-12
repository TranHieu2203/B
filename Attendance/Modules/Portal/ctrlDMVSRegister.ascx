<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMVSRegister.ascx.vb"
    Inherits="Attendance.ctrlDMVSRegister" %>
<asp:HiddenField ID="hidStartDate" runat="server" />
<asp:HiddenField ID="hidEndDate" runat="server" />
<tlk:RadSplitter runat="server" ID="splitFull" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="pnlRegistInfo" Height="75px" Visible="true">
        <div style="vertical-align: middle; height: 70px; padding-top: 5px">
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Loại đi muộn về sớm")%>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" ID="cboleaveType" DataTextField="NAME_VN" DataValueField="ID">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Thời gian đăng ký")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" Width="120px" ID="rntxtWLEOValue">
                        </tlk:RadNumericTextBox>
                        <%# Translate("(Phút)") %>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Lý do") %>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox runat="server" ID="txtReason" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
        </div>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane1" Height="45px" Visible="true" Scrolling="None">
        <div style="vertical-align: middle; height: 40px; padding-top: 5px">
            <%# Translate("Điều kiện lọc : ") %>
            <tlk:RadButton runat="server" ID="chkRegister" Text="<%$ Translate: Đăng ký %>" Checked="true"
                ButtonType="ToggleButton" ToggleType="CheckBox" Value="0" ForeColor="Green">
            </tlk:RadButton>
            <tlk:RadButton runat="server" ID="chkWaitForApprove" Text="<%$ Translate: Chờ phê duyệt %>"
                Checked="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="1" ForeColor="DarkOrange">
            </tlk:RadButton>
            <tlk:RadButton runat="server" ID="chkApproved" Text="<%$ Translate: Đã phê duyệt %>"
                Checked="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="2" ForeColor="Blue">
            </tlk:RadButton>
            <tlk:RadButton runat="server" ID="chkDenied" Text="<%$ Translate: Từ chối %>" Checked="true"
                ButtonType="ToggleButton" ToggleType="CheckBox" Value="3" ForeColor="Red">
            </tlk:RadButton>
            <br />
            <b style="color: Red;">Chú ý: </b>
            <b>Chuột phải để đăng ký, gửi duyệt và xóa đăng ký nghỉ</b>
        </div>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="pnlSchedule" Scrolling="None" Visible="true" Height="530px">
        <tlk:RadScheduler runat="server" ID="sdlRegister" Height="100%" SelectedView="MonthView"
            AllowInsert="false" AllowEdit="false" AllowDelete="false" FirstDayOfWeek="Monday"
            LastDayOfWeek="Sunday" StartInsertingInAdvancedForm="false" DataKeyField="ID"
            DataSubjectField="SUBJECT" DataStartField="WORKINGDAY" DataEndField="WORKINGDAY"
            DataRecurrenceField="RecurrenceRule" DataRecurrenceParentKeyField="RecurrenceParentID"
            ShowFullTime="true" WorkDayStartTime="00:00:00" WorkDayEndTime="23:59:59" OnClientTimeSlotContextMenuItemClicking="OnClientTimeSlotContextMenuItemClicking"
            OnClientAppointmentMoveStart="OnClientAppointmentMoveStart">
            <Localization HeaderToday="Today" />
            <AdvancedForm Modal="true" EnableResourceEditing="False" />
            <DayView UserSelectable="false" />
            <MonthView MinimumRowHeight="3" HeaderDateFormat="MMMM, yyyy" ColumnHeaderDateFormat="dddd"
                FirstDayHeaderDateFormat="dd MMMM"></MonthView>
            <WeekView UserSelectable="false" />
            <TimelineView UserSelectable="false" />
            <AppointmentTemplate>
                <%# Eval("SUBJECT")%>
            </AppointmentTemplate>
            <AppointmentContextMenus>
                <tlk:RadSchedulerContextMenu ID="rscmMenuAppointment" runat="server">
                    <Items>
                        <tlk:RadMenuItem Value="SENDAPPROVE" Text="<%$ Translate:Gửi duyệt %>" />
                        <tlk:RadMenuItem IsSeparator="true" />
                        <tlk:RadMenuItem Value="DEL" Text="<%$ Translate:Xóa %>" />
                    </Items>
                </tlk:RadSchedulerContextMenu>
            </AppointmentContextMenus>
            <TimeSlotContextMenus>
                <tlk:RadSchedulerContextMenu ID="RadSchedulerContextMenu1" runat="server">
                    <Items>
                        <tlk:RadMenuItem Value="ADD" Text="<%$ Translate:Đăng ký %>" />
                        <tlk:RadMenuItem Value="SENDAPPROVEINDATE" Text="<%$ Translate:Gửi duyệt %>" />
                        <tlk:RadMenuItem IsSeparator="true" />
                        <tlk:RadMenuItem Value="DELINDATE" Text="<%$ Translate:Xóa trong ngày %>" />
                    </Items>
                </tlk:RadSchedulerContextMenu>
            </TimeSlotContextMenus>
        </tlk:RadScheduler>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OnClientTimeSlotContextMenuItemClicking(sender, args) {
            if (sender.get_selectedSlots().length > 0) {
                var firstSlotFromSelection = sender.get_selectedSlots()[0];
                var lastSlotFromSelection = sender.get_selectedSlots()[sender.get_selectedSlots().length - 1];
                var hifStart = $("#<%= hidStartDate.ClientID %>");
                var hifEnd = $("#<%= hidEndDate.ClientID%>");
                hifStart.val(firstSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
                hifEnd.val(lastSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
            }
        }
        function OnClientAppointmentMoveStart(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
    </script>
</tlk:RadCodeBlock>
