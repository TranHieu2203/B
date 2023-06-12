<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindOrgImportPopupDialog.ascx.vb" Inherits="Profile.ctrlFindOrgImportPopupDialog" %>
<%@ Import Namespace="Framework.UI" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="280" Width="100%" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="TopPanel" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <%# Translate("Tên phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrgName" runat="server" Width="100px">
                            </tlk:RadTextBox>
                        </td>                       
                          <td class="lb">
                            <%# Translate("Ngày xét thưởng")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdCommendDate" runat="server" Width="100%" AutoPostBack="True">
                            </tlk:RadDatePicker>
                        </td>   
                          <td class="lb">
                            <%# Translate("Danh hiệu khen thưởng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cbCommenList" runat="server">
                            </tlk:RadComboBox>
                        </td>         
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                         <td></td>
                         <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane> 
            <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgEmployeeInfo" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,ORG_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã phòng ban %>" DataField="ORG_ID"
                                UniqueName="ORG_ID" SortExpression="ORG_ID" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="150px" />                          
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày xét thưởng %>" DataField="COMMEND_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="COMMEND_DATE" UniqueName="COMMEND_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="130px" />
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