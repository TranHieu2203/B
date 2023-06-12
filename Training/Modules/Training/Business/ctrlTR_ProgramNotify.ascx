<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramNotify.ascx.vb"
    Inherits="Training.ctrlTR_ProgramNotify" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="1px" Scrolling="None">
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <center>
            <table class="table-form">
                <tr>
                    <td style="padding: 10px">
                        <tlk:RadButton ID="btnSendMail" runat="server" CausesValidation="false" Text="Gửi mail"
                            Width="100px">
                        </tlk:RadButton>
                    </td>
                    <td style="padding: 10px">
                        <tlk:RadButton ID="btnPrint" runat="server" CausesValidation="false" Text="In" Width="100px">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </center>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        var oldSize = 0;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
