<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMVSMng.ascx.vb"
    Inherits="Attendance.ctrlDMVSMng
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTungay" Text='<%# Translate("Từ ngày")%>'></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdtungay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Từ ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Từ ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbDenngay" Text='<%# Translate("Đến ngày")%>'></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server"
                                ToolTip="" Width="150px">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdDenngay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đến ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Đến ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbStatus" Text='<%# Translate("Trạng thái")%>'></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
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
                    <MasterTableView DataKeyNames="ID, EMPLOYEE_ID,STATUS"
                        ClientDataKeyNames="ID, EMPLOYEE_ID,STATUS">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE" Visible="false"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME" Visible="false"
                                UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh%>" DataField="TITLE_NAME" Visible="false"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME" Visible="false"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày đăng ký %>"
                                DataField="WORKINGDAY" UniqueName="WORKINGDAY" SortExpression="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại thông tin %>" HeaderStyle-Width="200px"
                                DataField="REGIST_INFO_NAME" SortExpression="REGIST_INFO_NAME" UniqueName="REGIST_INFO_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đăng ký %>" DataField="TYPE_DMVS_NAME"
                                SortExpression="TYPE_DMVS_NAME" UniqueName="TYPE_DMVS_NAME" HeaderStyle-Width="100px" />

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ giờ %>" DataField="FROM_HOUR" HeaderStyle-Width="70px"
                                AllowFiltering="false" UniqueName="FROM_HOUR" DataFormatString="{0:HH:mm}" SortExpression="FROM_HOUR">
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến giờ %>" DataField="TO_HOUR" HeaderStyle-Width="70px"
                                AllowFiltering="false" UniqueName="TO_HOUR" DataFormatString="{0:HH:mm}" SortExpression="TO_HOUR">
                            </tlk:GridDateTimeColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số phút %>" DataField="MINUTE" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px"
                                DataFormatString="{0:n0}" SortExpression="MINUTE" UniqueName="MINUTE">
                            </tlk:GridNumericColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="REMARK" SortExpression="REMARK" HeaderStyle-Width="100px"
                                UniqueName="REMARK" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị checkin %>" DataField="ORG_CHECK_IN_NAME" SortExpression="ORG_CHECK_IN_NAME" HeaderStyle-Width="100px"
                                UniqueName="ORG_CHECK_IN_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt%>" DataField="REASON" SortExpression="REASON" HeaderStyle-Width="100px"
                                UniqueName="REASON" />--%>

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
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EDIT") {
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
                    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlDSVMNewEdit&id=' + id);
                    args.set_cancel(true);
                }
        }
        else if (args.get_item().get_commandName() == "CREATE") {
            if ($('#<%= hidValid.ClientID %>').val() == "1") {
                    var m = '<%= Translate(CommonMessage.MESSAGE_EXIST_INFOR) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'error' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }

            OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlDMVSTime_Timesheet&typeUser=User');
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == "SUBMIT") {
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                        var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        args.set_cancel(true);
                    }
                    else {//Check danh sách submit hop le
                        var rg = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems();
                        for (i = 0; i < bCheck; i++) {
                            var id = rg[i].getDataKeyValue('ID');
                            var status = rg[i].getDataKeyValue('STATUS');
                            if (status == 17 || status == 18 || status == 21) {
                                m = '<%# Translate("The action only applies for the records that have status as Saved or Unverified by HR. Please select other record.") %>';
                                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                                args.set_cancel(true);
                            }

                        }
                    }
                }
                else if (args.get_item().get_commandName() == 'DELETE') {
                    bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                        var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        args.set_cancel(true);
                    }
                }
                //else if (args.get_item().get_commandName() == 'SENDMAIL') {
                //    window.open('/Default.aspx?mid=Attendance&fid=ctrlTR_ProgramNotify&group=Business', "_self"); 
                //    var pos = $("html").offset();
                //    oWindow.moveTo(pos.left, pos.top);
                //    oWindow.setSize(300, 50);
                //    args.set_cancel(true);
                //}

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
             OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlDSVMNewEdit&id=' + id);
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
            $("#ctl00_MainContent_ctrlDMVSMng_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlDMVSMng_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMVSMng_MainPane").stop().animate({ height: winH, width: winW }, 0);
           $("#ctl00_MainContent_ctrlDMVSMng_rgMain").stop().animate({ height: winH-76, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMVSMng_RadPane2").stop().animate({ height: winH, width: winW }, 0);
            
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
