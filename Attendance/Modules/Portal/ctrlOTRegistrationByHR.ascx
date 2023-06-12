<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOTRegistrationByHR.ascx.vb"
    Inherits="Attendance.ctrlOTRegistrationByHR" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Từ ngày làm thêm")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdRegDateFrom">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày làm thêm")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdRegDateTo">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdRegDateTo"
                                    ControlToCompare="rdRegDateFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày làm thêm phải lớn hơn Từ ngày làm thêm %>"
                                    ToolTip="<%$ Translate: Đến ngày làm thêm phải lớn hơn Từ ngày làm thêm %>"></asp:CompareValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Trạng thái")%>
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
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true" AllowMultiRowSelection="true">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowDblClick="gridRowDblClick" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, FULLNAME, DEPARTMENT_NAME, TITLE_NAME, 
                 REGIST_DATE,SIGN_CODE, OT_TYPE_NAME, TOTAL_OT, OT_100,OT_150, OT_200, OT_210, OT_270,OT_300,OT_370,
                 NOTE,REASON,STATUS, STATUS_NAME, MODIFIED_BY,MODIFIED_DATE" ClientDataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, FULLNAME, DEPARTMENT_NAME, TITLE_NAME, 
                 REGIST_DATE,SIGN_CODE, OT_TYPE_NAME, TOTAL_OT, OT_100,OT_150, OT_200, OT_210, OT_270,OT_300,OT_370,
                 NOTE,REASON,STATUS, STATUS_NAME, MODIFIED_BY,MODIFIED_DATE">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                        HeaderStyle-Width="130px" ItemStyle-Width="130px" UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                        HeaderStyle-Width="80px" ItemStyle-Width="80px" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Verified by HR %>" DataField="HR_REVIEW"
                                        HeaderStyle-Width="130px" ItemStyle-Width="130px" UniqueName="HR_REVIEW" SortExpression="HR_REVIEW" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME" UniqueName="FULLNAME"
                                        HeaderStyle-Width="140px" ItemStyle-Width="140px" SortExpression="FULLNAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEPARTMENT_NAME"
                                        HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="DEPARTMENT_NAME" SortExpression="DEPARTMENT_NAME"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                        HeaderStyle-Width="270px" ItemStyle-Width="270px" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày làm thêm %>" DataField="REGIST_DATE"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="REGIST_DATE" SortExpression="REGIST_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ký hiệu ca %>" DataField="SIGN_CODE"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="SIGN_CODE" SortExpression="SIGN_CODE" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại làm thêm %>" DataField="OT_TYPE_NAME"
                                        HeaderStyle-Width="150px" ItemStyle-Width="150px" UniqueName="OT_TYPE_NAME" SortExpression="OT_TYPE_NAME"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm %>" DataField="TOTAL_OT"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="TOTAL_OT" SortExpression="TOTAL_OT" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 100% %>" DataField="OT_100" UniqueName="OT_100"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_100" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 150% %>" DataField="OT_150" UniqueName="OT_150"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_150" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 200% %>" DataField="OT_200" UniqueName="OT_200"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_200" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 210% %>" DataField="OT_210" UniqueName="OT_210"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_210" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 270% %>" DataField="OT_270" UniqueName="OT_270"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_270" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 300% %>" DataField="OT_300" UniqueName="OT_300"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_300" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: 390% %>" DataField="OT_370" UniqueName="OT_370"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" SortExpression="OT_370" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lí do làm thêm %>" DataField="NOTE"
                                        HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="NOTE" SortExpression="NOTE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật %>" DataField="MODIFIED_BY"
                                        HeaderStyle-Width="140px" ItemStyle-Width="140px" UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY" />
                                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="MODIFIED_DATE"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" UniqueName="MODIFIED_DATE" SortExpression="MODIFIED_DATE"
                                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REASON"
                                        HeaderStyle-Width="200px" ItemStyle-Width="200px" UniqueName="REASON" SortExpression="REASON" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlCommon_Reject ID="ctrlCommon_Reject" runat="server" />
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
                    var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                    var empId = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistrationNewEdit&id=' + id + '&typeUser=HR&empId=' + empId);
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

                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistrationByHRNewEdit&id=0');
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == "SUBMIT") {
                bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
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
                            m = '<%# Translate(CommonMessage.MESSAGE_APPROVED_WANRNING) %>';
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
                //args.set_cancel(true);
            }
            else if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                //args.set_cancel(true);
            }
            else {
                var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID')
                var empId = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistrationNewEdit&id=' + id + '&typeUser=HR&empId=' + empId);
                //args.set_cancel(true);
            }
        }

        function OpenInNewTab(url) {
            window.location.href = url;
        }
    </script>
</tlk:RadCodeBlock>
