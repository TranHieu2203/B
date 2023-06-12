<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramEnrollRecord.ascx.vb"
    Inherits="Training.ctrlTR_ProgramEnrollRecord" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidStartTime" runat="server" />
<asp:HiddenField ID="hidEndTime" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="85px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidClassID" />
        <table class="table-form">
             <tr>
                <td class="lb">
                    <%# Translate("Ngày điểm danh")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdClassDate" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqClassDate" ControlToValidate="rdClassDate"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn ngày điểm danh %>"
                        ToolTip="<%$ Translate: Chưa chọn ngày điểm danh %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true" AllowMultiRowEdit="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,ATTEND,UNATTEND,REMARK,TR_CLASS_ID" EditMode="InPlace" ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID,FULLNAME,ATTEND,UNATTEND,REMARK,TR_CLASS_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" >
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                        SortExpression="EMPLOYEE_CODE" ReadOnly="true"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME" ReadOnly="true"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" ReadOnly="true" AllowFiltering="false"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ReadOnly="true" AllowFiltering="false"/>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Tham gia %>" DataField="ATTEND" DataType="System.Boolean" FilterControlWidth="20px"   
                        SortExpression="ATTEND" UniqueName="ATTEND" >
                        <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                    </tlk:GridCheckBoxColumn>--%>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Tham gia %>" UniqueName="ATTEND" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ATTEND")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkAttend" runat="server"
                                CausesValidation="false" AutoPostBack="true" Width="100%" OnCheckedChanged="chkAttend_CheckedChanged">
                            </asp:CheckBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Ghi chú %>" UniqueName="REMARK" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "REMARK")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadTextBox ID="txtRemark" runat="server"
                                CausesValidation="false" AutoPostBack="true" Width="100%" OnTextChanged="txtRemark_TextChanged">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
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
                setTimeout(function () {
                    getRadWindow().close(null);
                    args.set_cancel(true);
                }, 200);
            } else if (item.get_commandName() == "CANCEL") {
                // Nếu nhấn nút SAVE thì resize
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }
        function rgMainEditRadGridDeSelecting() { }
        function rgMainRadGridDeSelecting() { }
        function rgMainEditOnClientRowSelected() { }
        function rgMainEditRadGridSelecting() { }
        function rgMainOnClientRowSelected() { }
        function rgMainRadGridSelecting() { }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
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

        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
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
