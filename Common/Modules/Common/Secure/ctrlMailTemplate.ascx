<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMailTemplate.ascx.vb"
    Inherits="Common.ctrlMailTemplate" %>
<%@ Import Namespace="Common" %>
    <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<%@ Register Src="../ctrlUpload.ascx" TagName="ctrlUpload" TagPrefix="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="50%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Width="100%" Height="50%">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="50%" Orientation="Vertical">
            <tlk:RadPane ID="LeftPane" runat="server" Width="100%">
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" runat="server" MaxLength="255">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập mã mẫu Email. %>" ToolTip="<%$ Translate: Bạn phải nhập mã mẫu Email. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtName" runat="server" MaxLength="255">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsMailCC" runat="server" MaxLength="255">
                            </asp:CheckBox>
                             <%# Translate("Bỏ qua CC người phê duyệt")%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tiêu đề")%>
                        </td>
                        <td colspan="8">
                            <tlk:RadTextBox ID="txtTitle" runat="server" MaxLength="255" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Cc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCC" runat="server" MaxLength="255">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhóm chức năng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGROUP" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboGROUP"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chức năng %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Nhóm chức năng %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="8">
                            <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td class="lb">
                            <%# Translate("Import từ Word")%>
                            <span class="lbReq">*</span>
                        </td>
                        <td>                            
                            <tlk:RadButton ID="btnImport" SkinID="ButtonView" runat="server" CausesValidation="false"
                                Width="40px">
                            </tlk:RadButton>                           
                        </td>
                    </tr>--%>
                </table>
                <tlk:RadGrid ID="rgGrid" runat="server" Height="73%">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,TITLE,MAIL_CC,REMARK,GROUP_MAIL,CONTENT,IS_MAIL_CC">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="GROUP_MAIL" Visible="false" />
                            <tlk:GridBoundColumn DataField="CONTENT" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã %>" DataField="CODE" UniqueName="CODE"
                                SortExpression="CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên %>" DataField="NAME" UniqueName="NAME"
                                SortExpression="NAME" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tiêu đề %>" DataField="TITLE" UniqueName="TITLE"
                                SortExpression="TITLE" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm %>" DataField="GROUP_MAIL_NAME"
                                UniqueName="GROUP_MAIL_NAME" SortExpression="GROUP_MAIL_NAME" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: CC %>" DataField="MAIL_CC" UniqueName="MAIL_CC"
                                SortExpression="MAIL_CC" HeaderStyle-Width="150px" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Bỏ qua CC cho người phê duyệt %>" DataField="IS_MAIL_CC" UniqueName="IS_MAIL_CC"
                                SortExpression="IS_MAIL_CC" HeaderStyle-Width="150px" AllowFiltering="false" ShowFilterIcon="false"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                                SortExpression="REMARK" HeaderStyle-Width="150px" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane runat="server" ID="RightPane" Scrolling="None" Width="100%" Height="100%">
                <tlk:RadEditor RenderMode="Lightweight" runat="server" ID="radEditContent" SkinID="DefaultSetOfTools"
                    Height="570px">
                </tlk:RadEditor>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {

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

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }
        function getFileName(box, id, name) {
            var gotopage = "/GetFileMLG.aspx?fp=" + box + "&fid=" + id + "&fname=" + name;
            window.open(gotopage);
        }

        function ResizeSplitter() {

        } 
    </script>
</tlk:RadCodeBlock>
