<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindTitlePopupDialog.ascx.vb"
    Inherits="Common.ctrlFindTitlePopupDialog" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadGrid ID="rgTitle" runat="server" Height="350px" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã chức danh %>" DataField="CODE"
                        SortExpression="CODE" UniqueName="CODE" ShowFilterIcon="false" FilterControlWidth="95%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chức danh TV %>" DataField="NAME_VN"
                        SortExpression="NAME_VN" UniqueName="NAME_VN" ShowFilterIcon="false" FilterControlWidth="95%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chức danh TA %>" DataField="NAME_EN"
                        SortExpression="NAME_EN" UniqueName="NAME_EN" ShowFilterIcon="false" FilterControlWidth="95%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <div style="margin: 10px 10px 10px 10px; position: absolute; bottom: 0px; right: 0px">
            <asp:HiddenField ID="hidSelected" runat="server" />
            <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                Font-Bold="true" CausesValidation="false">
            </tlk:RadButton>
            <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                Font-Bold="true" CausesValidation="false" OnClientClicked="btnCancelClick">
            </tlk:RadButton>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function btnYesClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = $("#<%=hidSelected.ClientID %>").val();
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function btnCancelClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = 'Cancel';
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

    </script>
</tlk:RadScriptBlock>
