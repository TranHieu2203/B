<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Program.ascx.vb"
    Inherits="Training.ctrlTR_Program" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true" Width="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowClick="gridRowClick" />
                        <%--<ClientEvents OnRowDblClick="gridRowDblClick" />--%>
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,SUM_RATIO,TR_TYPE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã khóa đào tạo %>" DataField="TR_PROGRAM_CODE"
                                SortExpression="TR_PROGRAM_CODE" UniqueName="TR_PROGRAM_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                                UniqueName="YEAR" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại chương trình %>" DataField="PROGRAM_TYPE"
                                SortExpression="PROGRAM_TYPE" UniqueName="PROGRAM_TYPE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đào tạo %>" DataField="TR_TYPE_NAME"
                                SortExpression="TR_TYPE_NAME" UniqueName="TR_TYPE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TRAIN_FIELD"
                                SortExpression="TRAIN_FIELD" UniqueName="TRAIN_FIELD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TRAIN_FORM_NAME"
                                SortExpression="TRAIN_FORM_NAME" UniqueName="TRAIN_FORM_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tính chất nhu cầu %>" DataField="PROPERTIES_NEED_NAME"
                                SortExpression="PROPERTIES_NEED_NAME" UniqueName="PROPERTIES_NEED_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField="CONTENT"
                                SortExpression="CONTENT" UniqueName="CONTENT" />
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                                SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng học viên %>" DataField="STUDENT_NUMBER"
                                SortExpression="STUDENT_NUMBER" UniqueName="STUDENT_NUMBER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí đào tạo %>" DataField="COST_TOTAL"
                                SortExpression="COST_TOTAL" UniqueName="COST_TOTAL" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="Centers_NAME"
                                SortExpression="Centers_NAME" UniqueName="Centers_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Cam kết đào tạo %>" DataField="TR_COMMIT_BOOL" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="TR_COMMIT_BOOL" SortExpression="TR_COMMIT_BOOL" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Bằng cấp/Chứng chỉ đạt được %>" DataField="CERTIFICATE_BOOL" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="CERTIFICATE_BOOL" SortExpression="CERTIFICATE_BOOL" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đánh giá sau đào tạo %>" DataField="TR_AFTER_TRAIN_BOOL" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="TR_AFTER_TRAIN_BOOL" SortExpression="TR_AFTER_TRAIN_BOOL" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />--%>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đánh giá sau đào tạo %>" DataField="TR_AFTER_TRAIN_BOOL"
                                ItemStyle-VerticalAlign="Middle" UniqueName="TR_AFTER_TRAIN_BOOL" SortExpression="TR_AFTER_TRAIN_BOOL" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" AllowFiltering="false"/>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đào tạo %>" DataField="VENUE" SortExpression="VENUE"
                                UniqueName="VENUE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnPrepare" runat="server" Text="<%$ Translate: Chuẩn bị khóa học %>"
                                OnClientClicking="btnPrepareClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnClass" runat="server" Text="<%$ Translate: Thông tin lớp học %>"
                                OnClientClicking="btnClassClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="rbResultDetail" runat="server" Text="<%$ Translate: Kết quả chi tiết %>"
                                OnClientClicking="btnResultDetailClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnResult" runat="server" Text="<%$ Translate: Kết quả khóa học %>"
                                OnClientClicking="btnResultClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnCommit" runat="server" Text="<%$ Translate: Cam kết đào tạo %>"
                                OnClientClicking="btnCommitClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
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
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var selectedID = -1;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            /* window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramNewEdit&group=Business', "_self"); 
           
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramNewEdit&group=Business&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            //if (args.get_item().get_commandName() == 'CREATE') {
            //    OpenNew();
            //    args.set_cancel(true);
            //}
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'SENDMAIL') {
                window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramNotify&group=Business', "_self"); /*
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.setSize(300, 50);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'DELETE' ||
            args.get_item().get_commandName() == 'APROVE' ||
            args.get_item().get_commandName() == 'REJECT') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
            }
            if (args.get_item().get_commandName() == "CREATE_CP") {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck > 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Training&fid=ctrlTR_ProgramCost&group=Business&PROGRAM_ID=' + id, "_self"); /*
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */

                args.set_cancel(true);
            }
        }

        function gridRowClick(sender, eventArgs) {
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            selectedID = id;
            return 0;
        }

        function gridRowDblClick(sender, eventArgs) {
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramNewEdit&group=Business&ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center();

            return 0;
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();

                oWnd = $find('<%#popupId %>');
                oWnd.setSize(screen.width - 250, screen.height - 300);
                oWnd.remove_close(OnClientClose);
                var arg = args.get_argument();
                if (arg) {
                    postBack(arg);
                }
            }
            $get("<%# btnSearch.ClientId %>").click();
        }


        function btnPrepareClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
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
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramPrepare&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }
        function btnClassClick(sender, args) {

            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
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
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramClass&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }

        function btnCommitClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
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
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramCommit&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }
        function btnResultClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
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
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var RATION = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('SUM_RATIO');
            if (RATION != 100) {
                 m = '<%# Translate("Tổng trọng số các lớp phải bằng 100") %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ProgramResult&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.maximize(true);
            oWindow.center();
        }
        function btnResultDetailClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
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
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var RATION = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('SUM_RATIO');
            var typeID = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('TR_TYPE_ID');
            if (typeID != 79662) {
                 m = '<%# Translate("Chức năng này chỉ dùng cho chương trình có loại đào tạo Thi xếp loại") %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (RATION == 100) {
                var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_Class_Result&group=Business&PROGRAM_ID=' + id + '&noscroll=1', "rwPopup");
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.setSize($(window).width(), $(window).height());
                oWindow.maximize(true);
                oWindow.center();
            }
            else {
                m = '<%# Translate("Tổng trọng số các lớp phải bằng 100") %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
        }
        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />