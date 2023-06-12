<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramClass.ascx.vb"
    Inherits="Training.ctrlTR_ProgramClass" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="320px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidProgramID" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chương trình đào tạo")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã chương trình")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtProgramName" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCourse" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdfrmdate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdtodate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin lớp học")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên lớp")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên lớp %>" ToolTip="<%$ Translate: Bạn phải nhập Tên lớp %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian học từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian học từ %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian học từ %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdEndDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian học đến %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian học đến %>">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdEndDate"
                        ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Thời gian học đến phải lớn hơn Thời gian học từ %>"
                        ToolTip="<%$ Translate: Thời gian học đến ngày phải lớn hơn Thời gian học từ %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tổng số ngày")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmTotalDay" runat="server" Enabled="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giảng viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTeacher" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" DropDownAutoWidth="Enabled" CheckedItemsTexts="DisplayAllInInput">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqTeacher" ControlToValidate="cboTeacher" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Giảng viên. %>" ToolTip="<%$ Translate: Bạn phải chọn Giảng viên. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Từ giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="rtTimeFrom" runat="server" ShowPopupOnFocus="true" DateInput-CausesValidation="false"
                        AutoPostBack="true">
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rtTimeFrom"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Từ giờ %>" ToolTip="<%$ Translate: Bạn phải nhập Từ giờ %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="rtTimeTo" runat="server" ShowPopupOnFocus="true" DateInput-CausesValidation="false"
                        AutoPostBack="true">
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rtTimeTo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Đến giờ %>" ToolTip="<%$ Translate: Bạn phải nhập Đến giờ %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tổng số giờ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmTotalTime" runat="server" Enabled="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Trọng số")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnRATIO" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rnRATIO"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập trọng số %>" ToolTip="<%$ Translate: Bạn phải nhập trọng số %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Địa chỉ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtAddress" runat="server" MaxLength="1023">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tỉnh/Thành phố")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Quận/Huyện")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboDictrict">
                    </tlk:RadComboBox>
                </td>
                
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="7">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung email")%>
                </td>
                <td colspan="7">
                    <tlk:RadTextBox ID="txtMailContent" runat="server" SkinID="Textbox1023" Width="100%">
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
            <MasterTableView DataKeyNames="ID,TEACHER_ID" ClientDataKeyNames="ID,TR_PROGRAM_ID,START_DATE,END_DATE,DISTRICT_ID,DISTRICT_NAME,PROVINCE_ID,PROVINCE_NAME,ADDRESS,NAME,TIME_FROM,TIME_TO,TOTAL_DAY,TOTAL_TIME,REMARK,TEACHER_ID,TEACHER_NAME,EMAIL_CONTENT,RATIO">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên lớp %>" DataField="NAME" UniqueName="NAME"
                        SortExpression="NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Trọng số %>" DataField="RATIO"
                        UniqueName="RATIO" SortExpression="RATIO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giảng viên %>" DataField="TEACHER_NAME"
                        UniqueName="TEACHER_NAME" SortExpression="TEACHER_NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                        UniqueName="START_DATE" SortExpression="START_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                        UniqueName="END_DATE" SortExpression="END_DATE" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số ngày %>" DataField="TOTAL_DAY"
                        UniqueName="TOTAL_DAY" SortExpression="TOTAL_DAY" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ giờ %>" DataField="TIME_FROM"
                        UniqueName="TIME_FROM" SortExpression="TIME_FROM" DataFormatString="{0:HH:mm}" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến giờ %>" DataField="TIME_TO"
                        UniqueName="TIME_TO" SortExpression="TIME_TO" DataFormatString="{0:HH:mm}" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ %>" DataField="TOTAL_TIME"
                        UniqueName="TOTAL_TIME" SortExpression="TOTAL_TIME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ %>" DataField="ADDRESS" UniqueName="ADDRESS"
                        SortExpression="ADDRESS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉnh/Thành phố %>" DataField="PROVINCE_NAME"
                        UniqueName="PROVINCE_NAME" SortExpression="PROVINCE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận/Huyện %>" DataField="DISTRICT_NAME"
                        UniqueName="DISTRICT_NAME" SortExpression="DISTRICT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung email %>" DataField="EMAIL_CONTENT" UniqueName="EMAIL_CONTENT"
                        SortExpression="EMAIL_CONTENT" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Height="50px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadButton ID="btnSchedule" runat="server" Text="<%$ Translate: 1. Lên lịch học %>"
                        OnClientClicking="btnScheduleClick" AutoPostBack="false" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnStudent" runat="server" Text="<%$ Translate: 2. Danh sách học viên %>"
                        OnClientClicking="btnStudentClick" AutoPostBack="false" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnClassRoll" runat="server" Text="<%$ Translate: 3. Điểm danh %>"
                        OnClientClicking="btnClassRoll" AutoPostBack="false" CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
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
<asp:PlaceHolder ID="phFindPrepare" runat="server"></asp:PlaceHolder>
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

            if (args.get_item().get_commandName() == "EDIT") {
                var bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                if (bCheck > 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
            }


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
                $find("<%= rgMain.ClientID %>").get_masterTableView().rebind();
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
                pane2.set_height(splitter.get_height() - oldSize - 50 - 1);
            }
        }


        function btnStudentClick(sender, args) {
            var bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var program_id = $find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('TR_PROGRAM_ID');
            var id = $find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramClassStudent&group=Business&noscroll=1&CLASS_ID=' + id + '&TR_PROGRAM_ID=' + program_id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }
        function btnScheduleClick(sender, args) {

            var bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramClassSchedule&group=Business&noscroll=1&CLASS_ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }
        function btnClassRoll(sender, args) {

            var bCheck = $find('<%# rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramClassRollcard&group=Business&noscroll=1&CLASS_ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }
        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }

    </script>
</tlk:RadCodeBlock>
