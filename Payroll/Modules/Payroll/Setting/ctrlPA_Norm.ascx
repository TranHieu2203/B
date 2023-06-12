<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Norm.ascx.vb"
    Inherits="Payroll.ctrlPA_Norm" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="Hid_IsEnter" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlPA_Formula_txtDesc {
            height: 56px;
        }
    }

    .RadGrid_Metro .rgRow > td:first-child {
        padding: 0 !important;
    }

    .RadGrid_Metro .rgAltRow > td:first-child {
        padding: 0 !important;
    }

    .RadGrid_Metro .rgHeader {
        padding: 0 !important;
    }

    #ctl00_MainContent_ctrlPA_Formula_rgData_ctl00_ctl02_ctl02_FilterTextBox_STATUS {
        display: none;
    }
</style>
<asp:HiddenField ID="hidIDEmp" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="240px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Theo phòng ban")%>
                </td>
                <td>
                    <asp:RadioButton ID="orgChk" runat="server" GroupName="group1" AutoPostBack="true"></asp:RadioButton>
                </td>
                <td class="lb">
                    <%# Translate("Theo nhân viên")%>
                </td>
                <td>
                    <asp:RadioButton ID="empChk" runat="server" GroupName="group1" AutoPostBack="true"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrgName" runat="server" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="reqOrg" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lblEmpCode" runat="server" Text="Mã nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="Họ tên nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTITLE" runat="server" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrg" runat="server" Text="Phòng ban"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại định mức")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcboNorm" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqObjSalary" ControlToValidate="rcboNorm"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại định mức %>" ToolTip="<%$ Translate: Bạn phải chọn Loại định mức %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb"><%# Translate("Giá trị")%><span class="lbReq">*</span></td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtValue" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="rtxtValue"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhóm lương %>" ToolTip="<%$ Translate: Bạn phải nhập nhóm lương %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td ID="tdYear_Seniority" runat="server" class="lb"><%# Translate("Định mức năm")%><span class="lbReq">*</span></td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxt_Year_Seniority" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                 </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpStartDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdpStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpEndDate" runat="server">
                    </tlk:RadDatePicker>
                    <%--<asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="rdpEndDate"
                        ControlToCompare="rdpStartDate" Operator="GreaterThanEqual" Type="Date"
                        ErrorMessage="Ngày kết thúc phải lớn hơn ngày hiệu lực" />--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtDesc" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" AutoPostBack="true" />
                </td>
                <td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true" AllowSorting="True">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="EMPLOYEE_ID,ID,ORG_NAME,EMP_NAME,EMP_CODE,TITLE_NAME,ORG_ID,ORG,EFFECT_DATE,EXPIRE_DATE,VALUE,YEAR_SENIORITY,NORM_ID,NOTE,NORM">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG" SortExpression="ORG" UniqueName="ORG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMP_CODE" SortExpression="EMP_CODE" UniqueName="EMP_CODE" HeaderStyle-Width="80px"  />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMP_NAME" SortExpression="EMP_NAME" UniqueName="EMP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại định mức %>" DataField="NORM" SortExpression="NORM" UniqueName="NORM" HeaderStyle-Width="250px"  />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Giá trị %>" DataField="VALUE" SortExpression="VALUE" UniqueName="VALUE"  DataFormatString="{0:N0}"/>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Định mức năm %>" DataField="YEAR_SENIORITY" SortExpression="YEAR_SENIORITY" UniqueName="YEAR_SENIORITY" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" SortExpression="NOTE" UniqueName="NOTE" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin thiết lập công thức lương%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Formula_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Formula_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Formula_RadPane2';
        var validateID = 'MainContent_ctrlPA_Formula_valSum';
        var oldSize = $('#' + pane1ID).height();
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

        function CheckValidate() {
            var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            return 0;
        }

        function OpenEdit() {
            var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_FormulaDetail&group=Setting&gUID=' + id + '', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = CheckValidate();
            var n;
            var m;
            if (bCheck == 0) {
                OpenEdit();
            }
            if (bCheck == 1) {
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            args.set_cancel(true);

        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                var grid = $find("<%=rgData.ClientID %>");
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
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }

            if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlPA_Formula_rdpStartDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlPA_Formula_rdpEndDate_dateInput').val('');
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
        }

        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        //window.addEventListener('keydown', function (e) {
        //    if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
        //        if (e.target.id !== 'ctl00_MainContent_ctrlPA_Norm_txtOrgName') {
        //            e.preventDefault();
        //            return false;
        //        }
        //    }
        //}, true);
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
