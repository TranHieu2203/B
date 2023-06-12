<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMCaLamViec.ascx.vb"
    Inherits="Attendance.ctrlDMCaLamViec" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<style type="text/css">
    @media screen and (-webkit-min-device-pixel-ratio:0)
    {
        #ctl00_MainContent_ctrlDMCaLamViec_txtNote
        {
            height: 58px;
        }
    }
    
    #ctl00_MainContent_ctrlDMCaLamViec_cboMaCong_DropDown .rcbWidth, #ctl00_MainContent_ctrlDMCaLamViec_cboSunDay_DropDown .rcbWidth, #ctl00_MainContent_ctrlDMCaLamViec_cboSaturday_DropDown .rcbWidth
    {
        width: 178px !important;
    }
    .style_checkbox
    {
        padding-right: 10px;
    }
    .RadGrid input
    {
        background: none !important;
        border: none;
        padding: 0 !important;
        font-family: arial, sans-serif;
        color: #069;
        text-decoration: underline;
        cursor: pointer;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="320px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã ca làm việc đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã ca làm việc đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã ca làm việc. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên ca làm việc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên ca làm việc. %>"></asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCongTy" runat="server" Width="180px" Style="display: none">
                    </tlk:RadComboBox>
                    <tlk:RadTextBox runat="server" ID="txtOrgName2" ReadOnly="true" Width="130px" SkinID="Readonly" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giờ bắt đầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHours_Start" AutoPostBack="true">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdHours_Start"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giờ kết thúc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHours_Stop" AutoPostBack="true">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rdHours_Stop"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc. %>"></asp:RequiredFieldValidator>
                    <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="rdHours_Start"
                        ControlToValidate="rdHours_Stop" Operator="GreaterThan"
                        ErrorMessage="<%$ Translate: Thiết lập giờ cho ca làm việc không hợp lệ %>"
                        ToolTip="<%$ Translate: Thiết lập giờ cho ca làm việc không hợp lệ %>">
                    </asp:CompareValidator>--%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkIS_HOURS_STOP" runat="server" />
                </td>
                <td>
                    <%# Translate("Qua ngày hôm sau")%>
                </td>
                <td class="lb">
                    <%# Translate("Ngày công ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboNgayCongCa" runat="server" Width="180px">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="cboNgayCongCa"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ngày công ca. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giờ đi trễ cho phép")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdLateHour">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt" runat="server">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="rdLateHour"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Giờ đi trễ cho phép. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giờ về sớm cho phép")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdEarlyHour">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="rdEarlyHour"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Giờ về sớm cho phép. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkIsTomorrow" runat="server" />
                </td>
                <td>
                    <%# Translate("Qua ngày hôm sau")%>
                </td>
                <td class="lb">
                    <%# Translate("Giờ công ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnShiftHour" runat="server" SkinID="Decimal" Width="180px">
                        <NumberFormat DecimalSeparator="," AllowRounding="False" KeepNotRoundedValue="True" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="rnShiftHour"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ công ca. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giờ OT tối thiểu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOTHourMin" runat="server" Width="72%">
                    </tlk:RadNumericTextBox>
                    Phút
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="rnOTHourMin"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Giờ OT tối thiểu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giờ OT tối đa")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOTHourMax" runat="server" Width="72%">
                    </tlk:RadNumericTextBox>
                    Phút
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="rnOTHourMax"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Giờ OT tối đa. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bắt đầu nghỉ giữa ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdSTART_MID_HOURS">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdSTART_MID_HOURS"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu nghỉ giữa ca. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Kết thúc nghỉ giữa ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdEND_MID_HOURS">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEND_MID_HOURS"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc nghỉ giữa ca. %>"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="rdSTART_MID_HOURS"
                        ControlToValidate="rdEND_MID_HOURS" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Thiết lập giờ nghỉ giữa ca không hợp lệ %>"
                        ToolTip="<%$ Translate: Thiết lập giờ nghỉ giữa ca không hợp lệ %>">
                    </asp:CompareValidator>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkIS_MID_END" runat="server" />
                </td>
                <td>
                    <%# Translate("Qua ngày hôm sau")%>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bắt đầu nhận quẹt thẻ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHOURS_STAR_CHECKIN">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rdHOURS_STAR_CHECKIN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu nhận quẹt thẻ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Kết thúc nhận quẹt thẻ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHOURS_STAR_CHECKOUT">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rdHOURS_STAR_CHECKOUT"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc nhận quẹt thẻ. %>">
                    </asp:RequiredFieldValidator>
                    <%--  <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="rdHOURS_STAR_CHECKIN"
                        ControlToValidate="rdHOURS_STAR_CHECKOUT" Operator="GreaterThan" ErrorMessage="<%$ Translate: Thiết lập giờ quẹt thẻ không hợp lệ %>"
                        ToolTip="<%$ Translate: Thiết lập giờ quẹt thẻ không hợp lệ %>">
                    </asp:CompareValidator>--%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkIS_HOURS_CHECKOUT" runat="server" />
                </td>
                <td>
                    <%# Translate("Qua ngày hôm sau")%>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="7">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="10">
                    <asp:Label runat="server" ID="lbIsLeave" Text="Ca thứ 7"></asp:Label>
                    <asp:CheckBox ID="chkSaturdayShift" CssClass="style_checkbox" runat="server" />
                    <asp:Label runat="server" ID="lbIS_REG_SHIFT" Text="Ca CN"></asp:Label>
                    <asp:CheckBox ID="chkSundayShift" CssClass="style_checkbox" runat="server" Checked="false" />
                    <asp:Label runat="server" ID="lbIsCalHoliday" Text="Ca lễ"></asp:Label>
                    <asp:CheckBox ID="chkHolydayShift" CssClass="style_checkbox" runat="server" />
                    <asp:Label runat="server" ID="lbMorningShift" Text="Ca sáng "></asp:Label>
                    <asp:CheckBox ID="chkMorningShift" CssClass="style_checkbox" runat="server" />
                    <asp:Label runat="server" ID="lbAfternoonShift" Text="Ca chiều "></asp:Label>
                    <asp:CheckBox ID="chkAfternoonShift" CssClass="style_checkbox" runat="server" />
                    <asp:Label runat="server" ID="lbMiddleShift" Text="Ca giữa "></asp:Label>
                    <asp:CheckBox ID="chkMiddleShift" CssClass="style_checkbox" runat="server" />
                    <asp:Label runat="server" ID="lbBrokenShift" Text="Ca gãy "></asp:Label>
                    <asp:CheckBox ID="chkBrokenShift" CssClass="style_checkbox" runat="server" />
                    <asp:Label runat="server" ID="lbTimeShift" Text="Ca thời gian "></asp:Label>
                    <asp:CheckBox ID="chkTimeShift" CssClass="style_checkbox" runat="server" />
                    <asp:Label runat="server" ID="lbSupportCHShift" Text="Ca hỗ trợ CH "></asp:Label>
                    <asp:CheckBox ID="chkSupportCHShift" CssClass="style_checkbox" runat="server" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgDanhMuc" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME_VN,NAME_EN,MANUAL_NAME,MANUAL_ID,HOURS_START,HOURS_STOP,NOTE,SUNDAY,IS_NOON,SATURDAY,MINHOUSER,ORG_ID,SHIFT_DAY,
                START_MID_HOURS,END_MID_HOURS,HOURS_STAR_CHECKIN,HOURS_STAR_CHECKOUT,IS_HOURS_STOP,IS_HOURS_CHECKOUT,IS_MID_END,ORG_NAME,LATE_HOUR,EARLY_HOUR,IS_TOMORROW,
                SHIFT_HOUR,OT_HOUR_MIN,OT_HOUR_MAX,IS_HOLYDAY_SHIFT,IS_MORNING_SHIFT,IS_AFTERNOON_SHIFT,IS_MIDDLE_SHIFT,IS_BROKEN_SHIFT,IS_TIME_SHIFT,IS_CH_SUPPORT_SHIFT,IS_SATURDAY_SHIFT,IS_SUNDAY_SHIFT">
                <Columns>
                    <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ca %>" DataField="CODE" UniqueName="CODE"
                        SortExpression="CODE" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ca  %>" DataField="NAME_VN" UniqueName="NAME_VN"
                        SortExpression="NAME_VN">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="130px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã công %>" DataField="MANUAL_CODE"
                        UniqueName="MANUAL_CODE" SortExpression="MANUAL_CODE" HeaderStyle-Width="100px" />
                    <tlk:GridButtonColumn HeaderText="<%$ Translate: Danh sách phòng ban %>" ButtonType="PushButton" Text="Danh sách" UniqueName="DETAIL"
                               CommandName="DETAIL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px"  />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_ID" SortExpression="ORG_ID" HeaderStyle-Width="200px" Visible="false" />
                    
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày công ca %>" DataField="SHIFT_DAY"
                        UniqueName="SHIFT_DAY" SortExpression="SHIFT_DAY" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả mã công %>" DataField="MANUAL_NAME"
                        UniqueName="MANUAL_NAME" SortExpression="MANUAL_NAME" HeaderStyle-Width="200px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ bắt đầu %>" DataField="HOURS_START"
                        UniqueName="HOURS_START" DataFormatString="{0:HH:mm}" SortExpression="HOURS_START" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ kết thúc %>" DataField="HOURS_STOP"
                        UniqueName="HOURS_STOP" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STOP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Qua ngày hôm sau %>" DataField="IS_HOURS_STOP_NAME"
                        UniqueName="IS_HOURS_STOP_NAME" SortExpression="IS_HOURS_STOP_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Bắt đầu nghỉ giữa ca %>" DataField="START_MID_HOURS"
                        UniqueName="START_MID_HOURS" DataFormatString="{0:HH:mm}" SortExpression="START_MID_HOURS" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Kết thúc nghỉ giữa ca %>" DataField="END_MID_HOURS"
                        UniqueName="END_MID_HOURS" DataFormatString="{0:HH:mm}" SortExpression="END_MID_HOURS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Qua ngày hôm sau %>" DataField="IS_MID_END_NAME"
                        UniqueName="IS_MID_END_NAME" SortExpression="IS_MID_END_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Bắt đầu nhận quyẹt thẻ %>" DataField="HOURS_STAR_CHECKIN"
                        UniqueName="HOURS_STAR_CHECKIN" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STAR_CHECKIN" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Kết thúc nhận quyẹt thẻ %>" DataField="HOURS_STAR_CHECKOUT"
                        UniqueName="HOURS_STAR_CHECKOUT" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STAR_CHECKOUT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Qua ngày hôm sau %>" DataField="IS_HOURS_CHECKOUT_NAME"
                        UniqueName="IS_HOURS_CHECKOUT_NAME" SortExpression="IS_HOURS_CHECKOUT_NAME" HeaderStyle-Width="100px" />

                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ đi trễ cho phép %>" DataField="LATE_HOUR"
                        UniqueName="LATE_HOUR" DataFormatString="{0:HH:mm}" SortExpression="LATE_HOUR" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ về sớm cho phép %>" DataField="EARLY_HOUR"
                        UniqueName="EARLY_HOUR" DataFormatString="{0:HH:mm}" SortExpression="EARLY_HOUR" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Qua ngày hôm sau( đi trễ, về sớm) %>" DataField="IS_TOMORROW_NAME"
                        UniqueName="IS_TOMORROW_NAME" SortExpression="IS_TOMORROW_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giờ OT tối thiểu %>" DataField="OT_HOUR_MIN"
                        UniqueName="OT_HOUR_MIN" SortExpression="OT_HOUR_MIN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giờ OT tối đa %>" DataField="OT_HOUR_MAX"
                        UniqueName="OT_HOUR_MAX" SortExpression="OT_HOUR_MAX" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca thứ 7 %>" DataField="IS_SATURDAY_SHIFT_NAME"
                        UniqueName="IS_SATURDAY_SHIFT_NAME" SortExpression="IS_SATURDAY_SHIFT_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca chủ nhật %>" DataField="IS_SUNDAY_SHIFT_NAME"
                        UniqueName="IS_SUNDAY_SHIFT_NAME" SortExpression="IS_SUNDAY_SHIFT_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca lễ %>" DataField="IS_HOLYDAY_SHIFT_NAME"
                        UniqueName="IS_HOLYDAY_SHIFT_NAME" SortExpression="IS_HOLYDAY_SHIFT_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca sáng %>" DataField="IS_MORNING_SHIFT_NAME"
                        UniqueName="IS_MORNING_SHIFT_NAME" SortExpression="IS_MORNING_SHIFT_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca chiều %>" DataField="IS_AFTERNOON_SHIFT_NAME"
                        UniqueName="IS_AFTERNOON_SHIFT_NAME" SortExpression="IS_AFTERNOON_SHIFT_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca giữa %>" DataField="IS_MIDDLE_SHIFT_NAME"
                        UniqueName="IS_MIDDLE_SHIFT_NAME" SortExpression="IS_MIDDLE_SHIFT_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca gãy %>" DataField="IS_BROKEN_SHIFT_NAME"
                        UniqueName="IS_BROKEN_SHIFT_NAME" SortExpression="IS_BROKEN_SHIFT_NAME" HeaderStyle-Width="100px" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca thời gian %>" DataField="IS_TIME_SHIFT_NAME"
                        UniqueName="IS_TIME_SHIFT_NAME" SortExpression="IS_TIME_SHIFT_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca hỗ trợ %>" DataField="IS_CH_SUPPORT_SHIFT_NAME"
                        UniqueName="IS_CH_SUPPORT_SHIFT_NAME" SortExpression="IS_CH_SUPPORT_SHIFT_NAME" HeaderStyle-Width="100px" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giờ công ca %>" DataField="SHIFT_HOUR"
                        UniqueName="SHIFT_HOUR" SortExpression="SHIFT_HOUR" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />--%>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <%--<ClientEvents OnCommand="ValidateFilter" />--%>
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlDMCaLamViec_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMCaLamViec_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMCaLamViec_RadPane2';
        var validateID = 'MainContent_ctrlDMCaLamViec_valSum';
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

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT_TEMP') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EXPORT") {
                var grid = $find("<%=rgDanhMuc.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }

            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
        }
    </script>
</tlk:RadCodeBlock>
