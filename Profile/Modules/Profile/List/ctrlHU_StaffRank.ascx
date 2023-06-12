<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_StaffRank.ascx.vb"
    Inherits="Profile.ctrlHU_StaffRank" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="140px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã cấp nhân sự")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="ReadOnly" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã cấp nhân sự %>" ToolTip="<%$ Translate: Bạn phải nhập mã cấp nhân sự %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã cấp nhân sự đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã cấp nhân sự đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên cấp nhân sự")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên cấp nhân sự %>" ToolTip="<%$ Translate: Bạn phải nhập tên cấp nhân sự %>"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <%# Translate("Cấp bậc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmlever" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="nmlever"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập cấp bậc%>" ToolTip="<%$ Translate: Bạn phải nhập cấp bậc%>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" Width="100%" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
        <tlk:RadGrid ID="rgMain" runat="server" AllowMultiRowSelection="True" 
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
            Height="100%" PageSize="50" CellSpacing="0" GridLines="None">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView ClientDataKeyNames="CODE,NAME,LEVEL_STAFF,REMARK" DataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" 
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" 
                        UniqueName="cbStatus">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="CODE" 
                        HeaderText="<%$ Translate: Mã cấp nhân sự %>" SortExpression="CODE" 
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn DataField="NAME" 
                        HeaderText="<%$ Translate: Tên cấp nhân sự %>" SortExpression="NAME" 
                        UniqueName="NAME" />
                    <tlk:GridNumericColumn DataField="LEVEL_STAFF" HeaderText="<%$ Translate: Cấp bậc %>" 
                        SortExpression="LEVEL_STAFF" UniqueName="LEVEL_STAFF" />
                    <tlk:GridBoundColumn DataField="REMARK" HeaderStyle-Width="150px" 
                        HeaderText="<%$ Translate: Ghi chú %>" SortExpression="REMARK" 
                        UniqueName="REMARK" />
                    <tlk:GridBoundColumn DataField="ACTFLG" 
                        HeaderText="<%$ Translate: Trạng thái %>" ShowFilterIcon="true" 
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        $(document).ready(function () {
            console.log("ready!");
//            var ul = $('#ctl00_MainContent_ctrlHU_StaffRank_tbarMain').find('div').get(0);
//            var li = $(ul).find('ul').find('li').get(3);
//            debugger;
//            $(li).find('a').click(function () {
//                var SHPname = $("#ctl00_MainContent_ctrlHU_StaffRank_txtName").val();
//                var textfieldmask = /^[a-z\.\s-]{2,}$/;
//                /// check full name 
//                if (SHPname.length > 0) {
//                    var testname = textfieldmask.test(SHPname);
//                    alert(testname);
//                    if (testname == "false") {
//                        alert("Enter the full name!");
//                        $('#fname').focus();
//                        return false;
//                    }
//                }
//            });
        });

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
