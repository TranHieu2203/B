<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_AssetMng.ascx.vb"
    Inherits="Profile.ctrlHU_AssetMng" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Profile.ProfileCommon" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarAssetMngs" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày cấp phát từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("đến")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server"></tlk:RadDatePicker>
                        </td>
                        <td colspan="2">
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
                <tlk:RadGrid PageSize="50" ID="rgAssetMng" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,WORK_STATUS,STATUS_ID" EditMode="InPlace" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="STATUS_ID" Display="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp bậc nhân sự %>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã loại tài sản %>" DataField="ASSET_CODE"
                                SortExpression="ASSET_CODE" UniqueName="ASSET_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài sản %>" DataField="ASSET_NAME"
                                SortExpression="ASSET_NAME" UniqueName="ASSET_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm tài sản %>" DataField="ASSET_GROUP_NAME"
                                SortExpression="ASSET_GROUP_NAME" UniqueName="ASSET_GROUP_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã vạch tài sản %>" DataField="ASSET_BARCODE"
                                SortExpression="ASSET_BARCODE" UniqueName="ASSET_BARCODE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Giá trị tài sản (VNĐ) %>" DataField="ASSET_VALUE"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="ASSET_VALUE"
                                UniqueName="ASSET_VALUE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền đặt cọc (VNĐ) %>" DataField="DEPOSITS"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="DEPOSITS"
                                UniqueName="DEPOSITS" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp phát %>" DataField="ISSUE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="ISSUE_DATE" UniqueName="ISSUE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thu hồi %>" DataField="RETURN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="RETURN_DATE" UniqueName="RETURN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="DESC" SortExpression="DESC"
                                UniqueName="DESC" HeaderStyle-Width="250px" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_AssetMng_RadSplitter3');
        }

        function CheckValidate()
        {
            var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
            {
                return 1;
            }
            return 0;
        }
        function OpenNew()
        {
            var extented = '';
            var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck > 0) {
                gUID = $find('<%= rgAssetMng.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                if (gUID)
                    extented = '&IsHasInfoEmp=true&gUID=' + gUID;
            }

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_AssetMngNewEdit&group=Business' + extented, "_self");

            /*window.open('/Default.aspx?mid=Profile&fid=ctrlHU_AssetMngNewEdit&group=Business', "_self");
            oWindow.setSize(900, 500);
            oWindow.center(); */
        }
        function OpenEdit()
        {
            var id = $find('<%= rgAssetMng.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var item = $find('<%= rgAssetMng.ClientID%>').get_masterTableView().get_selectedItems()[0];
            var cellValue = $(item.get_cell("STATUS_ID")).text();

            if (cellValue != '<%= HU_ASSET_MNG_STATUS.ASSET_WAIT %>')
            {
                var m = '<%= Translate("Không thể thực hiện thao tác sửa với bản ghi này.") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                args.set_cancel(true);
                return;
            }      
                
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_AssetMngNewEdit&group=Business&gUID=' + id + '', "_self"); /*
            oWindow.setSize(900, 500);
            oWindow.center(); */
        }

        function OnClientButtonClicking(sender, args)
        {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'CREATE')
            {
                var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }

                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT') 
            {                
                var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }

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
                $find("<%= rgAssetMng.ClientID %>").get_masterTableView().rebind();
            }

        }

    </script>
</tlk:RadCodeBlock>
