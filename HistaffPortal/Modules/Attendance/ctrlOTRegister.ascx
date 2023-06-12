<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOTRegister.ascx.vb"
    Inherits="Attendance.ctrlOTRegister" %>
<asp:HiddenField ID="hifStartDate" runat="server" />
<asp:HiddenField ID="hifEndDate" runat="server" />
<tlk:RadSplitter runat="server" ID="splitFull" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="pnlRegistInfo" Height="100px" Visible="true">
        <div style="vertical-align: middle; height: 65px; padding-top: 5px">
            <table class="table-form">
            <tr> 
     			<td colspan="12">
                    <tlk:RadTextBox  ID="rntxtOT_USED" runat="server" BorderWidth="0" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>		
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Từ ngày ")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdFromDate">
                    </tlk:RadDatePicker>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdToDate">
                    </tlk:RadDatePicker>
                </td>

            </tr>
			<tr>
			     <td style="text-align: left" class="lb">
                    <%# Translate("Từ giờ")%>
                </td>
                <td>
                   <%-- <tlk:RadTimePicker ID="rtpFROM" runat="server">
                    </tlk:RadTimePicker>--%>
                    <tlk:RadComboBox runat="server" ID="cboHoursFrom"></tlk:RadComboBox>
                    <tlk:RadComboBox runat="server" ID="cboMinuteFrom"></tlk:RadComboBox>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("đến giờ")%>
                </td>
                <td>
                    <%--<tlk:RadTimePicker ID="rtpTO" runat="server">
                    </tlk:RadTimePicker>--%>
                    <tlk:RadComboBox runat="server" ID="cboHoursTo"></tlk:RadComboBox>
                    <tlk:RadComboBox runat="server" ID="cboMinuteTo"></tlk:RadComboBox>
                </td>
                <td> <asp:CheckBox runat="server" ID="chkNB" Text=<%# Translate("Quy đổi nghỉ bù")%> /></td>
			</tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Lý do")%>
                </td>
                <td colspan="8">
                    <tlk:RadTextBox runat="server" ID="txtReason" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnRegister" runat="server" Text="<%$ Translate: Đăng ký %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
        </div>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane1" Height="30px" Visible="true" Scrolling="None">
        <div style="vertical-align: middle; height: 35px; padding-top: 5px">
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
        </div>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="pnlSchedule" Scrolling="None" Visible="true" Height="530px">
        <tlk:RadScheduler runat="server" ID="sdlRegister" Width="100%" Height="100%" SelectedView="MonthView"
            AllowInsert="false" AllowEdit="false" AllowDelete="false" FirstDayOfWeek="Monday"
            LastDayOfWeek="Sunday" StartInsertingInAdvancedForm="false" DataKeyField="ID"
            DataSubjectField="SUBJECT" DataStartField="WORKINGDAY" DataEndField="WORKINGDAY"
            DataRecurrenceField="RecurrenceRule" DataRecurrenceParentKeyField="RecurrenceParentID"
            ShowFullTime="true" WorkDayStartTime="00:00:00" WorkDayEndTime="23:59:59" OnClientTimeSlotContextMenuItemClicking="OnClientTimeSlotContextMenuItemClicking"
            OnClientAppointmentMoveStart="OnClientAppointmentMoveStart">
            <Localization HeaderToday="Today" />
            <AdvancedForm Modal="true" EnableResourceEditing="False" />
            <DayView UserSelectable="false" />
            <MonthView MinimumRowHeight="3" HeaderDateFormat="MMMM, yyyy" ColumnHeaderDateFormat="dddd" FirstDayHeaderDateFormat="dd MMMM">
            </MonthView>
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
                var hifStart = $("#<%= hifStartDate.ClientID %>");
                var hifEnd = $("#<%= hifEndDate.ClientID%>");
                hifStart.val(firstSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
                hifEnd.val(lastSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
            }
        }
        function OnClientAppointmentMoveStart(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
    </script>
</tlk:RadCodeBlock>
