<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListClassifi.ascx.vb"
    Inherits="Performance.ctrlListClassifi" %>
<tlk:RadGrid PageSize="50" ID="rgContract" runat="server" Height="100%" AllowFilteringByColumn="true"
    SkinID="GridSingleSelect">
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,VALUE_FROM,VALUE_TO">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" HeaderStyle-Width="100px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả xếp loại %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Khoảng điểm từ (%) %>" DataField="VALUE_FROM" UniqueName="VALUE_FROM"
                        SortExpression="VALUE_FROM" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Khoảng điểm đến (%) %>" DataField="VALUE_TO" UniqueName="VALUE_TO"
                        SortExpression="VALUE_TO" />
                </Columns>
            </MasterTableView>
    <ClientSettings>
        <ClientEvents OnCommand="RaiseCommand" />
    </ClientSettings>
</tlk:RadGrid>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName();
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        function POPUP(value) {
            var RadWinManager = GetRadWindow().get_windowManager();
            RadWinManager.set_visibleTitlebar(true);
            var oWindow = RadWinManager.open(value);
            var pos = $("html").offset();
            oWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close); //set the desired behavioursvar pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(parent.document.body.clientWidth, parent.document.body.clientHeight);
            setTimeout(function () { oWindow.setActive(true); }, 0);
            return false;
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function GetRadWindow() {
            var oWindow = null;

            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }

    </script>
</tlk:RadScriptBlock>
