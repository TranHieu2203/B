<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListUser.ascx.vb"
    Inherits="Common.ctrlListUser" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlListUser_rwMessage_C{
        overflow:hidden !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane4" runat="server" Height="52px" Scrolling="Y">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px">
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID,IS_AD">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài khoản %>" DataField="USERNAME"
                        UniqueName="USERNAME" SortExpression="USERNAME" HeaderStyle-Width="100px" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: App User %>" AllowFiltering ="False" DataField="IS_APP"
                        UniqueName="IS_APP" SortExpression="IS_APP">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Portal User %>" AllowFiltering ="False" DataField="IS_PORTAL"
                        UniqueName="IS_PORTAL" SortExpression="IS_PORTAL">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: HR User %>" AllowFiltering ="False" DataField="IS_HR"
                        UniqueName="IS_HR" SortExpression="IS_HR">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: AD User %>" AllowFiltering ="False" DataField="IS_AD"
                        UniqueName="IS_AD" SortExpression="IS_AD">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm tài khoản %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="EMAIL" UniqueName="EMAIL"
                        SortExpression="EMAIL" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mobile %>" DataField="TELEPHONE"
                        UniqueName="TELEPHONE" SortExpression="TELEPHONE" HeaderStyle-Width="80px" />

                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nghỉ việc %>" DataField="EFFECTDATE_TERMINATION" AllowFiltering ="False"
                        UniqueName="EFFECTDATE_TERMINATION" SortExpression="EFFECTDATE_TERMINATION" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" AllowFiltering ="False"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE" AllowFiltering ="False"
                        UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindow runat="server" ID="rwMessage" AutoSize="true" Behaviors="Close" VisibleStatusbar="false"
    Modal="true" EnableViewState="false" Title="<%$ Translate: Kết quả đồng bộ hóa %>">
    <ContentTemplate>
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadTextBox ID="txtResult" runat="server" TextMode="MultiLine" Width="400px"
                        Height="300px">
                    </tlk:RadTextBox></p>
                </td>
            </tr>
            <tr>
                <td>
                    <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                        <div style="margin: 0px 10px 10px 10px; text-align: center;">
                            <tlk:RadButton ID="btnClose" runat="server" Width="60px" Text="<%$ Translate: Đóng %>"
                                Font-Bold="true" CausesValidation="false" OnClientClicked="OnCloseClicked">
                            </tlk:RadButton>
                        </div>
                    </tlk:RadAjaxPanel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</tlk:RadWindow>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var oldSize = 0;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            } 
            else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
        }

        function OnCloseClicked() {
            $find("<%=rwMessage.ClientID %>").close();
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter1.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter1.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize);
            }
        }
    </script>
</tlk:RadCodeBlock>
