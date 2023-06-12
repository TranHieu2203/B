<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindEmployeePopupDialog.ascx.vb"
    Inherits="Common.ctrlFindEmployeePopupDialog" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="280" Width="280px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="TopPanel" runat="server" MinHeight="110" Height="110px" Scrolling="None">
                <div class="block-title">
                    <%# Translate("Tìm kiếm") %></div>
                <table class="table-form">
                    <tr>
                        <td>
                            <%# Translate("Mã/Tên NV")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployee_Code" runat="server" Width="100px">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <a href="#" onclick="btnAdvancedFind_Click();">
                                <%# Translate("Tìm kiếm nâng cao") %></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <tlk:RadButton ID="chkTerminate" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                Text="<%$ Translate: Hiển thị nhân viên nghỉ việc %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="BottomPanel" runat="server">
                <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgEmployeeInfo" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="true" HeaderStyle-Width="0px" />
                            <tlk:GridBoundColumn DataField="TITLE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_DESC" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ABBR" Visible="false" />
                            <tlk:GridBoundColumn DataField="LEVEL_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="JOIN_DATE" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="130px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" HeaderStyle-Width="130px" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" MinHeight="50" Height="50px" Scrolling="None">
                <div style="margin: 20px 10px 10px 10px; text-align: right; vertical-align: middle">
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

        function btnAdvancedFind_Click() {
            var oWnd = $find('<%=popupId %>');
            oWnd.add_close(OnClientClose);
            oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindEmployeePopupFilter&noscroll=1');
            oWnd.show();
        }

        function OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

        function postBack(arg) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }
    </script>
</tlk:RadScriptBlock>
