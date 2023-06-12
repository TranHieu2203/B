<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMngInfoEvalTarget.ascx.vb"
    Inherits="Performance.ctrlMngInfoEvalTarget" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMngInfoEvalTarget" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="68px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái KQĐG")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStatus" SkinID="dDropdownList" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        
                        <td>
                            <asp:CheckBox ID="chkMultiCriteria" runat="server" Text="NV có nhiều mục tiêu đánh giá"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server" DateInput-Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server" DateInput-Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMngInfoEvalTarget" runat="server" Height="100%"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,RATIO,EMPLOYEE,PE_PERIOD_ID,STATUS_ID,PORTAL_ID" ClientDataKeyNames="ID,RATIO,EMPLOYEE,PE_PERIOD_ID,STATUS_ID,PORTAL_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn DataField="ID" Visible="false" HeaderText="ID" />
                            <tlk:GridBoundColumn HeaderText="Mã NV" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Họ và tên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Tên kì đánh giá" DataField="PE_PERIOD_NAME" SortExpression="PE_PERIOD_NAME"
                                UniqueName="PE_PERIOD_NAME" />
                            <tlk:GridBoundColumn HeaderText="Từ ngày" DataField="START_DATE" SortExpression="START_DATE"
                                UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="Đến ngày" DataField="END_DATE" SortExpression="END_DATE"
                                UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="Ngày bắt đầu" DataField="EFFECT_DATE" SortExpression="EFFECT_DATE"
                                UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="Số tháng đánh giá" DataField="NUMBER_MONTH" SortExpression="NUMBER_MONTH"
                                UniqueName="NUMBER_MONTH" />
                            <tlk:GridBoundColumn HeaderText="Điểm đánh giá" DataField="EVALUATION_POINTS" SortExpression="EVALUATION_POINTS"
                                UniqueName="EVALUATION_POINTS" />
                            <tlk:GridBoundColumn HeaderText="Xếp loại" DataField="CLASSIFICATION" SortExpression="CLASSIFICATION"
                                UniqueName="CLASSIFICATION" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />

                            
                            <%--<tlk:GridBoundColumn HeaderText="Tỉ trọng" DataField="RATIO" SortExpression="RATIO"
                                UniqueName="RATIO" />--%>

                            <tlk:GridTemplateColumn HeaderText="Tỉ trọng" HeaderStyle-Width="100px"
                                UniqueName="RATIO">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="txtRATIO" Text='<%# (Eval("RATIO"))%>' runat="server" SkinID="Decimal" Enabled="false" Width="90px" NumberFormat-DecimalDigits="1">
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>


                            <tlk:GridBoundColumn HeaderText="Lý do đánh giá" DataField="GOAL" SortExpression="GOAL"
                                UniqueName="GOAL" />
                            <tlk:GridCheckBoxColumn HeaderText="ĐK từ Portal" DataField="IS_FROM_PORTAL" UniqueName="IS_FROM_PORTAL"
                                SortExpression="IS_FROM_PORTAL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                             <tlk:GridBoundColumn HeaderText="Trạng thái KPI" DataField="IS_CONFIRM_STT" SortExpression="IS_CONFIRM_STT"
                                UniqueName="IS_CONFIRM_STT" />
                            <tlk:GridBoundColumn HeaderText="Lý do không phê duyệt KPI" DataField="REASON_CONFIRM" SortExpression="REASON_CONFIRM"
                                UniqueName="REASON_CONFIRM" />

                            <tlk:GridBoundColumn HeaderText="Trạng thái KQĐG" DataField="STATUS_NAME" SortExpression="STATUS_NAME"
                                UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="Lý do không phê duyệt KQĐG" DataField="REASON" SortExpression="REASON"
                                UniqueName="REASON" />
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" OnClientClose="popupclose"
            Width="1000" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlMngInfoEvalTarget_RadSplitter1');
        }

        function OpenNew() {
            window.open('/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTargetNewEdit&group=Business&FormType=0', "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenEdit() {
            var id = $find('<%= rgMngInfoEvalTarget.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            window.open('/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTargetNewEdit&group=Business&FormType=1&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenView() {
            var id = $find('<%= rgMngInfoEvalTarget.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            window.open('/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTargetNewEdit&group=Business&FormType=2&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = CheckValidate();
            var n;
            var m;
            if (bCheck != 1) {
                OpenEdit();
            }
            if (bCheck == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }

        }

        function clientButtonClicking(sender, args) {
            var bCheck;
            var n;
            var m;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "SYNC") {
                window.open('/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTargetDetail&group=Business', "_self");
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = CheckValidate();
                if (bCheck == 0) {
                    OpenEdit();
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                if (bCheck == 2) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == "DELETE") {
                bCheck = CheckValidate();

                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
            }
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgMngInfoEvalTarget.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            if (bCheck > 1) {
                return 2;
            }
            return 0;
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
