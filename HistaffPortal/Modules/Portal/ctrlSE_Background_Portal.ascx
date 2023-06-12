<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSE_Background_Portal.ascx.vb"
    Inherits="Portal.ctrlSE_Background_Portal" %>
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" >
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtFromdate" runat="server" >
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="rtFromdate" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập từ ngày. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtTodate" runat="server" >
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rtTodate" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập đến ngày. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtNote" runat="server"  Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Background")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtBackground" runat="server" Width="80%" ReadOnly="true">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rtBackground" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn background. %>"></asp:RequiredFieldValidator>
                    
                     <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false" TabIndex="3" />
                    <tlk:RadButton runat="server" ID="btnView" Text="View" CausesValidation="false" TabIndex="3" />
                </td>
            </tr>
            <tr style="display:none">
                <td>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
     <%--   <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">--%>
        <tlk:RadGrid ID="rGrid" runat="server" AllowPaging="true" AllowMultiRowEdit="true">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="true" />
             </ClientSettings>
    <MasterTableView EditMode="InPlace" AllowPaging="true" AllowCustomPaging="true" DataKeyNames="ID" ClientDataKeyNames="ID,FROM_DATE,TO_DATE,NOTE,BACKGROUND,FILEPATH">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="FROM_DATE" UniqueName="FROM_DATE"
                SortExpression="FROM_DATE" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="TO_DATE" UniqueName="TO_DATE"
                SortExpression="TO_DATE"  />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                SortExpression="NOTE" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Background %>" DataField="BACKGROUND" UniqueName="BACKGROUND"
                SortExpression="BACKGROUND" />
        </Columns>
    </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
</tlk:RadGrid>
    <%--</tlk:RadPane>--%>
    </tlk:RadPane>
    
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winH;
        var winW;

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlSE_Background_Portal_RadSplitter1").stop().animate({ height: winH }, 0);
            $("#ctl00_MainContent_ctrlSE_Background_Portal_RadSplitter3").stop().animate({ height: winH }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSE_Background_Portal_MainPane").stop().animate({ height: winH}, 0);
            $("#ctl00_MainContent_ctrlSE_Background_Portal_rGrid").stop().animate({ height: winH-130 }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSE_Background_Portal_RadPane1").stop().animate({ height: winH }, 0);
            
            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function <%=ClientID%>_OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if(arg == null)
            {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }
</script>
</tlk:RadCodeBlock>

