<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CompetencyStandard.ascx.vb"
    Inherits="Profile.ctrlHU_CompetencyStandard" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboTitle" runat="server" AutoPostBack="true" CausesValidation="false"
                        Width="300px">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Chức danh %>" ClientValidationFunction="cusTitle">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm năng lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCompetencyGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCompetencyGroup" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm năng lực %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nhóm năng lực %>" ClientValidationFunction="cusCompetencyGroup">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Năng lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCompetency" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCompetency" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Năng lực %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Năng lực %>" ClientValidationFunction="cusCompetency">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mức")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLevelNumber" runat="server">
                        <Items>
                            <tlk:RadComboBoxItem Value="" Text="" />
                            <tlk:RadComboBoxItem Value="1" Text="1" />
                            <tlk:RadComboBoxItem Value="2" Text="2" />
                            <tlk:RadComboBoxItem Value="3" Text="3" />
                            <tlk:RadComboBoxItem Value="4" Text="4" />
                        </Items>
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusLevelNumber" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Mức %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Mức %>" ClientValidationFunction="cusLevelNumber">
                    </asp:CustomValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="COMPETENCY_GROUP_ID,COMPETENCY_ID,COMPETENCY_NAME,LEVEL_NUMBER">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm năng lực %>" DataField="COMPETENCY_GROUP_NAME"
                        UniqueName="COMPETENCY_GROUP_NAME" SortExpression="COMPETENCY_GROUP_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năng lực %>" DataField="COMPETENCY_NAME"
                        UniqueName="COMPETENCY_NAME" SortExpression="COMPETENCY_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức %>" DataField="LEVEL_NUMBER_NAME"
                        UniqueName="LEVEL_NUMBER_NAME" SortExpression="LEVEL_NUMBER_NAME" AllowFiltering="false">
                        <HeaderStyle Width="80px" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusCompetencyGroup(oSrc, args) {
            var cbo = $find("<%# cboCompetencyGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusCompetency(oSrc, args) {
            var cbo = $find("<%# cboCompetency.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusLevelNumber(oSrc, args) {
            var cbo = $find("<%# cboLevelNumber.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

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
