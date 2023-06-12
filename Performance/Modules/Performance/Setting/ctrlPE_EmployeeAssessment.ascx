<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_EmployeeAssessment.ascx.vb"
    Inherits="Performance.ctrlPE_EmployeeAssessment" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane5" runat="server" Height="35px" Scrolling="None" Visible=false >
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking"/>
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="40px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Kỳ đánh giá")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm đối tượng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjectGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgCanNotSchedule" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="EMPLOYEE_ID" Caption="<%$ Translate: Nhân viên chưa gán nhóm đối tượng %>">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị trực thuộc %>" DataField="ORG_PATH"
                                UniqueName="ORG_PATH" SortExpression="ORG_PATH" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bộ phận %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="50px">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnDelete" runat="server" Font-Bold="true" Text="<--" OnClientClicking="btnDelete_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsert" runat="server" Font-Bold="true" Text="-->" OnClientClicking="btnInsert_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgCanSchedule" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="ID" Caption="<%$ Translate: Nhân viên đã gán nhóm đối tượng %>">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị trực thuộc %>" DataField="ORG_PATH"
                                UniqueName="ORG_PATH" SortExpression="ORG_PATH" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bộ phận %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function btnInsert_Click(sender, args) {
            var cbo = $find("<%# cboObjectGroup.ClientID %>");
            if (cbo.get_value().length != 0) {
                var bCheck = $find('<%# rgCanNotSchedule.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            else {
                var m = '<%# Translate("Bạn phải chọn Nhóm đối tượng") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        function btnDelete_Click(sender, args) {
            var cbo = $find("<%# cboObjectGroup.ClientID %>");
            if (cbo.get_value().length != 0) {
                var bCheck = $find('<%# rgCanSchedule.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            else {
                var m = '<%# Translate("Bạn phải chọn Nhóm đối tượng") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                args.set_cancel(true);
            }
        }
    </script>
</tlk:RadCodeBlock>
