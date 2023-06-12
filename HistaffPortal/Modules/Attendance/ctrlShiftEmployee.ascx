<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlShiftEmployee.ascx.vb"
    Inherits="Attendance.ctrlShiftEmployee" %>
<asp:HiddenField ID="hidStartDate" runat="server" />
<asp:HiddenField ID="hidEndDate" runat="server" />
<style>
    .Holidays {
        background: #ff0000 !important;
    }

    .Compensatory, .OffDay {
        background: #ffff00 !important;
    }
</style>
<tlk:RadSplitter runat="server" ID="splitFull" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane ID="TopPanel" runat="server" MinHeight="130" Height="55px" Scrolling="None">
        <table class="table-form" style="padding-top: 8px;float:right;">
            <tr>
                <td style="width: 60px; text-align: right">
                    <%# Translate("Chú thích :")%>
                </td>
                <td style="width: 70px;">
                    <p style="border: 1px solid #000000; background-color: #ff0000; height: 17px; margin: 0 auto;"></p>
                </td>
                <td style="text-align: left">
                    <%# Translate("Ngày lễ, tết")%>
                </td>
                <td style="width: 15px"></td>
                <td style="width: 70px;">
                    <p style="border: 1px solid #000000; background-color: #ffff00; height: 17px; margin: 0 auto;"></p>
                </td>
                <td style="text-align: left">
                    <%# Translate("Ngày thứ 7, CN")%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="pnlSchedule" Scrolling="None" Visible="true" Height="450px">
        <tlk:RadScheduler runat="server" ID="sdlRegister" Height="100%" SelectedView="MonthView"
            AllowInsert="false" AllowEdit="false" AllowDelete="false" FirstDayOfWeek="Monday"
            LastDayOfWeek="Sunday" StartInsertingInAdvancedForm="false" DataKeyField="EFFECTIVEDATE"
            DataSubjectField="SUBJECT" DataStartField="EFFECTIVEDATE" DataEndField="EFFECTIVEDATE"
            DataRecurrenceField="RecurrenceRule" OnAppointmentDataBound="sdlRegister_AppointmentDataBound"
            DataRecurrenceParentKeyField="RecurrenceParentID" ShowFullTime="true" WorkDayStartTime="00:00:00"
            WorkDayEndTime="23:59:59" OnClientTimeSlotContextMenuItemClicking="OnClientTimeSlotContextMenuItemClicking" CssClass="center"
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
