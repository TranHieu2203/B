<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlControlsManage.ascx.vb"
    Inherits="Common.ctrlControlsManage" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="100px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking">
        </tlk:RadToolBar>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgListControls" runat="server" Height="100%">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
            </ClientSettings>
            <MasterTableView ClientDataKeyNames="ID,FID,ACTFLG" DataKeyNames="ID,FID,ACTFLG">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="FUNCTION_GROUP_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="FID" UniqueName="FID" SortExpression="FID" HeaderText="<%$ Translate: CODE %>" />
                    <tlk:GridBoundColumn DataField="NAME" UniqueName="NAME" SortExpression="NAME" HeaderText="<%$ Translate: Tên tính năng %>" />
                    <tlk:GridBoundColumn DataField="ACTFLG" UniqueName="ACTFLG" SortExpression="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'ACTIVE' || args.get_item().get_commandName() == 'DEACTIVE') {
                bCheck = $find('<%= rgListControls.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = OpenEdit();
            if (bCheck == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            if (bCheck == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }

        }

        function OpenNew() {
            window.open('/Default.aspx?mid=Common&fid=ctrlControlsNewEdit&group=Secure&FormType=0&noscroll=1', "_self")
        }

        function OpenEdit() {
            var bCheck = $find('<%= rgListControls.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%= rgListControls.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('FID');
            window.open('/Default.aspx?mid=Common&fid=ctrlControlsNewEdit&group=Secure&noscroll=1&FormType=1&ID=' + id, "_self")
            return 2;
        }
    </script>
</tlk:RadCodeBlock>
