<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_BHLD.ascx.vb"
    Inherits="Profile.ctrlHU_BHLD" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Năm %>" />
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnYear" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-AllowRounding="true" MinValue="2000">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>

                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Mã NV %>" />
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtempcode" runat="server">                                
                            </tlk:RadTextBox>
                        </td>
                         <td>
                            <tlk:RadButton ID="btnSeach" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>

                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false" AllowMultiRowEdit="True"
                    AllowPaging="True" Height="100%" AllowMultiRowSelection="true">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" Scrolling-AllowScroll="true"
                                    Scrolling-SaveScrollPosition="true" Scrolling-UseStaticHeaders="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="3" />
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                    <MasterTableView DataKeyNames="" ClientDataKeyNames="">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderStyle-Width="50px" HeaderText="<%$ Translate: Năm %>"
                                DataField="YEAR" SortExpression="YEAR" UniqueName="YEAR" ReadOnly="true" />
                           <%-- <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                DataField="EMP_CODE" SortExpression="EMP_CODE" UniqueName="EMP_CODE" />
                            <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Họ và tên %>"
                                DataField="EMP_NAME" SortExpression="EMP_NAME" UniqueName="EMP_NAME" />
                            <tlk:GridBoundColumn HeaderStyle-Width="200px" HeaderText="<%$ Translate: Đơn vị %>"
                                DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderStyle-Width="200px" HeaderText="<%$ Translate: Chức danh %>"
                                DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />--%>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                //var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                //if (rows == 0) {
                    //var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    //var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    //setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    //args.set_cancel(true);
                    //return;
                //}
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {

            } else if (args.get_item().get_commandName() == "EDIT") {
                //var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                //if (bCheck > 1) {
                //    var l = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                //    var k = noty({ text: l, dismissQueue: true, type: 'warning' });
                //    setTimeout(function () { $.noty.close(k.options.id); }, 5000);
                //    args.set_cancel(true);
                //    return;
                //}
            } else {

            }
}
function OnKeyPress(sender, eventArgs) {
    var c = eventArgs.get_keyCode();
    if (c == 13) {
        document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
    }
}
window.addEventListener('keydown', function (e) {
    if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
        if (e.target.id !== 'ctl00_MainContent_ctrlHU_BHLD_txtEmployeeCode') {
            e.preventDefault();
            return false;
        }
    }
}, true);
    </script>
</tlk:RadCodeBlock>
