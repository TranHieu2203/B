<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlActionLogPA.ascx.vb"
    Inherits="Payroll.ctrlActionLogPA" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="rtbActionLog" runat="server">
        </tlk:RadToolBar>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgActionLog" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" ReadOnly="true" Visible="false" HeaderText="<%$ Translate:Id %>"
                        SortExpression="ID" />
                    <tlk:GridBoundColumn DataField="username" ReadOnly="true" HeaderText="<%$ Translate:Tên tài khoản %>"
                        SortExpression="username" />
                    <tlk:GridBoundColumn DataField="fullname" ReadOnly="true" HeaderText="<%$ Translate:Họ và tên %>"
                        SortExpression="fullname" />
                    <tlk:GridBoundColumn DataField="email" ReadOnly="true" HeaderText="<%$ Translate:Email %>"
                        SortExpression="email" />
                    <tlk:GridBoundColumn DataField="mobile" ReadOnly="true" HeaderText="<%$ Translate:Mobile %>"
                        SortExpression="mobile" />
                    <tlk:GridBoundColumn DataField="action_name" ReadOnly="true" HeaderText="<%$ Translate:Thao tác %>"
                        SortExpression="action_name" />
                         <tlk:GridBoundColumn DataField="PERIOD_NAME" ReadOnly="true" HeaderText="<%$ Translate:Kỳ công %>"
                        SortExpression="PERIOD_NAME" />
                    <tlk:GridDateTimeColumn DataField="action_date" ReadOnly="true" HeaderText="<%$ Translate:Ngày thao tác %>"
                        SortExpression="action_date" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" AllowFiltering="false" />
                    <tlk:GridBoundColumn DataField="object_name" ReadOnly="true" HeaderText="<%$ Translate:Nội dung thao tác %>"
                        SortExpression="object_name" />
                    <tlk:GridBoundColumn DataField="IP" ReadOnly="true" HeaderText="<%$ Translate:Địa chỉ IP %>"
                        SortExpression="IP" />
                    <tlk:GridBoundColumn DataField="computer_name" ReadOnly="true" HeaderText="<%$ Translate:Tên máy %>"
                        SortExpression="computer_name" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="rwmMain" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
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
