<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSE_Mail.ascx.vb"
    Inherits="Common.ctrlSE_Mail" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbEffectDate" runat="server" Text="Ngày gửi từ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSEND_DATE_F" runat="server">
                    </tlk:RadDatePicker>
                </td>
                 <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSEND_DATE_T" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Email gửi đến")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMAIL_TO" runat="server">
                    </tlk:RadTextBox>
                </td>                
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="Ngày tạo từ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdCREATE_DATE_F" runat="server">
                    </tlk:RadDatePicker>
                </td>
                 <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdCREATE_DATE_T" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Email gửi đi")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMAIL_FROM" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Người thực hiện")%>
                </td>
                <td colspan="3">
                   <tlk:RadTextBox ID="txtCREATE_BY" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td></td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Subject email %>" DataField="SUBJECT"
                        UniqueName="SUBJECT" SortExpression="SUBJECT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email gửi đi %>" DataField="MAIL_FROM"
                        UniqueName="MAIL_FROM" SortExpression="MAIL_FROM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email gửi đến %>" DataField="MAIL_TO"
                        UniqueName="MAIL_TO" SortExpression="MAIL_TO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email CC %>" DataField="MAIL_CC"
                        UniqueName="MAIL_CC" SortExpression="MAIL_CC" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email BCC %>" DataField="MAIL_BCC"
                        UniqueName="MAIL_BCC" SortExpression="MAIL_BCC" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:Phân hệ thực hiện %>" DataField="VIEW_NAME"
                        UniqueName="VIEW_NAME" SortExpression="VIEW_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người thực hiện %>" DataField="CREATE_BY"
                        UniqueName="CREATE_BY" SortExpression="CREATE_BY" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày tạo %>" DataField="CREATE_DATE"
                        UniqueName="CREATE_DATE" SortExpression="CREATE_DATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày gửi %>" DataField="SEND_DATE"
                        UniqueName="SEND_DATE" SortExpression="SEND_DATE" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
