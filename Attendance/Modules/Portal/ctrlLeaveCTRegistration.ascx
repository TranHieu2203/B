<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveCTRegistration.ascx.vb"
    Inherits="Attendance.ctrlLeaveCTRegistration
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
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdtungay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Từ ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Từ ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server"
                                ToolTip="" Width="150px">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdDenngay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đến ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Đến ngày %>"> </asp:RequiredFieldValidator>
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
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, VN_FULLNAME, ORG_NAME, LEAVE_FROM, LEAVE_TO, MANUAL_NAME,  NOTE, STATUS, STATUS_NAME, IMPORT"
                        ClientDataKeyNames="ID, EMPLOYEE_ID, EMPLOYEE_CODE, VN_FULLNAME,LEAVE_FROM, LEAVE_TO, MANUAL_NAME, NOTE, STATUS, STATUS_NAME, IMPORT">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phê duyệt kế tiếp %>" DataField="EMP_APPROVES_NAME" Visible="true"
                                UniqueName="EMP_APPROVES_NAME" SortExpression="EMP_APPROVES_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
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
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME" Visible="false"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                           
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đăng ký %>" DataField="MANUAL_NAME"
                                UniqueName="MANUAL_NAME" SortExpression="MANUAL_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>"
                                DataField="LEAVE_FROM" UniqueName="LEAVE_FROM" SortExpression="LEAVE_FROM" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>"
                                DataField="LEAVE_TO" UniqueName="LEAVE_TO" SortExpression="LEAVE_TO" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày %>" DataField="DAY_NUM"
                                UniqueName="DAY_NUM" SortExpression="DAY_NUM" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Di chuyển từ %>" DataField="FROM_LOCATION"
                                UniqueName="FROM_LOCATION" SortExpression="FROM_LOCATION" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Di chuyển đến %>" DataField="TO_LOCATION"
                                UniqueName="TO_LOCATION" SortExpression="TO_LOCATION" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Khoảng cách %>" DataField="NUMBER_KILOMETER"
                                UniqueName="NUMBER_KILOMETER" SortExpression="NUMBER_KILOMETER" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉnh/TP công tác %>" DataField="PROVINCE_NAME"
                                UniqueName="PROVINCE_NAME" SortExpression="PROVINCE_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày trong kế hoạch %>" DataField="IN_PLAN_DAYS"
                                UniqueName="IN_PLAN_DAYS" SortExpression="IN_PLAN_DAYS" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày ngoài kế hoạch %>" DataField="NOT_IN_PLAN_DAYS"
                                UniqueName="NOT_IN_PLAN_DAYS" SortExpression="NOT_IN_PLAN_DAYS" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>--%>
                           
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do đăng ký %>" DataField="NOTE"
                                UniqueName="NOTE" SortExpression="NOTE" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                              
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật gần nhất %>" DataField="MODIFIED_BY"
                                UniqueName="MODIFIED_BY" SortExpression="MODIFIED_BY" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật gần nhất %>" DataField="MODIFIED_DATE"
                                UniqueName="MODIFIED_DATE" SortExpression="MODIFIED_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REASON"
                                UniqueName="REASON" SortExpression="REASON" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tự sinh do import bảng công %>" DataField="IMPORT" Visible="false"
                                UniqueName="IMPORT" SortExpression="IMPORT" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
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
        var idCtrl = 'ctrlLeaveRegistration';
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
                    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveCTRegistrationNewEdit&id=' + id + '&view=TRUE&typeUser=User&idCtrl=' + idCtrl);
                    args.set_cancel(true);
                }
            }
            else if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == "CREATE") {
                if ($('#<%= hidValid.ClientID %>').val() == "1") {
                    var m = '<%= Translate(CommonMessage.MESSAGE_EXIST_INFOR) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'error' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }

                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveCTRegistrationNewEdit&id=0&typeUser=User');
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
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveCTRegistrationNewEdit&id=' + id + '&view=TRUE&typeUser=User&idCtrl=' + idCtrl);
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
            $("#ctl00_MainContent_ctrlLeaveCTRegistration_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlLeaveCTRegistration_MainPane").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlLeaveCTRegistration_rgMain").stop().animate({ height: winH-76 }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlLeaveCTRegistration_RadPane2").stop().animate({ height: winH-76, width: winW }, 0);
            
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
