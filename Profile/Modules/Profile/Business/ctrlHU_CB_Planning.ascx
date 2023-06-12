<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CB_Planning.ascx.vb"
    Inherits="Profile.ctrlHU_CB_Planning" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
        <table class="table-form">

            <tr>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True"
            AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID"
                ClientDataKeyNames="ID,YEAR,DECISION_NO,EFFECT_DATE">

                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Năm" DataField="YEAR"
                        SortExpression="YEAR" UniqueName="YEAR" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                        SortExpression="DECISION_NO" UniqueName="DECISION_NO" HeaderStyle-Width="100px"  />
                    <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="Nội dung quy hoạch" DataField="CONTENT"
                        SortExpression="CONTENT" UniqueName="CONTENT" />
                    <tlk:GridBoundColumn HeaderText="Người quyết định" DataField="SIGNER_NAME" UniqueName="SIGNER_NAME"
                        SortExpression="SIGNER_NAME" Visible="false" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày ký" DataField="SIGN_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle Width="120px" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "EDIT") {
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
   
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                } else if (bCheck > 1 ) {
                    var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CB_PlanningNewEdit&group=Business&ID=' + id, "_self");;
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == "CREATE") {
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CB_PlanningNewEdit&group=Business', "_self");;
                args.set_cancel(true);
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;

            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            } else if (bCheck > 1) {
                var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CB_PlanningNewEdit&group=Business&ID=' + id, "_self");
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
