<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindProgramPopupView.ascx.vb"
    Inherits="Recruitment.ctrlFindProgramPopupView" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />                
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgOrgTitle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    AllowFilteringByColumn="true" Height="100%" AllowSorting="True" 
                    AllowMultiRowSelection="True">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,TITLE_ID,CODE,ORG_ID">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày gửi yêu cầu %>" DataField="SEND_DATE"
                                SortExpression="SEND_DATE" UniqueName="SEND_DATE" HeaderStyle-Width="120px" dataformatstring="{0:dd/MM/yyyy}" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tuyển dụng %>" DataField="CODE"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="CODE"
                                UniqueName="CODE" /> 

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
                
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" MinHeight="50" Height="50px" Scrolling="None">
                 <asp:HiddenField ID="hidSelected" runat="server" />
                 <div style="margin: 20px 10px 10px 10px; text-align: right; vertical-align: middle">
                    <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                        Font-Bold="true" CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                        Font-Bold="true" CausesValidation="false" OnClientClicked="btnCancelClick">
                    </tlk:RadButton>
                    </div> 
            </tlk:RadPane>
        </tlk:RadSplitter>
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

        function btnYesClick(resulst) {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();
            if (resulst == 1) {
                oArg.ID = 'OK';
            } else {
                oArg.ID = 'Error';
            }
            
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