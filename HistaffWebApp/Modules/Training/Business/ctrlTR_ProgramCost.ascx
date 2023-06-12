<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramCost.ascx.vb"
    Inherits="Training.ctrlTR_ProgramCost" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidOrgID" />
        <asp:HiddenField ID="hidProgramID" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtOrgName" runat="server"
                        ErrorMessage="<%$ Translate: Phòng ban đã tồn tại. %>" ToolTip="<%$ Translate: Phòng ban đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostCompany" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="rntxtCostCompany" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Chi phí đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Chi phí đào tạo %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số học viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtNumberStudent" runat="server" SkinID="Number" AutoPostBack="true"
                        CausesValidation="false">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtNumberStudent"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số học viên %>" ToolTip="<%$ Translate: Bạn phải nhập Số học viên %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo 1 học viên")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostOfStudent" runat="server" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="TR_PROGRAM_ID,COST_COMPANY,STUDENT_NUMBER,COST_OF_STUDENT,ORG_ID,ORG_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí đào tạo %>" DataField="COST_COMPANY"
                        UniqueName="COST_COMPANY" SortExpression="COST_COMPANY" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng học viên %>" DataField="STUDENT_NUMBER"
                        UniqueName="STUDENT_NUMBER" SortExpression="STUDENT_NUMBER" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí một học viên %>" DataField="COST_OF_STUDENT"
                        UniqueName="COST_OF_STUDENT" SortExpression="COST_OF_STUDENT" />
                </Columns>
                <ItemStyle Width="100px" />
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
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
