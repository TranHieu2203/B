<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlThietLapChamCongDB.ascx.vb"
    Inherits="Attendance.ctrlThietLapChamCongDB" %>
    <link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="140px" Scrolling="None">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidPosID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 100px">
                    <%# Translate("Cấp nhân sự")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCapNhanSu" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCapNhanSu" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn cấp nhân sự. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn cấp nhân sự. %>" ClientValidationFunction="cusCapNhanSu">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalStaff" ControlToValidate="cboCapNhanSu" runat="server"
                        ErrorMessage="<%$ Translate: Cấp nhân sự đã được thiết lập. %>" ToolTip="<%$ Translate: Cấp nhân sự đã được thiết lập. %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalActiveStaff" ControlToValidate="cboCapNhanSu" runat="server" ErrorMessage="<%$ Translate: Cấp nhân sự không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Cấp nhân sự không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb" style="width: 250px">
                    <%# Translate("Số lần quẹt thẻ tối thiểu tính đủ công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboccdb">
                        <Items>
                            <tlk:RadComboBoxItem runat="server" Value="1" Text="1" />
                            <tlk:RadComboBoxItem runat="server" Value="2" Text="2" />
                            <tlk:RadComboBoxItem runat="server" Value="3" Text="3" />
                            <tlk:RadComboBoxItem runat="server" Value="4" Text="4" />
                            <tlk:RadComboBoxItem runat="server" Value="5" Text="5" />
                        </Items>
                    </tlk:RadComboBox>
                    <%# Translate("Lần/ngày")%>
                    <asp:CustomValidator ID="cusccdb" runat="server" ErrorMessage="<%$ Translate:  Bạn phải chọn số lần quẹt tối thiểu tính đủ công. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn số lần quẹt tối thiểu tính đủ công. %>"
                        ClientValidationFunction="cusccdb">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" Width="100%" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgDanhMuc" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,STAFF_RANK_ID,STAFF_RANK_NAME,NUMBER_SWIPECARD,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự%>" DataField="STAFF_RANK_NAME"
                        UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" HeaderStyle-Width="200px" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lần quẹt%>" DataField="NUMBER_SWIPECARD"
                        UniqueName="NUMBER_SWIPECARD" SortExpression="NUMBER_SWIPECARD" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả%>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" HeaderStyle-Width="400px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" HeaderStyle-Width="200px">
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlThietLapChamCongDB_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlThietLapChamCongDB_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlThietLapChamCongDB_RadPane2';
        var validateID = 'MainContent_ctrlThietLapChamCongDB_valSum';
        var oldSize = $('#' + pane1ID).height();
        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }
        function cusccdb(oSrc, args) {
            var cbo = $find("<%# cboccdb.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusCapNhanSu(oSrc, args) {
            var cbo = $find("<%# cboCapNhanSu.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        /*function ResizeSplitter() {
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
        }*/
    </script>
</tlk:RadCodeBlock>
