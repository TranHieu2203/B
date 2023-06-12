<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMngIO.ascx.vb"
    Inherits="Attendance.ctrlMngIO" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling=None>
                <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
     <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling=None>
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100px">
                <table class="table-form"  onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td>
                            <asp:Label ID="lblProgram" runat="server" Font-Size="Large" Font-Bold="true">&nbsp;</asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:Label ID="lblRequest" runat="server" Font-Size="Large" Text ="..." Font-Bold="true" Visible="false">&nbsp;</asp:Label>
                        </td>
                         <td class="lb" style="padding-right: 5px; padding-left: 20px">
                            <%# Translate("CM_CTRLPROCESS_SCAN_TIME")%>&nbsp;
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="RadNumTB" MinValue="1" MaxValue="30" Width="50px"
                                Value="5" MaxLength="2" AutoPostBack="true" ShowSpinButtons="True">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:Label ID="lblStatus" runat="server" Text ="..."  Font-Size="Large" CssClass="spancustomstatus"></asp:Label>
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
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="150px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" SkinID ="Readonly" runat="server" ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" SkinID ="Readonly" runat="server" ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgMngIO" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="EMPLOYEE_CODE">
                        <Columns>
                           <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh chính %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="120px" />
                                 <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" HeaderStyle-Width="120px" />
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
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
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày quẹt %>" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WORKINGDAY">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 1 %>" DataField="VALIN1" UniqueName="VALIN1"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN1" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 1 %>" DataField="VALOUT1" UniqueName="VALOUT1"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT1" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 2 %>" DataField="VALIN2" UniqueName="VALIN2"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN2" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 2 %>" DataField="VALOUT2" UniqueName="VALOUT2"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT2" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 3 %>" DataField="VALIN3" UniqueName="VALIN3"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN3" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 3 %>" DataField="VALOUT3" UniqueName="VALOUT3"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT3" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 4 %>" DataField="VALIN4" UniqueName="VALIN4"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN4" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 4 %>" DataField="VALOUT4" UniqueName="VALOUT4"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT4" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 5 %>" DataField="VALIN5" UniqueName="VALIN5"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN5" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 5 %>" DataField="VALOUT5" UniqueName="VALOUT5"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT5" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 6 %>" DataField="VALIN6" UniqueName="VALIN6"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN6" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 6 %>" DataField="VALOUT6" UniqueName="VALOUT6"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT6" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 7 %>" DataField="VALIN7" UniqueName="VALIN7"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN7" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 7 %>" DataField="VALOUT7" UniqueName="VALOUT7"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT7" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 8 %>" DataField="VALIN8" UniqueName="VALIN8"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN8" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 8 %>" DataField="VALOUT8" UniqueName="VALOUT8"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT8" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 9 %>" DataField="VALIN9" UniqueName="VALIN9"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN9" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 9 %>" DataField="VALOUT9" UniqueName="VALOUT9"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT9" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 10 %>" DataField="VALIN10" UniqueName="VALIN10"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN10" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 10 %>" DataField="VALOUT10" UniqueName="VALOUT10"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT10" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 11 %>" DataField="VALIN11" UniqueName="VALIN11"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN11" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 11 %>" DataField="VALOUT11" UniqueName="VALOUT11"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT11" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 12 %>" DataField="VALIN12" UniqueName="VALIN12"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN12" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 12 %>" DataField="VALOUT12" UniqueName="VALOUT12"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT12" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 13 %>" DataField="VALIN13" UniqueName="VALIN13"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN13" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 13 %>" DataField="VALOUT13" UniqueName="VALOUT13"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT13" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 14 %>" DataField="VALIN14" UniqueName="VALIN14"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN14" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 14 %>" DataField="VALOUT14" UniqueName="VALOUT14"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT14" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 15 %>" DataField="VALIN15" UniqueName="VALIN15"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN15" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 15 %>" DataField="VALOUT15" UniqueName="VALOUT15"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT15" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: VÀO 16 %>" DataField="VALIN16" UniqueName="VALIN16"
                                DataFormatString="{0:HH:mm}" SortExpression="VALIN16" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="<%$ Translate: RA 16 %>" DataField="VALOUT9" UniqueName="VALOUT16"
                                DataFormatString="{0:HH:mm}" SortExpression="VALOUT16" AllowFiltering="false" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
--%>
                           
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
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
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlMngIO_RadSplitter3';
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

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            window.open('/Default.aspx?mid=Attendance&fid=ctrlNewInout&group=Business&FormType=0', "_self"); /*
            oWindow.setSize(800, 480);
            oWindow.center(); */
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgMngIO.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgMngIO.ClientID %>");
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
                $find("<%= rgMngIO.ClientID %>").get_masterTableView().rebind();
            }
            $get("<%# btnSearch.ClientId %>").click();
        }
    </script>
</tlk:RadScriptBlock>
