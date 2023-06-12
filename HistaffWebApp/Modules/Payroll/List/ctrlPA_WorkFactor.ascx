<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_WorkFactor.ascx.vb"
    Inherits="Payroll.ctrlPA_WorkFactor" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryTypes" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã xếp loại")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã. %>" ToolTip="<%$ Translate: Bạn phải nhập mã. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Xếp loại")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtXEPLOAI" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqtxtXEPLOAI" ControlToValidate="txtXEPLOAI" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập xếp loại. %>" ToolTip="<%$ Translate: Bạn phải nhập xếp loại. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hệ số hoàn thành công việc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFACTOR"  runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFACTOR" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập hệ số. %>" ToolTip="<%$ Translate: Bạn phải nhập hệ số. %>">
                    </asp:RequiredFieldValidator>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: hệ số phải là số %>"
                        ControlToValidate="txtFACTOR" ValidationExpression="^[.,0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mức thưởng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBONUSE" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtBONUSE" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mức thưởng. %>" ToolTip="<%$ Translate: Bạn phải nhập mức thưởng. %>">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<%$ Translate: Mức thưởng phải là số %>"
                        ControlToValidate="txtBONUSE" ValidationExpression="^[0-9_]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" >
                   
                        <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDateTimePicker ID="radEFFECT_DATE" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="radEFFECT_DATE" runat="server"
                        ErrorMessage="<%$ Translate: Ngày hiệu lực không được để trống. %>" ToolTip="<%$ Translate: Ngày hiệu lực không được để trống. %>">
                    </asp:RequiredFieldValidator>
                </td>
               
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,XEPLOAI,FACTOR,BONUSE,EFFECT_DATE,ACTFLG,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã xếp loại %>" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại %>" DataField="XEPLOAI" SortExpression="XEPLOAI"
                        UniqueName="XEPLOAI" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số hoàn thành công việc %>" DataField="FACTOR" SortExpression="FACTOR"
                        UniqueName="FACTOR" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức thưởng %>" DataField="BONUSE" SortExpression="BONUSE"
                        UniqueName="BONUSE" />
                   <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" SortExpression="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                        SortExpression="REMARK" UniqueName="REMARK" />
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
