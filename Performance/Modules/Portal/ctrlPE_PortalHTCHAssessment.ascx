﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_PortalHTCHAssessment.ascx.vb"
    Inherits="Performance.ctrlPE_PortalHTCHAssessment
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" CheckBoxes="All" CheckChildNodes="true" AutoPostBack="false" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="70px" Scrolling="None">
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
                            <asp:Label runat="server" ID="lbStatus" Text='<%# Translate("Trạng thái")%>'></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMngInfoEvalTarget" runat="server" Height="100%"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,PERIOD_ID,STATUS_ID" ClientDataKeyNames="ID,EMPLOYEE_ID,PERIOD_ID,STATUS_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn DataField="ID" Visible="false" HeaderText="ID" />
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" SortExpression="STATUS_NAME"
                                UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phê duyệt kế tiếp %>" DataField="EMP_APPROVES_NAME" Visible="true"
                                UniqueName="EMP_APPROVES_NAME" SortExpression="EMP_APPROVES_NAME" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Mã NV" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Họ và tên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Tên kỳ đánh giá" DataField="PERIOD_NAME" SortExpression="PERIOD_NAME"
                                UniqueName="PERIOD_NAME" />
                            <tlk:GridBoundColumn HeaderText="Từ ngày" DataField="START_DATE" SortExpression="START_DATE"
                                UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="Đến ngày" DataField="END_DATE" SortExpression="END_DATE"
                                UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="Điểm đánh giá" DataField="EVALUATION_POINTS" SortExpression="EVALUATION_POINTS"
                                UniqueName="EVALUATION_POINTS" />
                            <tlk:GridBoundColumn HeaderText="Xếp loại" DataField="CLASSIFICATION" SortExpression="CLASSIFICATION"
                                UniqueName="CLASSIFICATION" />
                            
                            <tlk:GridBoundColumn HeaderText="Điểm manh" DataField="STRENGTH_NOTE" SortExpression="STRENGTH_NOTE"
                                UniqueName="STRENGTH_NOTE" />
                            <tlk:GridBoundColumn HeaderText="Điểm cần cải thiện" DataField="IMPROVE_NOTE" SortExpression="IMPROVE_NOTE"
                                UniqueName="IMPROVE_NOTE" />
                            <tlk:GridBoundColumn HeaderText="Triển vọng" DataField="PROSPECT_NOTE" SortExpression="PROSPECT_NOTE"
                                UniqueName="PROSPECT_NOTE" />
                            <tlk:GridBoundColumn HeaderText="Đánh giá nhãn" DataField="BRANCH_EVALUATE" SortExpression="BRANCH_EVALUATE"
                                UniqueName="BRANCH_EVALUATE" />

                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                            <tlk:GridBoundColumn HeaderText="Lý do không phê duyệt" DataField="REASON" SortExpression="REASON"
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
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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

        function clientButtonClicking(sender, args) {
            var bCheck;
            var n;
            var m;
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT') {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "DELETE") {
                bCheck = CheckValidate();

                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == "SUBMIT") {
                var bCheck = $find('<%= rgMngInfoEvalTarget.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {//Check danh sách submit hop le
                    var rg = $find('<%= rgMngInfoEvalTarget.ClientID %>').get_masterTableView().get_selectedItems();
                    for (i = 0; i < bCheck; i++) {
                        var id = rg[i].getDataKeyValue('ID');
                        var status = rg[i].getDataKeyValue('STATUS');
                        if (status == 17 || status == 18 || status == 21) {
                            m = '<%# Translate("The action only applies for the records that have status as Saved or Unverified by HR. Please select other record.") %>';
                            var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                            setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                            args.set_cancel(true);
                        }

                    }

                }
            } else if (args.get_item().get_commandName() == "IMPORT") {
                var bCheck = $find('<%= rgMngInfoEvalTarget.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                } else if (bCheck == 2) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    var rg = $find('<%= rgMngInfoEvalTarget.ClientID %>').get_masterTableView().get_selectedItems();
                    var oWindow = radopen('Dialog.aspx?mid=Performance&fid=ctrlPE_PortalHTCHAssessmentResult&ID=' + rg[0].getDataKeyValue('ID'), "rwPopup");
                    var pos = $("html").offset();
                    oWindow.moveTo(pos.left, pos.top);
                    oWindow.maximize();
                    args.set_cancel(true);
                }
            }
            else if (args.get_item().get_commandName() == "SYNC") {
                window.open('/Default.aspx?mid=Performance&fid=ctrlPE_PortalHTCHAssessmentDetail', "_self");
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
            var status_code = $find('<%= rgMngInfoEvalTarget.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');
            if (status_code != 3) {
                return 3;
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

        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function OnClientClose(sender, args) {
            $find("<%= rgMngInfoEvalTarget.ClientID %>").get_masterTableView().rebind();
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgMngInfoEvalTarget.ClientID %>").get_masterTableView().rebind();
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        var winH;
         var winW;

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlPE_PortalHTCHAssessment_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPE_PortalHTCHAssessment_RadSplitter3").stop().animate({ height: winH }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPE_PortalHTCHAssessment_MainPane").stop().animate({ height: winH }, 0);
           $("#ctl00_MainContent_ctrlPE_PortalHTCHAssessment_rgMngInfoEvalTarget").stop().animate({ height: winH-106 }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPE_PortalHTCHAssessment_RadPane2").stop().animate({ height: winH - 72 }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPE_PortalHTCHAssessment_LeftPane").stop().animate({ height: winH }, 0);
            
            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
