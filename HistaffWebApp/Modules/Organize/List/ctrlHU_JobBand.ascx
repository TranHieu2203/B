<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobBand.ascx.vb"
    Inherits="Profile.ctrlHU_JobBand" %>
<asp:HiddenField ID="hidID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .RadGrid .rgFilterRow .RadInput {
        top: 3px;
}
    .rgRow td, .rgAltRow td
    {
        padding: 1px 15px 1px 21px !important;
    }
    table.rgClipCells tr td[align=right] 
    {
        padding: 0 25px !important;
    }
    .rgMasterTable .rgHeader 
    {
        padding: 1px 24px 1px 15px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr style="display: none;">
                <td class="lb text-align-left">
                    <%# Translate("Nhóm cấp bậc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboJobband" AutoPostBack="true" CausesValidation="false" >
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên cấp bậc (VN)")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Cấp bậc EN")%>
                    <span class="lbReq">*</span> 
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameEn" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb text-align-left">
                    <%# Translate("Cấp bậc")%><span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadTextBox ID="ntLevelFrom" runat="server" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td style="display: none">
                    <%# Translate("Cấp đến")%><span class="lbReq">*</span>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox ID="ntLevelTo" runat="server" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AllowMultiRowSelection="True" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="50" CellSpacing="0"
            GridLines="None">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView ClientDataKeyNames="ID,NAME_VN,NAME_EN,LEVEL_FROM,LEVEL_TO, STATUS, ACTFLG,TITLE_GROUP_NAME,TITLE_GROUP_ID"
                DataKeyNames="ID,COLOR">
                <Columns>
                    <%--<tlk:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"
                        ItemStyle-HorizontalAlign="Center" UniqueName="cbStatus">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="TITLE_GROUP_ID" Visible="false" />
                     <tlk:GridBoundColumn DataField="TITLE_GROUP_NAME" HeaderText="<%$ Translate: Nhóm cấp bậc %>"
                        HeaderStyle-Width="200px"
                        HeaderStyle-CssClass="text-align-left"
                        SortExpression="TITLE_GROUP_NAME" HeaderStyle-HorizontalAlign="Center" UniqueName="TITLE_GROUP_NAME" />
                    <tlk:GridBoundColumn DataField="NAME_VN" HeaderText="<%$ Translate: Tên cấp bậc (VN) %>"
                        HeaderStyle-Width="200px"
                        HeaderStyle-CssClass="text-align-left"
                        SortExpression="NAME_VN" HeaderStyle-HorizontalAlign="Center" UniqueName="NAME_VN" />
                    <tlk:GridBoundColumn DataField="NAME_EN" HeaderText="<%$ Translate: Cấp bậc EN %>"
                        HeaderStyle-Width="200px"
                        HeaderStyle-CssClass="text-align-left"
                        SortExpression="NAME_EN" HeaderStyle-HorizontalAlign="Center" UniqueName="NAME_EN" />
                    <tlk:GridNumericColumn DataField="LEVEL_FROM" HeaderText="<%$ Translate: Cấp bậc %>"
                        HeaderStyle-Width="200px"
                        HeaderStyle-CssClass="text-align-center"
                        HeaderStyle-HorizontalAlign="Center" SortExpression="LEVEL_FROM" UniqueName="LEVEL_FROM" />--%>
                   <%-- <tlk:GridNumericColumn DataField="LEVEL_TO" HeaderText="<%$ Translate: Cấp đến %>"
                        HeaderStyle-Width="200px"
                        HeaderStyle-CssClass="text-align-right"
                        HeaderStyle-HorizontalAlign="Center" SortExpression="LEVEL_TO" UniqueName="LEVEL_TO" />--%>
                   <%-- <tlk:GridBoundColumn DataField="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>"
                        HeaderStyle-CssClass="text-align-left"
                        HeaderStyle-HorizontalAlign="Center" UniqueName="ACTFLG" />--%>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;


        //        (function(){
        //            let listClass = document.getElementsByClassName('rrbButton rrbSmallButton');
        //            console.log('list', listClass)
        //        })();

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);

        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
