<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMngInfoEvalTargetDetail.ascx.vb"
    Inherits="Performance.ctrlMngInfoEvalTargetDetail" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:radpane id="LeftPane" runat="server" minwidth="200" width="250px" scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="MainPane" runat="server" scrolling="None">
        <tlk:radsplitter id="RadSplitter3" runat="server" width="100%" height="100%" orientation="Horizontal">
            <tlk:radpane id="RadPane3" runat="server" height="35px" scrolling="None">
                <tlk:radtoolbar id="tbarMngInfoEvalTarget" runat="server" onclientbuttonclicking="clientButtonClicking" />
            </tlk:radpane>
            <tlk:radpane id="RadPane1" runat="server" height="68px" scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:radcombobox id="cboYear" skinid="dDropdownList" runat="server" autopostback="true">
                            </tlk:radcombobox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%>
                        </td>
                        <td>
                            <tlk:radcombobox id="cboPeriod" skinid="dDropdownList" runat="server" autopostback="true">
                            </tlk:radcombobox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:radcombobox id="cboStatus" skinid="dDropdownList" runat="server" autopostback="true">
                            </tlk:radcombobox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:raddatepicker id="rdFromDate" runat="server" dateinput-enabled="false">
                            </tlk:raddatepicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:raddatepicker id="rdToDate" runat="server" dateinput-enabled="false">
                            </tlk:raddatepicker>
                        </td>
                        <td>
                            <tlk:radbutton id="btnSearch" runat="server" text="<%$ Translate: Tìm kiếm %>" skinid="ButtonFind">
                            </tlk:radbutton>
                        </td>
                    </tr>
                </table>
            </tlk:radpane>
            <tlk:radpane id="RadPane2" runat="server" scrolling="None">
                <tlk:radgrid pagesize="50" id="rgMngInfoEvalTarget" runat="server" height="100%"
                    allowmultirowselection="true">
                    <clientsettings enablerowhoverstyle="true">
                        <selecting allowrowselect="true" />
                        <clientevents onrowdblclick="gridRowDblClick" />
                        <clientevents ongridcreated="GridCreated" />
                        <clientevents oncommand="ValidateFilter" />
                        <scrolling allowscroll="true" usestaticheaders="true" frozencolumnscount="3" />
                    </clientsettings>
                    <mastertableview datakeynames="ID" clientdatakeynames="ID">
                        <columns>
                            <tlk:gridclientselectcolumn uniquename="cbStatus" headerstyle-horizontalalign="Center"
                                headerstyle-width="30px" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn datafield="ID" visible="false" headertext="ID" />
                            <tlk:gridboundcolumn headertext="ID Chỉ số đo lường" datafield="KPI_ASSESSMENT" visible="false"
                                readonly="true" uniquename="KPI_ASSESSMENT" sortexpression="KPI_ASSESSMENT" />
                            <tlk:gridboundcolumn headertext="Trạng thái" datafield="STATUS_NAME" headerstyle-width="100px"
                                readonly="true" uniquename="STATUS_NAME" sortexpression="STATUS_NAME" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Mã NV" datafield="EMPLOYEE_CODE" sortexpression="EMPLOYEE_CODE"
                                uniquename="EMPLOYEE_CODE" />
                            <tlk:gridboundcolumn headertext="Họ và tên" datafield="EMPLOYEE_NAME" sortexpression="EMPLOYEE_NAME"
                                uniquename="EMPLOYEE_NAME" />
                            <tlk:gridboundcolumn headertext="Chức danh" datafield="TITLE_NAME" sortexpression="TITLE_NAME"
                                uniquename="TITLE_NAME" />
                            <tlk:gridboundcolumn headertext="Phòng ban" datafield="ORG_NAME" sortexpression="ORG_NAME"
                                uniquename="ORG_NAME" />
                            <tlk:gridboundcolumn headertext="Chỉ số đo lường" datafield="KPI_ASSESSMENT_TEXT" headerstyle-width="100px"
                                readonly="true" uniquename="KPI_ASSESSMENT_TEXT" sortexpression="KPI_ASSESSMENT_TEXT" itemstyle-horizontalalign="Center" />
                            <tlk:GridBoundColumn HeaderText="Loại KPI" DataField="KPI_TYPE" HeaderStyle-Width="100px"
                                ReadOnly="true" UniqueName="KPI_TYPE" SortExpression="KPI_TYPE" ItemStyle-HorizontalAlign="Center" />
                            <tlk:gridboundcolumn headertext="Đơn vị tính" datafield="UNIT_NAME" uniquename="UNIT_NAME"
                                headerstyle-width="75px" readonly="true" sortexpression="UNIT_NAME" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Tần suất đo" datafield="FREQUENCY_NAME" uniquename="FREQUENCY_NAME"
                                headerstyle-width="75px" readonly="true" sortexpression="FREQUENCY_NAME" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Công thức tính" datafield="DESCRIPTION" uniquename="DESCRIPTION"
                                headerstyle-width="75px" readonly="true" sortexpression="DESCRIPTION" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Nguồn đo" datafield="SOURCE_NAME" uniquename="SOURCE_NAME"
                                headerstyle-width="75px" readonly="true" sortexpression="SOURCE_NAME" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Loại tiêu chí" datafield="GOAL_TYPE_NAME" uniquename="GOAL_TYPE_NAME"
                                headerstyle-width="75px" readonly="true" sortexpression="GOAL_TYPE_NAME" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Chỉ tiêu" datafield="TARGET" uniquename="TARGET"
                                headerstyle-width="75px" readonly="true" sortexpression="TARGET" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Ngưỡng" datafield="TARGET_MIN" uniquename="TARGET_MIN"
                                headerstyle-width="75px" readonly="true" sortexpression="TARGET_MIN" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Trọng số" datafield="RATIO" uniquename="RATIO"
                                headerstyle-width="75px" readonly="true" sortexpression="RATIO" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Điểm chuẩn" datafield="BENCHMARK" uniquename="BENCHMARK"
                                headerstyle-width="75px" readonly="true" sortexpression="BENCHMARK" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="NV thực hiện" datafield="EMPLOYEE_ACTUAL" uniquename="EMPLOYEE_ACTUAL"
                                headerstyle-width="75px" readonly="true" sortexpression="EMPLOYEE_ACTUAL" itemstyle-horizontalalign="Center" />
                            <tlk:gridnumericcolumn headertext="Điểm NV" datafield="EMPLOYEE_POINT" uniquename="EMPLOYEE_POINT" dataformatstring="{0:N0}"
                                headerstyle-width="75px" readonly="true" sortexpression="EMPLOYEE_POINT" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Ghi chú" datafield="NOTE" uniquename="NOTE"
                                headerstyle-width="75px" readonly="true" sortexpression="NOTE" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="QL đánh giá" datafield="DIRECT_ACTUAL" uniquename="DIRECT_ACTUAL"
                                headerstyle-width="75px" readonly="true" sortexpression="DIRECT_ACTUAL" itemstyle-horizontalalign="Center" />
                            <tlk:gridnumericcolumn headertext="Điểm đánh giá" datafield="DIRECT_POINT" uniquename="DIRECT_POINT" dataformatstring="{0:N0}"
                                headerstyle-width="75px" readonly="true" sortexpression="DIRECT_POINT" itemstyle-horizontalalign="Center" />
                            <tlk:gridboundcolumn headertext="Ghi chú của QLTT" datafield="NOTE_QLTT" uniquename="NOTE_QLTT"
                                headerstyle-width="75px" readonly="true" sortexpression="NOTE_QLTT" itemstyle-horizontalalign="Center" />
                        </columns>
                        <headerstyle width="120px" />
                    </mastertableview>
                </tlk:radgrid>
            </tlk:radpane>
        </tlk:radsplitter>
    </tlk:radpane>
</tlk:radsplitter>
<tlk:radwindowmanager id="RadWindowManager1" runat="server">
    <windows>
        <tlk:radwindow runat="server" id="rwPopup" visiblestatusbar="false" onclientclose="popupclose"
            width="1000" height="600px" enableshadow="true" behaviors="Close, Maximize, Move"
            modal="true" showcontentduringload="false">
        </tlk:radwindow>
    </windows>
</tlk:radwindowmanager>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
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
</tlk:radcodeblock>
