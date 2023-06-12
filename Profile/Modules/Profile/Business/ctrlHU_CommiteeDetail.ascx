<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommiteeDetail.ascx.vb"
    Inherits="Profile.ctrlHU_CommiteeDetail" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<asp:HiddenField ID="hidID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True"
            AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID"
                ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="Mã CBCNV" DataField="EMPLOYEE_CODE" HeaderStyle-Width="130px"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Mã thẻ" DataField="MATHE_NAME" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="MATHE_NAME" SortExpression="MATHE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Tên CBCNV" DataField="EMPLOYEE_NAME" HeaderStyle-Width="120px"
                        ReadOnly="true" UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" HeaderStyle-Width="120px"
                        ReadOnly="true" UniqueName="ORG_NAME" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Chức danh hiện tại" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="TITLE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Chức danh hội đồng" DataField="COMMITEE_TITLE_NAME" UniqueName="COMMITEE_TITLE_NAME"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="COMMITEE_TITLE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="STATUS_NAME" ItemStyle-HorizontalAlign="Center" />
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
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
