<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramClassRollcard.ascx.vb"
    Inherits="Training.ctrlTR_ProgramClassRollcard" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidStartTime" runat="server" />
<asp:HiddenField ID="hidEndTime" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidClassID" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin lớp học")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên lớp học")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtClassName" runat="server" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian học từ")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdClassStart" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdClassEnd" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr style="display: none">
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin điểm danh")%></b>
                    <hr />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmp" Text="Học viên"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboEmp" runat="server" SkinID="dDropdownList" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqEmp" ControlToValidate="cboEmp" runat="server"
                        ErrorMessage="Bạn phải chọn Học viên" ToolTip="Bạn phải chọn Học viên">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr style="display: none">
                <td class="lb">
                    <%# Translate("Ngày điểm danh")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdClassDate" runat="server" >
                    </tlk:RadDatePicker>
                </td>
                <td></td>
                <td><asp:CheckBox ID="chkAttend" runat="server" Text="<%$ Translate: Tham gia %>" /></td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,FULLNAME,CLASS_DATE,ATTEND,UNATTEND,REMARK,TR_CLASS_ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID,FULLNAME,CLASS_DATE,ATTEND,UNATTEND,REMARK,TR_CLASS_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                        SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" AllowFiltering="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" AllowFiltering="false"/>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày điểm danh %>" DataField="CLASS_DATE"
                        UniqueName="CLASS_DATE" SortExpression="CLASS_DATE" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Tham gia %>" DataField="ATTEND" DataType="System.Boolean" FilterControlWidth="20px"   
                        SortExpression="ATTEND" UniqueName="ATTEND" >
                        <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                    </tlk:GridCheckBoxColumn>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate:Vắng mặt %>" DataField="UNATTEND" DataType="System.Boolean" FilterControlWidth="20px"   
                        SortExpression="UNATTEND" UniqueName="UNATTEND" >
                        <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                    </tlk:GridCheckBoxColumn>--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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
                getRadWindow().close(null);
                args.set_cancel(true);
                ResizeSplitter();
            } else if (item.get_commandName() == "CANCEL") {
                // Nếu nhấn nút SAVE thì resize
                getRadWindow().close(null);
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == 'REFRESH') {
                setTimeout(function () {
                    openEditWindow();
                }, 1000);
            } else if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
}

function openEditWindow() {
    var classID = $get('<%=hidClassID.ClientID %>').value;
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramEnrollRecord&group=Business&noscroll=1&CLASS_ID=' + classID, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }
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
            $find("<%= rgMain.ClientID %>").get_masterTableView().rebind();
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

        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
    </script>
</tlk:RadCodeBlock>
