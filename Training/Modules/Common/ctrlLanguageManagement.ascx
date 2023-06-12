<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLanguageManagement.ascx.vb"
    Inherits="Common.ctrlLanguageManagement" %>
<tlk:RadToolBar ID="tbarLanguage" runat="server">
</tlk:RadToolBar>
<tlk:RadGrid ID="grvLanguage" runat="server" AllowMultiRowEdit="true" Height="500px">
    <GroupingSettings CaseSensitive="false" />
    <MasterTableView DataKeyNames="Key" EditMode="InPlace" AllowFilteringByColumn="true">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="Key" ReadOnly="true" HeaderText="<%$ Translate:Mã ngôn ngữ %>"
                ShowFilterIcon="false" FilterControlWidth="200px" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" SortExpression="KEY" />
            <tlk:GridBoundColumn DataField="Value" HeaderText="<%$ Translate:Phiên dịch %>" ShowFilterIcon="false"
                FilterControlWidth="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                SortExpression="VALUE" />
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
<script type="text/javascript">
    var enableAjax = true;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        }
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    } 
</script>
