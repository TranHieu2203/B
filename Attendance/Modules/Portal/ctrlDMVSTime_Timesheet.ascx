<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMVSTime_Timesheet.ascx.vb"
    Inherits="Attendance.ctrlDMVSTime_Timesheet
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" >
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
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
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px"
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
                            <asp:Label runat="server" ID="lbFilter" Text="Dữ liệu phát sinh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cbFilter" SkinID="dDropdownList" Width="160px"
                                RenderMode="Lightweight" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
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
                            <asp:CheckBox ID="ckReset" runat="server" Text="Cập nhật mới lại toàn bộ dữ liệu" Visible="false" />
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME"
                                ItemStyle-HorizontalAlign="Center" SortExpression="STATUS_NAME"
                                UniqueName="STATUS_NAME" HeaderStyle-Width="130px">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày làm việc" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WORKINGDAY">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Thứ trong tuần" DataField="DAYOFWEEK"
                                UniqueName="DAYOFWEEK" SortExpression="DAYOFWEEK" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Ca làm việc" DataField="SHIFT_CODE"
                                UniqueName="SHIFT_CODE" SortExpression="SHIFT_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridDateTimeColumn HeaderText="Giờ bắt đầu ca" DataField="HOURS_START"
                                UniqueName="HOURS_START" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="HOURS_START">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Giờ kết thúc ca" DataField="HOURS_STOP"
                                UniqueName="HOURS_STOP" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="HOURS_STOP">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Hôm sau" DataField="TOMORROW_SHIFT_NAME"
                                UniqueName="TOMORROW_SHIFT_NAME" SortExpression="TOMORROW_SHIFT_NAME" AllowFiltering="false" HeaderStyle-Width="100px" />
                            <tlk:GridDateTimeColumn HeaderText="Giờ Check-In" DataField="TIMEIN_REALITY"
                                UniqueName="TIMEIN_REALITY" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="TIMEIN_REALITY">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Giờ Check-Out" DataField="TIMEOUT_REALITY"
                                UniqueName="TIMEOUT_REALITY" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="TIMEOUT_REALITY">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Hôm sau" DataField="TOMORROW_TT_NAME"
                                UniqueName="TOMORROW_TT_NAME" SortExpression="TOMORROW_TT_NAME" AllowFiltering="false" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="OT ngày" DataField="WORKDAY_OT"
                                ItemStyle-HorizontalAlign="Center" SortExpression="WORKDAY_OT"
                                UniqueName="WORKDAY_OT" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="OT đêm" DataField="WORKDAY_NIGHT"
                                ItemStyle-HorizontalAlign="Center" SortExpression="WORKDAY_NIGHT"
                                UniqueName="WORKDAY_NIGHT" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Giờ làm ca đêm" DataField="MIN_NIGHT"
                                ItemStyle-HorizontalAlign="Center" SortExpression="MIN_NIGHT"
                                UniqueName="MIN_NIGHT" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Số phút đi trễ" DataField="MIN_LATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="MIN_LATE"
                                UniqueName="MIN_LATE" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Số phút về sớm" DataField="MIN_EARLY"
                                ItemStyle-HorizontalAlign="Center" SortExpression="MIN_EARLY"
                                UniqueName="MIN_EARLY" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Số phút đi trễ/về sớm Gốc" DataField="MIN_LATE_EARLY"
                                ItemStyle-HorizontalAlign="Center" SortExpression="MIN_LATE_EARLY"
                                UniqueName="MIN_LATE_EARLY" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                            
                            <tlk:GridBoundColumn HeaderText="Loại ngày công/nghỉ" DataField="MANUAL_CODE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="MANUAL_CODE"
                                UniqueName="MANUAL_CODE" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Đầu ca/Cuối ca" DataField="STATUS_SHIFT_NAME"
                                ItemStyle-HorizontalAlign="Center" SortExpression="STATUS_SHIFT_NAME"
                                UniqueName="STATUS_SHIFT_NAME" HeaderStyle-Width="60px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Số ngày ĐK" DataField="DAY_NUM"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DAY_NUM"
                                UniqueName="DAY_NUM" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var idCtrl = 'ctrlDMVSTime_Timesheet';
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "CREATE") {
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID')
                    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlDSVMNewEdit&TIME=' + id );
                    args.set_cancel(true);
                }
            }
            else {
                // Nếu nhấn các nút khác thì resize default
            }
}

function gridRowDblClick(sender, args) {
    var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
    if (bCheck == 0) {
        var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            else if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            else {
                var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID')
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlDSVMNewEdit&TIME=' + id);
            }
    }

    function OpenInNewTab(url) {
        window.location.href = url;
        }
        var winH;
        var winW;

         
         function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlDMVSTime_Timesheet_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlDMVSTime_Timesheet_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMVSTime_Timesheet_MainPane").stop().animate({ height: winH, width: winW }, 0);
           $("#ctl00_MainContent_ctrlDMVSTime_Timesheet_rgMain").stop().animate({ height: winH-104, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMVSTime_Timesheet_RadPane2").stop().animate({ height: winH, width: winW }, 0);
            
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
</tlk:RadCodeBlock>
