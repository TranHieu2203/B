<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGetSignDefault.ascx.vb"
    Inherits="Attendance.ctrlGetSignDefault" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidJoinDate" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:ValidationSummary ID="valSum" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpCode" AutoPostBack="true" runat="server" Width="130px">
                                <ClientEvents OnKeyPress="OnKeyPress" />  
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmpCode"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>"></asp:RequiredFieldValidator>
                            <tlk:RadButton Width="100" ID="btnChooseEmployee" runat="server" CausesValidation="false"
                                SkinID="ButtonView">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpName" SkinID="ReadOnly" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtOrg" SkinID="ReadOnly" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Vị trí công việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitle" SkinID="ReadOnly" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca T2")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSign" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSign" ControlToValidate="cboSign" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca thứ 2. %>" ToolTip="<%$ Translate: Bạn phải chọn ca thứ 2. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSign" runat="server" ControlToValidate="cboSign" ErrorMessage="<%$ Translate: Giờ ra ca Chủ nhật và giờ vào ca Thứ 2 nhỏ hơn 12 tiếng!! %>"
                                ToolTip="<%$ Translate: Giờ ra ca Chủ nhật và giờ vào ca Thứ 2 nhỏ hơn 12 tiếng! %>">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca T3")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignTue" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSingTue" ControlToValidate="cboSignTue" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca thứ 3. %>" ToolTip="<%$ Translate: Bạn phải chọn ca thứ 3. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSingTue" runat="server" ControlToValidate="cboSignTue"
                                ErrorMessage="<%$ Translate: Giờ ra ca Thứ 2 và giờ vào ca Thứ 3 nhỏ hơn 12 tiếng!! %>"
                                ToolTip="<%$ Translate: Giờ ra ca Thứ 2 và giờ vào ca Thứ 3 nhỏ hơn 12 tiếng!! %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực từ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server" AutoPostBack="true">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdFromDate"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực đến")%>
                            <%--<span class="lbReq">*</span>--%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdToDate"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hết hiệu lực. %>"></asp:RequiredFieldValidator>--%>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                                ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                                Type="Date" Operator="GreaterThan" ControlToCompare="rdFromDate" ControlToValidate="rdToDate"></asp:CompareValidator>
                            <asp:CustomValidator ID="cvalEffedate" runat="server" ErrorMessage="<%$ Translate: Khoảng thời gian hiệu lực bị trùng. %>"
                                ToolTip="<%$ Translate: Khoảng thời gian hiệu lực bị trùng. %>">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca T4")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignWed" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignWed" ControlToValidate="cboSignWed" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca thứ 4. %>" ToolTip="<%$ Translate: Bạn phải chọn ca thứ 4. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSignWed" runat="server" ControlToValidate="cboSignWed"
                                ErrorMessage="<%$ Translate: Giờ ra ca Thứ 3 và giờ vào ca Thứ 4 nhỏ hơn 12 tiếng!! %>"
                                ToolTip="<%$ Translate: Giờ ra ca Thứ 3 và giờ vào ca Thứ 4 nhỏ hơn 12 tiếng!!. %>">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca T5")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignThu" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignThu" ControlToValidate="cboSignThu" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca thứ 5. %>" ToolTip="<%$ Translate: Bạn phải chọn ca thứ 5. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSignThu" runat="server" ControlToValidate="cboSignThu"
                                ErrorMessage="<%$ Translate: Giờ ra ca Thứ 4 và giờ vào ca Thứ 5 nhỏ hơn 12 tiếng!! %>"
                                ToolTip="<%$ Translate: Giờ ra ca Thứ 4 và giờ vào ca Thứ 5 nhỏ hơn 12 tiếng!! %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca T6")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignFri" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignFri" ControlToValidate="cboSignFri" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca thứ 6. %>" ToolTip="<%$ Translate: Bạn phải chọn ca thứ 6. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSignFri" runat="server" ControlToValidate="cboSignFri"
                                ErrorMessage="<%$ Translate: Giờ ra ca Thứ 5 và giờ vào ca Thứ 6 nhỏ hơn 12 tiếng!! %>"
                                ToolTip="<%$ Translate: Giờ ra ca Thứ 5 và giờ vào ca Thứ 6 nhỏ hơn 12 tiếng!! %>">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca T7")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignSat" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignSat" ControlToValidate="cboSignSat" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca T7. %>" ToolTip="<%$ Translate: Bạn phải chọn ca T7. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSignSat" runat="server" ControlToValidate="cboSignSat"
                                ErrorMessage="<%$ Translate: Giờ ra ca Thứ 6 và giờ vào ca Thứ 7 nhỏ hơn 12 tiếng!! %>"
                                ToolTip="<%$ Translate: Giờ ra ca Thứ 6 và giờ vào ca Thứ 7 nhỏ hơn 12 tiếng!! %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca CN")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignSun" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignSun" ControlToValidate="cboSignSun" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca CN. %>" ToolTip="<%$ Translate: Bạn phải chọn ca CN. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalSignSun" runat="server" ControlToValidate="cboSignSun"
                                ErrorMessage="<%$ Translate: Giờ ra ca Thứ 7 và giờ vào ca Chủ nhật nhỏ hơn 12 tiếng!! %>"
                                ToolTip="<%$ Translate: Giờ ra ca Thứ 7 và giờ vào ca Chủ nhật nhỏ hơn 12 tiếng!! %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgWorkschedule" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC,JOIN_DATE" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,JOIN_DATE,
                                TITLE_ID,TITLE_NAME,ORG_ID,ORG_NAME,EFFECT_DATE_FROM,EFFECT_DATE_TO,SINGDEFAULE,NOTE,SING_SAT,SING_SUN,SIGN_TUE,SIGN_WED,SIGN_THU,SIGN_FRI">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                        SortExpression="ORG_NAME">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle Width="200px" />
                                    </tlk:GridBoundColumn>--%>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                    </asp:Label>
                                    <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                        RelativeTo="Element" Position="BottomCenter">
                                        <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                    </tlk:RadToolTip>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T2%>" DataField="SINGDEFAULF_NAME"
                                UniqueName="SINGDEFAULF_NAME" SortExpression="SINGDEFAULF_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T3%>" DataField="SIGN_TUE_NAME"
                                UniqueName="SIGN_TUE_NAME" SortExpression="SIGN_TUE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T4%>" DataField="SIGN_WED_NAME"
                                UniqueName="SIGN_WED_NAME" SortExpression="SIGN_WED_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T5%>" DataField="SIGN_THU_NAME"
                                UniqueName="SIGN_THU_NAME" SortExpression="SIGN_THU_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T6%>" DataField="SIGN_FRI_NAME"
                                UniqueName="SIGN_FRI_NAME" SortExpression="SIGN_FRI_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca  T7%>" DataField="SING_SAT_NAME"
                                UniqueName="SING_SAT_NAME" SortExpression="SING_SAT_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca CN%>" DataField="SING_SUN_NAME"
                                UniqueName="SING_SUN_NAME" SortExpression="SING_SUN_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực từ %>" DataField="EFFECT_DATE_FROM"
                                UniqueName="EFFECT_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE_FROM" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="120px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực đến %>" DataField="EFFECT_DATE_TO"
                                UniqueName="EFFECT_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE_TO" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="120px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                UniqueName="ACTFLG" SortExpression="ACTFLG">
                                <HeaderStyle Width="70px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose2" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlGetSignDefault_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlGetSignDefault_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlGetSignDefault_RadPane2';
        var validateID = 'MainContent_ctrlGetSignDefault_valSum';
        var oldSize = $('#' + pane1ID).height();

        var txtBoxName = 'ctl00_MainContent_ctrlGetSignDefault_txtEmpName_wrapper';
        var txtBoxTitle = 'ctl00_MainContent_ctrlGetSignDefault_txtTitle_wrapper';

        var enableAjax = true;

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
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    $('#' + txtBoxName + ',' + '#' + txtBoxTitle).css("width", "100%");
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgWorkschedule.ClientID %>");
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
            } else if (item.get_commandName() == "COPY") {
                var bCheck = $find('<%= rgWorkschedule.ClientID%>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                        var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        args.set_cancel(true);
                    } else {
                        OpenCopyWindow();
                        args.set_cancel(true);
                    }
            }
}

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusSign(oSrc, args) {
            var cbo = $find("<%# cboSign.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlGetSignDefault_rdFromDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlGetSignDefault_rdToDate_dateInput').val('');
        }
        function OnKeyPress(sender, eventArgs) 
        { 
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";   
           }      
        }
        function OpenCopyWindow() {
            var grid = $find('<%# rgWorkschedule.ClientID%>');
            var empID = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var ID = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlCopySignDefault&group=Setting&ID=' + ID + '&EMPID=' + empID + '&noscroll=1', "rwPopup");
            oWindow.setSize(350, 150);
            oWindow.center();
        }
        function OnClientClose2(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgWorkschedule.ClientID%>").get_masterTableView().rebind();
            }
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
