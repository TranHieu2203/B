<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDeclareEntitlementNB.ascx.vb"
    Inherits="Attendance.ctrlDeclareEntitlementNB" %>
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
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgDeclareEntitlementNB" runat="server" SkinID="GridSingleSelect"
                    Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên"
                                DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" HeaderStyle-Width="150px" UniqueName="TITLE_NAME" />
                                 <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="Đối tượng độc hại" DataField="HURT_TYPE_NAME"
                                SortExpression="HURT_TYPE_NAME" HeaderStyle-Width="150px" UniqueName="HURT_TYPE_NAME" />
                             <tlk:GridDateTimeColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="JOIN_DATE" UniqueName="JOIN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Loại điều chỉnh" DataField="MODIFY_TYPE_NAME"
                                SortExpression="MODIFY_TYPE_NAME" HeaderStyle-Width="150px" UniqueName="MODIFY_TYPE_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Hiệu lực" DataField="START_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="START_DATE" UniqueName="START_DATE"
                                DataFormatString="{0:MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Hết hiệu lực" DataField="END_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="END_DATE" UniqueName="END_DATE"
                                DataFormatString="{0:MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="Giá trị điều chỉnh"
                                DataField="ADJUST_MONTH_TN" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="ADJUST_MONTH_TN" UniqueName="ADJUST_MONTH_TN">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="Lý do điều chỉnh" DataField="REMARK_TN"
                                ItemStyle-HorizontalAlign="Left" SortExpression="REMARK_TN" HeaderStyle-Width="150px"
                                UniqueName="REMARK_TN">
                                <HeaderStyle Width="300px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
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
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlDeclareEntitlementNBNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(900, 500);
            oWindow.center();
        }
        function OpenInsertMultipleWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlDeclareEntitlementNBNewEditMultiple&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(900, 500);
            oWindow.center();
        }
        function OpenEditWindow() {
            var grid = $find('<%# rgDeclareEntitlementNB.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlDeclareEntitlementNBNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
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
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgDeclareEntitlementNB.ClientID %>").get_masterTableView().rebind();
            }
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'ADD_NEWS') {
                OpenInsertMultipleWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgDeclareEntitlementNB.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
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
