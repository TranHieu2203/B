<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlApproveOT.ascx.vb"
    Inherits="Attendance.ctrlApproveOT" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="80px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
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
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="150px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
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
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server" ToolTip="">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="500" ID="rgDeclaresOT" runat="server" Height="100%" AllowMultiRowEdit="true">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID,ORG_DESC,HOUR,APPROVE_ID" ClientDataKeyNames="ID"
                        EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" HeaderStyle-Width="120px"
                                DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                                ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" HeaderStyle-Width="120px"
                                DataField="VN_FULLNAME" SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME"
                                ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" HeaderStyle-Width="120px"
                                DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" HeaderStyle-Width="120px"
                                DataField="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME"
                                ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" HeaderStyle-Width="200px"
                                DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" ReadOnly="true" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày đăng ký %>" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WORKINGDAY"
                                ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ giờ %>" DataField="FROM_HOUR"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" AllowFiltering="false"
                                SortExpression="FROM_HOUR" UniqueName="FROM_HOUR" DataFormatString="{0:HH:mm}"
                                ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến giờ %>" DataField="TO_HOUR" ItemStyle-HorizontalAlign="Center"
                                SortExpression="TO_HOUR" UniqueName="TO_HOUR" HeaderStyle-Width="80px" AllowFiltering="false"
                                DataFormatString="{0:HH:mm}" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số làm thêm %>" DataField="HS_OT_NAME"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" SortExpression="HS_OT_NAME"
                                UniqueName="HS_OT_NAME" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT thực tế (h) %>" DataField="HOUR"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" SortExpression="HOUR"
                                UniqueName="HOUR" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: OT phê duyệt (h) %>" DataField="APPROVE_HOUR"
                                UniqueName="APPROVE_HOUR" DataFormatString="{0:n2}" SortExpression="APPROVE_HOUR"
                                HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="APPROVE_NAME"
                                SortExpression="APPROVE_NAME" UniqueName="APPROVE_NAME" HeaderStyle-Width="200px"
                                ReadOnly="true" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgDeclaresOT.ClientID %>").get_masterTableView().rebind();
            }
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
