<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroupUserNewEdit.ascx.vb"
    Inherits="Common.ctrlGroupUserNewEdit" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Tên tài khoản")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUSERNAME" runat="server" MaxLength="255" Width="140px">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Họ và tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFULLNAME" runat="server" MaxLength="255" Width="140px">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" AutoPostBack="true" Width="130px" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                </td>
                <td>
                    <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài khoản %>" DataField="USERNAME"
                        UniqueName="USERNAME" SortExpression="USERNAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                    UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="EMAIL" UniqueName="EMAIL"
                        SortExpression="EMAIL" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mobile %>" DataField="TELEPHONE"
                        UniqueName="TELEPHONE" SortExpression="TELEPHONE" HeaderStyle-Width="80px" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: App User %>" DataField="IS_APP"
                        UniqueName="IS_APP" SortExpression="IS_APP">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Portal User %>" DataField="IS_PORTAL"
                        UniqueName="IS_PORTAL" SortExpression="IS_PORTAL">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: AD User %>" DataField="IS_AD"
                        UniqueName="IS_AD" SortExpression="IS_AD">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

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

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('ctl00_MainContent_ctrlGroup_ctrlGroupUser_RadSplitter2');
        }

    </script>
</tlk:RadScriptBlock>
