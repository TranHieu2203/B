﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindTitlePopupDialog.ascx.vb"
    Inherits="Common.ctrlFindTitlePopupDialog" %>
    <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadGrid PageSize=50 ID="rgTitle" runat="server" Height="350px" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <ClientEvents OnGridCreated="GridCreated" />
                    <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã chức danh %>" DataField="CODE"
                        SortExpression="CODE" UniqueName="CODE" ShowFilterIcon="false" FilterControlWidth="95%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="ORG_ID_NAME" Visible="false" 
                        SortExpression="ORG_ID_NAME" UniqueName="ORG_ID_NAME" ShowFilterIcon="false" FilterControlWidth="95%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chức danh %>" DataField="NAME_VN"
                        SortExpression="NAME_VN" UniqueName="NAME_VN" ShowFilterIcon="false" FilterControlWidth="95%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chức danh %>" DataField="TITLE_GROUP_NAME"
                        SortExpression="TITLE_GROUP_NAME" UniqueName="TITLE_GROUP_NAME" ShowFilterIcon="false" FilterControlWidth="95%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" ShowFilterIcon="false" FilterControlWidth="95%" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <div style="margin: 10px 10px 10px 10px; position: absolute; bottom: 0px; right: 0px">
            <asp:HiddenField ID="hidSelected" runat="server" />
            <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                Font-Bold="true" CausesValidation="false">
            </tlk:RadButton>
            <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                Font-Bold="true" CausesValidation="false" OnClientClicked="btnCancelClick">
            </tlk:RadButton>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
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
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_RadPaneMain');
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function btnYesClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = $("#<%=hidSelected.ClientID %>").val();
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function btnCancelClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = 'Cancel';
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }
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
            registerOnfocusOut('ctrlFindTitlePopupDialog_RadSplitter1');
        }

    </script>
</tlk:RadScriptBlock>
