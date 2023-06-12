<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTimeTimesheet_machine.ascx.vb"
    Inherits="Attendance.ctrlTimeTimesheet_machine" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="120px" Scrolling="None">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                       <tr>
                        <td>
                            <asp:Label ID="lblProgram" runat="server" Font-Size="Large" Font-Bold="true">&nbsp;</asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:Label ID="lblRequest" runat="server" Font-Size="Large" Text ="..." Font-Bold="true" Visible="false">&nbsp;</asp:Label>
                        </td>
                         <td class="lb" style="padding-right: 5px; padding-left: 20px">
                            <%# Translate("CM_CTRLPROCESS_SCAN_TIME")%>&nbsp;
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="RadNumTB" MinValue="1" MaxValue="30" Width="50px"
                                Value="5" MaxLength="2" AutoPostBack="true" ShowSpinButtons="True">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:Label ID="lblStatus" runat="server" Text ="..."  Font-Size="Large" CssClass="spancustomstatus"></asp:Label>

                         </td>
                           <td class="lb">
                               <%# Translate("Nhân viên")%>
                           </td>
                          <td >
                                <tlk:RadTextBox ID="rtEmployee_Code" MaxLength="50" runat="server"
                                    ToolTip="Nhập mã hoặc tên nhân viên" onkeydown="clickHere(event)">
                                </tlk:RadTextBox>
                          </td>
                    </tr>
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
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                        <td style="text-align: right;width:164px">
                            <%# Translate("Đối tượng nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboEmpObj" runat="server"  AutoPostBack="true"
                                >
                            </tlk:RadComboBox>
                        </td>                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td> 
                        <td class="lb">
                            <asp:Label  runat ="server" ID="lbFilter" Text ="Dữ liệu phát sinh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox  ID="cbFilter" SkinID="dDropdownList" Width="160px"
                               RenderMode="Lightweight" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" >
                               <Items>
                                    <tlk:RadComboBoxItem Text="Đi trễ" Value ="1" />
                                    <tlk:RadComboBoxItem Text="Về sớm" Value ="2" />
                                    <tlk:RadComboBoxItem Text="Thiếu quẹt thẻ" Value ="3" />
                                    <tlk:RadComboBoxItem Text="Không quẹt thẻ" Value ="4" />
                                    <tlk:RadComboBoxItem Text="Sai ca, chưa đủ công" Value ="5" />
                                </Items>
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <asp:CheckBox ID ="ckReset" runat="server"  Text ="Cập nhật mới lại toàn bộ dữ liệu" Visible="false" />
                        </td>
                    </tr>
                    <tr> 
                        <td class="lb"></td>
                        <td>
                            <asp:CheckBox ID="chkLock_UnLock" runat="server" Text="Khóa/Mở tất cả dữ liệu theo phòng ban" />
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="X">
                <tlk:RadGrid PageSize="50" ID="rgTimeTimesheet_machine" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Họ tên" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" SortExpression="VN_FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="120px" SortExpression="TITLE_NAME" />
                            <tlk:GridTemplateColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                         </tlk:GridTemplateColumn>--%>
                            <%--<tlk:GridDateTimeColumn HeaderText="Ngày làm việc" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WORKINGDAY">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Ca làm việc" DataField="SHIFT_CODE"
                                UniqueName="SHIFT_CODE" SortExpression="SHIFT_CODE" HeaderStyle-Width="100px" />--%>
                           <%-- <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng giờ công %>" DataField="WORKINGHOUR"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="WORKINGHOUR"
                                UniqueName="WORKINGHOUR" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã công%>" DataField="MANUAL_CODE"
                                UniqueName="MANUAL_CODE" SortExpression="MANUAL_CODE" HeaderStyle-Width="100px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>--%>
                            <%--<tlk:GridDateTimeColumn HeaderText="Giờ vào" DataField="SHIFTIN"
                                UniqueName="SHIFTIN" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTIN">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Giờ ra" DataField="SHIFTBACKOUT"
                                UniqueName="SHIFTBACKOUT" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTBACKOUT">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Đối tượng chấm công" DataField="OBJECT_ATTENDANCE_NAME"
                                UniqueName="OBJECT_ATTENDANCE_NAME" HeaderStyle-Width="120px" SortExpression="OBJECT_ATTENDANCE_NAME" />                            
                            <tlk:GridNumericColumn HeaderText="Số phút ở cơ quan" DataField="MIN_IN_WORK"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_IN_WORK"
                                UniqueName="MIN_IN_WORK" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút ngoài cơ quan" DataField="MIN_OUT_WORK"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_OUT_WORK"
                                UniqueName="MIN_OUT_WORK" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút giảm trừ do đi việc công" DataField="MIN_DEDUCT_WORK"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_DEDUCT_WORK"
                                UniqueName="MIN_DEDUCT_WORK" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút trên đơn đăng ký nghỉ" DataField="MIN_ON_LEAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_ON_LEAVE"
                                UniqueName="MIN_ON_LEAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút giảm trừ" DataField="MIN_DEDUCT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_DEDUCT"
                                UniqueName="MIN_DEDUCT" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút ngoài cơ quan sau khi giảm trừ" DataField="MIN_OUT_WORK_DEDUCT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_OUT_WORK_DEDUCT"
                                UniqueName="MIN_OUT_WORK_DEDUCT" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút đi trễ" DataField="MIN_LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_LATE"
                                UniqueName="MIN_LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút về sớm" DataField="MIN_EARLY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_EARLY"
                                UniqueName="MIN_EARLY" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Số phút đi trễ/về sớm" DataField="MIN_LATE_EARLY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="MIN_LATE_EARLY"
                                UniqueName="MIN_LATE_EARLY" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>--%>

                            <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Vào giữa %>" DataField="SHIFTBACKIN"
                                UniqueName="SHIFTBACKIN" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTBACKIN">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ra cuối %>" DataField="SHIFTOUT"
                                UniqueName="SHIFTOUT" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTOUT">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>--%>
                            <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Về sớm(phút) %>" DataField="COMEBACKOUT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="COMEBACKOUT"
                                UniqueName="COMEBACKOUT" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="4" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:Timer ID="TimerRequest" runat="server" Interval="5000" Enabled="False" EnableViewState="True">
</asp:Timer>
<asp:HiddenField runat="server" ID="hidRequestID" Value="0" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlTimeTimesheet_machine_RadSplitter3';
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
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenEditWindow(states) {


        }

        function OpenInsertWindow() {


        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgTimeTimesheet_machine.ClientID %>");
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
            if (args.get_item().get_commandName() == 'EXPORT_TEMP') {
                enableAjax = false;
            }
        }

        function OnClientClose(oWnd, args) {
            postBack(oWnd.get_navigateUrl());
        }

        function postBack(url) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>
