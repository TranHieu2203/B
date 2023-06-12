<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlATTimeManual.ascx.vb"
    Inherits="Attendance.ctrlATTimeManual" %>
<%@ Import Namespace="Common" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<style>
    .controls-table{
        display: table;
        height: 60px;
    }
    .controls-table tbody{
        display: table-cell;
    }
    #MainContent_ctrlATTimeManual_chkIsOther{
        text-align: 15px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="130px" >
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <div style="display:flex">
        <table class="table-form" style="height: 60px">
            <tr>
                <td class="lb">
                    <%# Translate("Mã kiểu công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server" Width = "180px">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã kiểu công đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã kiểu công đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã kiểu công. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã kiểu công KH")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCodeKH" SkinID="Textbox50" runat="server" Width = "180px">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCodeKH" ControlToValidate="txtCodeKH" runat="server" ErrorMessage="<%$ Translate: Mã kiểu công KH đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã kiểu công KH đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalCodeKH2" ControlToValidate="txtCodeKH" runat="server" ErrorMessage="<%$ Translate: Mã kiểu công KH không được có khoảng trắng. %>"
                        ToolTip="<%$ Translate: Mã kiểu công KH không được có khoảng trắng. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="reqCodeKH" ControlToValidate="txtCodeKH"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã kiểu công KH. %>"></asp:RequiredFieldValidator>
                </td>   
                <td class="lb">
                    <%# Translate("Tên kiểu công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server" Width = "180px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên kiểu công. %>"></asp:RequiredFieldValidator>
                </td>     
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("KC nửa ngày đầu")%><span class="lbReq">*</span>
                </td>               
                <td>
                     <tlk:RadComboBox ID="cboMorningRate" runat="server" Width = "55px">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalMorningRate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỷ lệ kiểu công nữa ngày đầu. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn loại xử lý. %>" ClientValidationFunction="cvalMorningRate">
                    </asp:CustomValidator>     

                    <tlk:RadComboBox ID="cboMorning" runat="server" Width="120px">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusMorning" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn kiểu công nửa ngày. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn kiểu công nửa ngày. %>" ClientValidationFunction="cusMorning">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalMorning" ControlToValidate="cboMorning" runat="server" ErrorMessage="<%$ Translate: Kiểu công nửa ngày không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Kiểu công nửa ngày không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("KC nửa ngày cuối")%><span class="lbReq">*</span>
                </td>                
                <td>
                    <tlk:RadComboBox ID="cboAfternoonRate" runat="server"  Width = "55px">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalAfternoonRate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỷ lệ kiểu công nữa ngày cuối. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tỷ lệ kiểu công nữa ngày cuối. %>" ClientValidationFunction="cvalAfternoonRate">
                    </asp:CustomValidator>   


                    <tlk:RadComboBox ID="cboAfternoon" runat="server" Width="120px">
                    </tlk:RadComboBox>
                     <asp:CustomValidator ID="cusAfternoon" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn kiểu công nửa ngày. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn kiểu công nửa ngày. %>" ClientValidationFunction="cusAfternoon">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalAfternoon" ControlToValidate="cboAfternoon" runat="server" ErrorMessage="<%$ Translate: Kiểu công nửa ngày không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Kiểu công nửa ngày không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb" style="display:none">
                    <%# Translate("Công ty")%>
                </td>
                <td style="display:none">
                    <tlk:RadComboBox ID="cboCongTy" runat="server">
                    </tlk:RadComboBox>
                   <%--<asp:CustomValidator ID="cvalCongTy" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn công ty. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn công ty. %>" ClientValidationFunction="cvalCongTy">
                    </asp:CustomValidator>   --%>
                </td>
                <td class="lb">
                    <%# Translate("Mapping code")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMappingCode" runat="server" Width = "180px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqMappingCode" ControlToValidate="txtMappingCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Mapping code. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalMappingCode" ControlToValidate="txtMappingCode" runat="server" ErrorMessage="<%$ Translate: Mapping code đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mapping code đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>  
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại xử lý")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTypeProcess" runat="server" Width="135px">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalTypeProcess" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại xử lý. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn loại xử lý. %>" ClientValidationFunction="cvalTypeProcess">
                    </asp:CustomValidator>                 
                </td>  
                <td class="lb">
                    <%# Translate("ĐK đi công  tác")%>
                    <asp:CheckBox ID="chkIsOther" runat="server"/> 
                </td>  
                <td class="lb">
                    <%# Translate("ĐK nghỉ theo ca")%>
                    <asp:CheckBox ID="chkIsRegShift" runat="server"/> 
                </td>
                <td class="lb">
                    <%# Translate("Thứ tự")%>        
                </td> 
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="0" Width="60px"
                        Value="1" ShowSpinButtons="True" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>     
                </td>
            </tr>
            <%--<tr>
                <td class="lb">
                    <%# Translate("Hưởng công cơm")%>
                </td>
                <td>
                     <asp:CheckBox ID="chkIsPrice" runat="server" />
                </td>
                 <td class="lb">
                    <%# Translate("Giới hạn số ngày tối đa /lần")%>
                </td>
                <td>
                     <tlk:RadNumericTextBox ID="rdLimitDay" runat="server" />
                </td>
                 <td class="lb">
                    <%# Translate("Giới hạn số ngày tối đa /năm")%>
                </td>
                <td>
                     <tlk:RadNumericTextBox ID="rdLimitYear" runat="server" />
                </td>
            </tr>--%>
        </table>
        <table class="table-form controls-table" style="height: 60px">
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3" style="width: 30vw">
                    <tlk:RadTextBox ID="rdNote" runat="server" SkinID="Textbox1023" Width="100%" Height="50px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,IS_REG_SHIFT,CODE,NAME_VN,MORNING_ID,AFTERNOON_ID,TYPE_PROSS_ID,MORNING_RATE_ID,AFTERNOON_RATE_ID,ORG_ID,ORDERS,ACTFLG,NOTE,IS_OTHER,CODE_KH,MAPPING_CODE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã kiểu công %>" DataField="CODE" HeaderStyle-Width="80px"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã kiểu công KH %>" DataField="CODE_KH" HeaderStyle-Width="80px"
                        UniqueName="CODE_KH" SortExpression="CODE_KH" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="ORG_NAME" Visible="false"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên kiểu công %>" DataField="NAME_VN" HeaderStyle-Width="240px"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                   <tlk:GridCheckBoxColumn HeaderText="Đăng ký nghỉ theo ca" DataField="IS_REG_SHIFT" UniqueName="IS_REG_SHIFT"  AllowFiltering="false"
                        SortExpression="IS_REG_SHIFT" HeaderStyle-Width="100px" />
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: ĐK công tác %>" UniqueName="IS_OTHER" DataField="IS_OTHER" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIS_OTHER" Checked='<%# ParseBoolean(Eval("IS_OTHER").ToString()) %>' 
                                Text="" runat="server" AutoPostBack="false" CausesValidation="false" Enabled="false"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ lệ công nửa ngày đầu %>" DataField="MORNING_RATE_VALUE" HeaderStyle-Width="90px"
                        UniqueName="MORNING_RATE_VALUE" SortExpression="MORNING_RATE_VALUE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KC nửa ngày đầu %>" DataField="MORNING_NAME" HeaderStyle-Width="240px"
                        UniqueName="MORNING_NAME" SortExpression="MORNING_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ lệ công nửa ngày cuối %>" DataField="AFTERNOON_RATE_VALUE" HeaderStyle-Width="90px"
                        UniqueName="AFTERNOON_RATE_VALUE" SortExpression="AFTERNOON_RATE_VALUE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: KC nửa ngày cuối %>" DataField="AFTERNOON_NAME" HeaderStyle-Width="240px"
                        UniqueName="AFTERNOON_NAME" SortExpression="AFTERNOON_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại xử lý %>" DataField="TYPE_PROCESS_NAME"
                        UniqueName="TYPE_PROCESS_NAME" SortExpression="TYPE_PROCESS_NAME" /> 
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mapping code %>" DataField="MAPPING_CODE"
                        UniqueName="MAPPING_CODE" SortExpression="MAPPING_CODE" /> 
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thứ tự sắp xếp %>" DataField="ORDERS"
                        UniqueName="ORDERS" SortExpression="ORDERS" />                   
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                         <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hưởng công cơm %>" AllowFiltering="false"
                                DataField="IS_PAID_RICE" SortExpression="IS_PAID_RICE" UniqueName="IS_PAID_RICE" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới hạn số ngày tối đa /lần %>" DataField="LIMIT_DAY"
                        UniqueName="LIMIT_DAY" SortExpression="LIMIT_DAY" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới hạn số ngày tối đa /năm %>" DataField="LIMIT_YEAR"
                        UniqueName="LIMIT_YEAR" SortExpression="LIMIT_YEAR" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlATTimeManual_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlATTimeManual_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlATTimeManual_RadPane2';
        var validateID = 'MainContent_ctrlATTimeManual_valSum';
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
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                var grid = $find("<%=rgDanhMuc.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
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
        function cusMorning(oSrc, args) {
            var cbo = $find("<%# cboMorning.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusAfternoon(oSrc, args) {
            var cbo = $find("<%# cboAfternoon.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cvalTypeProcess(oSrc, args) {
            var cbo = $find("<%# cboTypeProcess.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cvalMorningRate(oSrc, args) {
            var cbo = $find("<%# cboMorningRate.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cvalAfternoonRate(oSrc, args) {
            var cbo = $find("<%# cboAfternoonRate.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cvalCongTy(oSrc, args) {
            var cbo = $find("<%# cboCongTy.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

    </script>
</tlk:RadCodeBlock>
