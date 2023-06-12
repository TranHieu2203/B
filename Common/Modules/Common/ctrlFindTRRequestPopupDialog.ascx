<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindTRRequestPopupDialog.ascx.vb"
    Inherits="Common.ctrlFindTRRequestPopupDialog" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadGrid PageSize="50" ID="rgRquest" runat="server" Height="400px" AllowFilteringByColumn="true" AllowMultiRowSelection="false">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người yêu cầu %>" DataField="REQUEST_SENDER_NAME"
                        SortExpression="REQUEST_SENDER_NAME" UniqueName="REQUEST_SENDER_NAME"  ShowFilterIcon="false"/>                   
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        SortExpression="ORG_NAME" UniqueName="ORG_NAME"  ShowFilterIcon="false"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME"
                        SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" ShowFilterIcon="false"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã YCĐT %>" DataField="REQUEST_CODE"
                        SortExpression="REQUEST_CODE" UniqueName="REQUEST_CODE" ShowFilterIcon="false"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đào tạo %>" DataField="TR_PLACE"
                        SortExpression="TR_PLACE" UniqueName="TR_PLACE" ShowFilterIcon="false"/>
                    
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian dự kiến đào tạo từ %>" DataField="EXPECTED_DATE"
                        SortExpression="EXPECTED_DATE" UniqueName="EXPECTED_DATE" DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false"/>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian dự kiến đào tạo đến %>" DataField="EXPECTED_DATE_TO"
                        SortExpression="EXPECTED_DATE_TO" UniqueName="EXPECTED_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false"/>
                    
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số học viên %>" DataField="TRAINER_NUMBER"
                        SortExpression="TRAINER_NUMBER" UniqueName="TRAINER_NUMBER" DataFormatString="{0:N0}" ShowFilterIcon="false"/>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí dự kiến %>" DataField="EXPECTED_COST"
                        SortExpression="EXPECTED_COST" UniqueName="EXPECTED_COST" DataFormatString="{0:N0}" ShowFilterIcon="false"/>

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
