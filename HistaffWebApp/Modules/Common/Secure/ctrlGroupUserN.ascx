<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroupUserN.ascx.vb"
    Inherits="Common.ctrlGroupUserN" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" MinHeight="30" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" Width="100%" onclientbuttonclicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="ViewData" runat="server">
            <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                    <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="100%">
                        <MasterTableView DataKeyNames="ID">
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                                <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài khoản %>" DataField="USERNAME"
                                    UniqueName="USERNAME" SortExpression="USERNAME" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                                    UniqueName="FULLNAME" SortExpression="FULLNAME" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                    UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="EMAIL" UniqueName="EMAIL"
                                    SortExpression="EMAIL" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mobile %>" DataField="TELEPHONE"
                                    UniqueName="TELEPHONE" SortExpression="TELEPHONE" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: App User %>" DataField="IS_APP"
                                    UniqueName="IS_APP" SortExpression="IS_APP" AllowFiltering="false">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridCheckBoxColumn>
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Portal User %>" DataField="IS_PORTAL"
                                    UniqueName="IS_PORTAL" SortExpression="IS_PORTAL" AllowFiltering="false">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridCheckBoxColumn>
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: AD User %>" DataField="IS_AD"
                                    UniqueName="IS_AD" SortExpression="IS_AD" AllowFiltering="false">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <ClientEvents OnGridCreated="GridCreated" />
                            <ClientEvents OnCommand="ValidateFilter" />
                        </ClientSettings>
                    </tlk:RadGrid>
                </tlk:RadPane>
            </tlk:RadSplitter>
        </asp:PlaceHolder>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
            registerOnfocusOut('ctl00_MainContent_ctrlGroup_ctrlGroupUser_RadSplitter2');
        }

        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadScriptBlock>
