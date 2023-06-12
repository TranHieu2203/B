<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramClassSchedule.ascx.vb"
    Inherits="Training.ctrlTR_ProgramClassSchedule" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidStartTime" runat="server" />
<asp:HiddenField ID="hidEndTime" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="190px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidClassID" />
         <table class="table-form">
             <tr>
                <td class="item-head" colspan="4">     
                        <%# Translate("Thông tin lớp đào tạo")%>
                    <hr />
                </td>
                <td class="item-head" colspan="6">     
                        <%# Translate("Thông tin lịch đào tạo")%>
                    <hr />
                </td>
            </tr>
             <tr>
                 <td class="lb">
                    <%# Translate("Tên lớp học")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtClassName" runat="server" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpFromDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                 <td class="lb">
                 </td>
                 <td></td>
             </tr>
             <tr>
                <td class="lb">
                    <%# Translate("Thời gian học từ")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdClassStart" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdClassEnd" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                 <td class="lb">
                    <%# Translate("Từ giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="dtpFromTime" runat="server" AutoPostBack="true">
                        <DateInput ID="diFrom" runat="server" DateFormat="HH:mm">
                        </DateInput>
                    </tlk:RadTimePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="dtpToTime" runat="server"  AutoPostBack="true">
                        <DateInput ID="diTo" runat="server" DateFormat="HH:mm" >
                        </DateInput>
                    </tlk:RadTimePicker>
                </td>

                    <td class="lb">
                    <%# Translate("Số giờ học")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox MinValue="0" MaxValue="999999999" ID="txtTOTAL_TIME" runat="server">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                    </tlk:RadNumericTextBox>
                            </td>

                <td>
                    <tlk:RadButton ID="btnRegist" runat="server" Text="<%$ Translate: Thiết lập%>" CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>
             <tr>
                  <td class="lb">
                  </td>
                 <td></td>
                 <td class="lb">
                  </td>
                 <td></td>
                <td class="lb">
                    <%# Translate("Nội dung")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtContent" Width="100%">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                  </td>
                 <td></td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadScheduler runat="server" ID="sdlRegister" Width="100%" Height="100%" SelectedView="MonthView"
            AllowInsert="false" AllowEdit="false" AllowDelete="false" FirstDayOfWeek="Monday"
            LastDayOfWeek="Sunday" StartInsertingInAdvancedForm="false" DataKeyField="ID"
            DataSubjectField="SUBJECT" DataStartField="START_TIME" DataEndField="END_TIME"
            ShowFullTime="true" WorkDayStartTime="00:00:00" WorkDayEndTime="23:59:59" OnClientTimeSlotContextMenuItemClicking="OnClientTimeSlotContextMenuItemClicking"
            OnClientAppointmentMoveStart="OnClientAppointmentMoveStart" Culture="vi-VN">
            <Localization HeaderToday="Today" />
            <AdvancedForm Modal="true" EnableResourceEditing="False" />
            <MonthView HeaderDateFormat="MMMM, yyyy" ColumnHeaderDateFormat="dddd" FirstDayHeaderDateFormat="dd MMMM">
            </MonthView>
            <WeekView UserSelectable="false" />
            <DayView UserSelectable="false" />
            <TimelineView UserSelectable="false" />
            <AppointmentTemplate>
                <%# Eval("SUBJECT")%>
            </AppointmentTemplate>
            <AppointmentContextMenus>
                <tlk:RadSchedulerContextMenu ID="rscmMenuAppointment" runat="server">
                    <Items>
                        <tlk:RadMenuItem Value="DEL" Text="<%$ Translate:Xóa %>" />
                    </Items>
                </tlk:RadSchedulerContextMenu>
            </AppointmentContextMenus>
            <TimeSlotContextMenus>
                <tlk:RadSchedulerContextMenu ID="RadSchedulerContextMenu1" runat="server">
                    <Items>
                        <%--<tlk:RadMenuItem Value="DELINDATE" Text="<%$ Translate:Xóa %>" />--%>
                    </Items>
                </tlk:RadSchedulerContextMenu>
            </TimeSlotContextMenus>
        </tlk:RadScheduler>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindPrepare" runat="server"></asp:PlaceHolder>
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
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
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

        function OnClientTimeSlotContextMenuItemClicking(sender, args) {
            if (sender.get_selectedSlots().length > 0) {
                var firstSlotFromSelection = sender.get_selectedSlots()[0];
                var lastSlotFromSelection = sender.get_selectedSlots()[sender.get_selectedSlots().length - 1];
                var hidStart = $("#<%= hidStartTime.ClientID %>");
                var hidEnd = $("#<%= hidEndTime.ClientID%>");
                hidStart.val(firstSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
                hidEnd.val(lastSlotFromSelection.get_startTime().format('dd/MM/yyyy'));
            }
        }
        function OnClientAppointmentMoveStart(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }

        function pageLoad() {
            var scheduler = $find('<%= sdlRegister.ClientID %>');
            var scrolledIntoViewSlot = $telerik.getElementByClassName(scheduler.get_element(), "FirstLaunch", "td");
            if (scrolledIntoViewSlot)
                setTimeout(function () { scrolledIntoViewSlot.scrollIntoView(); }, 200);
        }
    </script>
</tlk:RadCodeBlock>
