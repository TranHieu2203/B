<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveDisplay.ascx.vb"
    Inherits="Attendance.ctrlLeaveDisplay" %>
<asp:HiddenField ID="hidStartDate" runat="server" />
<asp:HiddenField ID="hidEndDate" runat="server" />
<tlk:RadSplitter runat="server" ID="RadSplitter2" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="RadPane5" Scrolling="None" Height="390px">
        <tlk:RadTabStrip runat="server" ID="tabTypeApprove" SelectedIndex="0" MultiPageID="pageTypeApprove">
            <Tabs>
                <tlk:RadTab Text='<%$ Translate: Theo ngày %>' PageViewID="RadPageView1" Selected="true">
                </tlk:RadTab>
                <tlk:RadTab Text='<%$ Translate: Theo tháng %>' PageViewID="RadPageView2">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage runat="server" ID="pageTypeApprove">
            <tlk:RadPageView ID="RadPageView1" runat="server" Selected="true" Width="100%">
                <div style="display: block; padding: 5px">
                    <%# Translate("Chọn ngày:")%>
                    <tlk:RadDatePicker runat="server" ID="rdDate" AutoPostBack="true" DateInput-EmptyMessage="<%$ Translate: Ngày tra cứu %>">
                    </tlk:RadDatePicker>
                </div>
                <tlk:RadGrid PageSize=50 runat="server" ID="rgGrid" Width="99%" Height="330px">
                    <MasterTableView>
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên nhân viên %>' DataField="FULLNAME_VN">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Loại đăng ký %>' DataField="NAME_VN">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Chi tiết đăng ký %>' DataField="NVALUE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="NOTE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Trạng thái %>' DataField="STATUS_NAME">
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
            <tlk:RadPageView ID="RadPageView2" runat="server" Height="350px">
                <div style="display: block; padding: 5px">
                    <%# Translate("Chọn nhân viên:")%>
                    <tlk:RadComboBox runat="server" ID="cboEmployee" Width="300px" AutoPostBack="true"
                        DateInput-EmptyMessage="<%$ Translate: Nhân viên tra cứu %>">
                    </tlk:RadComboBox>
                </div>
                <div style="vertical-align: middle; height: 30px; padding-top: 5px">
                    <%# Translate("Điều kiện lọc : ") %>
                    <tlk:RadButton runat="server" ID="chkWaitForApprove" Text="<%$ Translate: Chờ phê duyệt %>"
                        Checked="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="1" ForeColor="DarkOrange">
                    </tlk:RadButton>
                    <tlk:RadButton runat="server" ID="chkApproved" Text="<%$ Translate: Đã phê duyệt %>"
                        Checked="true" ButtonType="ToggleButton" ToggleType="CheckBox" Value="2" ForeColor="Blue">
                    </tlk:RadButton>
                </div>
                <tlk:RadScheduler runat="server" ID="sdlRegister" Height="295" SelectedView="MonthView"
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
                </tlk:RadScheduler>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
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
</tlk:RadScriptBlock>
