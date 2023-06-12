<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommendFormula.ascx.vb" Inherits="Profile.ctrlHU_CommendFormula" %>
<%@ Import Namespace="Framework.UI" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="192px" Scrolling="Both">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Loại khen thưởng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbCommendList" runat="server" AutoPostBack="true" CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="CusTYPE" runat="server" ControlToValidate="cbCommendList"  ErrorMessage="<%$ Translate: Bạn phải chọn Loại khen thưởn %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại khen thưởn %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalTYPE" ControlToValidate="cbCommendList" runat="server" ErrorMessage="<%$ Translate: Loại khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50" ReadOnly="true">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã công thức khen thưởng %>" ToolTip="<%$ Translate: Bạn phải nhập Mã công thức khen thưởng %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã công thức khen thưởng đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã công thức khen thưởng đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên công thức khen thưởng %>" ToolTip="<%$ Translate: Bạn phải nhập Tên công thức khen thưởng %>">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtNameVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thứ tự tính thưởng")%><span class="lbReq">*</span>
                </td>
                <td> 
                    <tlk:RadNumericTextBox ID="nmNumberOrder" MinValue="0" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="cvalNumberOrder" ControlToValidate="nmNumberOrder"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập thứ tự tính thưởng. %>" ToolTip="<%$ Translate: Bạn phải nhập thứ tự tính thưởng. %>">
                    </asp:RequiredFieldValidator>
                </td>     
            </tr>           
             <tr>
                <td class="lb">
                    <%# Translate("Công thức")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtFormula" runat="server" SkinID="Textbox1023" Width="100%" TextMode ="MultiLine">
                    </tlk:RadTextBox>                 
                </td>              
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%" TextMode ="MultiLine">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <%-- <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>--%>
            <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,COMMENDLIST_ID,FORMULA,NUMBER_ORDER,REMARK,COMMENDOBJECT_NAME,COMMENDTYPE_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Công thức %>" DataField="FORMULA"
                         UniqueName="FORMULA" SortExpression="FORMULA" >
                         <HeaderStyle Width="400px" />
                    </tlk:GridBoundColumn> 
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại khen thưởng %>" DataField="COMMENDLIST_NAME"
                        UniqueName="COMMENDLIST_NAME" SortExpression="COMMENDLIST_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại DM Khen thưởng %>" DataField="COMMENDTYPE_NAME"
                        UniqueName="COMMENDTYPE_NAME" SortExpression="COMMENDTYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng khen thưởng %>" DataField="COMMENDOBJECT_NAME"
                         UniqueName="COMMENDOBJECT_NAME" SortExpression="COMMENDOBJECT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Độ ưu tiên %>" DataField="NUMBER_ORDER"
                        UniqueName="NUMBER_ORDER" SortExpression="NUMBER_ORDER" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                </Columns>
            </MasterTableView>
             <HeaderStyle Width="120px" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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
       /* function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }*/
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var height2 = splitter.get_height() - height;
                splitter.set_height(splitter.get_height() + pane.get_height() + pane2.get_height() - height - height2);
                pane.set_height(height);
                pane2.set_height(height2);
                //alert(height, height2, splitter.get_height() + pane.get_height() + pane2.get_height() - height - height2);
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
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

    </script>
</tlk:RadCodeBlock>