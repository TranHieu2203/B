<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_ViewEmpSalary.ascx.vb"
    Inherits="Payroll.ctrlPA_ViewEmpSalary" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="250px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
             <tr>
                <td class="lb">
                    <asp:HiddenField ID="hidEmpID" runat="server" />
                    <asp:Label runat="server" ID="lbCode" Text="Mã nhân viên"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmpCode" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbFullName" Text="Họ tên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFullName" runat="server" SkinID="ReadOnly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
             </tr>
             <tr>
                <td class="lb">
                    <asp:HiddenField ID="hidOrgID" runat="server" />
                    <asp:Label runat="server" ID="lbOrgName" Text="Phòng ban"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" SkinID="ReadOnly" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:HiddenField ID="hidTitleID" runat="server" />
                    <asp:Label runat="server" ID="lbTitleName" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" SkinID="ReadOnly" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
             </tr>
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbYear" Text="Năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" runat="server" >
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbMonth" Text="Kỳ công"></asp:Label>
                </td>
                <td>
                    <asp:HiddenField ID="hidPeriodId" runat="server" />
                    <tlk:RadComboBox ID="cboMonth" runat="server" >
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind" >
                    </tlk:RadButton>
                </td>
             </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="500" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="False" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">          
            <MasterTableView>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="Mã tiêu chí" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE"/>
                    <tlk:GridBoundColumn HeaderText="Tên tiêu chí" DataField="NAME" SortExpression="NAME"
                        UniqueName="NAME"/>
                    <tlk:GridBoundColumn HeaderText="Công thức" DataField="FOMULER" SortExpression="FOMULER"
                        UniqueName="FOMULER"/>
                    <tlk:GridBoundColumn HeaderText="Giá trị" DataField="VALUE" SortExpression="VALUE"
                        UniqueName="VALUE"/>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false"> 
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_ViewEmpSalary_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_ViewEmpSalary_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_ViewEmpSalary_RadPane2';
        var validateID = 'MainContent_ctrlPA_ViewEmpSalary_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

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

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } 
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
