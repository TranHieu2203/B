<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlManagementNB.ascx.vb"
    Inherits="Attendance.ctrlManagementNB" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Height="50px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbYear" Text="Năm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgManagementNB" runat="server" Height="100%">
                <%--    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="2" />
                    </ClientSettings>--%>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME_VN"
                                UniqueName="TITLE_NAME_VN" SortExpression="TITLE_NAME_VN" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày hết hạn nghỉ bù năm trước" DataField="EXPIREDATE"
                                UniqueName="EXPIREDATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIREDATE">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Phép bù năm trước chuyển sang %>" DataField="PREV_HAVE" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="PREV_HAVE" UniqueName="PREV_HAVE">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Phép bù cũ còn lại %>" DataField="PREV_HAVE1" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="PREV_HAVE1" UniqueName="PREV_HAVE1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tăng T1 %>" DataField="AL_T1" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T1" UniqueName="AL_T1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T2 %>" DataField="AL_T2" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T2" UniqueName="AL_T2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T3 %>" DataField="AL_T3" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T3" UniqueName="AL_T3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T4 %>" DataField="AL_T4" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T4" UniqueName="AL_T4">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T5 %>" DataField="AL_T5" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T5" UniqueName="AL_T5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T6 %>" DataField="AL_T6" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T6" UniqueName="AL_T6">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T7 %>" DataField="AL_T7" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T7" UniqueName="AL_T7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T8 %>" DataField="AL_T8" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T8" UniqueName="AL_T8">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T9 %>" DataField="AL_T9" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="AL_T9" UniqueName="AL_T9">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T10 %>" DataField="AL_T10"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T10"
                                UniqueName="AL_T10">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T11 %>" DataField="AL_T11"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T11"
                                UniqueName="AL_T11">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tăng T12 %>" DataField="AL_T12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AL_T12"
                                UniqueName="AL_T12">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng số ngày được nghỉ bù %>" DataField="TOTAL_CUR_HAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" HeaderStyle-Width="100px"
                                SortExpression="TOTAL_CUR_HAVE" UniqueName="TOTAL_CUR_HAVE">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T1 %>" DataField="CUR_USED1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED1"
                                UniqueName="CUR_USED1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T2 %>" DataField="CUR_USED2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED2"
                                UniqueName="CUR_USED2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T3 %>" DataField="CUR_USED3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED3"
                                UniqueName="CUR_USED3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T4 %>" DataField="CUR_USED4"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED4"
                                UniqueName="CUR_USED4">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T5 %>" DataField="CUR_USED5"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED5"
                                UniqueName="CUR_USED5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T6 %>" DataField="CUR_USED6"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED6"
                                UniqueName="CUR_USED6">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T7 %>" DataField="CUR_USED7"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED7"
                                UniqueName="CUR_USED7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T8 %>" DataField="CUR_USED8"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED8"
                                UniqueName="CUR_USED8">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T9 %>" DataField="CUR_USED9"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED9"
                                UniqueName="CUR_USED9">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T10 %>" DataField="CUR_USED10"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED10"
                                UniqueName="CUR_USED10">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T11 %>" DataField="CUR_USED11"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED11"
                                UniqueName="CUR_USED11">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Sử dụng T12 %>" DataField="CUR_USED12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED12"
                                UniqueName="CUR_USED12">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số ngày đã nghỉ bù %>" DataField="CUR_USED"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" DataFormatString="{0:n2}"
                                SortExpression="CUR_USED" UniqueName="CUR_USED">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày nghỉ bù còn lại %>" DataField="CUR_HAVE"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" DataFormatString="{0:n2}"
                                SortExpression="CUR_HAVE" UniqueName="CUR_HAVE">
                            </tlk:GridNumericColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
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
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgManagementNB.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'NEXT' || args.get_item().get_commandName() == 'PRINT') {
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
