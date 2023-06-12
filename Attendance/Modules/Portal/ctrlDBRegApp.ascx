<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDBRegApp.ascx.vb"
    Inherits="Attendance.ctrlDBRegApp" %>
<style type="text/css">
    .lblInfo
    {
        font-weight: bold;
        color: #2196f3;
    }
</style>
<link href="../../../../Styles/font-awesome.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="95%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="EmpReg" runat="server" Scrolling="None">
        <span class="title-dbportal">
            Phê duyệt</span>
        <div class="boxdbPortal">
            <asp:ListView ID="ltrTime_LEAVE" runat="server">
                <ItemTemplate>
                    <div class='box25'>
                        <i style="font-size: 15px;" class='fa fa-exclamation-circle'></i>
                        <%# Eval("EMPLOYEE_NAME") + " đăng ký " + Eval("SIGN_NAME").ToString() + " (từ " + Eval("FROM_DATE").ToString() + " đến " + Eval("TO_DATE").ToString() + ")"%>
                        <a href='/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationByManager' target="_blank">Chi tiết</a>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="ltrOVERTIME" runat="server">
                <ItemTemplate>
                    <div class='box25'>
                        <i style="font-size: 15px;" class='fa fa-exclamation-circle'></i>
                        <%# Eval("EMPLOYEE_NAME") + " đăng ký " + Eval("SIGN_NAME").ToString() + " (từ " + Eval("FROM_DATE").ToString() + " đến " + Eval("TO_DATE").ToString() + ")"%>
                        <a href='/Default.aspx?mid=Attendance&fid=ctrlOTRegistrationByLM' target="_blank">Chi tiết</a>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <%--<asp:ListView ID="ltrWLEO" runat="server">
                <ItemTemplate>
                    <div class='box25'>
                        <i style="font-size: 15px;" class='fa fa-exclamation-circle'></i>
                        <%# Eval("EMPLOYEE_NAME") + " đăng ký " + Eval("SIGN_NAME").ToString() + " (từ " + DateTime.Parse(Eval("FROM_DATE").ToString()).ToString("dd/MM/yyyy") + " đến " + DateTime.Parse(Eval("TO_DATE").ToString()).ToString("dd/MM/yyyy") + ")"%>
                        <a href='/Default.aspx?mid=Attendance&fid=ctrlDMVSApprove' target="_blank">Chi tiết</a>
                    </div>
                </ItemTemplate>
            </asp:ListView>--%>
            <%--<asp:ListView ID="ltrASSESS" runat="server">
                <ItemTemplate>
                    <div class='box25'>
                        <i style="font-size: 15px;" class='fa fa-exclamation-circle'></i>
                        <%# Eval("TB_ASSESS")%>
                        <a href='/Default.aspx?mid=Performance&fid=ctrlPortalAssessmentDirect' target="_blank">Chi tiết</a>
                    </div>
                </ItemTemplate>
            </asp:ListView>--%>

        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
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

        $(document).ready(InIEvent);
    </script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
</tlk:RadScriptBlock>
