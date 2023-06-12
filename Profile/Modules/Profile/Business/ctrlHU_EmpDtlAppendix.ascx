<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlAppendix.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlAppendix" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Scrolling="None">
    <%--<tlk:RadPane runat="server" ID="ToolbarPane" Height="33px" Scrolling="None">
        <tlk:RadToolBar runat="server" ID="tbarMainToolBar" OnClientButtonClicking="ClientButtonClicking">
        </tlk:RadToolBar>
    </tlk:RadPane>--%>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
</tlk:RadCodeBlock>
