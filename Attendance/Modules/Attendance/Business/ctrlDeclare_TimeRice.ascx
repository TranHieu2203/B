﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDeclare_TimeRice.ascx.vb"
    Inherits="Attendance.ctrlDeclare_TimeRice" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
                <table class="table-form"  onkeydown="return (event.keyCode!=13)">
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
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" SkinID ="Readonly" MaxLength="12" runat="server" ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" SkinID ="Readonly" MaxLength="12"  runat="server" ToolTip="">
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
                <tlk:RadGrid PageSize=50 ID="rgDeclare_TimeRice" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" HeaderStyle-Width="200px"
                                DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày đăng ký %>" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" ShowFilterIcon="false" DataFormatString="{0:dd/MM/yyyy}"
                                SortExpression="WORKINGDAY">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Đơn giá tiền ăn %>" DataField="PRICE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" SortExpression="PRICE"
                                UniqueName="PRICE">
                            </tlk:GridNumericColumn>
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
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlDeclare_TimeRiceNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(900, 500);
            oWindow.center();
        }
        function OpenEditWindow() {
            var grid = $find('<%# rgDeclare_TimeRice.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlDeclare_TimeRiceNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
                oWindow.setSize(900, 500);
                oWindow.center();
            }
        }
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
                $find("<%= rgDeclare_TimeRice.ClientID %>").get_masterTableView().rebind();
            }
            $get("<%# btnSearch.ClientId %>").click();
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgDeclare_TimeRice.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadScriptBlock>
