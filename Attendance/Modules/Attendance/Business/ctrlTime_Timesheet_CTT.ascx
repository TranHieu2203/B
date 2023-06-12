<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTime_Timesheet_CTT.ascx.vb"
    Inherits="Attendance.ctrlTime_Timesheet_CTT" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type="text/css" href="/Styles/StyleCustom.css" rel="Stylesheet" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    .RadGrid tr.Row50 {
        height: 30px;
    }

    .RadGrid .Row50 td {
        padding-top: 0;
        padding-bottom: 0;
        height: 30px;
        vertical-align: middle;
    }
    .rtTitlebar{
            margin: 0 auto 0 auto !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="140px" Scrolling="None">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td>
                            <asp:Label ID="lblProgram" runat="server" Font-Size="Large" Font-Bold="true">&nbsp;</asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:Label ID="lblRequest" runat="server" Font-Size="Large" Text="..." Font-Bold="true" Visible="false">&nbsp;</asp:Label>
                        </td>
                        <td class="lb" style="padding-right: 5px; padding-left: 20px">
                            <%# Translate("CM_CTRLPROCESS_SCAN_TIME")%>&nbsp;
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="RadNumTB" MinValue="1" MaxValue="30" Width="50px"
                                Value="5" MaxLength="2" AutoPostBack="true" ShowSpinButtons="True">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:Label ID="lblStatus" runat="server" Text="..." Font-Size="Large" CssClass="spancustomstatus"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" AutoPostBack="true" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboObjectEmployee" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" SkinID="Readonly" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" SkinID="Readonly" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="lb">
                            <asp:CheckBox ID="chkSummary" runat="server" Text="Tổng hợp từ bảng công gốc" />
                        </td>
                        <td></td>
                        <td>
                            <asp:CheckBox id="ckimport" Text ="Dữ liệu import"  runat="server">
                            </asp:CheckBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="ckClearCache" Text="Clear Cache" runat="server"></asp:CheckBox>
                        </td>

                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <table class="table-form">
                    <tr>
                        <%--<td>Ghi chú: </td>
                        <td>
                            <tlk:radtextbox id="txtRed" runat="server" backcolor="Red" readonly="true" width="40px">
                            </tlk:radtextbox>
                            Không có dữ liệu nào
                        </td>
                        <td>
                            <tlk:radtextbox id="txtBlue" runat="server" backcolor="LightBlue" readonly="true" width="40px">
                            </tlk:radtextbox>
                            Tăng ca
                        </td>--%>
                        <%--<td>
                            <asp:RadioButton GroupName="Color" ID="chkAll" Checked="true" runat="server" />
                            <%# Translate("All")%>
                        </td>
                        <td>
                            <tlk:radtextbox id="txtLightBlue" runat="server" backcolor="LightBlue" readonly="true"
                                width="40px">
                            </tlk:radtextbox>
                        </td>
                        <td>
                            <asp:RadioButton GroupName="Color" ID="chkBlue" runat="server" />
                            <%# Translate("Có scan + đăng ký nghỉ")%>
                        </td>
                        <td>
                            <tlk:radtextbox id="txtDarkGreen" runat="server" backcolor="DarkGreen" readonly="true"
                                width="40px">
                            </tlk:radtextbox>
                        </td>
                        <td>
                            <asp:RadioButton GroupName="Color" ID="chkGreen" runat="server" />
                            <%# Translate("Đi muộn, về sớm")%>
                        </td>
                        <td>
                            <tlk:radtextbox id="txtYellow" runat="server" backcolor="Yellow" readonly="true"
                                width="40px">
                            </tlk:radtextbox>
                        </td>
                        <td>
                            <asp:RadioButton GroupName="Color" ID="chkYellow" runat="server" />
                            <%# Translate("Khuyết dữ liệu vào ra")%>
                        </td>
                        <td>
                            <asp:RadioButton GroupName="Color" ID="chkRed" runat="server" />
                            <%# Translate("Không có dữ liệu nào")%>
                        </td>
                        <td>
                            <tlk:radtextbox id="txtRed" runat="server" backcolor="Red" readonly="true" width="40px">
                            </tlk:radtextbox>
                        </td>--%>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgTimeTimesheet_cct" AllowCustomPaging="true" AllowPaging="true" AllowSorting="True"
                    runat="server" AutoGenerateColumns="false" Height="100%" SkinID="GridAT">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                        <ClientEvents OnCellSelected="cellSelected" OnRowDblClick="OnRowDblClick"></ClientEvents>
                        <Selecting AllowRowSelect="true" CellSelectionMode="SingleCell"></Selecting>
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE" ClientDataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" Visible="false"
                                DataField="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_ID" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" SortExpression="VN_FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc chính %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="150px" SortExpression="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng chấm công %>" DataField="OBJECT_ATTENDANCE_NAME" UniqueName="OBJECT_ATTENDANCE_NAME"
                                SortExpression="OBJECT_ATTENDANCE_NAME" HeaderStyle-Width="200px" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày tính lương %>" DataField="TOTAL_DAY_SAL" UniqueName="TOTAL_DAY_SAL"
                                SortExpression="TOTAL_DAY_SAL" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày nghỉ không lương %>" DataField="TOTAL_DAY_NON_SAL" UniqueName="TOTAL_DAY_NON_SAL"
                                SortExpression="TOTAL_DAY_NON_SAL" HeaderStyle-Width="200px" />--%>
                            <tlk:GridTemplateColumn HeaderText="D1" AllowFiltering="false" Visible="false" UniqueName="D1">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D1") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D2" AllowFiltering="false" Visible="false" UniqueName="D2">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D2")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D3" AllowFiltering="false" Visible="false" UniqueName="D3">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D3")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D4" AllowFiltering="false" Visible="false" UniqueName="D4">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D4")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D5" AllowFiltering="false" Visible="false" UniqueName="D5">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D5")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D6" AllowFiltering="false" Visible="false" UniqueName="D6">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D6") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D7" AllowFiltering="false" Visible="false" UniqueName="D7">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D7")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D8" AllowFiltering="false" Visible="false" UniqueName="D8">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D8")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D9" AllowFiltering="false" Visible="false" UniqueName="D9">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D9")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D10" AllowFiltering="false" Visible="false" UniqueName="D10">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D10")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D11" AllowFiltering="false" Visible="false" UniqueName="D11">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D11")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D12" AllowFiltering="false" Visible="false" UniqueName="D12">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D12") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D13" AllowFiltering="false" Visible="false" UniqueName="D13">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D13")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D14" AllowFiltering="false" Visible="false" UniqueName="D14">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D14")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D15" AllowFiltering="false" Visible="false" UniqueName="D15">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D15") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D16" AllowFiltering="false" Visible="false" UniqueName="D16">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D16") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D17" AllowFiltering="false" Visible="false" UniqueName="D17">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D17")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D18" AllowFiltering="false" Visible="false" UniqueName="D18">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D18")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D19" AllowFiltering="false" Visible="false" UniqueName="D19">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D19")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D20" AllowFiltering="false" Visible="false" UniqueName="D20">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D20")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D21" AllowFiltering="false" Visible="false" UniqueName="D21">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D21")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D22" AllowFiltering="false" Visible="false" UniqueName="D22">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D22")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D23" AllowFiltering="false" Visible="false" UniqueName="D23">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D23")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D24" AllowFiltering="false" Visible="false" UniqueName="D24">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D24")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D25" AllowFiltering="false" Visible="false" UniqueName="D25">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D25")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D26" AllowFiltering="false" Visible="false" UniqueName="D26">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D26")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D27" AllowFiltering="false" Visible="false" UniqueName="D27">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D27")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D28" AllowFiltering="false" Visible="false" UniqueName="D28">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D28")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D29" AllowFiltering="false" Visible="false" UniqueName="D29">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D29") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D30" AllowFiltering="false" Visible="false" UniqueName="D30">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D30") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D31" AllowFiltering="false" Visible="false" UniqueName="D31">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D31")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                        </Columns>
                        <ItemStyle CssClass="Row50" />
                        <AlternatingItemStyle CssClass="Row50" />
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:Timer ID="TimerRequest" runat="server" Interval="5000" Enabled="False" EnableViewState="True">
</asp:Timer>
<asp:HiddenField runat="server" ID="hidRequestID" Value="0" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<input type="hidden" id="employeeTemp" value="" />
<input type="hidden" id="rdDay" value="" />
<input type="hidden" id="ManualCode" value="" />
<input id="StatusScanColumn" name="StatusScanColumn" type="hidden" value="" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlTime_Timesheet_CTT_RadSplitter3';
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
        var enableAjax = true;
        // open khi click sửa

        function OpenEditWindow() {
            var valueEmp = document.getElementById("employeeTemp").value;
            var valueDay = document.getElementById("rdDay").value;
            var cboperiod = $find('<%=cboPeriod.ClientID %>');
            var period_id = cboperiod.get_selectedItem().get_value();
            var valueManual = document.getElementById("ManualCode").value;
            if (document.getElementById("StatusScanColumn").value == "1" || document.getElementById("StatusScanColumn").value == "") {
                var m = 'Bạn chọn không đúng cột cần sửa';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlTime_TimeSheet_CCTEdit&group=Business&VIEW=TRUE&FormType=0&EMPLOYEE_ID=' + valueEmp + '&WORKINGDAY=' + valueDay + '&PERIOD_ID=' + period_id + '&ManualCode=' + valueManual + '&noscroll=1', "rwPopup");
            oWindow.setSize(550, 350);
            oWindow.center();
        }
        function getColumnTextByUniqueName(columnName) {
            var masterTableView = $find('<%= rgTimeTimesheet_cct.ClientID %>').get_masterTableView();
            var column = masterTableView.getColumnByUniqueName(columnName);
            var element = column.get_element();
            return element.innerHTML;
        }
        // lay chỉ số cột của grid
        function getColumnIndexByUniqueName(columnName) {
            var masterTableView = $find('<%= rgTimeTimesheet_cct.ClientID %>').get_masterTableView();
            var column = masterTableView.getColumnByUniqueName(columnName);
            var element = column.get_element();
            return element.cellIndex;
        }
        // khi seleced cell
        var titleFirst = "";
        function cellSelected(sender, eventArgs) {
            var columnName = eventArgs.get_column().get_uniqueName();
            var col = getColumnIndexByUniqueName(columnName);
            var dataItem = eventArgs.get_gridDataItem();
            var masterTableView = $find('<%= rgTimeTimesheet_cct.ClientID %>').get_masterTableView();
            var row = masterTableView.get_dataItems()[dataItem.get_itemIndexHierarchical()];
            //var cellEmployee = masterTableView.getCellByColumnUniqueName(row, "EMPLOYEE_CODE");

            var Employee = row.getDataKeyValue("EMPLOYEE_ID");

            if (col <= 3 || col == null) {
                document.getElementById("StatusScanColumn").value = "1";
            }
            else {
                document.getElementById("StatusScanColumn").value = "0";
            }
            var cell = masterTableView.getCellByColumnUniqueName(row, columnName);
            document.getElementById("employeeTemp").value = Employee;

            var combo = $find("<%= cboYear.ClientID %>");
            var year = combo.get_selectedItem().get_value()
            if (isIE() == 8) {
                titleFirst = getColumnTextByUniqueName(columnName).replace("<", "").match(/[^A-Z]+/) + "/" + year;
            } else {
                titleFirst = getColumnTextByUniqueName(columnName).split("<br>", 1) + "/" + year;
            }
            document.getElementById("rdDay").value = titleFirst;
            document.getElementById("ManualCode").value = cell.innerHTML.trim();
        }
        function isIE() {
            var myNav = navigator.userAgent.toLowerCase();
            return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
        }
        // open khi Double click vào cell
        function OnRowDblClick(sender, eventArgs) {
            //            if (document.getElementById("StatusScanColumn").value == "1") {
            //                var m = 'Bạn chọn không đúng cột cần sửa';
            //                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
            //                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            //                return;
            //            }
            //            debugger;
            //            var cboperiod = $find('<%=cboPeriod.ClientID %>');
            //            var period_id = cboperiod.get_selectedItem().get_value();
            //            var valueEmp = document.getElementById("employeeTemp").value;
            //            var valueDay = document.getElementById("rdDay").value;
            //            var valueManual = document.getElementById("ManualCode").value;
            //            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlTime_TimeSheet_CCTEdit&group=Business&VIEW=TRUE&FormType=0&EMPLOYEE_ID=' + valueEmp + '&WORKINGDAY=' + valueDay + '&PERIOD_ID=' + period_id + '&ManualCode=' + valueManual + '&noscroll=1', "rwPopup");
            //            oWindow.setSize(550, 350);
            //            oWindow.center();
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EDIT') {
                OpenEditWindow();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EXPORT_ORIGIN') {
                var grid = $find("<%=rgTimeTimesheet_cct.ClientID %>");
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
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgTimeTimesheet_cct.ClientID %>");
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
            }
        }
        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgTimeTimesheet_cct.ClientID %>").get_masterTableView().rebind();
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function postBack(url) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(url);
        }

    </script>
</tlk:RadScriptBlock>
