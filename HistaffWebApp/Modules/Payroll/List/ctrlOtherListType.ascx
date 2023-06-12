<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOtherListType.ascx.vb"
    Inherits="Common.ctrlOtherListType" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOtherListTypes" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("OtherListType_Code")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOtherListTypeCode" MaxLength="255" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqTypeCode" ControlToValidate="txtOtherListTypeCode"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Validate_Code %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("OtherListType_Name")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOtherListTypeName" MaxLength="255" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqTypeTypeName" ControlToValidate="txtOtherListTypeName"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Validate_Name %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("OtherListType_Group_Id")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboOtherListTypeGroup" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgOtherListType" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="50" AllowSorting="True" AllowMultiRowSelection="true" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,GROUP_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: OtherListType_Code %>" DataField="CODE"
                                UniqueName="CODE" SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: OtherListType_Name %>" DataField="NAME"
                                UniqueName="NAME" SortExpression="NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Status %>" DataField="ACTFLG" UniqueName="ACTFLG"
                                SortExpression="ACTFLG" />
                            <tlk:GridBoundColumn DataField="GROUP_ID" UniqueName="GROUP_ID" SortExpression="GROUP_ID"
                                Visible="false" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var oldSize = 0;
        var enableAjax = true;

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
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
            }
        }
    </script>
</tlk:RadCodeBlock>
