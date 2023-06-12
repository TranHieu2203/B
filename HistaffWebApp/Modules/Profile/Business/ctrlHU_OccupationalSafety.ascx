<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_OccupationalSafety.ascx.vb"
    Inherits="Profile.ctrlHU_OccupationalSafety" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="BottomPanel" runat="server" MinWidth="200" Width="250px" Height="100%"
        Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal"
            Scrolling="None">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="50px" Width="100%">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày xảy ra tai nạn từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("đến")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                                                <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,WORK_STATUS" EditMode="InPlace" ClientDataKeyNames="ID,THE_COST_OF_ACCIDENTS">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                ReadOnly="true" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                ReadOnly="true" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                ReadOnly="true" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày xảy ra tai nạn %>" DataField="DATE_OF_ACCIDENT"
                                SortExpression="DATE_OF_ACCIDENT" UniqueName="DATE_OF_ACCIDENT" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nguyên nhân %>" DataField="REASON_NAME"
                                SortExpression="REASON_NAME" UniqueName="REASON_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày nghỉ do tai nạn %>" DataField="HOLIDAY_ACCIDENTS"
                                SortExpression="HOLIDAY_ACCIDENTS" UniqueName="HOLIDAY_ACCIDENTS" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả sự việc %>" DataField="DESCRIBED_INCIDENT"
                                SortExpression="DESCRIBED_INCIDENT" UniqueName="DESCRIBED_INCIDENT">
                                <HeaderStyle Width="250px" />
                                <ItemStyle Width="250px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức độ thương tật %>" DataField="EXTENT_OF_INJURY"
                                SortExpression="EXTENT_OF_INJURY" UniqueName="EXTENT_OF_INJURY" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí tai nạn %>" DataField="THE_COST_OF_ACCIDENTS"
                                SortExpression="THE_COST_OF_ACCIDENTS" UniqueName="THE_COST_OF_ACCIDENTS" DataFormatString="{0:n0}"
                                DataType="System.UInt64">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="SDESC" SortExpression="SDESC"
                                UniqueName="SDESC">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" OnClientClose="popupclose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_OccupationalSafety_RadSplitter3');
        }
        
        function CheckValidate()
        {
            var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
            {
                return 1;
            }
            return 0;
        }
        function OpenNew()
        {
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_OccupationalSafetyNewEdit&group=Business', "_self"); /*
            oWindow.setSize(900, 500);
            oWindow.center(); */
        }
        function OpenEdit()
        {
            if ($find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems().length == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            } else if ($find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems().length > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            } else {
                var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_OccupationalSafetyNewEdit&group=Business&gUID=' + id + '', "_self");
            }
        }

        function OnClientButtonClicking(sender, args)
        {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'CREATE')
            {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT')
            {
                OpenEdit();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EXPORT")
            {
                enableAjax = false;
            }
        }

        function gridRowDblClick(sender, eventArgs)
        {
            OpenEdit();
        }

        var enableAjax = true;
        var oldSize = 0;
        function onRequestStart(sender, eventArgs)
        {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function popupclose(sender, args)
        {
            var m;
            var arg = args.get_argument();
            if (arg == '1')
            {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }

        }


    </script>
</tlk:RadCodeBlock>
